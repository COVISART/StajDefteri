using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableKinematic : MonoBehaviour
{
    public GameObject[] Products;
    public Text text;
    public bool isKinematicDisabled;
    public void DisableIsKinematic()
    {
        foreach (GameObject product in Products)
        {
            product.GetComponent<Rigidbody>().isKinematic = isKinematicDisabled;
        }
        isKinematicDisabled = !isKinematicDisabled;
        Debug.Log("isKinematic:" + isKinematicDisabled.ToString());
        text.text = "isKinematic:" + isKinematicDisabled.ToString();
    }
}
