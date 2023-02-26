using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class OpenChest : Interactable
    {
        [Header("Chest Settings")]
        public Transform playerStandingPosition;
        public Transform itemSpawnPos;
        [Tooltip("Prefab of Item That will Sit In Scene to Be pickled Up")]
        public GameObject itemSpawner;
        [Tooltip("Scriptable Opject That will be assined to the Item Spawner")]
        public WeaponItem itemInChest;
        Animator animator;
        OpenChest openChest;
        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            openChest = GetComponent<OpenChest>();
        }

        protected  override void Start() {
            base.Start();

            if (!WorldSaveGameManager.instance.currentCharacterData.doorsInWorld.ContainsKey(itemPickUpID)){
                WorldSaveGameManager.instance.currentCharacterData.doorsInWorld.Add(itemPickUpID,false);
            }
            hasBeenInteractedWith = WorldSaveGameManager.instance.currentCharacterData.doorsInWorld[itemPickUpID];
            if(hasBeenInteractedWith){
                animator.Play("OpenChest");
                Destroy(openChest);
            }
        }
        public override void Interact(PlayerManager playerManager)
        {
            //notify character save data that this item has been looted
            if (WorldSaveGameManager.instance.currentCharacterData.doorsInWorld.ContainsKey(itemPickUpID)){
                WorldSaveGameManager.instance.currentCharacterData.doorsInWorld.Remove(itemPickUpID);
            }

            WorldSaveGameManager.instance.currentCharacterData.doorsInWorld.Add(itemPickUpID,true);
            hasBeenInteractedWith = true;

            //rotate player towards chest
            Vector3 rotationDirecton = transform.position - playerManager.transform.position;
            rotationDirecton.y = 0;
            rotationDirecton.Normalize();

            Quaternion tr = Quaternion.LookRotation(rotationDirecton);
            Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 300 * Time.deltaTime);
            playerManager.transform.rotation = targetRotation;

            //lo0ck transform to a certain point infront of chest
            playerManager.InteractableAnimtionTransformReset(playerStandingPosition,playerManager.ChestInteraction);
            animator.Play("OpenChest");
            StartCoroutine(SpawnItemInChest());
            WeaponPickUp weaponPickup= itemSpawner.GetComponent<WeaponPickUp>();
            if (weaponPickup != null)
            {
                weaponPickup.weapon = itemInChest;
            }
            //open the chest lid and animate the player
            

        }

        public IEnumerator SpawnItemInChest()
        {
            yield return new WaitForSeconds(1f);
            Instantiate(itemSpawner, itemSpawnPos);
            Destroy(openChest);

        }
    }
}

