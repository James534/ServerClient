using UnityEngine;
using System.Collections;
using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;

public class ListenClient : MonoBehaviour {

    Thread t;
    Client c;
    

    // Use this for initialization
    void Start () {
        c = new Client();

        t = new Thread(new ThreadStart(c.run));
        t.Start();
    }
	
	// Update is called once per frame
	void Update () {

    }
    void OnApplicationQuit()
    {
        c.stop();
        t.Join();
        Debug.Log("Stopped Thread");
    }

    public class Client
    {
        bool running;
        public void run()
        {
            running = true;
            try
            {
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("192.168.0.14");
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 1234);
                soc.Connect(remoteEP);
                soc.Send(System.Text.Encoding.ASCII.GetBytes("Output\n"));
                while (running)
                {
                    byte[] buffer = new byte[1024];
                    int iRx = soc.Receive(buffer);
                    char[] chars = new char[iRx];

                    System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                    int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
                    System.String recv = new System.String(chars);
                    //Console.WriteLine(recv);
                    Debug.Log(recv);
                }
                soc.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void stop()
        {
            running = false;
        }
    }
}
