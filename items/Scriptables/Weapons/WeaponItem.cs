using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public enum WeaponType { Sheild,Melee,SpellCaster,Ranged,Unarmed};
    public enum WeaponStatRequirment { None, Vitality, Strength, Dexterity};
    [CreateAssetMenu(menuName ="Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public WeaponStatRequirment weaponStatRequirment;
        public int StatLevelReq;
        public bool isUnarmed;
        public bool isLargeWeapon;
        public bool isPrimaryWeapon;

        [Header("Poise")]
        public float poiseBreak;
        public float offensivePoiseBonus;


        [Header("Damage")]
        public int baseDamage=25;
        public int criticalDamageMultiplier=4;

        [Header("Damage Absorption")]
        public float physicalDamageAbsorption = 0;
        public float darkDamageAbsorption = 0;

        [Header("Idle Animations")]
        public string right_hand_idle=  "Right_Hand_Empty";
        public string left_hand_idle=   "Left_Hand_Empty";
        public string right_arm_idle=   "Right_Arm_Empty";
        public string left_arm_idle=    "Left_Arm_Empty";
        public string Two_Handled_Idle= "Two_Hand_Empty";
        [Header("Item Actions")]
        public string OH_blocking = "Right_Hand_Empty";
        public string TH_Blocking = "Left_Hand_Block";
        [Header("Attack Animations")]
        public string[] RightHandAttackAnims;
        public string[] TwoHandedAttackAnims;
        public string[] RightHandHeavyAttackAnims;
        public string[] TwoHandHeavyAttackAnims;
        [Header("Weapon Art Animation")]
        public string WeaponArt ="Parry";

        [Space(6)]
        [Header("Stamina Costs")]
        [Range(10,100)] public int baseStamina= 10;
        [HideInInspector]public float lightAttackMuiltiplier=1f;
        [Range(2, 5)] public float heavyAttackMultiplier;

        [Header("Weapon Type")]
        public WeaponType weaponType;

        [Header("Item Actions")]
        public ItemAction hold_RB_action;
        public ItemAction Tap_RB_action;
        public ItemAction hold_LB_action;
        public ItemAction Tap_LB_action;
        public ItemAction hold_RT_action;
        public ItemAction Tap_RT_action;
        public ItemAction hold_LT_action;
        public ItemAction Tap_LT_action;



        public string[] ReturnAttackList(bool twoHanded=false, bool HeavyAttack = false)
        {
            if (HeavyAttack)//two handed heavy
            {
                if (twoHanded)
                    return TwoHandHeavyAttackAnims;
               
                
                else // right handed heavy
                    return RightHandHeavyAttackAnims;

                
            }
            else
            {
                if (twoHanded)
                    return TwoHandedAttackAnims;
                

                else // right handed light
                    return RightHandAttackAnims;
            }

        }

        public string ReturnBlockingAnim(bool twoHanded)
        {
            if (twoHanded) return TH_Blocking;
            else return OH_blocking;
        }

    }
}
