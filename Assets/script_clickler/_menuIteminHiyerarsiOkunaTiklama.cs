using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class _menuIteminHiyerarsiOkunaTiklama : MonoBehaviour, IPointerClickHandler
{
    #region GÖSTEREN OK A TIKLANDIĞINDA YAPILACAKLAR METODU
    public void OnPointerClick(PointerEventData eventData)
    {
        try
        {
            #region ---TIKLANAN NESNE VE İLGİLİ BİLGİ SINIFINI BULMA---
            int idsi = int.Parse(name);
            GameObject temp = bilgiList.getGameObject(idsi); //idList.getGAMEOBJECT_FROM_ID(idsi);
            if (temp==null) { Debug.Log("NESNE NULL ONPOINTER"); return; }

            
            _nesneBilgisi bilgi = bilgiList.getNesneBilgisi(temp);
            if (bilgi==null) { Debug.Log("NESNE NULL ONPOINTER"); return; }
            #endregion

            #region ---GÖRÜNÜRLÜĞÜ DEĞİŞTİRME---

            //GÖRÜNÜRLÜĞÜ DEĞİŞTİR
            bilgi.getCOCUKLAR_GORUNUR = !bilgi.getCOCUKLAR_GORUNUR;

            //GÖRÜNÜRLÜĞÜ İŞLE
            setGORUNURLUK(bilgi, bilgi.getCOCUKLAR_GORUNUR);

            #endregion


            #region--- RESİM DEĞİŞTİRME ---
            if (bilgi.getDURUM_COCUK==enumCocukDurum.asagi){ bilgi.getDURUM_COCUK = enumCocukDurum.sag; }
            else if(bilgi.getDURUM_COCUK == enumCocukDurum.sag){ bilgi.getDURUM_COCUK = enumCocukDurum.asagi; }

            //DURUMA GÖRE RESİM
            switch (bilgi.getDURUM_COCUK) 
            {
                case enumCocukDurum.sag: bilgi.setTEXTURE = textureSag; break;
                case enumCocukDurum.asagi: bilgi.setTEXTURE = textureAsagi; break;
                case enumCocukDurum.bos: bilgi.setTEXTURE = textureBos; break;
            }
            #endregion

            #region BİLGİLER YAZDIR
            //bilgiList.printBILGILER();
            #endregion
        }

        catch (System.Exception) { Debug.LogWarning("Orijinal tree iteme tıkladınız: "+name); }
    }
    #endregion

    #region DURUM BİLGİSİNE GÖRE OK UN KAPLAMASINI DEĞİŞTİRİR
    public void setTEXTURE_IMAGE(_nesneBilgisi bilgi)
    {
        switch (bilgi.getDURUM_COCUK)
        {
            case enumCocukDurum.sag: bilgi.setTEXTURE = textureSag; break;
            case enumCocukDurum.asagi: bilgi.setTEXTURE = textureAsagi; break;
            case enumCocukDurum.bos: bilgi.setTEXTURE = textureBos; break;
        }
    }
    #endregion

    #region OK A TIKLANDIĞINDA MENU ITEMİ GÖSTERME & GİZLEME METODU
    public void setGORUNURLUK(_nesneBilgisi tempBilgi, bool ac)
    {
        if (tempBilgi==null){ return; }
        if (tempBilgi.nesneCocuklar==null){  return;}

        foreach (GameObject item in tempBilgi.nesneCocuklar)
        {
            _nesneBilgisi temp = bilgiList.getNesneBilgisi(item);

            //GÖRÜNÜRLÜK AYARLAR
            if (ac==false){
                temp.setDURUM_ImageRaw(ac);
                setGORUNURLUK(temp, ac); //RECORSIVE
            }
            else
            {
                
                temp.setDURUM_ImageRaw(tempBilgi.getCOCUKLAR_GORUNUR);
                setGORUNURLUK(temp, tempBilgi.getCOCUKLAR_GORUNUR); //RECORSIVE
            }
        }
    }
    #endregion

    #region DEĞİŞKENLER && VARSAYILAN DEĞERLER
    public RawImage image;
    public Texture textureSag, textureAsagi, textureBos;
    _nesnelerBilgiListesi bilgiList;

    void Start()
    {
        bilgiList = _nesnelerBilgiListesi.getINSTANCE();
        image = GetComponent<RawImage>();
        printNULL_IMAGE();

        foreach (GameObject item in bilgiList.getKeys_GameObjects)
        {
            _nesneBilgisi bilgi = bilgiList.getNesneBilgisi(item);
            if (printNULL(bilgi)) { return; }
            setTEXTURE_IMAGE(bilgi);
        }
    }
    #endregion

    #region NULL DEĞERLER VAR MI KONTROL EDER
    bool getNULL(object obj) { return obj == null; }
    bool printNULL(object obj) { if (getNULL(obj)) { Debug.LogWarning("Nesne NULL"); return true; } else { return false; } }

    bool printNULL_IMAGE()   { if (getNULL_IMAGE) { Debug.LogWarning("RawImage NULL"); return true; } else { return false; } }
    bool getNULL_IMAGE {  get { if (image == null) {  return true; } else{ return false;} }}
    #endregion
}
