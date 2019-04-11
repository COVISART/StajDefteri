using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patlatma : MonoBehaviour {

	public GameObject incelemeObject;
	public GameObject sabitObject;
	public Animator anim;
	Component[] objectComponents;
    private bool state = true;

    // Update is called once per frame
    void Update () 
	{

		if(anim.gameObject.activeSelf)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("end"))
			{
				sabitObject.SetActive (false);
				incelemeObject.SetActive (true);
                //activate attach object feature
                state = false;
                Debug.Log("state = set false");
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("topla_end"))
            {
                state = true;
                Debug.Log("state = set true");
            }
        }
	}
    public void play_animation(string animation)
    {
        if (state)
        {
            anim.Play(animation);
            Debug.Log("state = true");
        }
        else
        {
            topla();
            Debug.Log("state = set true");
        }
    }

    void topla()
    {
        if (!sabitObject.activeInHierarchy)
        {
            Debug.Log("Topla");
            sabitObject.SetActive(true);
            incelemeObject.SetActive(false);
            anim.Play("Topla");
        }
    }
    void OnBecameVisible()
	{
		enabled = true;
	}
	void OnBecameInvisible()
	{
		enabled = false;
	}
}
