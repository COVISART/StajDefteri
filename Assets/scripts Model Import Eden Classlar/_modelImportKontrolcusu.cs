using System.Collections;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class _modelImportKontrolcusu : MonoBehaviour
{
    //TEST İÇİN MODEL SINIFI
    public _modelImport model;
    public _contentKontrolcusu menuKontroller;
    void Start(){
        model = GetComponent<_modelImport>();
        if (model == null) { Debug.LogError("Model importer script yok"); }

        if (menuKontroller == null) menuKontroller = GetComponent<_contentKontrolcusu>();
        if (menuKontroller == null) { Debug.LogError("Menu Kontroller script yok"); }
    }

    void Update()
    {
        //MODEL YOKSA MODELİ SERVERDAN İNDİRİR SONRA OYUNA YÜKLER
        //MODEL VARSA OYUNA YÜKLER
        if (Input.GetKeyDown(KeyCode.Space)){
            //StartCoroutine(model.IDownloadImportFbx("http://localhost/myphp/Model/MultiMatCube.fbx", "MultiMatCube.fbx", model.fileFolder.pathMODELRESOURCES));
            //StartCoroutine(model.IDownloadImportFbx("http://localhost/Model/Wolf.fbx", "Wolf.fbx", model.fileFolder.pathMODELRESOURCES));

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            
            Debug.Log("Menuye wolf yüklendi");
            StartCoroutine(model.IDownloadImportFbxMenu("http://localhost/Model/Wolf.fbx", "Wolf.fbx", model.fileFolder.pathMODELRESOURCES,menuKontroller));
        }
    }
    
    
}
