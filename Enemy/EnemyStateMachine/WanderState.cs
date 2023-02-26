using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Livia
{
    public class WanderState : State
    {
        [Header ("Wander AI")]
        [Tooltip("Found under 'Enemy States' on THIS enemy Object")]
        public PursueState pursueState;
        [Tooltip("The Walking Speed of the enemy, should be low to match animation")]
        [Range(0, 1)] public float speed = .8f;
        [Tooltip("How Close the enemy Needs to be to it's Wander Destination Before it Wander's Elsewhere")]
        [Range(1,2)]public float minimumDistanceToTarget = 2;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {
            if (enemyManager.isInteracting) return this;
            #region Handle Enemy Target Detection
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, enemyManager.detectionLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                PlayerStats characterStats = colliders[i].transform.GetComponent<PlayerStats>();
                if (characterStats != null)
                {
                    // check for team id

                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximimumDetectionAngle)
                    {
                        enemyManager.currentTarget = characterStats;
                    }
                }
            }
            #endregion

            #region choose place to wander

            float distanceFromTarget = Vector3.Distance(enemyManager.wanderTarget, enemyManager.transform.position);
            Vector3 wanderDirection =
                enemyManager.navMeshAgent.transform.forward;

            HandleRotateTowardsTarget(enemyManager);

            if (distanceFromTarget <= enemyManager.navMeshAgent.stoppingDistance ||
                distanceFromTarget > enemyManager.wanderingDistance)
            {
                
                enemyManager.wanderTarget = enemyManager.RandomNavMeshLocation();

            }





            //Walk towards target
            if (distanceFromTarget > minimumDistanceToTarget)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);

                wanderDirection.Normalize();
                wanderDirection.y = 0;

                wanderDirection *= speed;
                Vector3 projectedVelocity = Vector3.ProjectOnPlane(wanderDirection, Vector3.up);
                enemyManager.enemyRigidbody.velocity = projectedVelocity;
            }
            #endregion

            #region Handle Switching To Next State

            if (enemyManager.currentTarget != null)
            {
                return pursueState;
            }

            else
            {
                return this;
            }
            #endregion
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {

            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.wanderTarget);
            enemyManager.enemyRigidbody.velocity = enemyManager.navMeshAgent.velocity;
            enemyManager.transform.rotation =
                Quaternion.Slerp(enemyManager.transform.rotation,
                            enemyManager.navMeshAgent.transform.rotation,
                            enemyManager.rotationSpeed / Time.deltaTime);

        }



    }
}
