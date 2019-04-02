using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
public class _importResimler : MonoBehaviour
{
    #region *********************************************DEĞİŞKENLER*********************************************
    public GameObject nesne;
    public Renderer renderer1;

    private void Start() { renderer1 = nesne.GetComponent<Renderer>(); if (renderer1 == null) Debug.LogWarning("Nesnenin renderer yok"); }
    #endregion

    #region *********************************************TÜM PATHLER*********************************************
    string pathASSET { get { return Application.dataPath + "/"; } }
    string pathRESOURCES { get { return Application.dataPath + "/Resources/"; } }
    string pathMODEL { get { return Application.dataPath + "/Model/"; } }
    string pathFBX { get { return Application.dataPath + "/fbx/"; } }
    string pathDESKTOP { get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop); } }
    #endregion
    
    #region ********************************************RESİM İNDİRME******************************************************************
    
    //JPG RESİM KAYDEDER
    public void DownloadResimJPG(string urlString = "http://gazetekarinca.com/wp-content/uploads/2017/01/WhatsApp-Image-2017-01-05-at-11.29.47.jpeg",string isim= "ördek.jpeg") {
        StartCoroutine(IDownloadFilePathDesktop(urlString, isim)); }

    //PNG RESİM KAYDEDER
    public void DownloadResimPNG(string urlString = "http://pluspng.com/img-png/cat-png-cat-png-hd-1500.png",string isim = "kedi.png") {
        StartCoroutine(IDownloadFilePathDesktop(urlString, isim)); }

    //RESOURCES RESİM KAYDEDER
    public IEnumerator IDownloadFilePathResources(string urlString, string isim)
    {
        yield return StartCoroutine(IDownloadFilePath(urlString, isim, pathRESOURCES));
    }
    //MASAÜSTÜNE RESİM KAYDEDER
    public IEnumerator IDownloadFilePathDesktop(string urlString, string isim)
    {
        yield return StartCoroutine(IDownloadFilePath(urlString,isim, pathDESKTOP));
    }

    //RESİM DOSYASI KAYDEDER
    public IEnumerator IDownloadFilePath(string urlString, string isim,string pathString)
    {
        string path = pathString + isim;
        if (System.IO.File.Exists(path)) { Debug.Log("Dosya bulunmaktadır"); }

        var uwr = new UnityWebRequest(urlString, UnityWebRequest.kHttpVerbGET);
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError) Debug.LogError(uwr.error);
        else Debug.Log("Dosya Başarılı Şekilde Kaydedildi: " + path);
    }
    #endregion

    #region ********************************************PNG RESİM 3D KÜBE KAPLAMA***************************************************************
    public void setResimKaplamaKube(string urlString = "http://localhost/myphp/urlResim.php") { StartCoroutine(IGetResimKaplamaKube(urlString)); }
    public IEnumerator IGetResimKaplamaKube(string urlString)
    {
        using (UnityWebRequest www1 = UnityWebRequest.Get(urlString))
        {
            yield return www1.SendWebRequest();
            if (www1.isNetworkError){Debug.Log(www1.error);}
            else{
                using (UnityWebRequest uwr = new UnityWebRequest(www1.downloadHandler.text, UnityWebRequest.kHttpVerbGET))
                {
                    uwr.downloadHandler = new DownloadHandlerTexture();
                    yield return uwr.SendWebRequest();
                    renderer1.material.mainTexture = DownloadHandlerTexture.GetContent(uwr);
                    //Debug.Log(uwr.downloadHandler.text);
                }
            }
        }
    }


    #endregion

    #region ******************************************DENENMEDİ TAHMİNEN ÇALIŞIYOR ***************************************************
    /*public Texture2D getTexture2D(string path)
    {
        if (getYokMuDosya(path)) { Debug.LogWarning("Dosya bulunamadı"); return null; }
        Texture2D temp = (Texture2D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
        if (temp == null) Debug.LogWarning("Dosya null yüklenemedi");
        return temp;
    }

    public Texture3D getTexture3D(string path)
    {
        if (getYokMuDosya(path)) { Debug.LogWarning("Dosya bulunamadı"); return null; }
        Texture3D temp = (Texture3D)AssetDatabase.LoadAssetAtPath(path, typeof(Texture3D));
        if (temp == null) Debug.LogWarning("Dosya null yüklenemedi");
        return temp;
    }*/
    #endregion

    #region **************************************DOSYA VAR MI********************************************
    public bool getVarMiDosya(string pathh) { return System.IO.File.Exists(pathh); }
    public bool getYokMuDosya(string pathh) { return !System.IO.File.Exists(pathh); }
    #endregion
}
