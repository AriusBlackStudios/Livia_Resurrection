using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{

    //used to negate character's pushing each other around.
    public class CharacterBlockerCollider : MonoBehaviour
    {
        //the main character collider
        public CapsuleCollider characterCollider;
        public CapsuleCollider characterBlockerCollider;

        private void Start()
        {
            Physics.IgnoreCollision(characterCollider, characterBlockerCollider, true);
        }
    }
}
