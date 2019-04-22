using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class _menuItemGezinirkenRenklendirme : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //----------------SINGLETON-------------------
    _nesnelerBilgiListesi nesnelerBilgiListesi;

    public Color32 renkUzerinde,renkAyrildiginda;
    private RawImage raw;
    void Start(){ raw = GetComponent<RawImage>(); if (raw == null){ Debug.LogError("RawImage NULL");}
        nesnelerBilgiListesi = _nesnelerBilgiListesi.getINSTANCE();
        #region RENK BİLGİLERİ AYARLAMAZ :(
        //renkUzerinde = new Color32(30, 75, 147, 210);
        //renkAyrildiginda = Color.clear; 
        #endregion
    }

    //TIKLANDIĞINDA 
    public void OnPointerClick(PointerEventData eventData)
    {
        IgetGameObject_NesneBilgisi itemp = nesnelerBilgiListesi;
        _nesneBilgisi bilgi = itemp.getNesneBilgisi(name);
        Debug.Log(bilgi.nesne+" Tıklandı");
    }

    //ÜZERİNE GELDİĞİNDE
    public void OnPointerEnter(PointerEventData eventData)
    {
        raw.color = renkUzerinde;
    }

    //AYRILDIĞINDA
    public void OnPointerExit(PointerEventData eventData)
    {
        raw.color = renkAyrildiginda;
    }

}
