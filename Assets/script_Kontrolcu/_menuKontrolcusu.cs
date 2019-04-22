using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//MENU KONTROLLER
public class _menuKontrolcusu : MonoBehaviour
{
    //------------------------------------------------------------------------------------------------------------------------------------
    //DEĞİŞKENLER 
    public float kategoriAdim = 5f;

    //BAĞLANMASI GEREKENLER
    public GameObject kopyalaItem; //Kopyalanacak Ağaç Itemi RAWIMAGE
    public Transform content; //Content
    public GameObject nesne; //Hiyerarşi

    public _contentBoyutlandirma contentBoyutlandirma;
    public _menuItemOlusturmaGostermeGizleme menuOlusturma;
    
    //SİNGLETONLAR
    _nesnelerBilgiListesi bilgiList;

    //------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        bilgiList = _nesnelerBilgiListesi.getINSTANCE();
        menuOlusturma = GetComponent<_menuItemOlusturmaGostermeGizleme>();
        contentBoyutlandirma = content.GetComponent<_contentBoyutlandirma>();
        if (kopyalaItem == null) { Debug.LogWarning("kopyalaItem Boş"); return; }
        if (content == null) { Debug.LogWarning("content Boş"); return; }
        if (nesne == null) { Debug.LogWarning("nesne1 Boş"); return; }
        if (menuOlusturma==null) { Debug.LogWarning("menu Boş"); return; }
        if (contentBoyutlandirma == null) { Debug.LogError("contentBoyutlandirma Boş"); return; }

    

        //setMENU_YAP();
    }
    //------------------------------------------------------------------------------------------------------------------------------------
    //MENU ITEM YARATMA VE DAHİL ETME
    #region --------------------------------------- MENU ITEM YARATMA VE DAHİL ETME ----------------------------------------------------------------
    public GameObject getItem(GameObject tempName, GameObject kopyalaItem, Transform content,bool gorunurMu = true){
        return menuOlusturma.getMENUITEM(tempName,kopyalaItem,content,gorunurMu);
    }
    public GameObject getItem(GameObject tempName) { return getItem(tempName, kopyalaItem, content); }
    public void createMenuItem(GameObject nesne){ menuOlusturma.createMENUITEM_NEW(nesne,kopyalaItem,content);}

    #endregion

    //------------------------------------------------------------------------------------------------------------------------------------
    //YAPILAN İŞLEMLER
    #region -----------------------------------------------UPDATE METODU----------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){ setMENU_YAP();}
        else if (Input.GetKeyDown(KeyCode.Backspace)){ setItem_GosterGizle(kopyalaItem);}
        else if (Input.GetKeyDown(KeyCode.Delete)){ setItemleri_Silme(content);}

    }
    #endregion

    //------------------------------------------------------------------------------------------------------------------------------------
    //MENU AYARLAR TEMİZ ŞEKİLDE
    #region MENU AYARLAR TEMİZ ŞEKİLDE
    public void setMENU_YAP(){ setMENU_YAP(nesne);}
    public void setMENU_YAP(GameObject tempObj)
    {
        if (kopyalaItem == null) { Debug.LogWarning("kopyalaItem Boş"); return; }
        if (content == null) { Debug.LogWarning("content Boş"); return; }
        if (nesne == null) { Debug.LogWarning("nesne Boş"); return; }

        //Debug.Log("Silinmeden önce çocuk sayısı: " + content.childCount);
        setItemleri_Silme(content);
        //Debug.Log("Silindikten sonrası çocuk sayısı: " + content.childCount);
        createMenuItem(tempObj);
        //Debug.Log("Menu sonrası çocuk sayısı: " + content.childCount);
        contentBoyutlandirma.setBoyutlandir();
        //Debug.Log("Boyutlandırma sonrası çocuk sayısı: " + content.childCount);
        //BİLGİLERİ YAZDIR
        //bilgiList.printBILGILER();
    }
    #endregion

    //------------------------------------------------------------------------------------------------------------------------------------
    //GÖSTERME GİZLEME İŞLEMİ 
    #region GOSTERME GİZLEME İŞLEMİ
    private void setItem_GosterGizle(GameObject temp){ menuOlusturma.setItem_GosterGizle(temp);}
    private void setItem_GosterGizle(int temp){ menuOlusturma.setItem_GosterGizle(temp);}
    #endregion

    //MENUYU BOŞALTMA
    #region ------------------------------------------MENUYU BOSALTMA------------------------------------------
    private void setItemleri_Silme(Transform content1){
        int count = content1.childCount;
        Debug.Log("---------------------Çocuk sayısı: " + count+"------------------------------------");

        if (content.childCount>1)
        {
            for (int i = count - 1; i > 0; i--)
            {
                GameObject silObj = content1.GetChild(i).gameObject;
                //Debug.Log(silObj + " SİLİNİYOR");
                DestroyImmediate(silObj);

            
            }
            Debug.Log("silindikten sonraki çocuk sayısı: " + content1.childCount);
        }
        

      
   
    }
    #endregion
}
