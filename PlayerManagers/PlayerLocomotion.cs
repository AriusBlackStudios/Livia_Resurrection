using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class PlayerLocomotion : MonoBehaviour
    {
        PlayerManager player;
        [HideInInspector] public Vector3 moveDirection;
        [HideInInspector] public Transform myTransform;
        public new Rigidbody rigidbody;

        [Header("Ground and air Detection Stats")]
        [SerializeField] [Range(0,1)] private float m_groundDetectionRayStartPoint = 0.71f;
        [SerializeField] [Range(0, 5)] private float minimumDistanceToFall = 2.0f;
        [SerializeField] [Range(0, 1)] float groundDetectionRayDistance = 0.01f;
        [SerializeField] LayerMask ignoreForGroundCheck;
        [HideInInspector] public float inAirTimer;


        [Header("Movement Stats")]
        [SerializeField][Range(0, 10)] float walkingSpeed = 3;
        [SerializeField][Range(0, 10)] float movementSpeed = 5;
        [SerializeField][Range(0, 10)] float sprintSpeed = 7;
        [SerializeField][Range(0, 360)] float rotationSpeed = 10;
        [SerializeField][Range(0, 100)] float fallingSpeed = 45;


        [Header("Stamina Costs")]
        [SerializeField][Range(0, 100)] float rollStaminaCost = 15;
        [SerializeField][Range(0, 100)] float backStepStaminaCost = 12;
        [SerializeField][Range(0, 100)] float sprintStaminaCost= 10;

        [Header("Colliders")]
        public Collider characterCollider;
        public Collider CharacterCollisionCollider;




        public void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            player = GetComponent<PlayerManager>();

            myTransform = transform;

            player.isGrounded = true;
        }


        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

       
        public void HandleRotation()
        {
            if (player.animatorManager.canRotate &&( player.inputHandler.horizontal >0 || player.inputHandler.vertical > 0))
            {
                    if (player.inputHandler.sprintFlag || player.inputHandler.rollFlag)
                    {
                        Vector3 targetDirection = Vector3.zero;
                        targetDirection = player.main_camera.transform.forward * player.inputHandler.vertical;
                        targetDirection += player.main_camera.transform.right * player.inputHandler.horizontal;
                        targetDirection.Normalize();
                        targetDirection.y = 0;
                        if (targetDirection == Vector3.zero)
                        {
                            targetDirection = transform.forward;
                        }
                        Quaternion tr = Quaternion.LookRotation(targetDirection);
                        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);

                        transform.rotation = targetRotation;
                    }
                    else
                    {
                        Vector3 rotationDirection = moveDirection;
                        rotationDirection = player.main_camera.transform.forward;
                        rotationDirection.y = 0;
                        rotationDirection.Normalize();
                        Quaternion tr = Quaternion.LookRotation(rotationDirection);
                        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
                        transform.rotation = targetRotation;
                    }

                
               
            }



        }

        public void HandleMovement()
        {
            if (player.inputHandler.rollFlag) return;
            if (player.isInteracting) return;
            moveDirection = player.main_camera.transform.forward * player.inputHandler.vertical;
            moveDirection += player.main_camera.transform.right * player.inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;
            float speed = movementSpeed;
            if (player.inputHandler.sprintFlag && player.inputHandler.moveAmount>0.5f)
            {
                speed = sprintSpeed;
                player.isSprinting = true;
                moveDirection *= speed;
                if (player.isInCombat)
                    player.stats.TakeStaminaDamage(sprintStaminaCost);
            }
            else
            {
                if (player.inputHandler.moveAmount < 0.5)
                {
                    moveDirection *= walkingSpeed;
                    player.isSprinting = false;
                }
                else
                {
                    moveDirection *= speed;
                    player.isSprinting = false;
                }

            }

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;
            if (player.inputHandler.sprintFlag == false)
            {
                player.animatorManager.UpdateAnimatorValues(player.inputHandler.vertical, player.inputHandler.horizontal, player.isSprinting);
            }
            else
            {
                player.animatorManager.UpdateAnimatorValues(player.inputHandler.vertical, 0, player.isSprinting);
            }
            
            
          
            if (player.animatorManager.canRotate)
            {
                HandleRotation();
            }
        }
        public void handleRollingAndSprinting()
        {
            if (player.animatorManager.anim.GetBool("isInteracting")) return;//so you cant roll whenever

            if (player.stats.currentStamina <= 0) return;

            if (player.inputHandler.rollFlag)
            {
                moveDirection = player.main_camera.transform.forward * player.inputHandler.vertical;
                moveDirection += player.main_camera.transform.right * player.inputHandler.horizontal;

                if (player.inputHandler.moveAmount > 0)//if already moving
                {
                    player.animatorManager.PlayTargetAnimation("Rolling", true);
                    moveDirection.y = 0;
                    Quaternion _rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = _rollRotation;
                    player.stats.TakeStaminaDamage(rollStaminaCost);
                }
                else
                {
                    player.animatorManager.PlayTargetAnimation("Backstep", true);
                    moveDirection.y = 0;
                    player.stats.TakeStaminaDamage(backStepStaminaCost);
                }
            }

        }
        public void handleFalling( Vector3 moveDirection)
        {
            player.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += m_groundDetectionRayStartPoint;

            if (Physics.Raycast(origin, myTransform.forward, out hit, 0.7f))
            {
                moveDirection = Vector3.zero;
            }

            if (player.isInAir)
            {
                rigidbody.AddForce(-Vector3.up * fallingSpeed*3);
                rigidbody.AddForce(moveDirection * fallingSpeed / 5f);//hopping off edge w/ a lil bit o force
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groundDetectionRayDistance;
            targetPosition=myTransform.position;


            Debug.DrawRay(origin, -Vector3.up * minimumDistanceToFall, Color.red, 0.1f, false);
            if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceToFall, ignoreForGroundCheck))
            {

                normalVector = hit.normal;
                Vector3 tp = hit.point;
                player.isGrounded = true;
                targetPosition.y = tp.y;
                if (player.isInAir)
                {
                    if (inAirTimer > 0.5f)
                    {
                        player.stats.calculateFallDMG(inAirTimer);

                    }
                    else if (inAirTimer > 0.2f)
                    {

                        player.animatorManager.PlayTargetAnimation("Land", true);
                    }
                    else
                    {

                        player.animatorManager.PlayTargetAnimation("Empty", false);
                    }

                    player.isInAir = false;
                }
            }
            else
            {
                if (player.isGrounded)
                {
                    player.isGrounded = false;
                }
                if (!player.isInAir)
                {
                    if (!player.isInteracting)
                    {
                        player.animatorManager.PlayTargetAnimation("Falling", true);
                    }
                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (movementSpeed / 2);
                    player.isInAir = true;
                }
            }
            if (player.isInteracting || player.inputHandler.moveAmount > 0)
            {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                myTransform.position = targetPosition;
            }

        }
        public void handleJumping( Vector3 moveDirection)
        {
            if (player.isInteracting) return;
            if (player.stats.currentStamina <= 0) return;

            if (player.inputHandler.jump_input)
            {
                if(player.inputHandler.moveAmount> 0 && player.inputHandler.sprintFlag && player.inputHandler.moveAmount > 0.5f)
                {
                    moveDirection = player.main_camera.transform.forward * player.inputHandler.vertical;
                    moveDirection += player.main_camera.transform.right * player.inputHandler.horizontal;

                    player.animatorManager.PlayTargetAnimation("Jump", true);
                    moveDirection.y = 0;
                    Quaternion jumpRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = jumpRotation;
                }
            }

        }
        #endregion

    }
}
