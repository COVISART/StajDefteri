using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

public class _task : MonoBehaviour
{
    
    void Start()
    {
        Main();
    }



    public static void Main()
    {
        ShowThreadInfo("Application");

        var t = Task.Run(() => ShowThreadInfo("Task"));
        t.Wait();
    }

    static void ShowThreadInfo(string s)
    {
        Debug.Log(s+" Thread ID: " + Thread.CurrentThread.ManagedThreadId);
    }
}
