using System;
using System.Net;
using System.Net.Sockets;

namespace CSharpSocket
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try {
                Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("192.168.0.14");
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 1234);
                soc.Connect(remoteEP);

                //Start sending stuf..
                byte[] byData = System.Text.Encoding.ASCII.GetBytes("Output\n");
                soc.Send(byData);
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int iRx = soc.Receive(buffer);
                    char[] chars = new char[iRx];

                    System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                    int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
                    System.String recv = new System.String(chars);
                    Console.WriteLine(recv);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}