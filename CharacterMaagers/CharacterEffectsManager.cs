using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        CharacterStats characterStatsManager;

        [Header("Death Effects")]
        public SkinnedMeshRenderer[] skinnedMeshRenderers;
        public List<Material> deathFXMaterials;
        public float dissolveRate = 0.02f;
        public float refreshRate = 0.05f;

        [Header("Damage FXs")]
        public GameObject bloodSplatterFX;

        
        [HideInInspector]public WeaponFX rightWeaponEffects;
        [HideInInspector]public WeaponFX leftWeaponEffects;


        private bool isPoisoned;
        private float poisonBuildUp = 0f;//buiild up over time that poisons the player after reaching 100
        private float poisonAmount = 100f;//percent to process before becoing unpoisoned
        public float defaultPoisonAmount;// the default amount of poison a player has to process once they become poisoned


        protected virtual void Awake()
        {
            characterStatsManager = GetComponent<CharacterStats>();

            for( int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                
                for (int j = 0;j < skinnedMeshRenderers[i].materials.Length; j++)
                {
                    deathFXMaterials.Add(skinnedMeshRenderers[i].materials[j]);
                }
            }
        }

        public virtual void PlayerWeaponFX(bool isLeft)
        {
            if (isLeft == false)
            {
                if (rightWeaponEffects != null)
                {
                    rightWeaponEffects.PlayWeaponFX();
                }
            }
            else
            {
                if (leftWeaponEffects != null)
                {
                    leftWeaponEffects.PlayWeaponFX();
                }
            }
        }

        public virtual void HandleAllBuildUpEffect()
        {
            if (characterStatsManager.isDead) return;
            HandlePoisonBuildUp();
            HandleIsPoisonedEffect();
        }

        protected virtual void HandlePoisonBuildUp()
        {
            if (isPoisoned) return;
            
            if (poisonBuildUp > 0 && poisonBuildUp < 100)
            {
                poisonBuildUp = poisonBuildUp - 1 * Time.deltaTime;
            }
            else if (poisonBuildUp >= 100)
            {
                isPoisoned = true;
                poisonBuildUp = 0;//reset build bc we are now actually in the poison stage
            }

        }

        protected virtual void HandleIsPoisonedEffect()
        {
            if (isPoisoned)
            {
                if (poisonAmount > 0)
                {
                    poisonAmount = poisonAmount - 1 * Time.deltaTime;
                }
                else
                {
                    isPoisoned = false;
                }
            }
        }

        public  void disolveOnDeath()
        {

            StartCoroutine(Dissolve());
        }

        IEnumerator Dissolve(){

            float counter =0;
            Debug.Log("Trying to disolve");
            if( deathFXMaterials.Count > 0){
                while ( deathFXMaterials[0].GetFloat("_DissolveAmount") < 1){
                    counter += dissolveRate;
                    for( int i =0; i < deathFXMaterials.Count;i++)
                        deathFXMaterials[i].SetFloat("_DissolveAmount", counter);
                    yield return new WaitForSeconds(refreshRate);
                    Debug.Log("DISOLVING");
                }
                Destroy( this.gameObject);
            }
        }
    }
}
