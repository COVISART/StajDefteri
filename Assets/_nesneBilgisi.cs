using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region ENUM ÇOCUK DURUM
public enum enumCocukDurum
{
    sag,
    asagi,
    bos,
}
#endregion 

//NESNENİN BİLGİLERİ
public class _nesneBilgisi: IImageText
{
    //------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------IMAGE TEXT------------------------------------------------------------------------------------
    #region IMAGE TEXT
    public bool getNULL_Image { get { bool kontrol = image == null; if (kontrol) Debug.Log("Image NULL"); return kontrol; } }
    public bool getNULL_Text { get { bool kontrol = text == null; if (kontrol) Debug.Log("Text NULL"); return kontrol; } }
    public bool getNULL_ImageRaw { get { bool kontrol = imageRaw == null; if (kontrol) Debug.Log("ImageRaw NULL"); return kontrol; } }

    public Transform getTRANSFORM_ImageRaw { get { if (getNULL_ImageRaw) { return null; } return imageRaw.transform; } }
    public Transform getTRANSFORM_Image { get { if (getNULL_Image) { return null; } return image.transform; } }
    public Transform getTRANSFORM_Text { get { if (getNULL_Text) { return null; } return text.transform; } }

    public bool getDURUM_ImageRaw { get { return imageRaw.gameObject.activeSelf; } }
    public void setDURUM_ImageRaw() { imageRaw.gameObject.SetActive(!getDURUM_ImageRaw); }
    public void setDURUM_ImageRaw(bool temp) { imageRaw.gameObject.SetActive(temp); }

    //TREEITEM
    public RawImage image;

    //TREEITEM ÇOCUKLARI
    public RawImage imageRaw;
    public Text text;

    //TREEITEM TEXTURE
    public Texture setTEXTURE { set { image.texture = value; } }

    //------------------------------------------------------------------------------------------------------------------------------------
    //IMAGEİ(TREEITEM) RAWIMAGE ve TEXT EŞİTLER
    public void setImageText(GameObject imageRaw1)
    {
        imageRaw = imageRaw1.GetComponent<RawImage>();
        if (imageRaw1 == null) { Debug.Log("ImageRaw NULL"); return; }
        text = imageRaw1.transform.Find("Text").GetComponent<Text>();
        image = imageRaw1.transform.Find("RawImage").GetComponent<RawImage>();
        if (image == null) { Debug.Log("Image Başarı doldurulamadı"); }
        if (text == null) { Debug.Log("Text Başarı doldurulamadı"); }

        //KATEGORI KONUM AYARLAMASI
        setKATEGORI_KONUM(image.rectTransform.rect.width/2);//SAĞA KAYDIRMA
        text.text = nesne.name;
        text.name = imageRaw1.name;
        image.name = imageRaw1.name;
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //KATEGORİ KONUM
    public void setKATEGORI_KONUM(float adim)
    {
        if (getTRANSFORM_Image == null) return;
        if (getTRANSFORM_Text == null) return;

        float mesafe = getKATEGORI_DEGER(adim);
        Vector3 vector = Vector3.right * mesafe;

        getTRANSFORM_Text.Translate(vector);
        getTRANSFORM_Image.Translate(vector);
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //YAZDIRMA
    public string getBILGILER_ImageText() { return "Text null mu: " + text + " || Image null mu: " + image; }    //BİLGİLERİ AYARLAMAK

    #endregion

    //------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------NESNE DURUM BİLGİSİ---------------------------------------------------------------------------
    public enumCocukDurum getDURUM_COCUK = enumCocukDurum.asagi;//AÇILIŞTA GÖSTERSİN Mİ GÖSTERMESİN Mİ? AYARLA

    //-----------------------------------------------KATEGORİ NOSU---------------------------------------------------------------------------------
    public int kategoriNo = -1;
    public float getKATEGORI_DEGER(float temp) { return temp * kategoriNo; }

    //-----------------------------------------------NESNE && ÇOCUKLAR-----------------------------------------------------------------------------
    public GameObject nesne;                    //ANA NESNE
    public GameObject[] nesneCocuklar = null;   //ANA NESNENİN ÇOCUKLARI

    //ÇOCUKLAR GİZLENME BİLGİSİ BURADA AYARLANIR
    public bool getCOCUKLAR_GORUNUR = true;

    //-----------------------------------------------ÇOCUKLAR BOŞ MU && ÇOCUKLAR SAYISI------------------------------------------------------------
    public bool getNULL_COCUKLAR { get { return nesneCocuklar == null; } }
    public int getSAYISI_COCUKLAR { get { if (getNULL_COCUKLAR) { return 0; } return nesneCocuklar.Length; } }

    //-----------------------------------------------NESNENİN PARENTİ------------------------------------------------------------------------------
    public Transform getParent { get { if (nesne == null) { return null; } if (nesne.transform == null) { return null; } return nesne.transform.parent; } }

    //------------------------------------------------------------------------------------------------------------------------------------
    public _nesneBilgisi(GameObject nesne1)
    {
        //BOŞ MU KONTROL YAPIYOR
        if (nesne1 == null) { Debug.LogError("Nesne NULL"); return; }

        //NESNE BİLGİLERİ DÖNDÜRÜR
        _nesnelerinBilgileriniDondurur nesneBilgiDondurur = _nesnelerinBilgileriniDondurur.getInstance();

        //NESNE VE ÇOCUKLARI DOLDURUR
        nesne = nesne1;
        nesneCocuklar = nesneBilgiDondurur.getCocuklar_Dizi(nesne1);

        //KATEGORİ AYARLAMA
        if (getParent == null) { kategoriNo = 0; }

        //ÇOCUK OLUŞTURUR(VARSA)
        nesneCocuklar = nesneBilgiDondurur.getCocuklar_Dizi(nesne1);

        //ÇOCUK DURUM
        if (getNULL_COCUKLAR) { getDURUM_COCUK = enumCocukDurum.bos; }
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //YAZDIRMA İLE İLGİLİ
    internal string getBILGILER() { return "Nesne: " + nesne + " || Kategori No: " + kategoriNo + " || Durum: " + getDURUM_COCUK+ " || Cocuklar Gorunur: " + getCOCUKLAR_GORUNUR; }
}

//------------------------------------------------------------------------------------------------------------------------------------
interface IImageText
{
    bool getNULL_Image { get; }
    bool getNULL_Text { get; }
    bool getNULL_ImageRaw { get; }

    Transform getTRANSFORM_ImageRaw { get; }
    Transform getTRANSFORM_Image { get; }
    Transform getTRANSFORM_Text { get; }

    bool getDURUM_ImageRaw { get; }
    void setDURUM_ImageRaw();
    void setDURUM_ImageRaw(bool temp);

    Texture setTEXTURE { set; }
    void setImageText(GameObject imageRaw1);
    void setKATEGORI_KONUM(float adim);
    string getBILGILER_ImageText();
}
/*interface ICocuklar
{

}*/