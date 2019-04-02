using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class _uploadFileSample : MonoBehaviour
{
    //http://127.0.0.1/uploads/uploadPut.php?id=123&name=blahblah
    [SerializeField]
    public string uploadServer1 = "http://127.0.0.1/uploads/uploadPut.php";
    public string path1 = "C:/Users/ykegi/OneDrive/Desktop/balon.mp3";
    [SerializeField]
    public string uploadServer2 = "http://127.0.0.1/uploads/uploadPut.php";
    public string path2 = "C:/Users/ykegi/OneDrive/Desktop/kedi.png";



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)){
            StartCoroutine(Upload1(uploadServer1, path1));
        }
        else if (Input.GetKeyDown(KeyCode.S)){
            StartCoroutine(Upload2(uploadServer2, path2));
        }
        else if (Input.GetKeyDown(KeyCode.D)){
            StartCoroutine(Upload3(("C:/Users/ykegi/OneDrive/Desktop/kedi.png")));
        }
        else if (Input.GetKeyDown(KeyCode.F)){}
        else if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(Upload5("http://127.0.0.1/uploads/uploadphp.php", "C:/Users/ykegi/OneDrive/Desktop/kedi.png"));
        }
    }
    IEnumerator Upload1(string urlServer,string file_path)
    {
        using (var www = new UnityWebRequest(urlServer, UnityWebRequest.kHttpVerbPUT))
        {
            www.uploadHandler = new UploadHandlerFile(file_path);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) Debug.LogError(www.error);
            else
            {
                Debug.Log("Dosya gönderildi");
            }
        }
    }

    
    IEnumerator Upload2(string urlServer,string data)
    {
        //string server = "http://127.0.0.1/upload.php";
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(data);
        UnityWebRequest www = UnityWebRequest.Put(urlServer, myData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else { Debug.Log("Upload complete! " + www.downloadHandler.text); }
    }


    IEnumerator Upload3(string data)
    {
        WWWForm form = new WWWForm();
        form.AddField("uploaded_file", data);
        
        UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/uploads/uploadphp.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) { Debug.Log(www.error); }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
    void Upload4(string bytelanmis,string content)
    {
        byte[] payload = new byte[1024];
        // ... fill payload with data ...

        UnityWebRequest www = new UnityWebRequest("http://127.0.0.1/uploads/uploadPut.php");
        UploadHandler uploader = new UploadHandlerRaw(payload);

        // Sends header: "Content-Type: custom/content-type";
        uploader.contentType = "custom/content-type";

        www.uploadHandler = uploader;

    }

    // UPLOAD IENUMERATOR
    IEnumerator Upload5(string urlServer,string path)
    {
        WWWForm form = new WWWForm();                                 // Form nesnesi oluşturur.
        form.AddField("uploaded_file", path);                         // Form alanı

        UnityWebRequest www = UnityWebRequest.Post(urlServer, form);  // İndirme nesnesi
        yield return www.SendWebRequest();                            // İndirme işlemi tamamlanana kadar bekler

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error wwwing: " + www.error);
        }
        else { Debug.Log("Upload işlemi başarı ile tamamlandı: " + www.downloadHandler.text); }
    }
}
