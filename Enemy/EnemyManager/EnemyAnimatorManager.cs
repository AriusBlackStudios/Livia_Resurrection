using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class EnemyAnimatorManager : AnimatorManager
    {
        EnemyManager enemyManager;
        EnemyStats enemyStats;
        EnemyBossManager bossManager;
      
        private void Awake()
        {
            anim = GetComponent<Animator>();
            enemyManager = GetComponentInParent<EnemyManager>();
            enemyStats = GetComponentInParent<EnemyStats>();
            bossManager = GetComponentInParent<EnemyBossManager>();

          
        }
        public override void TakeCriticalDamageAnimationEvent()
        {
            enemyStats.TakeDamageNoAnimation(enemyManager.pendingCriticalDamage);
            enemyManager.pendingCriticalDamage = 0;
        }
        public void EnableCanBeReposted()
        {
            enemyManager.canBeRiposted = true;
        }

        public void DisableCanBeReposted()
        {
            enemyManager.canBeRiposted = false;
        }

        public void EnableCanBeParried()
        {
            enemyManager.canBeParried = true;
        }

        public void DisableCanBeParried()
        {
            enemyManager.canBeParried = false;
        }
        public void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }
        public void AwardSoulsOnDeath()
        {
            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();
            if (playerStats != null)
            {
                playerStats.AddSouls(enemyStats.ShardsAwardedOnDeath);
                if (soulCountBar != null)
                {
                    soulCountBar.SetSoulCountText(playerStats.ShardCount);
                }

            }
            
        }

        public void InstantiateBossParticleFX()
        {
            bossManager.particleFX.SetActive(true);
        }
        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyManager.enemyRigidbody.velocity = velocity;


            if (enemyManager.isRotatingWithRootMotion)
            {
                enemyManager.transform.rotation *= anim.deltaRotation;
            }


        }

        
    }
}
