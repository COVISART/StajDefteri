using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class LoginCore : MonoBehaviour
{
    /*public string serverAdress = "127.0.0.1";
    public int serverPort = 8080;

    public TcpClient _client;
    public NetworkStream _stream;
    public Thread _thread;

    private byte[] buffer = new byte[1024];
    private string _receivedMsg = "";
    private bool isConnected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetupConnection()
    {
        try
        {
            _thread = new Thread(ReceiveData);
            _thread.IsBackground = true;
            _client = new TcpClient(serverAdress, serverPort);
            _stream = _client.GetStream();
            _thread.Start();
            isConnected = true;
        }
        catch (System.Exception e)
        {
            CloseConnection();
            Debug.Log(e);
        }
        

    }

    private void ReceiveData()
    {
        if (!isConnected){ return;}
        int isNumberOfBytesRead = 0;
        while (isConnected && _stream.CanRead)
        {

        }
    }

    private void CloseConnection()
    {
        throw new NotImplementedException();
    }*/
}
