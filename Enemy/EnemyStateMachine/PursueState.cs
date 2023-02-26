using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
  
    public class PursueState : State
    {
        [Header ("OTHER STATES")]
        public IdleState idleState;
        public CombatStanceState combatStanceState;
        //public RotateTowardsTargetState rotateTowardsTargetState;
        [Space(10)]
        [Header("Pursue AI Parameters")]
        [Tooltip("The Speed at Which The Enemy WALKS")]
        [SerializeField]    [Range(0,5)]   private float WalkSpeed = 1.5f;

        [Tooltip("The Speed at Which The Enemy RUNS")]
        [SerializeField]    [Range(0, 5)]  private float RunSpeed = 3.0f;

        [Tooltip("The Distance From The Player At Which The Runs after you")]
        [SerializeField]    [Range(0, 15)]  public float tooDamnFar = 5f;

        [Tooltip("The Distance From The Player At Which The Runs after you")]
        [SerializeField][Range(0, 20)] public float disengageDistance = 10f;


        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (enemyManager.isInteracting) return this;

            //for combat music
            enemyManager.currentTarget.GetComponent<PlayerManager>().isInCombat = true;
            Vector3 targetDirection = enemyManager.navMeshAgent.velocity;

            float distanceFromTarget =  Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

            HandleRotateTowardsTarget(enemyManager);


            if (enemyManager.isPerformingAction) {
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                enemyManager.navMeshAgent.enabled = false;
                return this; 
            }


            
            if (distanceFromTarget >= enemyManager.maximumAttackingRange)
            {
                targetDirection.Normalize();
                targetDirection.y = 0;


                if (distanceFromTarget >= tooDamnFar)
                {
                    enemyAnimatorManager.anim.SetFloat("Vertical", 2, 0.1f, Time.deltaTime);
                    targetDirection *= RunSpeed;
                }
                else
                {
                    enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                    targetDirection *= WalkSpeed;
                }
                Vector3 projectedVelocity = Vector3.ProjectOnPlane(targetDirection, Vector3.up);
                enemyManager.enemyRigidbody.velocity = projectedVelocity;
            }



            if (distanceFromTarget >= disengageDistance)
            {
                enemyManager.currentTarget.GetComponent<PlayerManager>().isInCombat = false;
                enemyManager.currentTarget = null;
                return idleState;
            }
            if (distanceFromTarget< enemyManager.maximumAttackingRange)
            {
                return combatStanceState;
            }
            else
            {   
                return this;
            }

        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {

                enemyManager.navMeshAgent.enabled = true;
                enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidbody.velocity = enemyManager.navMeshAgent.desiredVelocity;
                if (-enemyManager.navMeshAgent.desiredVelocity.y > 0) Debug.Log("Desired y velocity greater than zero");
                enemyManager.transform.rotation = 
                    Quaternion.Slerp(enemyManager.transform.rotation,
                                enemyManager.navMeshAgent.transform.rotation, 
                                enemyManager.rotationSpeed / Time.deltaTime);

        }
    }
}