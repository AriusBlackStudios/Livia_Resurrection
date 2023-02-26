using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Livia
{
    public class QuickSlotsUI:MonoBehaviour
    {
        public Image rightWeaponIcon;
        public Image leftWeaponIcon;
        public Image spellIcon;
        public Image consumableIcon;
        public Text consumableQuantity;

        public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
        {
            if (isLeft == false)
            {
                if (weapon.itemIcon != null)
                {
                    rightWeaponIcon.sprite = weapon.itemIcon;
                    rightWeaponIcon.enabled = true;
                }
                else
                {
                    rightWeaponIcon.sprite = null;
                    rightWeaponIcon.enabled = false;
                }

            }
            else
            {
                if (weapon.itemIcon != null)
                {
                    leftWeaponIcon.sprite = weapon.itemIcon;
                    leftWeaponIcon.enabled = true;
                }
                else
                {
                    leftWeaponIcon.sprite = null;
                    leftWeaponIcon.enabled = false;
                }

            }
        }
        public void UpdateSpellSlotUI(SpellItem spellItem)
        {
            if (spellItem != null && spellItem.itemIcon != null)
            {
                spellIcon.sprite = spellItem.itemIcon;
                spellIcon.enabled = true;
            }
            else
            {
                spellIcon.sprite = null;
                spellIcon.enabled = false;
            }
        }

        public void UpdateConsumableSlotUI(ConsumeableItem consumableItem)
        {
            if (consumableItem != null && consumableItem.itemIcon != null)
            {
                consumableQuantity.text = consumableItem.currentItemAmount.ToString();
                //get reference to weather or not the consumable item is A FLASK ITEM
                FlaskItem flask = consumableItem as FlaskItem;// error here i need to be able to see if this object is a flask, a child ofr consumable
                if (flask != null)
                {
                    Debug.Log("Updating UI to Flask");
                    //check if count of current items is greater than zero
                    // full flask icon
                    if (flask.currentItemAmount > 0)
                    {
                        consumableIcon.sprite = consumableItem.itemIcon;
                        consumableIcon.enabled = true;
                    }
                    //check if count of current items is zero

                    else
                    {
                        //show empty flask icon
                        consumableIcon.sprite = flask.emptyImage;
                        consumableIcon.enabled = true;
                    }
                }
                //if its not a flask
                else
                {

                    consumableIcon.sprite = consumableItem.itemIcon;
                    consumableIcon.enabled = true;
                }

            }
            else
            {
                consumableIcon.sprite = null;
                consumableQuantity.text = "";
                consumableIcon.enabled = false;
            }
        }
    }
}