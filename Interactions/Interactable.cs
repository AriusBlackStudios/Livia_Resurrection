using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class Interactable : MonoBehaviour
    {
        [Header("Interactable Settings")]

        [Tooltip("Message Displayed When in Range")]
        public string interactableText;

        [Tooltip("Automatically open interaction when in range")]
        public bool AutoInteract = false;

        [Header("Item Information")]
        [SerializeField] protected int itemPickUpID;
        [SerializeField] protected bool hasBeenInteractedWith;

        protected virtual void Start(){

        }

        public virtual void Interact(PlayerManager playerManager)
        {

        }
    }
}
