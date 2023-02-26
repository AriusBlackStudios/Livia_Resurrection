using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    //assigned to a  trigger collider that hovers just infront of the player model, just wide enough to block an attack
    public class BlockingCollider : MonoBehaviour
    {
        BoxCollider blockingCol;
        public float blockingPhysicalDamageAbsorption;
        public float blockingDarkDamageAbsorption;
        

        private void Awake()
        {
            blockingCol = GetComponent<BoxCollider>();
        }

        //called when equipting item
        public void SetColliderDamageAbsorption(WeaponItem weapon)
        {
            if( weapon != null)
            {
                blockingPhysicalDamageAbsorption = weapon.physicalDamageAbsorption;
                blockingDarkDamageAbsorption= weapon.darkDamageAbsorption;
                //ect
            }
        }

        //animation Event
        public void EnableBlockingCollider()
        {
            blockingCol.enabled = true;
        }

        //animation Event
        public void DisableBLockingCollider()
        {
            blockingCol.enabled = false;
        }
    }
}

