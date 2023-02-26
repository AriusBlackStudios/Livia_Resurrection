using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{ 
    public enum CardSuit {Cups,Wands,Swords,Penacles,MajorArcana }
    public enum CardElement { Water, Fire, Earth, Air }
    [CreateAssetMenu(menuName = "TarotCard")]
    public class TarotCard : Collectable
    {
        [Header("tarot card stats")]
        [TextArea] public string cardMeaning;

        public CardSuit cardSuit;
        public CardElement cardElement;


        [Header("Player Base Stats Modifiers")]
        [Range(0, 100)] public float PlayerVitalityBoostPercent;//cups
        [Range(0, 100)] public float PlayerEnduranceBoostPercent;//swords or cups
        [Range(0, 100)] public float PlayerInteligenceBoostPercent;//wands
        [Range(0, 100)] public float PlayerStrengthBoostPercent;//swords
        [Range(0, 100)] public float PlayerDextarityBoostPercent;//penacles or swords
        [Range(0, 100)] public float PlayerMagicBoostPercent;//wands
        [Range(0, 100)] public float PlayerBlasphemyBoostPercent;//wands or penacles
        [Range(0, 100)] public float PlayerLuckBoostPercent;//penacles

        [Header("Player Vitality SubStats Modifiers")]
        [Range(0, 100)] public float PlayerVitality_MaxHealth_Boost_percent;
        [Range(0, 100)] public float PlayerVitality_PhysicalDefense_Boost_percent;
        [Range(0, 100)] public float PlayerVitality_CarryCapacity_Boost_percent;

        [Header("Player Endurance SubStats Modifiers")]
        [Range(0, 100)] public float PlayerEndurance_MaxStamina_Boost_percent;
        [Range(0, 100)] public float PlayerEndurance_EquiptableWeight_Boost_percent;
        [Range(0, 100)] public float PlayerEndurance_PoisonDefense_Boost_percent;
        [Range(0, 100)] public float PlayerEndurance_FireDefense_Boost_percent;
        [Range(0, 100)] public float PlayerEndurance_BleedDef_Boost_percent;

        [Header("Player Iteligence SubStats Modifiers")]
        [Range(0, 100)] public float PlayerInteligence_MaxMP_Boost_percent;
        [Range(0, 100)] public float PlayerInteligence_WeaponMastery_Boost_percent;
        [Range(0, 10)] public int PlayerInteligence_SpellMemorySlots_additional;

        [Header("Player Strength SubStats Modifiers")]
        [Range(0, 100)] public float PlayerStrength_AttackPower_Boost_percent;
        [Range(0, 100)] public float PlayerStrength_PoiseBreak_Boost_percent;
        [Range(0, 100)] public float PlayerStrength_PlayerMeleeWeapon_Boost_percent;

        [Header("Player Dexterity SubStats Modifiers")]
        [Range(0, 100)] public float PlayerDexterity_AttackPower_Boost_percent;
        [Range(0, 100)] public float PlayerDexterity_PoiseBreak_Boost_percent;
        [Range(0, 100)] public float PlayerDexterity_PlayerRangedWeapon_Boost_percent;

        [Header("Player Magic SubStats Modifiers")]
        [Range(0, 100)] public float PlayerMagic_SpellPower_Boost_percent;

        [Header("Player Blasphemy SubStats Modifiers")]
        [Range(0, 100)] public float PlayerBlasphemy_MagicDef_Boost_percent;
        [Range(0, 100)] public float PlayerBlasphemy_PhysicalDef_Boost_percent;








        public virtual void EquipTarotCard()
        {
            Debug.Log("Tarot card Equiped"+ itemName);
        }
        public virtual void UnequipTarotCard()
        {
            Debug.Log("Tarot card Unequiped" + itemName);
        }

        public string DisplayCardStats()
        {
            string cardStats = "";

            // this card is a vitality card
            if (PlayerVitalityBoostPercent > 0)
            {
                cardStats += "\nPlayer Vitality is Increased by " + PlayerVitalityBoostPercent.ToString() + "%";
                if (PlayerVitality_MaxHealth_Boost_percent>0)
                    cardStats += "\n Max Health is boosted by "+ PlayerVitality_MaxHealth_Boost_percent.ToString() + "%";
                if (PlayerVitality_PhysicalDefense_Boost_percent > 0)
                    cardStats += "\n Physical Defense is boosted by " + PlayerVitality_PhysicalDefense_Boost_percent.ToString() + "%";
                if (PlayerVitality_CarryCapacity_Boost_percent > 0)
                    cardStats += "\n CarryCapacity is boosted by " + PlayerVitality_CarryCapacity_Boost_percent.ToString() + "%";
            }

            //this card is a endurance card
            if (PlayerEnduranceBoostPercent > 0)
            {
                cardStats += "\nPlayer Endurance is Increased by " + PlayerEnduranceBoostPercent.ToString() + "%";
                if (PlayerEndurance_MaxStamina_Boost_percent > 0)
                    cardStats += "\n Max Stamina is boosted by " + PlayerEndurance_MaxStamina_Boost_percent.ToString() + "%";
                if (PlayerEndurance_BleedDef_Boost_percent > 0)
                    cardStats += "\n Bleed Defense is boosted by " + PlayerEndurance_BleedDef_Boost_percent.ToString() + "%";
                if (PlayerEndurance_EquiptableWeight_Boost_percent > 0)
                    cardStats += "\n Equiptable Weight is boosted by " + PlayerEndurance_EquiptableWeight_Boost_percent.ToString() + "%";
                if (PlayerEndurance_FireDefense_Boost_percent > 0)
                    cardStats += "\n Fire Defense is boosted by " + PlayerEndurance_FireDefense_Boost_percent.ToString() + "%";
                if (PlayerEndurance_PoisonDefense_Boost_percent > 0)
                    cardStats += "\n Poison defense is boosted by " + PlayerEndurance_PoisonDefense_Boost_percent.ToString() + "%";
            }

            if (PlayerInteligenceBoostPercent > 0)
            {
                cardStats += "\nPlayer Inteligence is Increased by " + PlayerInteligenceBoostPercent.ToString() + "%";
                if (PlayerInteligence_MaxMP_Boost_percent > 0)
                    cardStats += "\n Max MP is boosted by " + PlayerInteligence_MaxMP_Boost_percent.ToString() + "%";
                if (PlayerInteligence_WeaponMastery_Boost_percent > 0)
                    cardStats += "\n Weapon Mastry Rate is boosted by " + PlayerInteligence_WeaponMastery_Boost_percent.ToString() + "%";
                if (PlayerInteligence_SpellMemorySlots_additional > 0)
                    cardStats += "\n Spell Memory is Increased by " + PlayerInteligence_SpellMemorySlots_additional.ToString() + "slots";


            }

            if (PlayerStrengthBoostPercent > 0)
            {
                cardStats += "\nPlayer Strength is Increased by " + PlayerStrengthBoostPercent.ToString() + "%";
                if (PlayerStrength_AttackPower_Boost_percent > 0)
                    cardStats += "\n Attack Power is boosted by " + PlayerStrength_AttackPower_Boost_percent.ToString() + "%";
                if (PlayerStrength_PoiseBreak_Boost_percent > 0)
                    cardStats += "\n Poise Break is boosted by " + PlayerStrength_PoiseBreak_Boost_percent.ToString() + "%";
                if (PlayerStrength_PlayerMeleeWeapon_Boost_percent > 0)
                    cardStats += "\n Melee Weapon damage Increased by " + PlayerStrength_PlayerMeleeWeapon_Boost_percent.ToString() + "%";
            }

            if (PlayerDextarityBoostPercent > 0)
            {
                cardStats += "\nPlayer Dextarity is Increased by " + PlayerDextarityBoostPercent.ToString() + "%";
                if (PlayerDexterity_AttackPower_Boost_percent > 0)
                    cardStats += "\n Attack Power is boosted by " + PlayerDexterity_AttackPower_Boost_percent.ToString() + "%";
                if (PlayerDexterity_PoiseBreak_Boost_percent > 0)
                    cardStats += "\n Poise Break is boosted by " + PlayerDexterity_PoiseBreak_Boost_percent.ToString() + "%";
                if (PlayerDexterity_PlayerRangedWeapon_Boost_percent > 0)
                    cardStats += "\n Ranged Weapon damage Increased by " + PlayerDexterity_PlayerRangedWeapon_Boost_percent.ToString() + "%";
            }

            if (PlayerMagicBoostPercent > 0)
            {
                cardStats += "\nPlayer Magic is Increased by " + PlayerMagicBoostPercent.ToString() + "%";
                if (PlayerMagic_SpellPower_Boost_percent > 0)
                    cardStats += "\n Spell Effectiveness is boosted by " + PlayerMagic_SpellPower_Boost_percent.ToString() + "%";

            }

            if (PlayerBlasphemyBoostPercent > 0)
            {
                cardStats += "\nPlayer Blasphemy is Increased by " + PlayerBlasphemyBoostPercent.ToString() + "%";
                if (PlayerBlasphemy_MagicDef_Boost_percent > 0)
                    cardStats += "\n Magic Def is boosted by " + PlayerBlasphemy_MagicDef_Boost_percent.ToString() + "%";
                if (PlayerBlasphemy_PhysicalDef_Boost_percent > 0)
                    cardStats += "\n Physical Def is boosted by " + PlayerBlasphemy_PhysicalDef_Boost_percent.ToString() + "%";

            }

            if (PlayerLuckBoostPercent > 0)
            {
                cardStats += "\nPlayer Luck is Increased by " + PlayerLuckBoostPercent.ToString() + "%";
                
            }

            return cardStats;
        }

    }
}
