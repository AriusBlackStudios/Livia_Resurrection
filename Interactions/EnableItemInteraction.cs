using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia{
    public class EnableItemInteraction : Interactable
    {   

        public GameObject objectToEnable;
        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            objectToEnable.SetActive(true);

            //pick up the item and add it to the palyer inventory
        }


    }
}
