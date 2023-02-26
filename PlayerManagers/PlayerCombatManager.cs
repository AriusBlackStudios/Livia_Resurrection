using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class PlayerCombatManager : MonoBehaviour
    {

        PlayerManager playerManager;





        public string lastAttack;

        public List<CharacterManager> availTargets= new List<CharacterManager>();
        public CharacterManager nearestAutoAttackTarget;
        public float AutoAttackDistance;

        [Header("Critical Attack Layers")]
        [Tooltip("The Layer that The Backstab Collider exists")]
        public  LayerMask backStabLayer;

        [Tooltip("The Layer that The riposte Collider exists")]
        public LayerMask riposteLayer;


        public int leftHandComboIndex=-1;
        public int rightHandComboIndex=-1;
        public int twoHandComboIndex = -1;

        private void Awake()
        {
            

            playerManager = GetComponent<PlayerManager>();

        }

        #region Player Attack Support Functions
        public void DetectEnemyInRadius()
        {
            float shortestDistance = Mathf.Infinity;
            Collider[] colliders = Physics.OverlapSphere(transform.position, AutoAttackDistance);

            for (int i=0; i < colliders.Length; i++)
            {
                CharacterManager character = colliders[i].GetComponent<CharacterManager>();
                if (character!= null)
                {
                    Vector3 LockTargetDirection = character.transform.position - transform.position;
                    float distanceFromTarget = Vector3.Distance(transform.position,character.transform.position);
                    float viewableAngle = Vector3.Angle(LockTargetDirection,playerManager.main_camera.transform.forward);
                    if (character.transform.root != transform.root && //not livia
                        viewableAngle>-50 && viewableAngle<50 && //within livia's sight
                        distanceFromTarget <= AutoAttackDistance//not too far
                        && character.GetComponent<CharacterStats>().isDead ==false)//not dead
                    {
                        availTargets.Add(character);
                    }
                }
            }

            for (int i = 0; i < availTargets.Count; i++)
            {


                float distanceFromTarget = Vector3.Distance(transform.position, availTargets[i].transform.position);
                    if (distanceFromTarget < shortestDistance)
                    {
                        shortestDistance = distanceFromTarget;
                        nearestAutoAttackTarget = availTargets[i];
                    }
                
            }
            availTargets.Clear();


        }

        public string ReturnNextAttackString(string[] attackCombo,bool isLeft,bool isTwoHanded)
        {
      
            if (isTwoHanded)
            {
                leftHandComboIndex = -1;
                rightHandComboIndex = -1;
                twoHandComboIndex += 1;
                if (twoHandComboIndex >= attackCombo.Length)
                {
                    twoHandComboIndex = 0;
                }
                return attackCombo[twoHandComboIndex];
            }
            else
            {
                if (isLeft)
                {

                    rightHandComboIndex = -1;
                    twoHandComboIndex = -1;

                    leftHandComboIndex += 1;
                    if (leftHandComboIndex >= attackCombo.Length)
                    {
                        leftHandComboIndex = 0;
                    }
                    return attackCombo[leftHandComboIndex];
                }
                else
                {
                    leftHandComboIndex = -1;
                    twoHandComboIndex = -1;

                    rightHandComboIndex += 1;
                    if (rightHandComboIndex >= attackCombo.Length)
                    {
                        rightHandComboIndex = 0;
                    }
        
                    return attackCombo[rightHandComboIndex];
                }
            }

            
           
        }


        #endregion


        #region Animation Events








        //called in animation, dont let this 0 reference nonsense decieve you
        private void SuccessfullyCastSpellAttack()
        {

            playerManager.inventory.currentSpell.SuccessfullyCastSpell(playerManager.animatorManager, playerManager.stats, playerManager.weaponSlotManager, playerManager.main_camera.transform.rotation);
            playerManager.animatorManager.anim.SetBool("isFiringSpell", true);
        }



        #endregion



        public void AttemptBackstabOrReposte()
        {

            Debug.Log("Backstab and repost Called");
            playerManager.weaponSlotManager.attackingWeapon = playerManager.inventory.rightWeapon;
            RaycastHit hit;

            //back stabbing logic
            if (Physics.Raycast(playerManager.inputHandler.criticalAttackRaycastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, .5f, backStabLayer))
            {
                Debug.Log("backstabcollider detected");
                CharacterManager characterManager = hit.transform.gameObject.GetComponent<CharacterManager>();
                DamageCollider rightWeapon = playerManager.weaponSlotManager.rightHandDamageCollider;
                if (characterManager != null)
                {
                    playerManager.transform.position = characterManager.backstabCollider.critDamageTransform.position;
                    playerManager.transform.rotation = characterManager.backstabCollider.critDamageTransform.rotation;
                  
                    float criticalDamage = playerManager.inventory.rightWeapon.criticalDamageMultiplier * rightWeapon.currentWeaponDamage;
                    Debug.Log("crit"+ criticalDamage);
                    characterManager.pendingCriticalDamage = criticalDamage;
                    Debug.Log("Backstabbed ");
                    playerManager.animatorManager.PlayTargetAnimation("Back Stab", true);

                        
                    characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Back Stabbed",true);

                }
            }

            //riposte
            else if (Physics.Raycast(playerManager.inputHandler.criticalAttackRaycastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, .7f, riposteLayer))
            {
                Debug.Log(" repost colider detected");
                CharacterManager characterManager = hit.transform.gameObject.GetComponent<CharacterManager>();
                DamageCollider rightWeapon = playerManager.weaponSlotManager.rightHandDamageCollider;
                if (characterManager != null && characterManager.canBeRiposted == true)
                {

                    playerManager.transform.position = characterManager.riposteCollider.critDamageTransform.position;
                    playerManager.transform.rotation = characterManager.riposteCollider.critDamageTransform.rotation;

                    float criticalDamage = playerManager.inventory.rightWeapon.criticalDamageMultiplier * (playerManager.fateManager.Multiply(playerManager.inventory.rightWeapon) + ((rightWeapon.currentWeaponDamage * playerManager.stats.AttackPower) / 100));
                    characterManager.pendingCriticalDamage = criticalDamage;
                    Debug.Log(" reposted");
                    playerManager.animatorManager.PlayTargetAnimation("Riposte", true);
                    characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Riposted", true);

                }
            }
        }
    }
}
