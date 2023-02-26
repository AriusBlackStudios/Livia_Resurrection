using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Livia
{
    public class PlayerStats : CharacterStats
    {
        //---------------------Reference Scripts For Other Classes-----------------
       
        PlayerAnimatorManager animatorHandler;
        PlayerManager playerManager;
        [HideInInspector] public SoulCountBar soulCountBar;

        //---------------------Game Objects In The Scene Related To Player Health And Player Death-----------------
        [Header("~~~~~~~~~~~~~~~~~~~~~~~~~~Player Stats ~~~~~~~~~~~~~~~~~~~~~~~~~~")]
        [Tooltip("Parent GameObject For The Screen THat Pops Up When The Player Dies")]
        public GameObject DeathScreen;
        [Tooltip("Button GameObject For the first button to be selected on the death screen automatically")]
        public GameObject firstButton_deathScreen;
        HealthBar healthBar;

        //---------------------Stamina Regeneration-----------------
        [Header("                  ~~~~~~Stamina Stats~~~~~~~                       ")]
        [Tooltip("The Amount Of Stamina That is Regenerated Per-Second")]
        [SerializeField] [Range(80,300)] private float staminaRegenerationAmount = 100f;

        [Tooltip("The Amount Of time in seconds before stamina will begin to refill")]
        [SerializeField][Range(0, 1)] private float staminaRegenerationDelay = .5f;
        private float staminaRegenTimer = 0;

        public float fallBaseDMG = 100;


        public ConsumeableItem healthFlask;
        public ConsumeableItem manaFlask;


        private void Awake()
        {
            animatorHandler = GetComponent<PlayerAnimatorManager>();
            healthBar = GameObject.FindObjectOfType<HealthBar>();
            playerManager=GetComponent<PlayerManager>();
            soulCountBar = FindObjectOfType<SoulCountBar>();
            soulCountBar.SetSoulCountText(ShardCount);
        }

        // Start is called before the first frame update
        void Start()
        {
            playerFateManager = GetComponent<PlayerFateManager>();
            playerFateManager.FateAwake();

            //vitality
            maxHealth = SetMaxHealthFromHealthLevel();//player prefs later
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);


            //endurance
            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentStamina = maxStamina;
            healthBar.SetMaxStamina(maxStamina);
            healthBar.SetCurrentStamina(currentStamina);


            //sanity
            maxFocus = SetMaxFocusFromFocusLevel();
            currentFocus = maxFocus;
            healthBar.SetMaxFocus(maxFocus);
            healthBar.SetCurrentFocus(currentFocus);

            //strength

            SetUpStrengthSubStats();
            //dexterity

            //magic

            //blasphemy

            //luck
        }
        public override void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer = poiseResetTimer - Time.deltaTime;

            }
            else if (poiseResetTimer <= 0 && !playerManager.isInteracting)
            {
                totalPoiseDefense = CharacterPoiseBase;

            }

        }




        public override void TakeDamageNoAnimation(float damage, string deathAnim = null)
        {

            if (playerManager.isInvulnerable) return;
            if (isDead) return;

            base.TakeDamageNoAnimation(damage);

            healthBar.SetCurrentHealth(currentHealth);
            HandleDeath(deathAnim);

        }
        public override void TakeDamage(float damage,string DamageAnim = "Damage_01", string death_anim = "Death_Forward")
        {

            if (playerManager.isInvulnerable) return;
            if (isDead) return;
            base.TakeDamage(damage,DamageAnim,death_anim);
            healthBar.SetCurrentHealth(currentHealth);
            animatorHandler.PlayTargetAnimation(DamageAnim, true);
            HandleDeath(death_anim);
        }

        private void HandleDeath(string death_anim)
        {
           
            if (!isDead) return;
            
            animatorHandler.PlayTargetAnimation(death_anim, true);
            DeathScreen.SetActive(true);
            FindObjectOfType<MainMenu>().set_menu_item(firstButton_deathScreen);
        }

        public void TakeStaminaDamage(float damage)
        {
            currentStamina -= damage;
            healthBar.SetCurrentStamina(currentStamina);
           
        }

        public void RegenerateStanima()
        {
            if (playerManager.isInteracting)
            {
                staminaRegenTimer = 0;
            }
            else
            {
                staminaRegenTimer+=Time.deltaTime;
                if (currentStamina < maxStamina && staminaRegenTimer > staminaRegenerationDelay)
                {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    healthBar.SetCurrentStamina(currentStamina);

                }
            }

        }

        public void HealPlayer(float healAmount)
        {
            currentHealth = currentHealth + healAmount;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            healthBar.SetCurrentHealth(currentHealth);
        }

        public void DeductFocusPoints(float focusPoints)
        {
            currentFocus = currentFocus - focusPoints;

            if(currentFocus<0)currentFocus = 0;

            healthBar.SetCurrentFocus(currentFocus);

        }

        public void HealFocusPoints(float focusPoints)
        {
            currentFocus = currentFocus + focusPoints;

            if (currentFocus > maxFocus) currentFocus = maxFocus;

            healthBar.SetCurrentFocus(currentFocus);

        }

        public void calculateFallDMG(float fallingTime)
        {
            


            float totaldamage = fallingTime * fallBaseDMG;
            Debug.Log("falling DMG" + totaldamage);
            TakeDamage(totaldamage, "Fall Damage", "Death Fall");


        }
        public void CompleteHealLevelUpAndRest(){
            currentHealth = maxHealth;
            healthBar.SetCurrentHealth(currentHealth);

            currentFocus = maxFocus;
            healthBar.SetCurrentFocus(currentFocus);

            healthFlask.currentItemAmount= healthFlask.maxItemAmount;
            manaFlask.currentItemAmount = manaFlask.maxItemAmount;


        }
        public void SpendShards(int shardSpent){
            ShardCount -= shardSpent;
            soulCountBar.SetSoulCountText(ShardCount);
        }


    }
}
