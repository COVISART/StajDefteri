using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _menuItemOlusturmaGostermeGizleme : MonoBehaviour
{
    #region DEĞİŞKENLER && BAŞLANGIÇ DEĞERLER
    //SINGLETON
    _nesnelerBilgiListesi bilgiList;

    private void Start(){
        bilgiList = _nesnelerBilgiListesi.getINSTANCE();
    }
    #endregion

    #region MENU ITEM GÖSTERME VE GİZLEME İŞLEMİ 
    public void setItem_GosterGizle(GameObject temp)
    {
        if (temp.activeSelf) temp.SetActive(false);
        else { temp.SetActive(true); }
    }
    
    public void setItem_GosterGizle(int temp)
    {
        IgetGameObject_NesneBilgisi itemp = bilgiList;
        GameObject gameObject = itemp.getGameObject(temp);// idList.getGAMEOBJECT_FROM_ID(temp);

        if (gameObject == null) { Debug.LogWarning("NESNE NULL BULUNAMADI"); return; }

        setItem_GosterGizle(gameObject);
    }
    #endregion

    #region MENU ITEM YARATMA VE DAHİL ETME
    public GameObject getMENUITEM(GameObject tempName, GameObject kopyalaItem, Transform content, bool gorunurMu = true)
    {
        if (tempName == null) { Debug.LogError("Nesne NULL"); return null; }
        if (kopyalaItem == null) { Debug.LogError("KopyalaItem NULL"); return null; }
        if (content == null) { Debug.LogError("Content NULL"); return null; }

        GameObject temp = Instantiate(kopyalaItem);

        temp.transform.SetParent(content);
        temp.transform.localScale = Vector3.one; //temp.transform.Find("Text").GetComponent<Text>().text = tempName.name;
        temp.name = tempName.GetInstanceID().ToString();
        temp.SetActive(gorunurMu);

        return temp;
    }
    //TEMİZ MENUITEM YARATMA
    public void createMENUITEM_NEW(GameObject nesne, GameObject kopyalaItem, Transform content)
    {
        if (nesne == null) { Debug.LogError("Nesne NULL"); return; }
        IAdd iadd = bilgiList;
        iadd.AddNew(nesne);//TEMİZ KAYITLARI EKLER  (BAŞKA HİYERARŞİ EKLENECEKSE ADD KULLANILIR)
        createMENUITEM_ORTAK(nesne, kopyalaItem, content);
    }
    //VAROLANIN ÜSTÜNE EKLEME YAPARAK MENUITEM YARATMA
    public void createMENUITEM(GameObject nesne, GameObject kopyalaItem, Transform content)
    {
        if (nesne == null) { Debug.LogError("Nesne NULL"); return; }
        IAdd iadd = bilgiList;
        iadd.Add(nesne);//KAYITLARI EKLER  
        createMENUITEM_ORTAK(nesne,kopyalaItem,content);
    }
    private void createMENUITEM_ORTAK(GameObject nesne, GameObject kopyalaItem, Transform content)
    {
        if (bilgiList == null) { Debug.LogError("Bilgi List NULL"); return; }
        if (kopyalaItem == null) { Debug.LogError("KopyalaItem NULL"); return; }
        if (content == null) { Debug.LogError("Content NULL"); return; }

        IgetKeysValues ikeysValues = bilgiList;
        IgetGameObject_NesneBilgisi igame = bilgiList;
        foreach (GameObject item in ikeysValues.getKeys_GameObjects)
        {
            GameObject clone = getMENUITEM(item, kopyalaItem, content);
            _nesneBilgisi bilgi = igame.getNesneBilgisi(item);
            bilgi.setImageText(clone);
            bilgi.nesne.transform.gameObject.SetActive(nesne.gameObject.activeSelf);
        }
        if (kopyalaItem.activeSelf) kopyalaItem.SetActive(false);
    }
    #endregion

    #region MENU ITEM SİLME TEMİZLEME
    public void setMenuyuTemizle(Transform content1)
    {
        int count = content1.childCount;
        Debug.Log("Çocuk sayısı: "+count);

        for (int i = count - 1; i > 0; i--){ Destroy(content1.GetChild(i).gameObject); }
        Debug.Log("Sildikten sonra Çocuk sayısı: " + count);

    }
    #endregion

}
