using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Livia
{
    public class LevelUpUI : MonoBehaviour
    {
        PlayerManager player;
        public Button confirmLvlUp;

        [Header("Player Level")]
        public int currentPlayerLevel;
        public int projectedPlayerLevel;
        public TMP_Text currentPlayerLevelText;
        public TMP_Text projectedPlayerLevelText;

        [Header("Shards Level")]
        public TMP_Text currentShardsText;
        public TMP_Text requiredShardsText;
        private int ShardsReqToLevelUp;
        [SerializeField] int baseLevelUpCost=5;

        [Header("Vitality Level")]
        public Slider vitalitySlider;
        public TMP_Text currentVitalityLevelText;
        public TMP_Text projectedVitalityLevelText;

        [Header("Endurance Level")]
        public Slider enduranceSlider;
        public TMP_Text currentEnduranceLevelText;
        public TMP_Text projectedEnduranceLevelText;

        [Header("Sanity Level")]
        public Slider sanitySlider;
        public TMP_Text currentSanityLevelText;
        public TMP_Text projectedSanityLevelText;

        [Header("Strength Level")]
        public Slider StrengthSlider;
        public TMP_Text currentStrengthLevelText;
        public TMP_Text projectedStrengthLevelText;

        [Header("Dexterity Level")]
        public Slider dexteritySlider;
        public TMP_Text currentDexterityLevelText;
        public TMP_Text projecteDexterityLevelText;

        [Header("Magic Level")]
        public Slider magicSlider;
        public TMP_Text currentMagicLevelText;
        public TMP_Text projecteMagicLevelText;

        [Header("Blasphemy Level")]
        public Slider BlasphemySlider;
        public TMP_Text currentBlasphemyLevelText;
        public TMP_Text projecteBlasphemyLevelText;

        [Header("Luck Level")]
        public Slider LuckSlider;
        public TMP_Text currentLuckLevelText;
        public TMP_Text projecteLuckLevelText;

        private void OnEnable()
        {
            player=FindObjectOfType<PlayerManager>();

            //current player level set up
            currentPlayerLevel = player.stats.char_lvl;
            currentPlayerLevelText.text = currentPlayerLevel.ToString();
            projectedPlayerLevel = player.stats.char_lvl;
            projectedPlayerLevelText.text = projectedPlayerLevel.ToString();

            currentShardsText.text = player.stats.ShardCount.ToString();
            ShardsReqToLevelUp = 0;
            requiredShardsText.text = ShardsReqToLevelUp.ToString();

            //vitality set up
            vitalitySlider.value = player.stats.vitalityLevel;
            vitalitySlider.minValue = player.stats.vitalityLevel;
            vitalitySlider.maxValue = 99;
            currentVitalityLevelText.text = player.stats.vitalityLevel.ToString();
            projectedVitalityLevelText.text = player.stats.vitalityLevel.ToString();


            enduranceSlider.value = player.stats.EnduranceLevel;
            enduranceSlider.minValue = player.stats.EnduranceLevel;
            enduranceSlider.maxValue = 99;
            currentEnduranceLevelText.text = player.stats.EnduranceLevel.ToString();
            projectedEnduranceLevelText.text = player.stats.EnduranceLevel.ToString();


            sanitySlider.value = player.stats.SanityLevel;
            sanitySlider.minValue = player.stats.SanityLevel;
            sanitySlider.maxValue = 99;
            currentSanityLevelText.text = player.stats.SanityLevel.ToString();
            projectedSanityLevelText.text = player.stats.SanityLevel.ToString();


            StrengthSlider.value = player.stats.StrengthLevel;
            StrengthSlider.minValue = player.stats.StrengthLevel;
            StrengthSlider.maxValue = 99;
            currentStrengthLevelText.text = player.stats.StrengthLevel.ToString();
            projectedStrengthLevelText.text = player.stats.StrengthLevel.ToString();

            dexteritySlider.value = player.stats.DexterityLevel;
            dexteritySlider.minValue = player.stats.DexterityLevel;
            dexteritySlider.maxValue = 99;
            currentDexterityLevelText.text = player.stats.DexterityLevel.ToString();
            projecteDexterityLevelText.text = player.stats.DexterityLevel.ToString();

            magicSlider.value = player.stats.MagicLevel;
            magicSlider.minValue = player.stats.MagicLevel;
            magicSlider.maxValue = 99;
            currentMagicLevelText.text = player.stats.MagicLevel.ToString();
            projecteMagicLevelText.text = player.stats.MagicLevel.ToString();


            BlasphemySlider.value = player.stats.BlasphemyLevel;
            BlasphemySlider.minValue = player.stats.BlasphemyLevel;
            BlasphemySlider.maxValue = 99;
            currentBlasphemyLevelText.text= player.stats.BlasphemyLevel.ToString();
            projecteBlasphemyLevelText.text = player.stats.BlasphemyLevel.ToString();

            LuckSlider.value = player.stats.LuckLevel;
            LuckSlider.minValue = player.stats.LuckLevel;
            LuckSlider.maxValue = 99;
            currentLuckLevelText.text =player.stats.LuckLevel.ToString();
            projecteLuckLevelText.text = player.stats.LuckLevel.ToString();




        }


        
        public void ConfirmPlayerLevelUpStats()
        {
            player.stats.char_lvl = projectedPlayerLevel;
            player.stats.vitalityLevel = Mathf.RoundToInt(vitalitySlider.value);
            player.stats.EnduranceLevel = Mathf.RoundToInt(enduranceSlider.value);
            player.stats.SanityLevel = Mathf.RoundToInt(sanitySlider.value);
            player.stats.DexterityLevel = Mathf.RoundToInt(dexteritySlider.value);
            player.stats.StrengthLevel = Mathf.RoundToInt(StrengthSlider.value);
            player.stats.MagicLevel = Mathf.RoundToInt(magicSlider.value);
            player.stats.BlasphemyLevel = Mathf.RoundToInt(BlasphemySlider.value);
            player.stats.LuckLevel = Mathf.RoundToInt(LuckSlider.value);

            player.stats.SetMaxHealthFromHealthLevel();
            player.stats.SetMaxStaminaFromStaminaLevel();
            player.stats.SetMaxFocusFromFocusLevel();
            player.stats.SetUpStrengthSubStats();


            player.stats.CompleteHealLevelUpAndRest();
            player.stats.SpendShards(ShardsReqToLevelUp);
            



        }

        private void CalculateCostToLevelUp()
        {
            for (int i =0; i < projectedPlayerLevel; i++)
            {
                ShardsReqToLevelUp = ShardsReqToLevelUp + Mathf.RoundToInt((projectedPlayerLevel * baseLevelUpCost) * 1.5f);
            }
            requiredShardsText.text = ShardsReqToLevelUp.ToString();
        }
        private void UpdateProjectedPlayerLevel()
        {
            ShardsReqToLevelUp = 0;
            projectedPlayerLevel = currentPlayerLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(vitalitySlider.value) - (int)player.stats.vitalityLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(enduranceSlider.value) - (int)player.stats.EnduranceLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(sanitySlider.value) - (int)player.stats.SanityLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(dexteritySlider.value) - (int)player.stats.DexterityLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(StrengthSlider.value) - (int)player.stats.StrengthLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(magicSlider.value) - (int)player.stats.MagicLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(BlasphemySlider.value) - (int)player.stats.BlasphemyLevel;
            projectedPlayerLevel = projectedPlayerLevel + Mathf.RoundToInt(LuckSlider.value) - (int)player.stats.LuckLevel;

            CalculateCostToLevelUp();
            if (player.stats.ShardCount >= ShardsReqToLevelUp)
            {
                confirmLvlUp.interactable = true;
            }
            else
            {
                confirmLvlUp.interactable = false;
            }
            projectedPlayerLevelText.text = projectedPlayerLevel.ToString();


        }

        public void UpdateVitalitySlider()
        {
            projectedVitalityLevelText.text = vitalitySlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }
        public void UpdateEnduranceSlider()
        {
            projectedEnduranceLevelText.text = enduranceSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }
        public void UpdateDexSlider()
        {
            projecteDexterityLevelText.text = dexteritySlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }
        public void UpdateSanitySlider()
        {
            projectedSanityLevelText.text = sanitySlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }
        public void UpdateStrengthSlider()
        {
            projectedStrengthLevelText.text = StrengthSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }
        public void UpdateMagicSlider()
        {
            projecteMagicLevelText.text = magicSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }
        public void UpdateBlasphemySlider()
        {
            projecteBlasphemyLevelText.text = BlasphemySlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }
        public void UpdateLuckSlider()
        {
            projecteLuckLevelText.text = LuckSlider.value.ToString();
            UpdateProjectedPlayerLevel();
        }

    }
}
