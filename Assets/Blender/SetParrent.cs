using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParrent : MonoBehaviour
{
    Transform parrent;
    private void Awake()
    {
        parrent = transform.parent;
    }
    //Invoked when a button is pressed.
    public void SetParent()
    {
        //Makes the GameObject "newParent" the parent of the GameObject "player".
        transform.parent = parrent;
    }
}
