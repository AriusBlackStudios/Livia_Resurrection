using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("Lock On Transform")]
        public Transform lockOnTransform;

        [Header("Combat Colliders")]
        public CriticalDamageCollider backstabCollider;
        public CriticalDamageCollider riposteCollider;


        [HideInInspector] public float pendingCriticalDamage;

        public bool canBeRiposted=false;
        [HideInInspector] public bool isParrying;
        [HideInInspector] public bool canBeParried;
        [HideInInspector] public bool isBlocking;
        [HideInInspector] public bool isInvulnerable;

         public bool isInteracting;
         public bool isInAir;
         public bool isGrounded;
        [HideInInspector] public bool canDoCombo;
        [HideInInspector] public bool isUsingLeftHand;
        [HideInInspector] public bool isUsingRightHand;
        [HideInInspector] public bool isInCombat;



        [HideInInspector] public bool isRotatingWithRootMotion;
        [HideInInspector] public bool canRotate;

        [HideInInspector] public bool isFiringSpell;



        public void ResetHandBeingUsed(bool isLeft)
        {
            if (isLeft)
            {
                isUsingLeftHand = true;
                isUsingRightHand = false;
            }
            else
            {
                isUsingLeftHand = false;
                isUsingRightHand = true;
            }
        }


    }
}
