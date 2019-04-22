using System.Collections;
using UnityEngine;

public class _modelImport : MonoBehaviour
{

    #region ------------------------------------MODEL DEĞİŞKENLER && ÖNEMSİZ ŞEYLER--------------------------------------------
    
    //------------------------------------DOSYA İŞLEMLERİ İÇİN SINIFIN ÖRNEKLENMESİ-----------------------------------------------------------
    public _dosyaKlasorKontrolcusu fileFolder = new _dosyaKlasorKontrolcusu();

    //----------------------------LOGLAMA YAPARKEN ANLAŞILIR OLMASI İÇİN AYRAÇ KULLANDIM------------------------------------------------------
    private void printAyrac() { Debug.Log("-------------------------------------------------------------------------------------------------"); }

    #endregion

    #region -------------------------------------DOSYA KONUMUNDAN MODEL İMPORT EDER-------------------------------------------------

    //-----------------------------------NESNEYİ BELİRLENEN KLASOR İÇİNE OLUŞTURUP DONDURUR--------------------------------------------------------------
    public GameObject getObj(string path, string isim){
        return getObj(path + isim);
    }

    //-----------------------------------NESNEYİ BELİRLENEN KLASOR İÇİNE OLUŞTURUP DONDURUR--------------------------------------------------------------
    public GameObject getObj(string pathFull){
        GameObject instance = null;

        //FBX MODEL İMPORT İŞLEMİ
        if (!string.IsNullOrEmpty(pathFull)){instance = ModelImporter.Importer.Import(pathFull);}
        else { Debug.LogWarning("Verilen adreste model olmadığından yüklenemedi " + pathFull); return null; }

        if (instance == null) Debug.LogWarning("Model yüklenemedi NULL");
        return instance;
    }

    //-----------------------------------NESNEYİ DONDURUR MODEL KLASORUN İÇİNE KAYDEDER--------------------------------------------------------------
    public GameObject getObj_PathResourceModel(string isim){

        GameObject instance = null;

        if (!string.IsNullOrEmpty(isim)){
            isim = fileFolder.pathMODELRESOURCES + isim;

            //FBX MODEL İMPORT İŞLEMİ
            instance = ModelImporter.Importer.Import(isim);
        }
        else { Debug.LogWarning("Verilen adreste model olmadığından yüklenemedi "+isim); return null; }

        //-----------------------------------GEÇMİŞ OLSUN MODEL OLUŞTURULAMADI :)----------------------------------------
        if (instance == null) {
            fileFolder.YazdirAyrac();
            Debug.LogWarning("Model yüklenemedi NULL");
            Debug.LogWarning("Wampserveri aç bazen ondan dolayı hata veriyor. Server olmayınca çalışmıyor çünkü :-)");
            fileFolder.YazdirAyrac();
        }
        return instance;
    }
    
    #endregion

    #region -------------------------------------MODEL YOKSA iNDİRİR VARSA İMPORT EDER---------------------------------------------

    //DOSYA YOKSA SERVERDAN İNDİRME İŞLEMİ -------FBXLİSİ-----------
    public IEnumerator IDownloadImportFbx(string urlServer, string isim, string path)
    {
        //FBX DOSYASI OLUP OLMADIĞININ KONTROLÜNÜ YAPAR
        if (fileFolder.getVarMiDosya(path+isim)){
            if (!isim.ToLower().EndsWith(".fbx")) {
                printAyrac();
                Debug.LogWarning("Dosyanın isminin uzantısı fbx olması lazım gelen isim bilgisi = " + isim);
                Debug.LogWarning("Dosyanın yolu bilgisi = " + path);
                printAyrac();
                yield return null;
            }
        }

        yield return fileFolder.IDownload(urlServer, isim, path);
        getObj_PathResourceModel(isim);
    }
    //DOSYA YOKSA SERVERDAN İNDİRME İŞLEMİ -------FBXLİSİ-----------
    public IEnumerator IDownloadImportFbxMenu(string urlServer, string isim, string path,_menuKontrolcusu menuKontroller)
    {
        //FBX DOSYASI OLUP OLMADIĞININ KONTROLÜNÜ YAPAR
        if (fileFolder.getVarMiDosya(path + isim))
        {
            if (!isim.ToLower().EndsWith(".fbx"))
            {
                printAyrac();
                Debug.LogWarning("Dosyanın isminin uzantısı fbx olması lazım gelen isim bilgisi = " + isim);
                Debug.LogWarning("Dosyanın yolu bilgisi = " + path);
                printAyrac();
                yield return null;
            }
        }

        yield return fileFolder.IDownload(urlServer, isim, path);
        menuKontroller.setMENU_YAP(getObj_PathResourceModel(isim));
    }

    #endregion

    #region -------------------------------------MODEL YOKSA iNDİRİR VARSA İMPORT EDER---------------------------------------------

    //DOSYA YOKSA SERVERDAN İNDİRME İŞLEMİ -------FBXSONSUZLİSİ-----------
    public IEnumerator IDownloadImportFbxSonsuz(string urlServer, string isim, string path)
    {
        //FBX DOSYASI OLUP OLMADIĞININ KONTROLÜNÜ YAPAR
        if (fileFolder.getVarMiDosya(path + isim))
        {
            if (!isim.ToLower().EndsWith(".fbx"))
            {
                printAyrac();
                Debug.LogWarning("Dosyanın isminin uzantısı fbx olması lazım gelen isim bilgisi = " + isim);
                Debug.LogWarning("Dosyanın yolu bilgisi = " + path);
                printAyrac();
                yield return null;
            }
        }

        yield return fileFolder.IDownload(urlServer, isim, path);
        getObj_PathResourceModel(isim);
    }


    #endregion

    /*#region KONUM AYARLA
    public static void setGameObjectKonum(GameObject gameObject,Vector3 konum)
    {
        gameObject.Transform.position = konum;
    }
    public static void setGameObjectKonum(GameObject gameObject,float kx,float ky, float kz)
    {
        Vector3 konum = new Vector3(kx,ky,kz);
        setGameObjectKonum(gameObject,konum);
    }
    #endregion*/

}

