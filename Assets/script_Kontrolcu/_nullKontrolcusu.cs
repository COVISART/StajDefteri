using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _nullKontrolcusu
{
    #region ------------------------SINGLETON------------------------------------
    private _nullKontrolcusu() { }
    private static _nullKontrolcusu instance = new _nullKontrolcusu();
    public static _nullKontrolcusu getInstance() { return instance; }
    #endregion

    #region - KONTROL KOMUTLARI -

    public bool getNULL(object temp) { return (temp == null); }
    public bool getNULL(object[] temp) { return (temp == null); }
    public bool getNULL(List<object> temp) { return (temp == null); }

    public bool getNULL_YAZDIR(object temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }
    public bool getNULL_YAZDIR(object[] temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }
    public bool getNULL_YAZDIR(List<object> temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }

    /*public bool getNULL(GameObject temp) { return (temp == null); }
    public bool getNULL(GameObject[] temp) { return (temp == null); }
    public bool getNULL(List<GameObject> temp) { return (temp == null); }

    public bool getNULL(Transform temp) { return (temp == null); }
    public bool getNULL(Transform[] temp) { return (temp == null); }
    public bool getNULL(List<Transform> temp) { return (temp == null); }

    public bool getNULL(RectTransform temp) { return (temp == null); }
    public bool getNULL(RectTransform[] temp) { return (temp == null); }
    public bool getNULL(List<RectTransform> temp) { return (temp == null); }*/

    /*public bool getNULL_YAZDIR(GameObject temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }
    public bool getNULL_YAZDIR(GameObject[] temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }
    public bool getNULL_YAZDIR(List<GameObject> temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }

    public bool getNULL_YAZDIR(Transform temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }
    public bool getNULL_YAZDIR(Transform[] temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }
    public bool getNULL_YAZDIR(List<Transform> temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }

    public bool getNULL_YAZDIR(RectTransform temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }
    public bool getNULL_YAZDIR(RectTransform[] temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }
    public bool getNULL_YAZDIR(List<RectTransform> temp) { bool tempNull = (temp == null); if (tempNull) { Debug.Log("NESNE NULL"); } return tempNull; }*/

    #endregion
}
