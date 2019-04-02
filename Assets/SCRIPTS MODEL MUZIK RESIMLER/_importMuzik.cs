using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class _importMuzik : MonoBehaviour
{
    #region ---------------------------------------DEĞİŞKENLER---------------------------------------------------
    _controlFileFolder fileFolder = new _controlFileFolder();
    public bool getMP3Mu(string isim) { return isim.ToLower().EndsWith(".mp3"); }
    public bool getOGGMu(string isim) { return isim.ToLower().EndsWith(".ogg"); }
    #endregion
    #region -----------------------------------SES KAYNAĞI KONTROLÜ YAPILIR------------------------------------------
    public AudioSource audio1;
    void Start() {
        if (audio1 == null) { audio1 = GetComponent<AudioSource>();
        }
        if (audio1 == null) {
            gameObject.AddComponent<AudioSource>();
            Debug.LogWarning("Ses kaynağı eklenmemiş ben otomatik ekledim");
        }
    }
    #endregion

    #region *********************************SERVERDAN MÜZİĞİ RESOURCES İNDİRİP OYNATIR ************************************************

    //SERVERDAN MUZIK İNDİRİR RESOURCES KLASORÜNE
    
    public IEnumerator IDownloadPlayMuzik_RESOURCES(string urlServer, string isim)
    {
        
        string sarki = isim.Split('.')[0].ToLower();
        string tipi = isim.Split('.')[1].ToLower();

        UnityWebRequest uwr;
        //RESOURCE KLASORU SİLİNSE BİLE ÇALIŞIR.
        fileFolder.createKlasorYoksa(fileFolder.pathRESOURCES);

        //DESTEKLEDİĞİ MÜZİK FORMATLARININ KONTROLÜ YAPALIM
        string path = fileFolder.pathRESOURCES + isim;
        if (fileFolder.getYokMuDosya(path))
        {
            if (tipi == "mp3"){
                PlayMuzikMP3(urlServer);
            }
            else if (tipi == "ogg"){
                PlayMuzikOGG(urlServer);
            }
            else
            {
                Debug.Log("tutmadı");
            }
            Debug.Log("MüZİK İNDİRİLİYOR...");

            uwr = new UnityWebRequest(urlServer, UnityWebRequest.kHttpVerbGET);
            uwr.downloadHandler = new DownloadHandlerFile(path);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError) Debug.LogError("IDownloadImport Hatası: " + uwr.error);
            else
            {
                if (uwr.isDone){
                    Debug.Log("MÜZİK KAYDEDİLDİ: " + path);
                    PlayMuzik_RESOURCES(isim);
                }
            }
        }
        else
        {
            PlayMuzik_RESOURCES(isim);
               
        }
        
    }
    public void DownloadPlayMuzik_RESOURCES(string urlServer, string isim)
    {
        StartCoroutine(IDownloadPlayMuzik_RESOURCES(urlServer, isim));
    }
    #endregion


    //-------------------------------------------RESOURCESDAN MÜZİK OYNATMAK-------------------------------------------------------------
    public void PlayMuzik_RESOURCES(string isim){
        AudioClip clip = null;
        if (fileFolder.getVarMiDosya(fileFolder.pathRESOURCES+isim))
        {
            string sarki = isim.Split('.')[0];
            clip = Resources.Load<AudioClip>(sarki);
            if (clip==null)
            {
                Debug.LogWarning("Audioclip ses kaynağı içi boş o yüzden çalamıyorum");
                //Debug.Log("Gelen isim bilgisi: " + isim+" Çıkan isim: "+sarki);
                Debug.Log("Audioclip içi: " + clip);
                return;
            }
            
            audio1.clip = clip;
            audio1.Play();
            Debug.Log("Müzik resourcesdan oynatılıyor: " + isim);
        }
        else
        {
            Debug.LogWarning("Dosya yerinde yok o yüzden çalamıyorum");
            Debug.LogWarning(fileFolder.pathRESOURCES + isim);
            return;
        }
    }

    #region ********************************SERVERDAN MÜZİK DİNLETİR KAYDETMEZ *******************************************************
    public void PlayMuzikMP3(string urlPlay) { StartCoroutine(IPlayMuzikClipMP3(urlPlay)); }
    public void PlayMuzikOGG(string urlPlay) { StartCoroutine(IPlayMuzikClipOGG(urlPlay)); }

    IEnumerator IPlayMuzikClipMP3(string urlString){yield return StartCoroutine(IPlayMuzikClip(urlString, AudioType.MPEG));}
    IEnumerator IPlayMuzikClipOGG(string urlString){yield return StartCoroutine(IPlayMuzikClip(urlString, AudioType.OGGVORBIS));}
    IEnumerator IPlayMuzikClip(string urlString, AudioType audioType)
    {
        using (UnityWebRequest www1 = UnityWebRequest.Get(urlString))
        {
            yield return www1.SendWebRequest();
            if (www1.isNetworkError) { Debug.Log(www1.error); }
            else
            {
                using (var www = UnityWebRequestMultimedia.GetAudioClip(www1.downloadHandler.text, audioType))
                {
                    yield return www.SendWebRequest();

                    if (www.isNetworkError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        Debug.Log("Muzik dosyası yükleniyor...");
                        AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                        audio1.Stop();
                        audio1.PlayOneShot(myClip);
                        Debug.Log("Müzik oynatılıyor!");

                        using (FileStream fileStream =  File.Create(@"C:\programs\example1.txt"))
                        using (StreamWriter writer = new StreamWriter(fileStream))
                        {
                            writer.WriteLine("Example 1 written");
                        }

                    }
                }

            }
        }
    }
    #endregion

}
