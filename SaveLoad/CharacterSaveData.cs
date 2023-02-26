using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia
{
    [System.Serializable]
    public class CharacterSaveData
    {
        public string characterName;
        public int character_level;
        public int shardCount;

         [Header("Inventory Slots")]
        public int rightHandQickSlot0ItemID;
        public int rightHandQickSlot1ItemID;

        public int leftHandQickSlot0ItemID;
        public int leftHandQickSlot1ItemID;

        [Header("Equipment")]
        public int currentRightHandWeaponID;
        public int currentLeftHandWeaponID;
        
        public int currentTarotA_ID;
        public int currentTarotB_ID;
        public int currentTarotC_ID;
        public int currentTarotD_ID;
        public int currentTarotE_ID;


        [Header("Volume Settings")]
        public float master_volume;
        public float FX_volume;
        public float music_volume;
        public float magic_volume;

        [Header("World Co-ordinates")]
        public float xPosition;
        public float yPosition;
        public float zPosition;


        [Header("Major Stats")]
        public float vitalityLevel;
        public float EnduranceLevel;
        public float SanityLevel;
        public float StrengthLevel;
        public float DexterityLevel;
        public float MagicLevel;
        public float BlasphemyLevel;
        public float LuckLevel;

        [Header("Items Looted From The World")]
        public SerializableDictionary<int, bool> itemsInWorld;//uniqueId,beenLooted

        [Header("Chests Looted From The World")]
        public SerializableDictionary<int, bool> chestsInWorld;//uniqueId,beenLooted

        [Header("Doors Opened In World")]
        public SerializableDictionary<int, bool> doorsInWorld;//uniqueId,beenLooted

        [Header("Inventories")]
        public SerializableDictionary<int, int> tarotCardsInInventory;//cardID,quantity

        public SerializableDictionary<int, int> primaryWeaponItemInventory;//weaponItemId, quantity
         public SerializableDictionary<int, int> secondaryWeaponItemInventory;//weaponItemId, quantity



        public CharacterSaveData(){
            itemsInWorld = new SerializableDictionary<int, bool>();
            chestsInWorld = new SerializableDictionary<int, bool>();
            doorsInWorld = new SerializableDictionary<int, bool>();
        }

    }
}
