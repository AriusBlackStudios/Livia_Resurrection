using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class PlayerFateManager : MonoBehaviour
    {

        [Header("Current Cards Equipted")]
        public TarotCard card_one;
        public TarotCard card_two;
        public TarotCard card_Three;
        public TarotCard card_four;
        public TarotCard card_Five;

        [Header("Boosts")]

        [Range(0, 100)] public float VitalityBoost;
        [Range(0, 100)] public float EnduranceBoost;
        [Range(0, 100)] public float InteligenceBoost;
        [Range(0, 100)] public float StrengthBoost;
        [Range(0, 100)] public float DextarityBoost;
        [Range(0, 100)] public float MagicBoost;
        [Range(0, 100)] public float BlasphemyBoost;
        [Range(0, 100)] public float LuckBoost;


        [Range(0, 100)] public float MaxHealth_Boost;
        [Range(0, 100)] public float MaxStamina_Boost;
        [Range(0, 100)] public float MaxMP_Boost;

        [Range(0, 100)] public float CarryCapacity_Boost_percent;
        [Range(0, 100)] public float EquiptableWeight_Boost_percent;

        [Range(0, 100)] public float AttackPower_Boost;
        [Range(0, 100)] public float PoiseBreak_Boost_percent;

        [Range(0, 100)] public float PhysicalDefense_Boost;
        [Range(0, 100)] public float PoisonDefense_Boost;
        [Range(0, 100)] public float FireDefense_Boost;
        [Range(0, 100)] public float BleedDef_Boost;
        [Range(0, 100)] public float MagicDef_Boost;



        [Range(0, 100)] public float WeaponMastery_Boost;
        [Range(0, 10)]  public int   SpellMemorySlots_additional;
        [Range(0, 100)] public float MeleeWeapon_Boost;
        [Range(0, 100)] public float RangedWeapon_Boost;
        [Range(0, 100)] public float SpellPower_Boost;


        public void FateAwake()
        {
            if (card_one != null) AddCardStats(card_one);
            if (card_two != null) AddCardStats(card_two);
            if (card_Three != null) AddCardStats(card_Three);
            if (card_four != null) AddCardStats(card_four);
            if (card_Five != null) AddCardStats(card_Five);
        }


        public float Multiply(WeaponItem weapon)
        {
            //melee
            if (weapon.weaponType == WeaponType.Melee)
            {
                return (MeleeWeapon_Boost/100)*weapon.baseDamage;
            }
            //strength
            else if(weapon.weaponType == WeaponType.SpellCaster)
            {
                return (SpellPower_Boost / 100)* weapon.baseDamage;
            }
            //dexterity
            else if (weapon.weaponType == WeaponType.Ranged)
            {
                return (RangedWeapon_Boost / 100) * weapon.baseDamage;
            }
            return 1;//no bonus
        }


        public void AddCardStats(TarotCard tarotCard)
        {
            VitalityBoost += tarotCard.PlayerVitalityBoostPercent;
            EnduranceBoost += tarotCard.PlayerEnduranceBoostPercent;
            InteligenceBoost += tarotCard.PlayerInteligenceBoostPercent;
            StrengthBoost+= tarotCard.PlayerStrengthBoostPercent;
            DextarityBoost += tarotCard.PlayerDextarityBoostPercent;
            MagicBoost += tarotCard.PlayerMagicBoostPercent;
            BlasphemyBoost += tarotCard.PlayerBlasphemyBoostPercent;
            LuckBoost += tarotCard.PlayerLuckBoostPercent;

            MaxHealth_Boost += tarotCard.PlayerVitality_MaxHealth_Boost_percent;
            MaxStamina_Boost += tarotCard.PlayerEndurance_MaxStamina_Boost_percent;
            MaxMP_Boost += tarotCard.PlayerInteligence_MaxMP_Boost_percent;

            CarryCapacity_Boost_percent += tarotCard.PlayerVitality_CarryCapacity_Boost_percent;
            EquiptableWeight_Boost_percent += tarotCard.PlayerEndurance_EquiptableWeight_Boost_percent;

            AttackPower_Boost += tarotCard.PlayerDexterity_AttackPower_Boost_percent+ tarotCard.PlayerStrength_AttackPower_Boost_percent;
            PoiseBreak_Boost_percent += tarotCard.PlayerDexterity_PoiseBreak_Boost_percent + tarotCard.PlayerStrength_PoiseBreak_Boost_percent;

            PhysicalDefense_Boost += tarotCard.PlayerBlasphemy_MagicDef_Boost_percent;
            PoisonDefense_Boost += tarotCard.PlayerEndurance_PoisonDefense_Boost_percent;
            FireDefense_Boost += tarotCard.PlayerEndurance_FireDefense_Boost_percent;
            BleedDef_Boost+= tarotCard.PlayerEndurance_BleedDef_Boost_percent;
            MagicDef_Boost += tarotCard.PlayerBlasphemy_MagicDef_Boost_percent;



            WeaponMastery_Boost += tarotCard.PlayerInteligence_WeaponMastery_Boost_percent;
            SpellMemorySlots_additional += tarotCard.PlayerInteligence_SpellMemorySlots_additional;
            MeleeWeapon_Boost += tarotCard.PlayerStrength_PlayerMeleeWeapon_Boost_percent;
            RangedWeapon_Boost += tarotCard.PlayerDexterity_PlayerRangedWeapon_Boost_percent;
            SpellPower_Boost += tarotCard.PlayerMagic_SpellPower_Boost_percent;


        }

        public void RemoveCardStats(TarotCard tarotCard)
        {
            VitalityBoost -= tarotCard.PlayerVitalityBoostPercent;
            EnduranceBoost -= tarotCard.PlayerEnduranceBoostPercent;
            InteligenceBoost -= tarotCard.PlayerInteligenceBoostPercent;
            StrengthBoost -= tarotCard.PlayerStrengthBoostPercent;
            DextarityBoost -= tarotCard.PlayerDextarityBoostPercent;
            MagicBoost -= tarotCard.PlayerMagicBoostPercent;
            BlasphemyBoost -= tarotCard.PlayerBlasphemyBoostPercent;
            LuckBoost -= tarotCard.PlayerLuckBoostPercent;

            MaxHealth_Boost -= tarotCard.PlayerVitality_MaxHealth_Boost_percent;
            MaxStamina_Boost -= tarotCard.PlayerEndurance_MaxStamina_Boost_percent;
            MaxMP_Boost -= tarotCard.PlayerInteligence_MaxMP_Boost_percent;

            CarryCapacity_Boost_percent -= tarotCard.PlayerVitality_CarryCapacity_Boost_percent;
            EquiptableWeight_Boost_percent -= tarotCard.PlayerEndurance_EquiptableWeight_Boost_percent;

            AttackPower_Boost -= tarotCard.PlayerDexterity_AttackPower_Boost_percent + tarotCard.PlayerStrength_AttackPower_Boost_percent;
            PoiseBreak_Boost_percent -= tarotCard.PlayerDexterity_PoiseBreak_Boost_percent + tarotCard.PlayerStrength_PoiseBreak_Boost_percent;

            PhysicalDefense_Boost -= tarotCard.PlayerBlasphemy_MagicDef_Boost_percent;
            PoisonDefense_Boost -= tarotCard.PlayerEndurance_PoisonDefense_Boost_percent;
            FireDefense_Boost -= tarotCard.PlayerEndurance_FireDefense_Boost_percent;
            BleedDef_Boost -= tarotCard.PlayerEndurance_BleedDef_Boost_percent;
            MagicDef_Boost -= tarotCard.PlayerBlasphemy_MagicDef_Boost_percent;



            WeaponMastery_Boost -= tarotCard.PlayerInteligence_WeaponMastery_Boost_percent;
            SpellMemorySlots_additional -= tarotCard.PlayerInteligence_SpellMemorySlots_additional;
            MeleeWeapon_Boost -= tarotCard.PlayerStrength_PlayerMeleeWeapon_Boost_percent;
            RangedWeapon_Boost -= tarotCard.PlayerDexterity_PlayerRangedWeapon_Boost_percent;
            SpellPower_Boost -= tarotCard.PlayerMagic_SpellPower_Boost_percent;


        }


        






    }
}
