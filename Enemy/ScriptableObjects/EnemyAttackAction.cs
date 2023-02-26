using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    [CreateAssetMenu(menuName = "A.I/Enemy Actions/Attack Action")]
    public class EnemyAttackAction : EnemyAction
    {

        public bool canCombo;
        public EnemyAttackAction comboAction;
        // Start is called before the first frame update
        public int attackScore = 3;//likeliness of attack
        public float recoveryTime = 2;//time it takes to reconver after attack

        //angle you have to be for them to throw the attack
        public float maximumAttackAngle = 35;
        public float minimumAttackAngle = -35;

        //distnace to use attack
        public float minimumDistanceNeededToAttack = 0;
        public float maximumDistanceNeededToAttack = 3;


    }
}
