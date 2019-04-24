using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _contentBoyutlandirma : MonoBehaviour
{
    #region --------------------------------------------------------DEĞİŞKENLER && BAŞLANGIÇ DEĞERLER--------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------------
    _yazdirmaKontrolcusu yazdirmaKontrolcusu;

    //CONTENT
    //private GameObject content;
    private _nullKontrolcusu nullMu;
    private _nesnelerinBilgileriniDondurur bilgiDondurur;
    private _nesnelerBilgiListesi nesnelerBilgiListesi;
    private void Start()
    {
        bilgiDondurur = _nesnelerinBilgileriniDondurur.getInstance();
        yazdirmaKontrolcusu = _yazdirmaKontrolcusu.getInstance();
        nullMu = _nullKontrolcusu.getInstance();
        nesnelerBilgiListesi = _nesnelerBilgiListesi.getINSTANCE();
        //bilgiDondurur.print()

    }
    #endregion

    #region --------------------------------------------------------ÇOCUKLAR--------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------------
    //ÇOCUKLAR ÖĞRENİR
    public int getCocukSayisi
    {
        get
        {
            int sayisi = bilgiDondurur.getCocukSayisi(gameObject) - 1;
            if (sayisi == 0) { Debug.LogWarning("Sadece varsayılan item var"); return sayisi; }
            else if (sayisi == -1) { Debug.LogWarning("Cocuk Sayısı 0"); return sayisi; }
            return sayisi;
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //ÇOCUKLARI YAZDIRMA
    public void printCocukSayisi(){ Debug.Log("Çocuk Sayısı: " + getCocukSayisi); }

    //------------------------------------------------------------------------------------------------------------------------------------
    //ÇOCUKLAR DONDURUR
    public GameObject[] getCocuklar
    {
        get
        {
            GameObject[] gameObjects = null;
            gameObjects = bilgiDondurur.getCocuklar_Dizi(gameObject);
            if (gameObjects == null) { Debug.LogWarning("Cocuk Nesneler NULL"); }
            return gameObjects;
        }
    }
    #endregion

    #region --------------------------------------------------------CONTENT GENİŞLİK YÜKSEKLIK DEĞERLERİNİ OKUR--------------------------------------------------------
    //GENİŞLİK YÜKSEKLİK BULMA
    public Rect getRect(){ return getRect(gameObject);}
    public Rect getRect(GameObject obj)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        if (rectTransform == null) { Debug.LogError("RECT TRANSFORM NULL"); return Rect.zero; }
        else { return rectTransform.rect; }
    }
    #endregion

    #region --------------------------------------------------------YUKSEKLIK HESAPLAR--------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------------
    //YUKSEKLIK HESAPLAR AÇIK OLANLAR BİLGİSİ GİRİLECEK ONA GÖRE
    public float getHeight(int cocukSayisi, GameObject treeItem)
    {
        if (nullMu.getNULL_YAZDIR(gameObject)) { return 0; }
        RectTransform rectTreeItem = treeItem.GetComponent<RectTransform>();
        if (nullMu.getNULL_YAZDIR(rectTreeItem)) { return 0; }
        return rectTreeItem.rect.height * cocukSayisi;
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    
    
    //------------------------------------------------------------------------------------------------------------------------------------
    public int getCocukSayisiNew()
    {
        //Debug.Log(nesnelerBilgiListesi.list_GameObject_Bilgi_Dictionary.Count);
        return nesnelerBilgiListesi.list_GameObject_Bilgi_Dictionary.Count; 
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //***DESTROY SORUNLUYDU KULLANMADIM***
    public float getHeight()
    {
        RectTransform rectTreeItem = null;
        Transform treeItemTransform = null;
        Transform contentTransform = gameObject.transform;
        int adet = gameObject.transform.childCount - 1;
        if (adet > 0) { treeItemTransform = contentTransform.GetChild(adet); rectTreeItem = (RectTransform)treeItemTransform; }
        else yazdirmaKontrolcusu.printNULL_UYARI();

        if (treeItemTransform == null) { Debug.Log("transform NULL"); return 0F; }
        if (rectTreeItem == null) { Debug.Log("item NULL"); return 0F; }
        if (rectTreeItem.rect == null) { Debug.Log("rect NULL"); return 0F; }

        return rectTreeItem.rect.height * contentTransform.childCount;
    }
    //YUKSEKLIK HESAPLAR HEPSİ AÇIK MIŞ KADAR
    public float getHeightNew()
    {
        RectTransform rectTreeItem = null;
        Transform treeItemTransform = null;
        Transform contentTransform = gameObject.transform;
        int adet = getCocukSayisiNew();
        if (adet > 0) { treeItemTransform = contentTransform.GetChild(adet); rectTreeItem = (RectTransform)treeItemTransform; }
        else yazdirmaKontrolcusu.printNULL_UYARI();

        if (treeItemTransform == null) { Debug.Log("transform NULL"); return 0F; }
        if (rectTreeItem == null) { Debug.Log("item NULL"); return 0F; }
        if (rectTreeItem.rect == null) { Debug.Log("rect NULL"); return 0F; }

        return rectTreeItem.rect.height * nesnelerBilgiListesi.list_GameObject_Bilgi_Dictionary.Count;
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    public float getWidth()
    {
        IgetKeysValues itemp = nesnelerBilgiListesi;
        _nesneBilgisi tempNesneBilgisi = null;
        _nesneBilgisi[] values = itemp.getValues_Bilgiler;
        if (values == null){ return 0;}
        
        foreach (_nesneBilgisi item in values)
        {
            if (tempNesneBilgisi == null){ tempNesneBilgisi = item;}
            else
            {
                IImageText imageText = tempNesneBilgisi;
                Transform transformNesne = tempNesneBilgisi.getTRANSFORM_Text;
                Transform transformItem = item.getTRANSFORM_Text;
                if (transformNesne.position.x+ ((RectTransform)transformNesne).rect.width < transformItem.position.x+ ((RectTransform)transformItem).rect.width)
                {
                    tempNesneBilgisi = item;
                }
            }
            
        }
        Transform transformNesneBilgisi = tempNesneBilgisi.getTRANSFORM_Text;
        //Debug.Log(((RectTransform)transform).rect.width/6);//350
        //float widdh = 350;
        float widdh = ((RectTransform)transform).rect.width / 12;
        return transformNesneBilgisi.position.x + ((RectTransform)transformNesneBilgisi).rect.width - widdh;
        //return transformNesneBilgisi.position.x + ((RectTransform)transformNesneBilgisi).rect.width-200;
    }
    #endregion

    #region --------------------------------------------------------HESAPLANAN DEĞERLERİ UYGULAR--------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------------------------------
    public void setBoyutlandir()
    {
        RectTransform transContent = (RectTransform)gameObject.transform;
        float yukseklik = getHeightNew();
        float genislik = getWidth();
        //float genislik = transContent.rect.width;
        /*Debug.Log("****************");
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject item = gameObject.transform.GetChild(i).gameObject;
            Debug.Log(item);
        }
        Debug.Log("****************");*/
        /*print("content çocuk sayısı: " + gameObject.transform.childCount + " genislik " + genislik + " yukseklik " + yukseklik); //transContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 12f);
        print("content çocuk sayısıNew: " + getCocukSayisiNew() + " genislik " + genislik + " yukseklik " + yukseklik); //transContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 12f);
        Debug.Log("****************");
        
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject item = gameObject.transform.GetChild(i).gameObject;
            Debug.Log(item);
        }
        Debug.Log("****************");*/

        transContent.sizeDelta = new Vector2(genislik, yukseklik);

        
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    public void setBoyutlandir2()
    {
        RectTransform transContent = (RectTransform)gameObject.transform;
        float yukseklik = getHeight();
        float genislik = transContent.rect.width;
        print(gameObject.transform.childCount + "genislik " + genislik + " yukseklik " + yukseklik); //transContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 12f);

        transContent.sizeDelta = new Vector2(genislik, yukseklik);


    }

    #endregion





}
