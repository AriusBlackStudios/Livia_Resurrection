using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class WeaponSlotManager : MonoBehaviour
    {
        //----------------- CLASS Variables ---------------------------
        #region Class Variables

        PlayerManager playerManager;

        [HideInInspector] public WeaponHolderSlot leftHandSlot;
         public WeaponHolderSlot rightHandSlot;
        WeaponHolderSlot backSlot;
        WeaponHolderSlot hipSlot;


        [HideInInspector] public WeaponItem attackingWeapon;
        [HideInInspector] public DamageCollider leftHandDamageCollider;
        [HideInInspector] public DamageCollider rightHandDamageCollider;


        QuickSlotsUI quickSlotsUI;


      


        #endregion

        private void Awake()
        {
            //search through playermodel for weapon slot scripts and assigns them
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();

            quickSlotsUI= FindObjectOfType<QuickSlotsUI>();


            playerManager = GetComponent<PlayerManager>();


            foreach(WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
                else if (weaponSlot.isBackSlot)
                {
                    backSlot = weaponSlot;
                }
                else if (weaponSlot.isHipSlot)
                {
                    hipSlot = weaponSlot;
                }
            }
        }

        //CALLED IN PLAYER EFFECTS MANAGER
        public void LoadBothWeaponsOnSlots()
        {
            playerManager.stats.offensivePoiseBonus =0f;
            LoadWeaponOnSlot(playerManager.inventory.rightWeapon,false);
            LoadWeaponOnSlot(playerManager.inventory.leftWeapon, true);
        }

        //CALLED IN INPUT HANDLER
        //CALLED IN PLAYER INVENTORY
        //CALLED IN WEAPON INVENTORY SLOT UI
        public void LoadWeaponOnSlot(WeaponItem weaponItem,bool isLeft)
        {

          //  playerStats.offensivePoiseBonus += weaponItem.offensivePoiseBonus;

            if (isLeft)
            {
                if (playerManager.inputHandler.TwoHand_Flag) return;
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
                #region Handle Left Weapon Idle Animation
                if (weaponItem != null)
                {
                    playerManager.animator.CrossFade(weaponItem.left_hand_idle, 0.2f);
                    playerManager.animator.CrossFade(weaponItem.left_arm_idle, 0.2f);
                }
                else
                {
                    playerManager.animator.CrossFade("Left Arm Empty", 0.2f);
                }
                #endregion
            }
            else
            {
              
                if (playerManager.inputHandler.TwoHand_Flag)
                {
                    if (leftHandSlot.currentWeapon.isLargeWeapon)
                    {


                        backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    }
                    else
                    {
                        hipSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    }
                    leftHandSlot.UnloadWeaponAndDestroy();
                    playerManager.animator.CrossFade(weaponItem.Two_Handled_Idle, 0.2f);

                }
                else
                {
                    playerManager.animator.CrossFade("Both Arms Empty", 0.2f);
                    backSlot.UnloadWeaponAndDestroy();
                    hipSlot.UnloadWeaponAndDestroy();
                    #region Handle Right Weapon Idle Animation
                    if (weaponItem != null)
                    {
                        playerManager.animator.CrossFade(weaponItem.right_hand_idle, 0.2f);
                        playerManager.animator.CrossFade(weaponItem.right_arm_idle, 0.2f);
                    }
                    else
                    {
                        playerManager.animator.CrossFade("Right Arm Empty", 0.2f);
                    }
                    #endregion

                }
                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();

                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);

            }
            
        }



        #region Handle Weapon's Damage Collider
        private void LoadLeftWeaponDamageCollider()
        {
            leftHandDamageCollider= leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            if(leftHandDamageCollider != null)
            {
                leftHandDamageCollider.currentWeaponDamage = playerManager.inventory.leftWeapon.baseDamage;

                //leftHandDamageCollider.currentWeaponDamage = ((playerManager.inventory.leftWeapon.baseDamage * playerManager.stats.AttackPower)/100) + (playerManager.inventory.leftWeapon.baseDamage * playerManager.stats.playerFateManager.Multiply(playerManager.inventory.leftWeapon));
                leftHandDamageCollider.characterManager = GetComponent<CharacterManager>();
                leftHandDamageCollider.poiseBreak = playerManager.inventory.leftWeapon.poiseBreak;
                leftHandDamageCollider.teamIDnumber = playerManager.stats.teamIDnumber;

            }
            playerManager.effectsManager.leftWeaponEffects = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
            


        }

        private void LoadRightWeaponDamageCollider()
        {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            if(rightHandDamageCollider != null)
            {
                Debug.Log("Player manager" + playerManager);
                Debug.Log("right weapon" + playerManager.inventory.rightWeapon);
                Debug.Log("fate manager" +playerManager.fateManager);
                rightHandDamageCollider.currentWeaponDamage = 
                    (playerManager.inventory.rightWeapon.baseDamage * playerManager.stats.AttackPower)/100 +
                        (playerManager.inventory.rightWeapon.baseDamage * 
                        playerManager.fateManager.Multiply(playerManager.inventory.rightWeapon));
                rightHandDamageCollider.characterManager = GetComponent<CharacterManager>();
                rightHandDamageCollider.poiseBreak = playerManager.inventory.rightWeapon.poiseBreak;
                rightHandDamageCollider.teamIDnumber = playerManager.stats.teamIDnumber;
            }
            playerManager.effectsManager.rightWeaponEffects =rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();

        }


        public void OpenDamageCollider()
        {
            if (playerManager.isUsingRightHand)
            {
                rightHandDamageCollider.EnableDamageCollider();
            }
            if (playerManager.isUsingLeftHand)
            {
                leftHandDamageCollider.EnableDamageCollider();
            }
        }

        public void CloseDamageCollider()
        {
            if (leftHandDamageCollider != null)
            {
                leftHandDamageCollider.DisableDamageCollider();
            }
            if(rightHandDamageCollider != null)
            {
                rightHandDamageCollider.DisableDamageCollider();
            }

        }
        #endregion


        #region Handle Weapon Stamina Drainage
        public void DrainStaminaLightAttack()
        {
            playerManager.stats.TakeStaminaDamage(attackingWeapon.baseStamina * attackingWeapon.lightAttackMuiltiplier);
        }

        public void DrainStaminaHeavyAttack()
        {
            playerManager.stats.TakeStaminaDamage(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier);
        }
        #endregion

        #region Handle weapon's Poise Bonus ANIMATION EVENTS

        public void GrantWeaponAttackingPoiseBonus()
        {
            playerManager.stats.totalPoiseDefense = playerManager.stats.totalPoiseDefense + attackingWeapon.offensivePoiseBonus;

        }
        public void ResetWeaponAttackingPoiseBonus()
        {
            playerManager.stats.totalPoiseDefense = playerManager.stats.CharacterPoiseBase;
        }


        #endregion
    }
}
