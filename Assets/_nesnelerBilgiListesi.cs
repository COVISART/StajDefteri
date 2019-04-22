//*********************************************************BİLGİLİST*************************************************************************************************
//GAMEOBJECT VE BİLGİSİ ŞEKLİNDE VERİLERİ SAKLAYAN SINIF
using System.Collections.Generic;
using UnityEngine;

public class _nesnelerBilgiListesi: IgetGameObject_NesneBilgisi, IgetKeysValues, IgetCocuklar_NesneBilgileri,IAdd,IsetKategoriNo
{
    //_idList idList;
    #region ------------------------SINGLETON------------------------------------
    private _nesnelerBilgiListesi() { }
    private static _nesnelerBilgiListesi instance = new _nesnelerBilgiListesi();
    public static _nesnelerBilgiListesi getINSTANCE() { return instance; }
    #endregion

    //HASHMAPIN C# KARŞILIĞI OLAN DICTIONARY SINIFI
    public Dictionary<int, _nesneBilgisi> list_GameObject_Bilgi_Dictionary = new Dictionary<int, _nesneBilgisi>();
    public void newLIST() { list_GameObject_Bilgi_Dictionary = new Dictionary<int, _nesneBilgisi>(); }

    //------------------------------------------------------------------------------------------------------------------------------------
    //BİLGİLERİ YAZDIRMA
    public void printBILGILER()
    {
        Debug.Log("----------------------------------BİLGİLER SAYISI "+list_GameObject_Bilgi_Dictionary.Count+"----------------------------------------");
        foreach (KeyValuePair<int, _nesneBilgisi> bilgiler in list_GameObject_Bilgi_Dictionary)
        {
            Debug.Log(bilgiler.Value.getBILGILER());
        }
    }
    public void printBILGILER(GameObject obj)
    {
        printBILGILER(obj.GetInstanceID());
    }
    public void printBILGILER(int id)
    {
        Debug.Log("----------------------------------BİLGİLER " + id + " NOLU OBJECT----------------------------------------");
        _nesneBilgisi nesneBilgisi = getNesneBilgisi(id);
        if (nesneBilgisi == null) { Debug.LogError("Nesne bulunamadı"); return; }
        Debug.Log(nesneBilgisi.getBILGILER());
    }
    public void printBILGILER(string id)
    {
        Debug.Log("----------------------------------BİLGİLER " + id + " NOLU OBJECT----------------------------------------");
        try{ printBILGILER(int.Parse(id));}
        catch (System.Exception){ Debug.LogError("Nesne bulunamadı");}
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //TEMİZ GAMEOBJECTLER EKLER
    public void AddNew_SonsuzParametreli(params GameObject[] nesne1)
    {
        newLIST();
        foreach (GameObject item in nesne1)  Add(item);
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //TEMİZ GAMEOBJECT KAYDEDER
    public void AddNew(GameObject nesne1)
    {
        newLIST();
        Add(nesne1);
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //GAMEOBJECT KAYDEDER
    public void Add(GameObject nesne1)
    {
        //NESNE BOŞ MU GELDİ
        if (nesne1 == null) { Debug.LogWarning("NESNE NULL"); return; }

        //NESNE KAYDEDİLİR
        AddGameObject(nesne1);

        //ÇOCUK VARSA ONLARI YÜKLER
        int cocuk = nesne1.transform.childCount;
        if (cocuk > 0)
        {
            //ÇOCUĞU VARSA ONU DA KAYDET "TORPİL" :-) 
            for (int i = 0; i < cocuk; i++)
            {
                Add(nesne1.transform.GetChild(i).gameObject);
            }
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------
    //DICTIONARY LİSTE KAYDETME İŞLEMİ
    public _nesneBilgisi AddGameObject(GameObject tempObj)
    {
        _nesneBilgisi tempValue = null;
        if (tempObj == null) { Debug.LogWarning("Oyun nesnesi null kaydedilemedi"); return null; }

        if (getNesneBilgisi(tempObj) == null)
        {
            tempValue = new _nesneBilgisi(tempObj);
            list_GameObject_Bilgi_Dictionary.Add(tempObj.GetInstanceID(), tempValue);
            setKategoriNo(tempObj);//KATEGORİ NOLARDA TEK GAMEOBJECT DÜZENLER
        }
        
        return tempValue;
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //KATEGORİ NOLARDA TEK GAMEOBJECT DÜZENLER(INSTANCE ID İLE)
    public void setKategoriNo(int gameObj)
    {
        _nesneBilgisi bilgi = getNesneBilgisi(gameObj);

        if (bilgi == null) { Debug.LogWarning("Hash bulunamadı"); return; }
        if (bilgi.getParent == null) { bilgi.kategoriNo = 0; }
        else
        {
            //PARENT BULUYORUZ
            _nesneBilgisi tempParent = getNesneBilgisi(bilgi.getParent.gameObject);
            bilgi.kategoriNo = tempParent.kategoriNo + 1;
        }
    }
    //KATEGORİ NOLARDA TEK GAMEOBJECT DÜZENLER(GAMEOBJECT İLE)
    public void setKategoriNo(GameObject gameObj){ setKategoriNo(gameObj.GetInstanceID());}

    //------------------------------------------------------------------------------------------------------------------------------------
    //KATEGORİ NOLARIN HEPSİNİ DÜZENLER
    public void setKategoriNolar()
    {
        foreach (KeyValuePair<int, _nesneBilgisi> gameObj in list_GameObject_Bilgi_Dictionary)
        {
            setKategoriNo(gameObj.Key);
        }
    }

    #region ----------------------------------------------NESNE BİLGİSİ ERİŞME-----------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------------
    //NESNE BİLGİSİ DÖNDÜRME (GAMEOBJECT İLE)
    public _nesneBilgisi getNesneBilgisi(GameObject temp)
    {
        //NULL NESNE GÖNDERME HATASI
        if (temp == null) { Debug.LogError("Null nesne gönderemezsin!"); return null; }
        _nesneBilgisi value1 = null;
        try
        {
            if (list_GameObject_Bilgi_Dictionary.Count == 0) { Debug.LogWarning("Hash kayıt sayısı sıfır o yüzden bir şey bulunamadı!"); return null; }
            value1 = list_GameObject_Bilgi_Dictionary[temp.GetInstanceID()];
        }
        catch (System.Exception)
        {
            //İLGİLİ NESNENİN DATALARINI DÖNDÜRÜR
            Debug.LogWarning("Data bilgisi döndürülemedi: " + value1 + " || Hash null mu: " + (list_GameObject_Bilgi_Dictionary == null) + " || Hash Count: " + list_GameObject_Bilgi_Dictionary.Count);
        }
        return value1;
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //NESNE BİLGİSİ DÖNDÜRME (GAMEOBJECT İLE LOGSUZ)
    public _nesneBilgisi getNesneBilgisi_logsuz(GameObject temp)
    {
        //NULL NESNE GÖNDERME HATASI
        if (temp == null) { Debug.LogError("Null nesne gönderemezsin!"); return null; }
        _nesneBilgisi value1 = null;


        if (list_GameObject_Bilgi_Dictionary.Count == 0) { Debug.LogWarning("Hash kayıt sayısı sıfır o yüzden bir şey bulunamadı!"); return null; }
        value1 = list_GameObject_Bilgi_Dictionary[temp.GetInstanceID()];
        

        return value1;
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //NESNE BİLGİSİ DÖNDÜRME (INSTANCE ID İLE)
    public _nesneBilgisi getNesneBilgisi(int temp)
    {
        if (list_GameObject_Bilgi_Dictionary.Count == 0) { Debug.LogWarning("Hash kayıt sayısı sıfır o yüzden bir şey bulunamadı!"); return null; }

        _nesneBilgisi value1 = null;

        try { value1 = list_GameObject_Bilgi_Dictionary[temp];}
        catch (System.Exception){}

        return value1;
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //NESNE BİLGİSİ DÖNDÜRME (INSTANCE ID İLE)
    public _nesneBilgisi getNesneBilgisi(string temp)
    {
        _nesneBilgisi value1 = null;

        try{ value1 = getNesneBilgisi(int.Parse(temp));}
        catch (System.Exception){}

        return value1;
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    #endregion
    #region -------------------------------------------------GAMEOBJECT ERİŞME-----------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------------
    public GameObject getGameObject(int temp)
    {
        if (list_GameObject_Bilgi_Dictionary.Count == 0) { Debug.LogWarning("Hash kayıt sayısı sıfır o yüzden bir şey bulunamadı!"); return null; }

        GameObject value1 = null;

        try { value1 = list_GameObject_Bilgi_Dictionary[temp].nesne; }
        catch (System.Exception) { }

        return value1;
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    public GameObject getGameObject(string temp)
    {
        if (list_GameObject_Bilgi_Dictionary.Count == 0) { Debug.LogWarning("Hash kayıt sayısı sıfır o yüzden bir şey bulunamadı!"); return null; }

        GameObject value1 = null;

        try { value1 = list_GameObject_Bilgi_Dictionary[int.Parse(temp)].nesne; }
        catch (System.Exception) { }

        return value1;
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    #endregion

    //GET KEYS = GAMEOBJECT && GET VALUES = _BİLGİ
    #region --------------------------------COLLECTİONS-----------------------------------------
    private Dictionary<int, _nesneBilgisi>.KeyCollection getKeys_Collection { get { return list_GameObject_Bilgi_Dictionary.Keys; } }
    private Dictionary<int, _nesneBilgisi>.ValueCollection getValues_Collection { get { return list_GameObject_Bilgi_Dictionary.Values; } }

    //------------------------------------------------------------------------------------------------------------------------------------
    //GAMEOBJECT LİSTESİNİ STRİNG OLARAK DÖNDÜRÜR
    public string[] getKeys_GameObject_Strings
    {
        get
        {
            Dictionary<int, _nesneBilgisi>.ValueCollection values = getValues_Collection;
            if (values == null) { return null; }

            string[] temp = new string[values.Count];

            int i = 0;
            foreach (_nesneBilgisi item in values) { temp[i] = item.nesne.ToString(); i++; }
            return temp;
        }

    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //ID LİSTESİNİ DÖNDÜRÜR 
    public int[] getKeys_Ids
    {
        get
        {
            Dictionary<int, _nesneBilgisi>.KeyCollection keys = getKeys_Collection;
            if (keys == null) { return null; }

            int[] temp = new int[keys.Count];

            int i = 0;
            foreach (int item in getKeys_Collection) { temp[i] = item; i++; }
            return temp;
        }

    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //GAMEOBJECT LİSTESİNİ DÖNDÜRÜR 
    public GameObject[] getKeys_GameObjects
    {
        get
        {
            Dictionary<int, _nesneBilgisi>.ValueCollection values = getValues_Collection;
            if (values == null) { return null; }

            GameObject[] temp = new GameObject[values.Count];

            int i = 0;
            foreach (_nesneBilgisi item in values) { temp[i] = item.nesne; i++; }
            return temp;
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //BİLGİLİST DÖNDÜRÜR
    public _nesneBilgisi[] getValues_Bilgiler
    {
        get
        {
            Dictionary<int, _nesneBilgisi>.ValueCollection keys = getValues_Collection;
            if (keys == null) { return null; }

            _nesneBilgisi[] temp = new _nesneBilgisi[keys.Count];

            int i = 0;
            foreach (_nesneBilgisi item in getValues_Collection) { temp[i] = item; i++; }
            return temp;
        }

    }
    //------------------------------------------------------------------------------------------------------------------------------------
    #region ÇOCUKLAR BİLGİLİST DÖNDÜRÜR
    public _nesneBilgisi[] getCocuklar_NesneBilgileri(GameObject gameObject){
        return getCocuklar_NesneBilgileri(gameObject.GetInstanceID());
    }
    public _nesneBilgisi[] getCocuklar_NesneBilgileri(int id)
    {
        _nesneBilgisi[] temp = null;

        _nesneBilgisi aile = getNesneBilgisi(id);
        int adet = aile.nesneCocuklar.Length;
        if (adet > 0)
        {
            temp = new _nesneBilgisi[adet];
            int i = 0;
            foreach (GameObject cocuk in aile.nesneCocuklar)
            {
                _nesneBilgisi tempCocuk = getNesneBilgisi(cocuk);
                temp[i] = tempCocuk; i++;
            }
        }
        return temp;
    }
    public _nesneBilgisi[] getCocuklar_NesneBilgileri(string temp)
    {
        int id;
        try{ id = int.Parse(temp); }
        catch (System.Exception){ return null; }

        return getCocuklar_NesneBilgileri(id);
    }
    #endregion

    #endregion

}
//------------------------------------------------------------------------------------------------------------------------------------
//İNTERFACELER
interface IgetGameObject_NesneBilgisi

{   //GAMEOBJECT ARAMASI YAPAR
    GameObject getGameObject(string temp);  //INSTANCE IDNIN STRING HALİ İLE ARAMA YAPAR
    GameObject getGameObject(int temp);     //INSTANCE ID İLE ARAMA YAPAR

    //NESNEBİLGİSİ ARAMASI YAPAR
    _nesneBilgisi getNesneBilgisi(GameObject temp);         //GAMEOBJECT İLE ARAMA YAPAR
    _nesneBilgisi getNesneBilgisi_logsuz(GameObject temp);  //GAMEOBJECT İLE ARAMA YAPAR
    _nesneBilgisi getNesneBilgisi(int temp);                //INSTANCE ID İLE ARAMA YAPAR
    _nesneBilgisi getNesneBilgisi(string temp);             //INSTANCE IDNIN STRING HALİ İLE ARAMA YAPAR
}
interface IgetKeysValues
{
    //-DICTIONARY HARİCİ ÖZELLİKLERİ- KEYLERİ VE VALUELARI DÖNDÜRME
    string[] getKeys_GameObject_Strings { get; }//GAMEOBJECTLER STRINGLER ŞEKLİNDE HEPSİNİ DÖNDÜRÜR
    int[] getKeys_Ids { get; }                  //INSTANCE IDLER HEPSİNİ DÖNDÜRÜR 
    GameObject[] getKeys_GameObjects { get; }   //GAMEOBJECTLER HEPSİNİ DÖNDÜRÜR
    _nesneBilgisi[] getValues_Bilgiler { get; } //NESNEBİLGİLER HEPSİNİ DÖNDÜRÜR
}
interface IgetCocuklar_NesneBilgileri
{
    //ÇOCUKLARIN NESNEBİLGİLERİ DÖNDÜRÜLÜR
    _nesneBilgisi[] getCocuklar_NesneBilgileri(GameObject gameObject);  //ÇOCUKLARIN NESNEBİLGİSİNİ GAMEOBJECT İLE DÖNDÜRÜR
    _nesneBilgisi[] getCocuklar_NesneBilgileri(int id);                 //ÇOCUKLARIN NESNEBİLGİSİNİ INSTANCE ID İLE DÖNDÜRÜR
    _nesneBilgisi[] getCocuklar_NesneBilgileri(string temp);            //ÇOCUKLARIN NESNEBİLGİSİNİ INSTANCE ID STRING HALİ İLE DÖNDÜRÜR
}
interface IAdd
{   //NESNENİN KENDİSİ VE ÇOCUKLARINI HİYERARŞİK OLARAK EKLEME YAPAR
    void AddNew_SonsuzParametreli(params GameObject[] nesne1);  //TEMİZ GAMEOBJECT EKLEME YAPAR(ÖNCEKİLERİN BİLGİSİNİ TUTMAZ) SADECE İSTEDİĞİ KADAR GAMEOBJECT EKLENEBİLİR
    void AddNew(GameObject nesne1);                             //TEMİZ GAMEOBJECT EKLEME YAPAR(ÖNCEKİLERİN BİLGİSİNİ TUTMAZ)
    void Add(GameObject nesne1);                                //GAMEOBJECT EKLEME YAPAR(ÖNCEKİLERİN BİLGİSİNİ TUTAR)
    _nesneBilgisi AddGameObject(GameObject tempObj);            //DİCTONARY KAYIT EKLER(ASIL İŞ BURADA YAPILIYOR)
}
interface IsetKategoriNo
{
    //KATEGORİ NOLARI AYARLAR
    void setKategoriNo(int gameObj);//TEK KATEGORİ NO AYARLANIR
    void setKategoriNolar();        //BÜTÜN KATEGORİ NOLAR AYARLANIR
}