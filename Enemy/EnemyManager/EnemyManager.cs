using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Livia
{
    public class EnemyManager : CharacterManager
    {
       
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAnimatorManager enemyAnimatorManager;
        EnemyStats enemyStats;

        


        public bool isPerformingAction;
        [Header("Combat Flag")]


        [Header("State Machine Data")]
        public State currentState;
        public PlayerStats currentTarget;
        public LayerMask detectionLayer;
        public NavMeshAgent navMeshAgent;
        public Rigidbody enemyRigidbody;

        [Header("-Idle State Settings")]
        public float detectionRadius = 20;
        //feild of view
        public float minimumDetectionAngle = -50f;
        public float maximimumDetectionAngle = 50f;
        public float currentRecoveryTime = 0;


        [Header("-Pursue State Setting")]

        public float rotationSpeed = 60;
        

        [Header("-Attack Stance State Data")]
        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack;
        public float maximumAttackingRange = 3f;



        [Header("-Wander State Data")]
        public float returnToIdleDistance = 20f;
        public Transform wanderingCenterTransform;
        public float wanderingDistance = 5f;
        public Vector3 wanderTarget;
        public LayerMask wanderingLayer;

        [Header("AI Combo Settings")]
        public bool allowAIToPerformCombo;
        public float comboLikelihood;
        public bool isPhaseShifting;



        private void Awake()
        {
            enemyLocomotionManager= GetComponent<EnemyLocomotionManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            enemyStats= GetComponent<EnemyStats>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRigidbody = GetComponent<Rigidbody>();
            backstabCollider = GetComponentInChildren<CriticalDamageCollider>();
         //   returnToIdleTransform = this.transform;
            navMeshAgent.enabled = false ;
        //    wanderTarget= RandomNavMeshLocation();


    }
        private void Start()
        {
            enemyRigidbody.isKinematic = false;
        }

        private void Update()
        {
            HandleRecoveryTimer();

            isInteracting = enemyAnimatorManager.anim.GetBool("isInteracting");
            canDoCombo = enemyAnimatorManager.anim.GetBool("canDoCombo");
            isRotatingWithRootMotion = enemyAnimatorManager.anim.GetBool("isRotatingWithRootMotion");
            canRotate = enemyAnimatorManager.anim.GetBool("canRotate");
            isPhaseShifting = enemyAnimatorManager.anim.GetBool("isPhaseShifting");
            isInvulnerable = enemyAnimatorManager.anim.GetBool("isInvulnerable");


            enemyAnimatorManager.anim.SetBool("isDead", enemyStats.isDead);
        }
        private void FixedUpdate()
        {

            HandleStateMachine();
            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.nextPosition = enemyAnimatorManager.transform.position;
            navMeshAgent.transform.localRotation = Quaternion.identity;
            enemyLocomotionManager.handleFalling(enemyRigidbody.velocity);

        }
        private void HandleStateMachine()
        {

            if (currentState != null&& enemyStats.isDead==false)
            {
                State nextstate = currentState.Tick(this, enemyStats, enemyAnimatorManager);
                if(nextstate != null)
                {
                    SwitchToNextState(nextstate);
                }

            }

            
        }
        private void SwitchToNextState(State state)
        {
            currentState = state;
        }
        private void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }
            if (isPerformingAction)
            {
                if(currentRecoveryTime <= 0) 
                { 
                    isPerformingAction = false; 
                }
            }

        }

        public Vector3 RandomNavMeshLocation()
        {
            Vector3 finalPosition = Vector3.zero;

            Vector3 randomPostion = UnityEngine.Random.insideUnitSphere * wanderingDistance;
            randomPostion += wanderingCenterTransform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPostion, out hit, wanderingDistance, wanderingLayer))
                return hit.position;
            return transform.position;
        }


    }
}
