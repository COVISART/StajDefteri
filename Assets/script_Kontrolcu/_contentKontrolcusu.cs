using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//MENU KONTROLLER
public class _contentKontrolcusu : MonoBehaviour
{
    //------------------------------------------------------------------------------------------------------------------------------------
    //DEĞİŞKENLER 
    public float kategoriAdim = 5f;

    //BAĞLANMASI GEREKENLER
    private GameObject kopyalaItem; //Kopyalanacak Ağaç Itemi RAWIMAGE
    private Transform contentTransform; //Content
    public GameObject nesne; //Hiyerarşi

    //CONTENT BOYUTLANDIRMA
    public _contentBoyutlandirma contentBoyutlandirma;

    //SİNGLETONLAR
    _nesnelerBilgiListesi bilgiList;

    //------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        bilgiList = _nesnelerBilgiListesi.getINSTANCE();

        //CONTENT TRANSFORM
        contentTransform = gameObject.transform;
        if (contentTransform == null) { Debug.LogWarning("content Boş"); return; }

        //KOPYALA ITEM
        kopyalaItem = contentTransform.GetChild(0).gameObject;
        if (kopyalaItem == null) { Debug.LogWarning("kopyalaItem Boş"); return; }

        //MENU BİLEŞENLERİ
        contentBoyutlandirma = contentTransform.GetComponent<_contentBoyutlandirma>();
        if (contentBoyutlandirma == null) { Debug.LogError("contentBoyutlandirma Boş"); return; }

        //HİYERARŞİK ALINACAK NESNE 
        if (nesne == null) { Debug.LogError("nesne1 Boş"); return; }
       
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //MENU ITEM YARATMA VE DAHİL ETME
    #region --------------------------------------- MENU ITEM YARATMA VE DAHİL ETME ----------------------------------------------------------------
    public GameObject getItem(GameObject tempName,bool gorunurMu = true)
    {
        if (tempName == null) { Debug.LogError("Nesne NULL"); return null; }
        if (kopyalaItem == null) { Debug.LogError("KopyalaItem NULL"); return null; }
        if (contentTransform == null) { Debug.LogError("Content NULL"); return null; }

        GameObject temp = Instantiate(kopyalaItem);

        temp.transform.SetParent(contentTransform);
        temp.transform.localScale = Vector3.one; //temp.transform.Find("Text").GetComponent<Text>().text = tempName.name;
        temp.name = tempName.GetInstanceID().ToString();
        temp.SetActive(gorunurMu);

        return temp;
    }
    public void createMenuItemNew(GameObject nesneNew)
    {
        if (bilgiList == null) { Debug.LogError("Bilgi List NULL"); return; }
        if (kopyalaItem == null) { Debug.LogError("KopyalaItem NULL"); return; }
        if (contentTransform == null) { Debug.LogError("Content NULL"); return; }
        if (nesne == null) { Debug.LogError("Nesne NULL"); return; }

        
        IAdd tempAdd = bilgiList;
        IgetKeysValues tempKeysValues = bilgiList;
        IgetGameObject_NesneBilgisi tempGameList = bilgiList;

        setItemleri_Silme();
        tempAdd.AddNew(nesneNew);//TEMİZ KAYITLARI EKLER  (BAŞKA HİYERARŞİ EKLENECEKSE ADD KULLANILIR)
        foreach (GameObject item in tempKeysValues.getKeys_GameObjects)
        {
            GameObject clone = getItem(item);
            _nesneBilgisi bilgi = tempGameList.getNesneBilgisi(item);
            bilgi.setImageText(clone);            //bilgi.nesne.transform.gameObject.SetActive(nesne.gameObject.activeSelf);
            bilgi.nesne.SetActive(nesne.activeSelf);
        }
        if (kopyalaItem.activeSelf) kopyalaItem.SetActive(false);
        contentBoyutlandirma.setBoyutlandir();
    }

    #endregion

    //------------------------------------------------------------------------------------------------------------------------------------
    //YAPILAN İŞLEMLER
    #region -----------------------------------------------UPDATE METODU----------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { createMenuItemNew(nesne); }
        else if (Input.GetKeyDown(KeyCode.Backspace)) { setItem_GosterGizle(kopyalaItem); }
        else if (Input.GetKeyDown(KeyCode.Delete)) { setItemleri_Silme(); }

    }
    #endregion

    //------------------------------------------------------------------------------------------------------------------------------------
    //GÖSTERME GİZLEME İŞLEMİ 
    #region GOSTERME GİZLEME İŞLEMİ
    private void setItem_GosterGizle(GameObject temp)
    {
        //MENU ITEM GÖSTERME VE GİZLEME İŞLEMİ 
        if (temp.activeSelf) temp.SetActive(false);
        else { temp.SetActive(true); }
        
    }
    //INSTANCE ID İLE GOSTER GİZLE
    private void setItem_GosterGizle(int temp) {
        IgetGameObject_NesneBilgisi itemp = bilgiList;
        GameObject gameObject = itemp.getGameObject(temp);// idList.getGAMEOBJECT_FROM_ID(temp);

        if (gameObject == null) { Debug.LogWarning("NESNE NULL BULUNAMADI"); return; }

        setItem_GosterGizle(gameObject);
    }
    #endregion

    //MENUYU BOŞALTMA
    #region ------------------------------------------MENUYU BOSALTMA------------------------------------------
    private void setItemleri_Silme()
    {
        int count = contentTransform.childCount;
        //Debug.Log("---------------------Çocuk sayısı: " + count+"------------------------------------");

        if (contentTransform.childCount > 1)
        {
            for (int i = count - 1; i > 0; i--)
            {
                GameObject silObj = contentTransform.GetChild(i).gameObject;
                //Debug.Log(silObj + " SİLİNİYOR");
                DestroyImmediate(silObj);


            }
            //Debug.Log("silindikten sonraki çocuk sayısı: " + content1.childCount);
        }




    }
    #endregion
}
