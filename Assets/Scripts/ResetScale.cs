using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScale : MonoBehaviour
{
    Transform parrent;
    private void Awake()
    {
        parrent = transform;
    }
    //Invoked when a button is pressed.
    public void ResetScaleOfObject()
    {
        //Makes the GameObject "newParent" the parent of the GameObject "player".
        transform.position = parrent.position;
        transform.localScale = parrent.localScale;
    }

}
