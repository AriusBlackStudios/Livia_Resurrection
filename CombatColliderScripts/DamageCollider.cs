using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class DamageCollider : MonoBehaviour
    {
        [HideInInspector]   public CharacterManager characterManager;
                            private BoxCollider damageCollider;

        [HideInInspector]   public float poiseBreak;
                            public float currentWeaponDamage;

        [HideInInspector]   public int teamIDnumber = 0;

        private bool shield_has_been_hit = false;
        private bool has_been_parried = false;
        private string currentDamageAnimation;
        private string currentDeathAnimation;
        private void Awake()
        {
            damageCollider = GetComponent<BoxCollider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;


        }
       

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Character")
            {
                Debug.Log("Object hit" + collision.gameObject.name);
                
                shield_has_been_hit = false;
                has_been_parried = false;
                CharacterStats otherCharacterStats = collision.GetComponent<CharacterStats>();
                CharacterManager otherCahracterManager = collision.GetComponent<CharacterManager>();
                    //character effects manager
                BlockingCollider shield = collision.GetComponentInChildren<BlockingCollider>();
                if(otherCahracterManager != null)
                {
                    if (otherCharacterStats.teamIDnumber == teamIDnumber) return;
                    CheckForParry(otherCahracterManager);
                    CheckForBlock(otherCahracterManager,shield,otherCharacterStats);
                }
                if (otherCharacterStats != null)
                {
                    if (otherCharacterStats.teamIDnumber == teamIDnumber) return;
                    if (shield_has_been_hit) return;
                    if (has_been_parried) return;

         

                    //DETECT WHERE ON THE COLLIDER OUR WEAPON FIRST MAKES CONTACT
                    Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    float directionHitFrom = (Vector3.SignedAngle(characterManager.transform.forward, otherCahracterManager.transform.forward, Vector3.up));
                    ChooseWhichDirectionDamageCameFrom(directionHitFrom);
                    //otherCharacterEffects.PlayBloodSplatterFX(contactPoint);

                    UpdatePoiseWhenTriggered(otherCharacterStats);

                }
            }

            if (collision.tag == "Illusionary Wall")
            {
                
            }



                


        }

        //------------------Called In ON Trigger Enter --------------
        protected void CheckForParry(CharacterManager otherCharacter)
        {
            if (otherCharacter.isParrying && characterManager.canBeParried)
            {
                characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                has_been_parried = true;
            }
        }
        protected void CheckForBlock(CharacterManager otherCharacter, BlockingCollider shield,CharacterStats otherCharacterStats)
        {
            if (shield != null && otherCharacter.isBlocking)
            {
                float pysicalDamageAfterBlock = currentWeaponDamage - (currentWeaponDamage * shield.blockingPhysicalDamageAbsorption) / 100;
                if (otherCharacterStats != null)
                {
                    otherCharacterStats.TakeDamage(pysicalDamageAfterBlock, "Block Guard");
                    shield_has_been_hit = true;
                }
            }
        }
        protected void UpdatePoiseWhenTriggered(CharacterStats otherCharacterStats)
        {
            otherCharacterStats.poiseResetTimer = otherCharacterStats.totalPoiseRestTime;
            otherCharacterStats.totalPoiseDefense = otherCharacterStats.totalPoiseDefense - poiseBreak;


            if (otherCharacterStats.totalPoiseDefense > poiseBreak)
            {
                otherCharacterStats.TakeDamageNoAnimation(currentWeaponDamage, currentDeathAnimation);
            }
            else
            {

                otherCharacterStats.TakeDamage(currentWeaponDamage,currentDamageAnimation,currentDeathAnimation);
            }
        }

        protected void ChooseWhichDirectionDamageCameFrom(float dir)
        {
            if (dir >= 145 && dir < 180)
            {
                //hit from the front
                currentDamageAnimation = "Damage_Forward_01";
                currentDeathAnimation = "Death_Forward";
            }
            else if (dir<= -145 && dir>= -180)
            {
                currentDamageAnimation = "Damage_Forward_01";
                currentDeathAnimation = "Death_Forward";
            }
            else if (dir >= -45 && dir <= 45)
            {
                currentDamageAnimation = "Damage_Back_01";
                currentDeathAnimation = "Death_Back";
            }
            else if (dir >= -144 && dir <= -45)
            {
                currentDamageAnimation = "Damage_Left_01";
                currentDeathAnimation = "Death_Left";
            }
            else if (dir >= 45 && dir <= 144)
            {
                currentDamageAnimation = "Damage_Right_01";
                currentDeathAnimation = "Death_Right";
            }

        }
        //----------------CALLED IN ANIMATION EVENTS ----------
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }
        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

    }
}
