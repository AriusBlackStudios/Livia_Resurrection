using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Livia{
[System.Serializable]
public class SerializableDictionary<Tkey,TValue>: Dictionary<Tkey,TValue>, ISerializationCallbackReceiver
{

    [SerializeField] private List<Tkey> keys = new List<Tkey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    //called right before cerialization
    // saves the dictionary to listd
    public void OnBeforeSerialize(){
        keys.Clear();
        values.Clear();
        foreach(KeyValuePair<Tkey,TValue> pair in this){
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize(){
        Clear();
        if(keys.Count != values.Count){
            Debug.LogError("the amount of keys does not match the amount of values");
        }
        for(int i = 0; i< keys.Count; i++)
        {
            Add(keys[i],values[i]);
        }
    }
}
}
