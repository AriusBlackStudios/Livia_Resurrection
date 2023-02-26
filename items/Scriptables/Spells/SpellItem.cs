using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public enum SpellType { LightSpell, DarkSpell,bloodSpell};
    public class SpellItem : Item
    {
        [Header("FX for Spell")]
        [Tooltip("Optional: The Spell effect that is stationary as the spell is 'warming up'")]
        [SerializeField]protected GameObject spellWarmUpFX;

        [Tooltip("Optional: The Spell effect that is Created When the spell is successfully Cast")]
        [SerializeField] protected GameObject spellCastFX;

        [Tooltip("The EXACT name of the Right Hand Animation In The Caster's Animator Controller")]
        [SerializeField] protected string RightHandSpellAnim;

        [Tooltip("The EXACT name of the Left Hand Animation In The Caster's Animator Controller")]
        [SerializeField] protected string LeftHandSpellAnim;

        [Tooltip("The Amount Of Sanity (MP) Consumed By Casting")]
        public float SanityPointCost;

        [Header("Spell Type")] //spell types will probably be light, dark, blood
        public SpellType spellType;

        protected bool isLeftHand;

        public virtual void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerstats, WeaponSlotManager weaponSlot, bool isLeft = false)
        {
            Debug.Log("You Attempt to cast a Spell");
            isLeftHand = isLeft;
        }

        public virtual void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerstats, WeaponSlotManager weaponSlot, Quaternion cameraRotation)
        {
            Debug.Log("You succesfully cast a Spell");
            playerstats.DeductFocusPoints(SanityPointCost);
        }

    }
}
