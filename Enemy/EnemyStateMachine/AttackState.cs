using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia{
    public class AttackState : State
    {

        public EnemyAttackAction currentAttack;
        public PursueState pursueState;
        public RotateTowardsTargetState rotateTowardsTargetState;
        public CombatStanceState combatStanceState;

        public bool willDoComboOnNextAttack=false;
        public bool hasPerformedAttack = false;

        // Start is called before the first frame update
        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
        {

            if (currentAttack == null) return combatStanceState;
           
            enemyManager.currentTarget.GetComponent<PlayerManager>().isInCombat = true;

            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            HandleRotateTowardsTarget(enemyManager);
            hasPerformedAttack = false;
            if (distanceFromTarget >= enemyManager.maximumAttackingRange)
            {
                return pursueState;
            }
            if(willDoComboOnNextAttack && enemyManager.canDoCombo)
            {
                AttackTargetWithCombo(enemyAnimatorManager,enemyManager);

            }
            if (!hasPerformedAttack)
            {

                AttackTarget(enemyAnimatorManager,enemyManager);
                RollForComboChance(enemyManager);
            }

            if (willDoComboOnNextAttack && hasPerformedAttack)
            {

                return this;
            }

            return rotateTowardsTargetState;
;
        }
    
        private void AttackTarget(EnemyAnimatorManager enemyAnimatorManager,EnemyManager enemyManager)
        {
            if (enemyManager.isInteracting) return;
            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
            hasPerformedAttack = true;
        }

        private void AttackTargetWithCombo(EnemyAnimatorManager enemyAnimatorManager, EnemyManager enemyManager)
        {
            if (enemyManager.isInteracting) return;
            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
            willDoComboOnNextAttack = false;
            currentAttack = null;
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
            //rotate manually
            if (enemyManager.canRotate && enemyManager.isInteracting)//if is attacking
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
                direction.y = 0;
                direction.Normalize();
                if (direction == Vector3.zero)
                {
                    direction = enemyManager.transform.forward;
                }
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
            ////pathfinding
            //else
            //{
            //    Vector3 relativeDirection = enemyManager.transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            //    Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;
            //    enemyManager.navMeshAgent.enabled = true;
            //    enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            //    enemyManager.enemyRigidbody.velocity = targetVelocity;
            //    enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);

            //}


        }
        private void RollForComboChance(EnemyManager enemyManager)
        {
            float comboChance = Random.Range(0,100);
            if(enemyManager.allowAIToPerformCombo && comboChance <= enemyManager.comboLikelihood)
            {
                if (currentAttack.comboAction != null)
                {
                    willDoComboOnNextAttack = true;
                    currentAttack = currentAttack.comboAction;
                }
                else
                {
                    willDoComboOnNextAttack = false;
                    currentAttack = null;
                }
            }
            
        }
    }
}
