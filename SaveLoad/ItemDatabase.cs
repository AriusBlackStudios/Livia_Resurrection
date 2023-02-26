using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Livia{
public class ItemDatabase : MonoBehaviour
    {
        public static ItemDatabase instance;

        public List<WeaponItem> weaponItems = new List<WeaponItem>();
        public List<TarotCard> TarotCardItems = new List<TarotCard>();

        private void Awake() 
        {
            if(instance== null){
            instance = this;
            }
            else{
                Destroy(this);
            }
        }

        public WeaponItem GetWeaponItemByID(int weaponID){
            return weaponItems.FirstOrDefault(weapon => weapon.itemID == weaponID);
        }

         public TarotCard GetTarotCardItemByID(int tarotCardID){
            return TarotCardItems.FirstOrDefault(tarot => tarot.itemID == tarotCardID);
        }
    }
}
