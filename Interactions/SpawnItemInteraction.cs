using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia{
    public class SpawnItemInteraction : Interactable
    {
        public GameObject prefabSpawned;
        public Transform spawningPoint;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            SpawnItem();

            //pick up the item and add it to the palyer inventory
        }

        private void SpawnItem()
        {
            Instantiate(prefabSpawned,spawningPoint.position,spawningPoint.rotation);
        }
    }
}

