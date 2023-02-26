using UnityEngine;


namespace Livia
{
    
    public class CharacterStats : MonoBehaviour
    {
        [Header("Shards")]
        public int ShardCount = 0;

        [Header("Team I.D")]
        public int teamIDnumber = 0;

        [Header("Character Level")]
        public int char_lvl = 1;

        public bool isDead;
        public GameObject characterBlockerCollider;
        public PlayerFateManager playerFateManager;

        [Header("~~~~~~~~~~~~~~~~Chararacter Stat Levels~~~~~~~~~~~~~~~~~~~~~~")]

        [Tooltip("Health leveling, Physical Defense, and Max Carry Capacity")]
        [Range(8,99)]   public float vitalityLevel = 8;

        [Tooltip("Max Stamina,Max Equiptable Weight, Natural Defenses against poision, fire, bleed")]
        [Range(8, 99)]  public float EnduranceLevel = 8; 

        [Tooltip("How Many Spells Player Can Have In Inventory At Once and Max MP")]
        [Range(8, 99)] public float SanityLevel = 8;

        [Tooltip("Attack Power And  Allows to use Equiptment That Requires Strength")]
        [Range(8, 99)] public float StrengthLevel = 8;

        [Tooltip("Attack Power And Safe Landing Distance and Allows to use  weapons That Require Dexterity")]
        [Range(8, 99)] public float DexterityLevel = 8;

        [Tooltip("Magic Power (Damage Caused by spells) and Allows to use Equiptment that require Magic")]
        [Range(8, 99)] public float MagicLevel = 8;


        [Tooltip("Magic Defense and Psysical Defense ")]
        [Range(8, 99)] public float BlasphemyLevel = 8;

        [Tooltip("Resitance To Fragmentation and Increases Chance Of Aquiring Items")]
        [Range(8, 99)] public float LuckLevel = 8;
        [Space(10)]


        [Header("                                   ~~~Health~~~")]

        [Tooltip("Minimum Health Level ")]
        [Range(300, 600)]   public float baseHealth = 512;

        [Tooltip("Ammount Used To increase with each level")]
        [Range(10, 50)] public float healthStep = 22;

        [Tooltip("Level at which healthStep Doubles")]
        [Range(20, 40)] public float DoubleHealthGainLevel = 35;

        [Tooltip("Level at which healthStep is cut backdown")]
        [Range(41, 60)] public float lowerHealthGainLevel = 45;

        [Tooltip("Level at which healthStep is cut in half of healthStep")]
        [Range(61, 90)] public float halfHealthGainLevel = 65;

         public float maxHealth;
        [HideInInspector]   public float currentHealth;


        [Header("                                 ~~~Stamina~~~")]
        [Tooltip("Minimum Stamina Level ")]
        [Range(300, 600)] public float baseStamina = 512;

        [Tooltip("Ammount Used To increase with each level")]
        [Range(10, 50)] public float staminaStep = 22;

        [Tooltip("Level at which staminaStep Doubles")]
        [Range(20, 40)] public float DoubleStaminaGainLevel = 35;

        [Tooltip("Level at which staminaStep is cut backdown")]
        [Range(41, 60)] public float lowerStaminaGainLevel = 45;

        [Tooltip("Level at which staminaStep is cut in half of staminaStep")]
        [Range(61, 90)] public float halfStaminaGainLevel = 65;

        [HideInInspector] public float maxStamina;
        [HideInInspector] public float currentStamina;



        [Header("                                 ~~~Sanity~~~")]
        [Tooltip("Minimum MP Level ")]
        [Range(300, 600)] public float baseSanity = 512;

        [Tooltip("Ammount Used To increase with each level")]
        [Range(10, 50)] public float sanityStep = 22;

        [Tooltip("Level at which sanityStep Doubles")]
        [Range(20, 40)] public float DoubleSanityGainLevel = 35;

        [Tooltip("Level at which sanityStep is cut backdown")]
        [Range(41, 60)] public float lowerSanityGainLevel = 45;

        [Tooltip("Level at which sanityStep is cut in half of staminaStep")]
        [Range(61, 90)] public float halfSanityGainLevel = 65;

        [HideInInspector] public float maxFocus;
        [HideInInspector] public float currentFocus;



        [Header("                                          ~~~Poise~~~")]
        [Tooltip("Used For Enemies To Compute Their Poise")]
        [Range(0,100)]public float CharacterPoiseBase=50f; //poise you GAIN from what ever you have equipted;

        [Tooltip("Maximum Time after an attack that poise is reset")]
        [Range(0, 10)] public float totalPoiseRestTime = 5;

        [HideInInspector] public float totalPoiseDefense;//total poice calc after damage
        [HideInInspector] public float offensivePoiseBonus; //poise you GAIN during an attack with a weapon
        [HideInInspector] public float poiseResetTimer = 0;


        [Header("                  ~~~~~~Strength SubStats~~~~~~~                       ")]

        [Tooltip("Minimum MP Level ")]
        public float baseAttack = 100;
        public float basePhysicalDefense=5;
        

        [Tooltip("Ammount Used To increase with each level")]
        public float AttackStep = 10;
        public float physicalDefenseStep=2;


        [Tooltip("Level at which step Doubles")]
        [Range(20, 40)] public float DoubleStrengthGain = 35;

        [Tooltip("Level at which ste is cut backdown")]
        [Range(41, 60)] public float LowerStrengthGain = 45;

        [Tooltip("Level at which step is cut in half ")]
        [Range(61, 90)] public float HalfStrengthGain = 65;


        public float AttackPower=0;
        public float physicalDefense;




        [HideInInspector] public float FortuneCardDamageAbsortion_A;
        [HideInInspector] public float FortuneCardDamageAbsortion_B;
        [HideInInspector] public float FortuneCardDamageAbsortion_C;
        [HideInInspector] public float FortuneCardDamageAbsortion_D;
        [HideInInspector] public float FortuneCardDamageAbsortion_E;

        private void Start()
        {
            totalPoiseDefense = CharacterPoiseBase;
        }
        protected virtual void Update()
        {
            HandlePoiseResetTimer();
        }
        public void AddSouls(int souls)
        {
            ShardCount = ShardCount + souls;
        }

        //called in DamageCollider Script if poise is low enough to reach
        public virtual void TakeDamage(float PhysicalDmg,string damageanimaton = "Damage_Forward_01", string death_anim = "Death_Forward")
        {
            

            float totalPhysDmgAbsorbed = 1 -
                (1 - physicalDefense / 100);

          
            PhysicalDmg = PhysicalDmg - PhysicalDmg * totalPhysDmgAbsorbed;
            float finalDamage = PhysicalDmg; // + fire + blah, blah;

            currentHealth -= finalDamage;
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                characterBlockerCollider.SetActive(false);
            }

        }


        //called in DamageCollider Script if poise is too high
        //also called when doing repost/parry animations 
        public virtual void TakeDamageNoAnimation(float damage, string deathAnim = null)
        {
          

            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                characterBlockerCollider.SetActive(false);
            }
        }


        public virtual void HandlePoiseResetTimer()
        {
            if (poiseResetTimer > 0)
            {
                poiseResetTimer = poiseResetTimer - Time.deltaTime;
            }
            else
            {
                totalPoiseDefense = CharacterPoiseBase;
            }
        }

        public float SetMaxHealthFromHealthLevel()
        {
            //if vitality level is less than the level where things increase faster
            float vit_lev = vitalityLevel;
            if (playerFateManager != null)
                vit_lev = vit_lev + (vit_lev * playerFateManager.VitalityBoost);

            if (vit_lev < DoubleHealthGainLevel)
            {
                maxHealth = baseHealth + (vit_lev - 7) * healthStep;
            }

            else if (vit_lev < lowerHealthGainLevel)
            {
                maxHealth = baseHealth + (DoubleHealthGainLevel - 7) * healthStep;// add up all of the base levels
                maxHealth += (vit_lev - DoubleHealthGainLevel) * (healthStep * 2); // add up everything past where its doubled
            }

            else if (vit_lev < halfHealthGainLevel)
            {
                maxHealth = baseHealth + DoubleHealthGainLevel * healthStep;
                maxHealth += (lowerHealthGainLevel - DoubleHealthGainLevel) * (healthStep * 2);
                maxHealth += (vitalityLevel - lowerHealthGainLevel) * healthStep;
            }

            else
            {
                maxHealth = baseHealth + DoubleHealthGainLevel * healthStep;
                maxHealth += (lowerHealthGainLevel - DoubleHealthGainLevel) * (healthStep * 2);
                maxHealth += (halfHealthGainLevel - lowerHealthGainLevel) * healthStep;
                maxHealth += (vit_lev - halfHealthGainLevel) * (healthStep / 2);
            }


            if (playerFateManager != null)  
                maxHealth += playerFateManager.MaxHealth_Boost;

            return maxHealth;
        }

        public float SetMaxStaminaFromStaminaLevel()
        {
            float end_lev = EnduranceLevel;
            if (playerFateManager != null)
                end_lev = end_lev + (end_lev * playerFateManager.EnduranceBoost);

            //if vitality level is less than the level where things increase faster
            if (end_lev < DoubleStaminaGainLevel)
            {
                maxStamina = baseStamina + (end_lev - 7) * staminaStep;
            }

            else if (end_lev < lowerStaminaGainLevel)
            {
                maxStamina = baseStamina + (DoubleStaminaGainLevel - 7) * staminaStep;// add up all of the base levels
                maxStamina += (end_lev - DoubleStaminaGainLevel) * (staminaStep * 2); // add up everything past where its doubled
            }

            else if (end_lev < halfStaminaGainLevel)
            {
                maxStamina = baseStamina + DoubleStaminaGainLevel * staminaStep;
                maxStamina += (lowerStaminaGainLevel - DoubleStaminaGainLevel) * (staminaStep * 2);
                maxStamina += (end_lev - lowerStaminaGainLevel) * staminaStep;
            }

            else
            {
                maxStamina = baseStamina + DoubleStaminaGainLevel * staminaStep;
                maxStamina += (lowerStaminaGainLevel - DoubleStaminaGainLevel) * (staminaStep * 2);
                maxStamina += (halfStaminaGainLevel - lowerStaminaGainLevel) * staminaStep;
                maxStamina += (end_lev - halfStaminaGainLevel) * (staminaStep / 2);
            }

            if (playerFateManager != null)
                maxStamina += playerFateManager.MaxStamina_Boost;


            return maxStamina;
        }

        public float SetMaxFocusFromFocusLevel()
        {
            float san_level = SanityLevel;
            if (playerFateManager != null)
                san_level = san_level + (san_level * playerFateManager.InteligenceBoost);
            if (san_level < DoubleSanityGainLevel)
            {
                maxFocus = baseSanity + (san_level - 7) * sanityStep;
            }

            else if (san_level < lowerSanityGainLevel)
            {
                maxFocus = baseSanity + (DoubleSanityGainLevel - 7) * sanityStep;// add up all of the base levels
                maxFocus += (san_level - DoubleStaminaGainLevel) * (sanityStep * 2); // add up everything past where its doubled
            }

            else if (san_level < halfSanityGainLevel)
            {
                maxFocus = baseSanity + DoubleSanityGainLevel * sanityStep;
                maxFocus += (lowerSanityGainLevel - DoubleSanityGainLevel) * (sanityStep * 2);
                maxFocus += (san_level - lowerSanityGainLevel) * sanityStep;
            }

            else
            {
                maxFocus = baseSanity + DoubleSanityGainLevel * sanityStep;
                maxFocus += (lowerSanityGainLevel - DoubleSanityGainLevel) * (sanityStep * 2);
                maxFocus += (halfSanityGainLevel - lowerSanityGainLevel) * sanityStep;
                maxFocus += (san_level - halfSanityGainLevel) * (sanityStep / 2);
            }

            if (playerFateManager != null)
                maxFocus += playerFateManager.MaxMP_Boost;
            return maxFocus;
        }


        public void SetUpStrengthSubStats()
        {
            //attack power
            float str_lev = StrengthLevel;
            if (playerFateManager != null)
                str_lev = str_lev + (str_lev * playerFateManager.StrengthBoost);

            if (str_lev < DoubleStrengthGain)
            {
                //attack power
                AttackPower = baseAttack +((str_lev - 7) * AttackStep);


                //physical Def
                physicalDefense = basePhysicalDefense + (str_lev - 7) * physicalDefenseStep;
            }

            else if (str_lev < LowerStrengthGain)
            {
                //attack power
                AttackPower = baseAttack + (DoubleStrengthGain - 7) * AttackStep;// add up all of the base levels
                AttackPower += (str_lev - DoubleStrengthGain) * (AttackStep * 2); // add up everything past where its doubled


                //physicalDef
                physicalDefense = basePhysicalDefense + (DoubleStrengthGain - 7) * physicalDefenseStep;// add up all of the base levels
                physicalDefense += (str_lev - DoubleStrengthGain) * (physicalDefenseStep * 2); // add up everything past where its doubled
            }

            else if (str_lev < HalfStrengthGain)
            {
                //attack power
                AttackPower = baseAttack + DoubleStrengthGain * AttackStep;
                AttackPower += (LowerStrengthGain - DoubleStrengthGain) * (AttackStep * 2);
                AttackPower += (str_lev - LowerStrengthGain) * AttackStep;


                //physicalDef
                physicalDefense = basePhysicalDefense + DoubleStrengthGain * physicalDefenseStep;
                physicalDefense += (LowerStrengthGain - DoubleStrengthGain) * (physicalDefenseStep * 2);
                physicalDefense += (str_lev - LowerStrengthGain) * physicalDefenseStep;
            }

            else
            {
                AttackPower = baseAttack + DoubleStrengthGain * AttackStep;
                AttackPower += (LowerStrengthGain - DoubleStrengthGain) * (AttackStep * 2);
                AttackPower += (HalfStrengthGain - LowerStrengthGain) * AttackStep;
                AttackPower += (str_lev - HalfStrengthGain) * (AttackStep / 2);


                physicalDefense = basePhysicalDefense + DoubleStrengthGain * physicalDefenseStep;
                physicalDefense += (LowerStrengthGain - DoubleStrengthGain) * (physicalDefenseStep * 2);
                physicalDefense += (HalfStrengthGain - LowerStrengthGain) * physicalDefenseStep;
                physicalDefense += (str_lev - HalfStrengthGain) * (physicalDefenseStep / 2);
            }

            if (playerFateManager != null)
            {
                AttackPower = AttackPower + (playerFateManager.AttackPower_Boost);
                physicalDefense*= ( playerFateManager.PhysicalDefense_Boost /100) +1;
            }





            //bleed defense
            //poise break
            //poise (lesser)

        }

    }
}
