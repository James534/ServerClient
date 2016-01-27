using System;
using System.Net;
using System.Net.Sockets;

namespace CSharpSocket
{
    class MainClass
    {
        static Socket soc;
        static void init()
        {
            try
            {
                soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse("192.168.0.14");
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 1234);
                soc.Connect(remoteEP);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Main(string[] args)
        {
            init();
            while (true) {
                string s = Console.ReadLine() + "\n";
                //Start sending stuf..
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(s);
                soc.Send(byData);

                if (s.Equals("closeServer\n"))
                    break;

                byte[] buffer = new byte[1024];
                int iRx = soc.Receive(buffer);
                char[] chars = new char[iRx];

                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
                System.String recv = new System.String(chars);
                Console.WriteLine(recv);
            }
            soc.Close();
        }
    }
}