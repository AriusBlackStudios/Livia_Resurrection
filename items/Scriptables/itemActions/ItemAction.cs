using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{
    public class ItemAction : ScriptableObject
    {

        public virtual void PerformAction(PlayerManager player)
        {
            player.combatManager.DetectEnemyInRadius();
            if (player.combatManager.nearestAutoAttackTarget != null)
            {
                // Look at including x and z leaning
                player.transform.LookAt(player.combatManager.nearestAutoAttackTarget.transform);
 
             // Euler angles are easier to deal with. You could use Quaternions here also
             // C# requires you to set the entire rotation variable. You can't set the individual x and z (UnityScript can), so you make a temp Vec3 and set it back
            Vector3 eulerAngles = player.transform.rotation.eulerAngles;
             eulerAngles.x = 0;
            eulerAngles.z = 0;
 
            // Set the altered rotation back
            player.transform.rotation = Quaternion.Euler(eulerAngles);

            }

        }
    }
}
