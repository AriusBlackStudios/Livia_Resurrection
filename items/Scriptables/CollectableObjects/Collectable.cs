using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Livia {
    [CreateAssetMenu(menuName = "Items/Collectable")]
    public class Collectable : Item
    {
        [TextArea]
        public string LiviasComment;
        public Sprite itemImage;
        public bool isSprite = false;
    }
}
