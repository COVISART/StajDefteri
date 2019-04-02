using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class _thread1 : MonoBehaviour
{
    Thread thread1;
    void method1()
    {
        int i;
        for (i = 0; i < 1000; i++)
        {
           
            //Thread.Sleep(50);
        }
        Debug.Log(i + ". rakam= " + (i * 100));

    }

    void Start()
    {
        thread1 = new Thread(method1);
    }
    void printThread()
    {
        try
        {
            Debug.Log("------------------------------------------------------Hatali------------------------------------------------------");
            if (thread1.ThreadState == ThreadState.Unstarted)
            {
                
                thread1.Start();
                Debug.Log("UNSTARTED: "+thread1.ThreadState);
            }
            else if (thread1.ThreadState == ThreadState.Stopped)
            {
                
                thread1.Abort();
                thread1.Start();
                Debug.Log("STOPPED: " + thread1.ThreadState);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("THREAD STATUS: "+thread1.ThreadState+"\t\t"+ex.Message);
        }
       
        
    }
    void printThreadHatasiz()
    {
        try
        {
            Debug.Log("--------------------------------------------Hatasız------------------------------------------------------");
            if (thread1.ThreadState == ThreadState.Unstarted)
            {
                thread1 = new Thread(method1);
                thread1.Start();
                Debug.Log("UNSTARTED: " + thread1.ThreadState);
            }
            else if (thread1.ThreadState == ThreadState.Stopped)
            {
                Debug.Log("STOPPED: " + thread1.ThreadState);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("THREAD STATUS: " + thread1.ThreadState + "\t\t" + ex.Message);
        }


    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            printThread();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            printThreadHatasiz();
        }
    }
}
