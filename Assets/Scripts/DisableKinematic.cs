using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableKinematic : MonoBehaviour
{
    public GameObject[] Products;
    public Text text;
    public MeshRenderer colorSate;
    public bool isKinematicDisabled;
    public void DisableIsKinematic()
    {
        foreach (GameObject product in Products)
        {
            product.GetComponent<Rigidbody>().isKinematic = isKinematicDisabled;
        }
        if (Products[0].GetComponent<Rigidbody>().isKinematic)
        {
            colorSate.material.color = Color.green;
        }
        else
        {
            colorSate.material.color = Color.white;
        }
        Debug.Log("isKinematic:" + isKinematicDisabled.ToString());
        text.text = "isKinematic:" + isKinematicDisabled.ToString();
        isKinematicDisabled = !isKinematicDisabled; 
    }
}
