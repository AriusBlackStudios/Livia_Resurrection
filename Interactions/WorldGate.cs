using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Livia
{
    public class WorldGate : Interactable
    {
        public string levelName;

        public Transform interaction;

        [Header("BonfireTeleportTransform")]
        public Transform worldGateTeleportTransform;


        [Header("Activation Status")]
        public bool hasBeenActivated;


        [Header("World Gate FX")]
        public ParticleSystem activationFX;
        public ParticleSystem activeFX;

        private void Awake()
        {
            if (hasBeenActivated)
            {
                if (activeFX != null)
                {
                    activeFX.gameObject.SetActive(true);
                    activeFX.Play();
                }
                interactableText = "Rest";
            }
            else
            {
                interactableText = "Activate";
            }
        }

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            playerManager.InteractableAnimtionTransformReset(worldGateTeleportTransform, playerManager.WorldGateInteraction);

            if (hasBeenActivated)
            {

            }
            else
            {
                interactableText = "Rest";
                if (activationFX != null)
                {
                    activationFX.gameObject.SetActive(true);
                    activationFX.Play();
                }
                if (activeFX != null)
                {
                    activeFX.gameObject.SetActive(true);
                    activeFX.Play();
                }


            }
            
            //open fast travel menu
            playerManager.stats.CompleteHealLevelUpAndRest();

        }
        public IEnumerator WaitForAnimation()
        {
            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene(levelName);

        }
    }
    
}
