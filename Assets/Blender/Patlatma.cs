using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patlatma : MonoBehaviour {

	public GameObject inceleme_object;
	public GameObject sabit_object;
	public Animator anim;
	Component[] object_components;
    private bool state = true;

    // Update is called once per frame
    void Update () 
	{

		if(anim.gameObject.activeSelf)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("end"))
			{
				sabit_object.SetActive (false);
				inceleme_object.SetActive (true);
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
        if (!sabit_object.activeInHierarchy)
        {
            Debug.Log("Topla");
            sabit_object.SetActive(true);
            inceleme_object.SetActive(false);
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
