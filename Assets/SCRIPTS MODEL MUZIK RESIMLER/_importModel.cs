using System.Collections;
using UnityEngine;

public class _importModel : MonoBehaviour
{

    #region ------------------------------------MODEL DEĞİŞKENLER && ÖNEMSİZ ŞEYLER--------------------------------------------
    
    //------------------------------------DOSYA İŞLEMLERİ İÇİN SINIFIN ÖRNEKLENMESİ-----------------------------------------------------------
    public _controlFileFolder fileFolder = new _controlFileFolder();

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
        if (instance == null) Debug.LogWarning("Model yüklenemedi NULL");
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

}

