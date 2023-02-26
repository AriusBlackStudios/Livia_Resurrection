using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia {
    public class EnemyBossManager : MonoBehaviour
    {
        public string bossName;

        BossHealth bossHealthBar;
        EnemyStats enemyStats;
        EnemyAnimatorManager enemyAnimatorManager;
        BossCombatStanceState bossCombatStanceState;
        public List<FogWall> fogWalls;

        public GameObject bossfightBarriersParent;
        public GameObject BosstelleportStation;
        public bool bossFightIsActive;
        public bool bossHasBeenAwakened;//so you dont need to watch the cutscne everytime you die, or if you've come back for more
        public bool bossHasBeenDefeated;

        [Header("Second Phase FX")]
        public GameObject particleFX;

        void Awake()
        {
            bossHealthBar = FindObjectOfType<BossHealth>();
            enemyStats = GetComponent<EnemyStats>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
        }

        private void Start()
        {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossHealth(enemyStats.maxHealth);
        }

        //CALLED BY EVENT COLLIDER FOR A SPECIFIC BOSS
        public void StartBossFight()
        {
            //set up health bar
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossHealth(enemyStats.maxHealth);

            bossFightIsActive = true;
            bossHasBeenAwakened = true;
            for (int i = 0; i < fogWalls.Count; i++)
            {
                fogWalls[i].ActivateFogWall();
            }
            UpdateBossHealthBar(enemyStats.currentHealth,enemyStats.maxHealth);
            bossHealthBar.SetUIHealthBarToActive();
        }

        //CALLED IN TAKE DAMAGE FUCTION ON ENEMY
        public void UpdateBossHealthBar(float currentHealth,float maxHealth)
        {
            bossHealthBar.SetBossCurrentHealth(currentHealth);

            if (currentHealth <= maxHealth / 2 && bossCombatStanceState.hasPhaseShifted ==false)
            {
                ShiftToSecondPhase();
            }
        }

        //CALLED IN HANDLE DEATH FUNCTION
        public void EndBossFight()
        {
            bossHealthBar.SetUIHealthBarToInactive();
            if (enemyStats.isDead)
            {
                bossHasBeenDefeated = true;
                bossFightIsActive = false;
            }
            for (int i = 0; i < fogWalls.Count; i++)
            {
                fogWalls[i].DeactivateFogWall();
            }
        }

        public void DestroyBossOnLoad()
        {
            // turn on telleporter
            BosstelleportStation.SetActive(true);
            Destroy(bossfightBarriersParent);
            Destroy(this.gameObject);
            //destroy barriers
            //destoy boss
        }

        private void ShiftToSecondPhase()
        {
            //play an animation with an event that triggers particle effects
            //switch attack actions
            enemyAnimatorManager.anim.SetBool("isInvulnerable", true);
            enemyAnimatorManager.PlayTargetAnimation("Phase Shift", true);
            bossCombatStanceState.hasPhaseShifted = true;
            
        }
    }
}
