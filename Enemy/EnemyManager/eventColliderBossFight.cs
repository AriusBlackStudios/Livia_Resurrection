using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia {
    public class eventColliderBossFight : MonoBehaviour
    {

        public EnemyBossManager targetBoss;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                if (!targetBoss.bossHasBeenDefeated)
                {
                    if (targetBoss.bossHasBeenAwakened)
                    {
                        //skip cutscene
                        targetBoss.StartBossFight();

                    }
                    else
                    {
                        //play cutscne
                        targetBoss.StartBossFight();
                    }
                    


                }
                
            }
        }
    }
}
