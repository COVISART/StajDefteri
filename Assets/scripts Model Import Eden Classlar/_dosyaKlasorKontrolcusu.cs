using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class _dosyaKlasorKontrolcusu : IPaths, IFileFolder, IYazdir, IEDownload
{
    #region ----------------------------------------DOSYA VAR MI----------------------------------------------------
    public bool getVarMiDosya(string pathFull) { return System.IO.File.Exists(pathFull); }
    public bool getYokMuDosya(string pathFull) { return !System.IO.File.Exists(pathFull); }
    public bool getVarMiDosya(string path, string isim) { return getVarMiDosya(path + isim); }
    public bool getYokMuDosya(string path, string isim) { return getYokMuDosya(path + isim); }
    #endregion

    #region ----------------------------------------KLASOR VAR MI----------------------------------------------------
    public bool getVarMiKlasor(string pathFull) { return System.IO.Directory.Exists(pathFull); }
    public bool getYokMuKlasor(string pathFull) { return !System.IO.Directory.Exists(pathFull); }

    #endregion

    #region ----------------------------------------KLASOR YOKSA OLUŞTUR----------------------------------------------------
    public void createKlasorYoksa(string pathFull) { if (!System.IO.Directory.Exists(pathFull)) System.IO.Directory.CreateDirectory(pathFull); }
    #endregion

    #region ----------------------------------------TÜM PATHLER----------------------------------------------------

    //ORTAK PATHLER
    public string getPATH(string temp) { return Application.dataPath + "/" + temp + "/"; }
    public string pathASSET { get { return Application.dataPath + "/"; } }
    public string pathRESOURCES { get { return Application.dataPath + "/Resources/"; } }
    public string pathDESKTOP { get { return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + @"\"; } }

    //MODEL PATHLERİ
    public string pathMODELRESOURCES { get { return Application.dataPath + "/Resources/Model/"; } }    
    public string pathMODEL { get { return Application.dataPath + "/Model/"; } }
    public string pathMODELKAYDET { get { return Application.dataPath + "/ModelKaydet/"; } }
    public string pathFBX { get { return Application.dataPath + "/fbx/"; } }

    //MUZİK PATHLERİ
    public string pathMUZIKRESOURCES { get { return Application.dataPath + "/Resources/Muzik/"; } }    
    public string pathMUZIK { get { return Application.dataPath + "/Muzik/"; } }
    public string pathMP3 { get { return Application.dataPath + "/Mp3/"; } }

    #endregion

    #region ----------------------------------------INDIRME(FBX MUZIK)-----------------------------------------------------
    //DOSYA YOKSA SERVERDAN İNDİRME METODU 
    public IEnumerator IDownload(string urlServer, string isim, string path)
    {
        //RESOURCE KLASORU SİLİNSE BİLE ÇALIŞIR.
        createKlasorYoksa(pathRESOURCES);

        //RESOURCE İÇİNDEKİ (MODEL,RESIM) KLASORU SİLİNSE BİLE ÇALIŞIR
        createKlasorYoksa(path);

        string tempPath = path + isim;
        if (getYokMuDosya(tempPath))
        {
            var uwr = new UnityWebRequest(urlServer, UnityWebRequest.kHttpVerbGET);
            uwr.downloadHandler = new DownloadHandlerFile(tempPath);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError) Debug.LogError("IDownloadImport Hatası: " + uwr.error);
            else Debug.Log("Dosya Başarılı Şekilde Kaydedildi: " + tempPath);
        }
    }
    #endregion

    #region ------------------------------------------LOGLAMA İŞLEMİ------------------------------------------------
    public object Yazdir { set {Debug.Log(value.ToString());} }
    public object YazdirUyari { set { Debug.LogWarning(value.ToString()); } }
    public object YazdirHata { set { Debug.LogError(value.ToString()); } }

    public void YazdirAyrac() { Debug.Log("-------------------------------------------------------------------------------------------------"); }
    #endregion
}

#region --------------------------------------------------INTERFACELER--------------------------------------------------------------------------
//------------------------------------------------------------INTERFACE YAZDIR--------------------------------------------------------
public interface IYazdir
{
    object Yazdir { set; }
    object YazdirUyari { set; }
    object YazdirHata { set; }

    void YazdirAyrac();
}

//------------------------------------------------------------INTERFACE PATHS--------------------------------------------------------
public interface IPaths
{
    //ORTAK PATHLER
    string getPATH(string temp);
    string pathASSET { get; }
    string pathRESOURCES { get; }
    string pathDESKTOP { get; }

    //MODEL PATHLERİ
    string pathMODELRESOURCES { get; }
    string pathMODEL { get; }
    string pathMODELKAYDET { get; }
    string pathFBX { get; }

    //MUZİK PATHLERİ
    string pathMUZIKRESOURCES { get; }
    string pathMUZIK { get; }
    string pathMP3 { get; }
}

//------------------------------------------------------------INTERFACE FILE FOLDER--------------------------------------------------------
public interface IFileFolder
{
    #region ----------------------------------------DOSYA VAR MI----------------------------------------------------
    bool getVarMiDosya(string pathFull);
    bool getYokMuDosya(string pathFull);
    bool getVarMiDosya(string path, string isim);
    bool getYokMuDosya(string path, string isim);
    #endregion

    #region ----------------------------------------KLASOR VAR MI----------------------------------------------------
    bool getVarMiKlasor(string pathFull);
    bool getYokMuKlasor(string pathFull);

    #endregion

    #region ----------------------------------------KLASOR YOKSA OLUŞTUR----------------------------------------------------
    void createKlasorYoksa(string pathFull);
    #endregion
}

//------------------------------------------------------------IENUMERATOR INDIRME--------------------------------------------------------
public interface IEDownload
{
    IEnumerator IDownload(string urlServer, string isim, string path);
}

//------------------------------------------------------------INTERFACE BU TÜR İLE BİTİYOR MU?-----------------------------------------------
public interface ITur
{
    bool getFbxMi(string pathFull);

    bool getMp3Mi(string pathFull);
    bool getOggMi(string pathFull);

    bool getPngMi(string pathFull);
    bool getJpegMi(string pathFull);

    bool getXmlMi(string pathFull);

}

#endregion
