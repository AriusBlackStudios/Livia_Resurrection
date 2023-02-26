using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorManager enemyAnimatorManager;
        [SerializeField] float groundDetectionRayStartPoint = 0.5f;
        [SerializeField] float minimumDistanceToFall = 1.0f;
        [SerializeField] float groundDetectionRayDistance = 0.2f;
        [SerializeField] LayerMask ignoreForGroundCheck;
        public float fallingSpeed=20f;
        public Transform myTransform;

        Vector3 normalVector;
        Vector3 targetPosition;


        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            myTransform = transform;

        }

        public void handleFalling(Vector3 moveDirection)
        {
            enemyManager.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += groundDetectionRayStartPoint;

            if (Physics.Raycast(origin, myTransform.forward, out hit, 0.7f))
            {
                moveDirection = Vector3.zero;
            }

            if (enemyManager.isInAir)
            {
                enemyManager.enemyRigidbody.AddForce(-Vector3.up * fallingSpeed * 3);
                enemyManager.enemyRigidbody.AddForce(moveDirection * fallingSpeed / 5f);//hopping off edge w/ a lil bit o force
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groundDetectionRayDistance;
            targetPosition = myTransform.position;


            Debug.DrawRay(origin, -Vector3.up * minimumDistanceToFall, Color.red, 0.1f, false);
            if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceToFall, ignoreForGroundCheck))
            {

                normalVector = hit.normal;
                Vector3 tp = hit.point;
                enemyManager.isGrounded = true;
                targetPosition.y = tp.y;
                if (enemyManager.isInAir)
                {
                    enemyManager.isInAir = false;
                }
            }
            else
            {
                if (enemyManager.isGrounded)
                {
                    enemyManager.isGrounded = false;
                }
                if (!enemyManager.isInAir)
                {

                    Vector3 vel = enemyManager.enemyRigidbody.velocity;
                    vel.Normalize();
                    enemyManager.enemyRigidbody.velocity = vel * (fallingSpeed / 2);
                    enemyManager.isInAir = true;
                }
            }
            myTransform.position = targetPosition;


        }





    }
}
