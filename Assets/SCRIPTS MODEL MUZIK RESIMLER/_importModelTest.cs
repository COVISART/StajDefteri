using System.Collections;
using UnityEngine;

public class _importModelTest : MonoBehaviour
{
    //TEST İÇİN MODEL SINIFI
    public _importModel model;

    void Start(){
        model = GetComponent<_importModel>();
        if (model == null) { Debug.LogError("Model importer script yok"); }
    }

    void Update()
    {
        //MODEL YOKSA MODELİ SERVERDAN İNDİRİR SONRA OYUNA YÜKLER
        //MODEL VARSA OYUNA YÜKLER
        if (Input.GetKeyDown(KeyCode.Space)){
            StartCoroutine(model.IDownloadImportFbx("http://localhost/myphp/Model/MultiMatCube.fbx", "MultiMatCube.fbx", model.fileFolder.pathMODELRESOURCES));
        }        
    }
}
