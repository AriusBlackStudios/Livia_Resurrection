using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Livia
{
    public class EnemyStats : CharacterStats
    {
      


        //------------------------------REFERENCES TO OTHER ENEMY SCRIPTS---------------------
        private EnemyAnimatorManager enemyAnimatorManager;
        private EnemyManager enemyManager;
        private UiEnemyHealthBar healthBar;
        private EnemyBossManager bossManager;
        private EnemyEffectManager effectManager;

        [Header ("Boss Enemy Stats")]
        public bool isBoss;
        //------------------------------Shard And Item Drops---------------------
        [Header ("Death Rewards")]
        [Tooltip("The number of points the player receives for defeating this enemy")]
        [Range(50,2000)]public int ShardsAwardedOnDeath=50;

        [Tooltip("Optional: Weapon Items That the Player May Receive from killing this enemy as a function of theier Luck")]
        [SerializeField]private WeaponItem DroppedItem;
        [Tooltip("A Blank Game Object That Acts as the transform For Where The Dropped item Will Form, recomend to have it a little infront the enemy")]
        [SerializeField] private GameObject ItemSpawner;

        private void Awake()
        {
            if (!isBoss)
                healthBar = GetComponentInChildren<UiEnemyHealthBar>();

            effectManager = GetComponent<EnemyEffectManager>();
            enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
            bossManager = GetComponent<EnemyBossManager>();
            enemyManager = GetComponentInParent<EnemyManager>();
            maxHealth = SetMaxHealthFromHealthLevel();//player prefs later
            currentHealth = maxHealth;
            


        }

        // Start is called before the first frame update
        void Start()
        {
            if(!isBoss)
            healthBar.SetMaxHealth(currentHealth);
            //else if (isBoss && bossManager != null)
            //    bossManager.SetBossHealthBar(currentHealth);

        }
        public void BreakGuard()
        {
            enemyAnimatorManager.PlayTargetAnimation("Break Guard", true);
        }
        

        public override void TakeDamageNoAnimation(float damage, string deathAnim = null)
        {

            if (isDead) return;

            base.TakeDamageNoAnimation(damage);

            if (enemyManager.currentTarget == null) enemyManager.currentTarget = FindObjectOfType<PlayerStats>();

            if (!isBoss)
            {
                healthBar.SetHealth(currentHealth);
            }

            else if (isBoss && bossManager != null)
            {
                bossManager.UpdateBossHealthBar(currentHealth, maxHealth);

            }


            HandleDeath(deathAnim);



        }
        public override void TakeDamage(float damage,string damageAnim = "Damage_Forward_01",string death_anim = "Death_Forward")
        {
            if (isDead) return; //cant hurt a dead enemy

            if (enemyManager.currentTarget == null) enemyManager.currentTarget = FindObjectOfType<PlayerStats>(); //if you hit him, he notices

            base.TakeDamage(damage, damageAnim, death_anim);
          


            if (!isBoss)
            {
                healthBar.SetHealth(currentHealth);
            }

            else if (isBoss && bossManager != null)
            {
                bossManager.UpdateBossHealthBar(currentHealth,maxHealth);

            }
            Debug.Log("Enemy Stats Damage Anim: " + damageAnim);
            enemyAnimatorManager.PlayTargetAnimation(damageAnim, true);
            HandleDeath(death_anim);

        }

        private void HandleDeath(string deathAnim=null)
        {

            
            if (currentHealth <= 0)
            {
                enemyManager.currentTarget.GetComponent<PlayerManager>().isInCombat = false;
                currentHealth = 0;
                isDead = true;

                if(deathAnim != null)
                    enemyAnimatorManager.PlayTargetAnimation(deathAnim, true);

                enemyAnimatorManager.AwardSoulsOnDeath();
                effectManager.disolveOnDeath();


                if (isBoss)
                {
                    bossManager.EndBossFight();
                    //increase bosses killed
                    //if bosses killed equals bosses in level, exit, play cut scene ,WE
                }
               

                if (DroppedItem != null && ItemSpawner != null)
                {
                    WeaponPickUp pickUp = ItemSpawner.GetComponent<WeaponPickUp>();
                    if (pickUp != null)
                    {
                        pickUp.weapon = DroppedItem;
                    }
                    Instantiate(ItemSpawner);
                }
                //scan for everyplayer in scene in award souls
 
            }
        }
    }
}
