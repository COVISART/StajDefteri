using System.IO;
using UnityEngine;

public class _json : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SupermanKahraman robot = new SupermanKahraman();
            robot.Adi = "Ozan Güven";
            robot.GizliKimlik = "Robot216";
            robot.Yasi = "26";
            robot.Boyu = "187";

            string filePath = Path.Combine(Application.dataPath, "save.txt");
            string jsonString = JsonUtility.ToJson(robot,true);

            if (jsonString=="{}"){
                Debug.LogError("Json çeviremedi hatası: "+jsonString);
            }
            else
            {
                File.WriteAllText(filePath, jsonString);
                Debug.Log("json dosyası oluşturuldu. "+filePath);
            }
            
        }
    }
}

//[Serializable]
public class SupermanKahraman
{
    //GET SET KULLANIRSAN ÇALIŞMAZ
    public string Adi;
    public string GizliKimlik;
    public string Yasi;
    public string BosDegerString;
    public int BosDegerInt;
    public string Boyu;
}
