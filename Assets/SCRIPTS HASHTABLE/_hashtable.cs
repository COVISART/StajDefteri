using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _hashtable : MonoBehaviour
{
    Hashtable hashtable = new Hashtable();
    void Start()
    {
        hashtable.Add("hali", "sunal");
        hashtable.Add("hasan","gec");
        hashtable.Add(1,"YILMAZ");

        foreach (DictionaryEntry item in hashtable)
        {
            Debug.Log(item.Key+" "+item.Value);
        }
    }
    //ArrayList arrayList = new ArrayList();
    void Update()
    {
        
    }
}
