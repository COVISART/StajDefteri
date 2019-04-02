using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class _xml : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

    }

    _controlFileFolder fileFolder = new _controlFileFolder();
    void Start()
    {
        createXml();
    }
    bool getXmlMi(string pathFull)
    {
        if (!pathFull.EndsWith(".xml"))
        {
            Debug.LogWarning("xmlAdi.xml şeklinde yazılacak siz ise bunu yazdınız: " + pathFull);
            return false;
        }
        else
        {
            return true;
        }
    }
    void createXml(){ createXml("ayarlar.xml");}
    void createXml(string adi)
    {
        if (!adi.EndsWith(".xml"))
        {
            Debug.LogWarning("xmlAdi.xml şeklinde yazılacak siz ise bunu yazdınız: "+adi);
            return;
        }
        string path = Application.dataPath + "/"+adi;
        string baslik = "baslik text";
        int width = 100;
        int height = 150;

        XmlDocument xdoc = new XmlDocument();
        XmlElement xayarlar = xdoc.CreateElement("ayarlar");
        XmlElement xbaslik = xdoc.CreateElement("baslik"); xbaslik.InnerText = baslik;
        XmlElement xwidth = xdoc.CreateElement("width"); xwidth.InnerText = width.ToString();
        XmlElement xheight = xdoc.CreateElement("height"); xheight.InnerText = height.ToString();

        xayarlar.AppendChild(xbaslik);
        xayarlar.AppendChild(xwidth);
        xayarlar.AppendChild(xheight);

        xdoc.AppendChild(xayarlar);
        xdoc.Save(path);

        Debug.Log("ayarlar.xml kaydedildi");
    }
    void loadXmlfromPath(string pathFull)
    {
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(pathFull);

        //string baslik = xdoc.GetElementsByTagName("baslik")[0].InnerText;
        XmlNode xbaslik = xdoc.GetElementsByTagName("baslik")[0];
        string baslik = xbaslik.InnerText;

        XmlNode xwidth = xdoc.GetElementsByTagName("width")[0];
        string width = xbaslik.InnerText;

        XmlNode xheight = xdoc.GetElementsByTagName("height")[0];
        string height = xheight.InnerText;

        fileFolder.Yazdir = baslik;
        fileFolder.Yazdir = width;
        fileFolder.Yazdir = height;

    }
    
}
