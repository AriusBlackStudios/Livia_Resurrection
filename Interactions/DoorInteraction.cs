using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class DoorInteraction : Interactable
    {
        Animator animator;
        DoorInteraction doorInteraction;
        
        
        public Transform playerStandingPosition;
        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            doorInteraction = GetComponent<DoorInteraction>();
        }

        protected override void Start() {
            if (!WorldSaveGameManager.instance.currentCharacterData.chestsInWorld.ContainsKey(itemPickUpID)){
                WorldSaveGameManager.instance.currentCharacterData.chestsInWorld.Add(itemPickUpID,false);
            }
            hasBeenInteractedWith = WorldSaveGameManager.instance.currentCharacterData.chestsInWorld[itemPickUpID];
            if(hasBeenInteractedWith){
                animator.Play("OpenDoors");
                Destroy(doorInteraction);
            }
        }
        public override void Interact(PlayerManager playerManager)
        {
            if (WorldSaveGameManager.instance.currentCharacterData.chestsInWorld.ContainsKey(itemPickUpID)){
                WorldSaveGameManager.instance.currentCharacterData.chestsInWorld.Remove(itemPickUpID);
            }

            WorldSaveGameManager.instance.currentCharacterData.chestsInWorld.Add(itemPickUpID,true);
            hasBeenInteractedWith = true;
            
            //rotate player towards Door
            Vector3 rotationDirecton = transform.position - playerManager.transform.position;
            rotationDirecton.y = 0;
            rotationDirecton.Normalize();

            Quaternion tr = Quaternion.LookRotation(rotationDirecton);
            Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 300 * Time.deltaTime);
            playerManager.transform.rotation = targetRotation;


            playerManager.InteractableAnimtionTransformReset(playerStandingPosition, playerManager.LargeDoorInteraction);
            animator.Play("OpenDoors");
            StartCoroutine(DestroyDoorOpen());



        }

        public IEnumerator DestroyDoorOpen()
        {
            yield return new WaitForSeconds(1f);
            Destroy(doorInteraction);

        }
    }
}
