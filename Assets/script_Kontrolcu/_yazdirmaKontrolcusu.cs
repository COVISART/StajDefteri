using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _yazdirmaKontrolcusu 
{
    #region ------------------------SINGLETON------------------------------------
    private _yazdirmaKontrolcusu() { }
    private static _yazdirmaKontrolcusu instance = new _yazdirmaKontrolcusu();
    public static _yazdirmaKontrolcusu getInstance() { return instance; }
    #endregion


    public void printNULL_UYARI() { Debug.LogWarning("NESNE NULL"); }
    public void printNULL_UYARI(object temp) { if(temp == null) Debug.LogWarning("NESNE NULL"); }

    public void printNULL_HATA() { Debug.LogError("NESNE NULL"); }
    public void printNULL_HATA(object temp) { if (temp == null) Debug.LogError("NESNE NULL"); }

    public void printAYRAC(object temp) { Debug.Log("----------------------------------------------------------" + temp.ToString() + "--------------------------------------------------------------"); }
    public void printAYRAC() { Debug.Log("------------------------------------------------------------------------------------------------------------------------"); }

    public void print(object temp) { Debug.Log(temp.ToString()); }
    public void print(string temp) { Debug.Log(temp); }
    public void printNULL(string temp) { Debug.Log(temp + " NULL"); }
    public void printNULL(object temp) { Debug.Log(temp + " NULL"); }

    public void printNESNE(GameObject temp){
        bool kontrol = temp == null;
        if (kontrol){printNULL_UYARI(); return; }
        print("NESNE: " + temp + " || ÇOCUK SAYISI: " + temp.transform.childCount);
    }
    
    public void printCOCUK_SAYISI(GameObject temp) { if (temp == null) Debug.Log("Çocuk Sayısı: 0"); else Debug.Log("Çocuk Sayısı: " + temp.transform.childCount); }
    public void printDIZI_BOYUTU(GameObject[] temp) { if (temp == null) Debug.Log("Sayısı: 0"); else Debug.Log("Sayısı: " + temp.Length); }
    public void printLISTE_BOYUTU(List<GameObject> temp) { if (temp == null) Debug.Log("Sayısı: 0"); else Debug.Log("Sayısı: " + temp.Count); }
   
    public void printCOCUKLAR(GameObject temp) {
        if (temp == null){ print("ÇOCUĞU YOKTUR"); }
        int cocukSayisi = temp.transform.childCount;
        if (cocukSayisi == 0) { print("ÇOCUĞU YOKTUR"); return; }
        printAYRAC("NESNE: " + temp.name + " ÇOCUKLARI");
        for (int i = 0; i < cocukSayisi; i++){
            print((i+1)+".ÇOCUK: "+temp.transform.GetChild(i));
        }
    }
}
