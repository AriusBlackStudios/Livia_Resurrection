using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia {
    
    public class EnemyWeaponSlotManager : MonoBehaviour
    {
        [Header ("Enemy Equiped Weapons On Load")]
        public WeaponItem rightHandWeapon;
        public WeaponItem leftHandWeapon;

        //----------------PRIVATE CLASS REFERENCES -------------------------------
        WeaponHolderSlot rightHandSlot;
        WeaponHolderSlot leftHandSlot;
        EnemyStats enemyStats;
        DamageCollider leftHandDamageCollider;
        DamageCollider rightHandDamageCollider;

        private void Awake()
        {
            enemyStats = GetComponentInParent<EnemyStats>();
            LoadWeaponHolderSlots();
        }
        private void Start()
        {
            enemyStats.offensivePoiseBonus = 0f;
            LoadWeaponOnBothHands();
        }

        private void LoadWeaponHolderSlots()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();

            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        private void LoadWeaponOnBothHands()
        {
            if (rightHandWeapon != null)
            {
                LoadWeaponsOnSlot(rightHandWeapon, false);
            }
            if (leftHandWeapon != null)
            {
                LoadWeaponsOnSlot(leftHandWeapon, true);
            }
        }

        private void LoadWeaponsOnSlot(WeaponItem weapon, bool isLeft)
        {
            enemyStats.offensivePoiseBonus += weapon.offensivePoiseBonus;
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weapon;
                leftHandSlot.LoadWeaponModel(weapon);
            }
            else
            {
                rightHandSlot.currentWeapon = weapon;
                rightHandSlot.LoadWeaponModel(weapon);
            }
            LoadWeaponDamageCollider(isLeft);
        }

        private void LoadWeaponDamageCollider(bool isLeft)
        {
            if (isLeft)
            {

                leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                leftHandDamageCollider.characterManager =GetComponentInParent<CharacterManager>();
                leftHandDamageCollider.teamIDnumber = enemyStats.teamIDnumber;
                leftHandDamageCollider.poiseBreak = leftHandWeapon.poiseBreak;
            }
            else
            {
                rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                rightHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
                rightHandDamageCollider.teamIDnumber = enemyStats.teamIDnumber;
                rightHandDamageCollider.poiseBreak = rightHandWeapon.poiseBreak;
            }
        }


        // -----------------ANIMATOR EVENTS------------------------------------------

        #region animator events
        public void OpenDamageCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
        }

        public void CloseDamageCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }
        public void DrainStaminaLightAttack()
        {

        }

        public void DrainStaminaHeavyAttack()
        {

        }

        public void EnableCombo()
        {
        }

        public void DisableCombo()
        {

        }
        public void GrantWeaponAttackingPoiseBonus()
        {
            enemyStats.totalPoiseDefense = enemyStats.totalPoiseDefense + enemyStats.offensivePoiseBonus;


        }
        public void ResetWeaponAttackingPoiseBonus()
        {
            enemyStats.totalPoiseDefense = enemyStats.CharacterPoiseBase;
        }
        #endregion

    }


}
