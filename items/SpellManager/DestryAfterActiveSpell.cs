using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia
{


    public class DestryAfterActiveSpell : MonoBehaviour
    {
        // Start is called before the first frame update
        CharacterManager characterCastingSpell;
        void Awake()
        {
            characterCastingSpell = GetComponentInParent<CharacterManager>();
            Destroy(gameObject, 1);
        }

        // Update is called once per frame
        void Update()
        {
            
            if (characterCastingSpell.isFiringSpell)
            {
                Destroy(gameObject);
            }

        }
    }
}
