using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _importMuzikTest : MonoBehaviour
{
    public _importMuzik muzik;
    void Start()
    {
        if (muzik==null)  muzik = GetComponent<_importMuzik>();
        if (muzik==null) { Debug.LogError("_muzik bileşenin eksik"); }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //muzik.DownloadPlayMuzik_RESOURCES("http://localhost/myphp/stayAlive.mp3", "stayAlive.mp3");
            muzik.DownloadPlayMuzik_RESOURCES("http://localhost/myphp/barisManco.ogg", "barisManco.ogg");
            //muzik.PlayMuzik_RESOURCES("barisManco.ogg");

        }
    }

}//DownloadResourcesMuzik(string urlServer = "http://localhost/myphp/barisManco.ogg", string isim = "barisManco")
//PlayMuzikMP3(string urlPlay = "http://localhost/myphp/urlMuzikMP3.php")
//PlayMuzikOGG(string urlPlay = "http://localhost/myphp/urlMuzikOGG.php")
