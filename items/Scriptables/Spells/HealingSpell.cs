using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{

    [CreateAssetMenu(menuName ="Spells/HealingSpell")]
    public class HealingSpell : SpellItem
    {
        [Header("Healing Spell Effect")]
        [Tooltip("The percent amount of health recovered by the spell, used as a function of the player's Blasphemy")]
        [SerializeField][Range(0, 100)] private float HealthRecoveryAmount = 50;

        public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler,PlayerStats playerstats,WeaponSlotManager weaponSlotManager, bool isLeft = false)
        {
            base.AttemptToCastSpell(animatorHandler, playerstats,weaponSlotManager,isLeft);
            if (spellWarmUpFX != null)
            {
                GameObject instantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
                instantiateWarmUpSpellFX.transform.parent = null;
                Destroy(instantiateWarmUpSpellFX,10f);
            }
            if (isLeftHand)
            animatorHandler.PlayTargetAnimation(LeftHandSpellAnim, true);
            else
                animatorHandler.PlayTargetAnimation(RightHandSpellAnim, true);
        }

        public override void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerstats, WeaponSlotManager weaponSlot, Quaternion cameraRotation)
        {
            base.SuccessfullyCastSpell(animatorHandler, playerstats, weaponSlot, cameraRotation);
            if (spellCastFX!=null)
            {
                GameObject instantiateSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
                instantiateSpellFX.transform.parent = null;
                Destroy(instantiateSpellFX, 10f);

            }
            float pointsHealed = (HealthRecoveryAmount/100) *  playerstats.maxHealth; // base percentage to heal

            Debug.Log(pointsHealed);
            playerstats.HealPlayer(pointsHealed);
        }
    }
}
