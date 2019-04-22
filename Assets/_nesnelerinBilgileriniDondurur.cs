using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//NESNE İLE İLGİLİ (BİLGİ ALMA && BİLGİ DONDURME) SINIFI = İŞLEME SINIFI DİYEBİLİRİZ
public class _nesnelerinBilgileriniDondurur
{
    #region ------------------------SINGLETON------------------------------------
    private _nesnelerinBilgileriniDondurur() { }
    private static _nesnelerinBilgileriniDondurur instance = new _nesnelerinBilgileriniDondurur();
    public static _nesnelerinBilgileriniDondurur getInstance() { return instance; }
    #endregion

    #region--------------------------------------------------------- YAZDIRMA METODLARI ------------------------------------------------------------------------------
    public void printCocukYok() { Debug.LogWarning("NULL ÇOCUĞUM YOK DEMEK"); }
    public void printCocukYok(object temp) { if (temp == null) printCocukYok(); }
    public void printAyrac(object temp) { Debug.Log("----------------------------------------------------------" + temp.ToString() + "--------------------------------------------------------------"); }
    public void printAyrac() { Debug.Log("------------------------------------------------------------------------------------------------------------------------"); }
    public void print(object temp) { Debug.Log(temp.ToString()); }
    
    //ÇOCUKLAR YAZDIRMAK
    public void printCocuklar(GameObject[] cocuklar)
    {
        //if (getBosMu(cocuklar)){ printCocukYok(); return;}
        if (getBosMu(cocuklar)) { return; }
        foreach (GameObject item in cocuklar)
        {
            print("ÇOCUK: " + item.ToString());
        }
    }
    //ÇOCUKLAR YAZDIRMAK
    public void printCocuklar(GameObject nesne)
    {
        //if (getBosMu(nesne)) { return; }
        if (getBosMu(nesne)) { printNull(); return; }
        printAyrac("NESNE: " + nesne.name + " ÇOCUKLARI");
        printCocuklar(getCocuklar_Dizi(nesne));
    }
    //ÇOCUK SAYISI
    public void printCocukSayisi(GameObject nesne) { print("TEMEL NESNE: " + nesne.name + " ÇOCUK SAYISI: " + getCocukSayisi(nesne)); }
    public void printCocugun_CocukSayisi(GameObject nesne) {
        printAyrac(nesne.name+"NESNESİNİN ÇOCUKLARIN ÇOCUK SAYILARI");
        foreach (GameObject item in getCocuklar_Dizi(nesne))
        {
            print(" ÇOCUK SAYISI: " + getCocukSayisi(item) + "-->" + item);
        }
    }
    //ÇOCUKLARININ ÇOCUKLARINA ERİŞMEK
    public void printCocuklarin_Cocuklari(GameObject nesne)
    {
        if (getBosMu(nesne)) { Debug.LogWarning("NESNE NULL"); return; }
        printAyrac("NESNE: "+nesne.name+ " ÇOCUKLARIN ÇOCUĞU");

        GameObject [] cocuklar = getCocuklar_Dizi(nesne);

        //if (getBosMu(cocuklar)) { printCocukYok(); return; }
        if (getBosMu(cocuklar)) { return; }
        foreach (GameObject cocuk in cocuklar)
        {
            GameObject[] torunlar = getCocuklar_Dizi(cocuk);
            if (getBosMu(torunlar)) { continue; }
            foreach (GameObject torun in torunlar)
            {
                printCocuklar(torun);
            }
        }
    }
    //ÇOCUKLAR DÖNDÜRÜR
    public GameObject[] getprintCocuklarr(GameObject nesne)
    {
        if (getBosMu(nesne)) { printNull(); return null; }
        printAyrac("NESNE: " + nesne.name + " ÇOCUKLARI");
        GameObject[] dizi = getCocuklar_Dizi(nesne);
        printCocuklar(dizi);
        return dizi;
    }
    
    //NULL YAZDIRMA
    private void printNull(){ Debug.LogWarning("NESNE NULL");}

    //ÇOCUKLAR YAZDIRMAK
    public void printCocuklar(List<GameObject> cocuklar)
    {
        printAyrac("ÇOCUKLAR");
        foreach (GameObject item in cocuklar)
        {
            print("ÇOCUK: " + item.ToString());
        }
    }
    #endregion

    #region-------------------------------------------------------- KONTROL KOMUTLARI---------------------------------------------------------------------------
    public bool getBosMu(object dizi) { return (dizi == null); }
    //NESNE BOŞ MU GELİYOR ONU KONTROL EDER.
    public bool getNesneNullMu(GameObject nesne)
    {
        bool temp = (nesne == null);
        if (temp) Debug.LogWarning("OYUN NESNESİNİ BOŞ GÖNDERDİN");
        return temp;
    }
    public bool getDiziSifirKucukMu(GameObject[] dizi){ if (getBosMu(dizi)){  return true;} return getDiziSifirKucukMu(dizi); }
    public bool getDiziSifirKucukMu(int boyut){ return (boyut <= 0);}

    public int getDiziBoyut(GameObject[] dizi) {
        if (getBosMu(dizi)){ return 0;}
        return dizi.Length;
    }
    public int getListBoyut(List<GameObject> temp)
    {
        if (getBosMu(temp)) { return 0; }
        return temp.Count;
    }

    #endregion

    #region -------------------------------------------------------ÇOCUKLAR SAYILARI--------------------------------------------------------------------
    //------------------------------------------------------İLGİLİ ÇOCUĞUN ÇOCUK SAYISINI DÖNDÜRÜR------------------------------------------------------
    public int getCocugunSayisi(GameObject tempNesne, int indeks)
    {
        GameObject nesne = getCocukNesne(tempNesne, indeks);
        return getCocukSayisi(nesne);
    }

    //------------------------------------------------------NESNENİN ÇOCUK SAYI SAYISINI DÖNDÜRÜR------------------------------------------------------
    public int getCocukSayisi(GameObject tempNesne){
        if (getBosMu(tempNesne)){ Debug.LogWarning("NESNE NULL"); return 0;}
        return tempNesne.transform.childCount;
    }
    //------------------------------------------------------ÇOCUKLARI ÇOCUK SAYILARI DİZİSİ ŞEKLİNDE DÖNDÜRÜR------------------------------------------------------
    public int[] getCocuklarin_CocukSayilari_Dizi(GameObject tempNesne)
    {
        //ÇOCUKLAR DİZİSİ
        if (getBosMu(tempNesne)) { return null; }
        GameObject[] objs = getCocuklar_Dizi(tempNesne);

        if (getBosMu(objs)) { return null; }
        if (getDiziSifirKucukMu(objs)) { return null; }
        int tempBoyut = objs.Length;

        int[] cocukSayi = new int[tempBoyut];
        for (int i = 0; i < tempBoyut; i++)
        {
            cocukSayi[i] = getCocukSayisi(objs[i]);
        }
        return cocukSayi;

    }
    //
    //------------------------------------------------------ÇOCUKLARI ÇOCUK SAYILARI LİSTESİ ŞEKLİNDE DÖNDÜRÜR------------------------------------------------------
    public List<int> getCocuklarin_CocukSayilari_List(GameObject tempNesne)
    {
        List<GameObject> tempCocuklar = getCocuklar_List(tempNesne);
        return getCocuklarin_CocukSayilari_List(tempCocuklar);
    }
    public List<int> getCocuklarin_CocukSayilari_List(List<GameObject> tempCocuklar)
    {
        //ÇOCUKLAR DİZİSİ
        if (getBosMu(tempCocuklar)) { return null; }
        
        int tempBoyut = tempCocuklar.Count;

        if (tempBoyut<=0){return null;}

        List<int> cocukSayilar = new List<int>();
        for (int i = 0; i < tempBoyut; i++)
        {
            cocukSayilar.Add(getCocukSayisi(tempCocuklar[i]));
        }
        return cocukSayilar;

    }
    #endregion

    #region ------------------------------------------------ÇOCUKLARI || COCUK GAMEOBJECTLERİ DÖNDÜRÜR-----------------------------------------------------
    //------------------------------------------------------İLGİLİ ÇOCUĞUNU GAMEOBJECT DÖNDÜRÜR------------------------------------------------------
    public GameObject getCocukNesne(GameObject tempNesne,int i)
    {
        if (getBosMu(tempNesne)) { return null; }

        GameObject temp = null;

        try { temp = tempNesne.transform.GetChild(i).gameObject;}
        catch (Exception){ Debug.Log("ÖYLE BİR İNDEKSTE ÇOCUK YOK: "+ i+" GÖNDERDİĞİNİZ İNDEKS NONUZ: "+tempNesne.transform.childCount);}
        return temp;
    }

    //------------------------------------------------------İLGİLİ ÇOCUĞUNU GAMEOBJECT DÖNDÜRÜR------------------------------------------------------
    public GameObject getCocukNesne(List<GameObject> nesneCocuklar, int i)
    {
        if (nesneCocuklar == null) { return null; }
        GameObject temp = null;
        int cocukSayisi = nesneCocuklar.Count;
        if (cocukSayisi >= i || cocukSayisi <= 0) { Debug.LogWarning("Dizi boş"); return temp; }

        try { temp = nesneCocuklar[i]; }
        catch (Exception) { Debug.LogWarning("öYLE BİR İNDEKS YOK: " + i + " MAKSİMUM İNDEKS: " + cocukSayisi); }

        return temp;
    }
    public GameObject getCocukNesne(GameObject[] nesneCocuklar, int i)
    {
        if (nesneCocuklar == null) { return null; }
        GameObject temp = null;
        int cocukSayisi = nesneCocuklar.Length;
        if (cocukSayisi >= i || cocukSayisi <= 0) { return temp; }

        if (cocukSayisi <= 0) { Debug.LogWarning("DİZİ NULL"); return temp; }

        try { temp = nesneCocuklar[i]; }
        catch (Exception) { Debug.LogWarning("öYLE BİR İNDEKS YOK: " + i + " MAKSİMUM İNDEKS: " + cocukSayisi); }

        return temp;
    }
    //------------------------------------------------------ÇOCUKLARI GAMEOBJECT DİZİSİ ŞEKLİNDE DÖNDÜRÜR------------------------------------------------------
    public GameObject[] getCocuklar_Dizi(GameObject tempNesne)
    {
        if (getBosMu(tempNesne)) { Debug.LogWarning("NESNE NULL"); return null; }

        GameObject[] dizi = null;
        int diziBoyut = getCocukSayisi(tempNesne);
        if (getDiziSifirKucukMu(diziBoyut)) { return null; }
        dizi = new GameObject[diziBoyut];
        for (int i = 0; i < diziBoyut; i++){ dizi[i] = tempNesne.transform.GetChild(i).gameObject;}
        return dizi;
    }

    //-----------------------------------------------------ÇOCUKLARI GAMEOBJECT LİSTESİ ŞEKLİNDE DÖNDÜRÜR------------------------------------------------------
    private List<GameObject> getCocuklar_List(GameObject tempNesne)
    {
        List<GameObject> list = null;

        if (getBosMu(tempNesne)) { Debug.LogWarning("NESNE NULL"); return null; }

        int diziBoyut = getCocukSayisi(tempNesne);
        if (getDiziSifirKucukMu(diziBoyut)) { return null; }
        list = new List<GameObject>();
        for (int i = 0; i < diziBoyut; i++)
        {
            GameObject gameObject = tempNesne.transform.GetChild(i).gameObject;
            list.Add(gameObject);
        }
        return list;

    }
    #endregion

    #region -------------------------------------------------------COCUKLARIN NESNEBİLGİLERİ DONDURME-------------------------------------------------------
    private List<_nesneBilgisi> GetCocuklar_nesneBilgiler_List(GameObject nesne1)
    {
        if (getBosMu(nesne1)){ Debug.Log("Nesne null"); return null;}

        List<GameObject> objs = getCocuklar_List(nesne1);
        if (objs == null) { Debug.LogWarning("LİST NULL ÇOCUĞU YOK ANLAMINDA"); return null; }


        int diziBoyut = objs.Count;
        if (diziBoyut <= 0) { Debug.LogWarning("LİST NULL"); return null; }

        List<_nesneBilgisi> temp = new List<_nesneBilgisi>();

        for (int i = 0; i < diziBoyut; i++)
        {
            _nesneBilgisi tempNesne = new _nesneBilgisi(getCocukNesne(objs, i));
            temp.Add(tempNesne);
        }

        return temp;
    }
    public _nesneBilgisi[] getCocuklar_nesneBilgiler_Dizi(GameObject nesne1)
    {
        if (getBosMu(nesne1)) { Debug.Log("Nesne null"); return null; }

        GameObject[] objs = getCocuklar_Dizi(nesne1);
        if (objs == null) { Debug.LogWarning("DİZİ NULL ÇOCUĞU YOK ANLAMINDA"); return null; }


        int diziBoyut = objs.Length;
        if (diziBoyut <= 0) { Debug.LogWarning("DiZİ NULL"); return null; }

        _nesneBilgisi[] temp = new _nesneBilgisi[diziBoyut];

        for (int i = 0; i < diziBoyut; i++)
        {
            temp[i] = new _nesneBilgisi(getCocukNesne(objs, i));
           
        }

        return temp;
    }
    public _nesneBilgisi getCocuklar_nesneBilgi(GameObject nesne1,int indeks)
    {
        GameObject objs = getCocukNesne(nesne1, indeks);
        if (objs == null) { Debug.LogWarning("DİZİ NULL ÇOCUĞU YOJ ANLAMINDA"); return null; }

        return new _nesneBilgisi(objs);
    }

    #endregion

    
}
