using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        //------------------------CLASS VARIABLES ---------------------------
        #region PRIVATE CLASS VARIABLES
        BlockingCollider blockingCollider;
        PlayerManager player;


        #endregion

        private void Awake()
        {

            player = GetComponent<PlayerManager>();
            blockingCollider = GetComponentInChildren<BlockingCollider>();

        }

        //CALLED IN PLAYER COMBAT MANAGER WHEN PRESSING LB
        #region OPEN AND CLOSS BLOCKING COLLIDER
        public void OpenBlockingCollider()
        {
            //if fighting two-handed
            if (player.inputHandler.TwoHand_Flag)
            {
                blockingCollider.SetColliderDamageAbsorption(player.inventory.rightWeapon);
            }
            //if dual weilding
            else
            {
                blockingCollider.SetColliderDamageAbsorption(player.inventory.leftWeapon);
            }
            blockingCollider.EnableBlockingCollider();
        }

        public void CloseBlockingCollider()
        {
            blockingCollider.DisableBLockingCollider();
        }
        #endregion
    }
}

