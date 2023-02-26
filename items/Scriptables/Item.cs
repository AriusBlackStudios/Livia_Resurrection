using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{

    public class Item : ScriptableObject
    {
        [Header("Item Information")]
        public Sprite itemIcon;
        public string itemName;
        public int itemID;
        [TextArea] public string itemDescription;
        [TextArea] public string itemLore;
    }
}
