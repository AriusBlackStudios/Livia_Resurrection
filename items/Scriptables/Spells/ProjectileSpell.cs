using UnityEngine;


namespace Livia
{
    [CreateAssetMenu(menuName = "Spells/ProjectileSpell")]
    public class ProjectileSpell : SpellItem
    {



        public override void AttemptToCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerstats, WeaponSlotManager weaponSlot, bool isLeft = false)
        {
            base.AttemptToCastSpell(animatorHandler, playerstats, weaponSlot,isLeft);
            //instantiate the spell in the caster's hand


            //Play warm-up animation
            if (isLeftHand)
            {
                GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlot.leftHandSlot.transform);
                SpellDamageCollider spellDamageCollider = instantiatedWarmUpSpellFX.GetComponent<SpellDamageCollider>();

                animatorHandler.PlayTargetAnimation(LeftHandSpellAnim, true);
               // Destroy(instantiatedWarmUpSpellFX, 3);
            }
            else
            {
                GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmUpFX, weaponSlot.rightHandSlot.transform);
                animatorHandler.PlayTargetAnimation(RightHandSpellAnim, true);
              //  Destroy(instantiatedWarmUpSpellFX, 3);
            }


        }

        public override void SuccessfullyCastSpell(PlayerAnimatorManager animatorHandler, PlayerStats playerstats, WeaponSlotManager weaponSlot,Quaternion cameraRotation)
        {
            base.SuccessfullyCastSpell(animatorHandler, playerstats, weaponSlot, cameraRotation);
            //TODO change magic to shoot out of hand not camera

            Vector3 spawnPoint;
            if (isLeftHand)
                spawnPoint = weaponSlot.leftHandSlot.transform.position;

            else
                spawnPoint = weaponSlot.rightHandSlot.transform.position;
            GameObject instantiatedSpellFX = 
                Instantiate(spellCastFX,
                            spawnPoint,
                            cameraRotation);

            SpellDamageCollider spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
            spellDamageCollider.teamIDnumber = playerstats.teamIDnumber;
            instantiatedSpellFX.transform.parent = null;

            PlayerManager player = playerstats.GetComponent<PlayerManager>();
            player.combatManager.DetectEnemyInRadius();
           // RFX1_TransformMotion fancyTarget = instantiatedSpellFX.GetComponentInChildren<RFX1_TransformMotion>();
            //if (player.combatManager.nearestAutoAttackTarget != null)
            //    fancyTarget.Target=player.combatManager.nearestAutoAttackTarget.gameObject;
        }


    }
}
