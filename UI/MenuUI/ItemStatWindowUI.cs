using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace Livia
{
    public class ItemStatWindowUI : MonoBehaviour
    {
        [Header("All Items Stats")]
        public TMP_Text itemNameText;
        public TMP_Text itemDescriptionText;
        public Image itemIconImage;
        public TMP_Text itemloreText;

        [Header("WeaponItem Stats")]
        public TMP_Text WeaponName;
        public TMP_Text WeaponStatRequirment;
        public TMP_Text OffensivePoiseBonus;
        public TMP_Text BaseDMG;
        public TMP_Text DMGMultiplier;
        public TMP_Text PhysDMGAbsorb;
        public TMP_Text DarkDMGAbsorb;
        public TMP_Text PoiseBreakText;

        [Header("Tarot Card Stats")]
        public TMP_Text cardName;
        public TMP_Text cardSuit;
        public TMP_Text cardElement;
        public TMP_Text bonusData;
        public TMP_Text cardMeaning;





        public void UpdateWeaponStatsUI(WeaponItem weapon)
        {
            if (weapon == null) return;
            UpdateBasicItemStats(weapon);
            WeaponName.text = weapon.itemName;
            WeaponStatRequirment.text = weapon.weaponStatRequirment.ToString() + " " + weapon.StatLevelReq.ToString();
            OffensivePoiseBonus.text = weapon.offensivePoiseBonus.ToString();
            BaseDMG.text = weapon.offensivePoiseBonus.ToString();
            DMGMultiplier.text = weapon.criticalDamageMultiplier.ToString();
            PhysDMGAbsorb.text = weapon.physicalDamageAbsorption.ToString();
            DarkDMGAbsorb.text = weapon.darkDamageAbsorption.ToString();
            PoiseBreakText.text = weapon.poiseBreak.ToString();
        }

        public void UpdateTarotItemStats(TarotCard card)
        {
            if (card == null) return;
            UpdateBasicItemStats(card);
            cardName.text = card.itemName;
            cardSuit.text = card.cardSuit.ToString();
            cardElement.text = card.cardElement.ToString();
            bonusData.text = card.DisplayCardStats();
            cardMeaning.text =card.cardMeaning;


    }
        public void UpdateBasicItemStats(Item item)
        {
            itemNameText.text = item.itemName;
            if (item.itemIcon != null)
            {
                itemIconImage.sprite = item.itemIcon;
                itemIconImage.enabled = true;
            }
            itemDescriptionText.text = item.itemDescription;
            itemloreText.text = item.itemLore;


        }


    }
}
