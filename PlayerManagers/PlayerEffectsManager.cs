using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Livia
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {

        PlayerManager player;

        [HideInInspector]public GameObject currentParticleFX;
        [HideInInspector]public GameObject instantiatedFXmodel;
        [HideInInspector]public float healthToBeHealed,sanityToBeHealed;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();

        }
        public void HealPlayerFromEffect()
        {
            player.stats.HealPlayer(healthToBeHealed);
            player.stats.HealFocusPoints(sanityToBeHealed);
            if(currentParticleFX != null)
            {
                Instantiate(currentParticleFX, player.stats.transform);
            }
         //   GameObject healParticles = Instantiate(currentParticleFX, playerStats.transform);
            if(instantiatedFXmodel != null)
            {
                Destroy(instantiatedFXmodel.gameObject);
            }

            player.weaponSlotManager.LoadBothWeaponsOnSlots();


        }

        
    }
}
