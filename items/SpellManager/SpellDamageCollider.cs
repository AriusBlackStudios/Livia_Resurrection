using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class SpellDamageCollider : DamageCollider
    {
        [HideInInspector]public CharacterManager caster;
        void Start()
        {
            caster = GetComponentInParent<CharacterManager>();
          //  var tm = GetComponentInChildren<RFX1_TransformMotion>(true);
           // if (tm != null) tm.CollisionEnter += Tm_CollisionEnter;
        }

 /*       private void Tm_CollisionEnter(object sender, RFX1_TransformMotion.RFX1_CollisionInfo e)
        {

            if (e.Hit.transform.tag == "Character")
            {

                CharacterStats otherCharacterStats = e.Hit.transform.GetComponent<CharacterStats>();
                CharacterManager otherCahracterManager = e.Hit.transform.GetComponent<CharacterManager>();
                //character effects manager
                BlockingCollider shield = e.Hit.transform.GetComponentInChildren<BlockingCollider>();

                if (otherCharacterStats != null)
                {
                    if (otherCharacterStats.teamIDnumber == teamIDnumber) return;

                    otherCharacterStats.TakeDamage(currentWeaponDamage);


                }
            }
            Destroy(gameObject);//remove thing


        }
    
    */
    }
}
