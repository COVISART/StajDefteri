using UnityEngine;

public class Analysis : MonoBehaviour
{
    public GameObject analysisObject;
    public GameObject animatedObject;
    public Animator anim;
    public MeshRenderer colorSate;
    private AnimatorStateInfo stateOfAnimatorStateInfo;
    private bool state = true;

    // Update is called once per frame
    private void Update()
    {
        if (!anim.gameObject.activeSelf) return;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("end"))
        {
            animatedObject.SetActive(false);
            analysisObject.SetActive(true);

            //activate attach object feature
            state = false;
            Debug.Log("Ürün Explode Mod olarak görüntüleniyor");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("topla_end"))
        {
            state = true;
        }
    }
    public void StartAnalysing()
    {
        if (state)
        {
            Patlat();
            colorSate.material.color = Color.green;
        }
        else
        {
            Topla();
            colorSate.material.color = Color.white;
        }
    }

    private void Topla()
    {
        if (animatedObject.activeInHierarchy) return;
        Debug.Log("Topla");
        animatedObject.SetActive(true);
        analysisObject.SetActive(false);
        anim.Play("Topla");
    }

    private void Patlat()
    {
        Debug.Log("Patlat");
        anim.Play("Patlat");
    }
}
