using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat_Threads;

internal class Server
{
    public static void StartServer()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
        UdpClient client = new UdpClient(12345);
        Console.WriteLine("Waiting incomming messages...");

        while (true)
        { 
            // ConsoleKeyInfo keyInfo = Console.ReadKey();
            // if (keyInfo.Key == ConsoleKey.Escape)
            // {
            //     break;
            // }

            try
            {
                byte[] buffer = client.Receive(ref endPoint);
                string str = Encoding.UTF8.GetString(buffer);
                Thread thread = new Thread(() =>
                {
                    Message? msg = Message.ConvertFromJSON(str);
                    if (msg != null)
                    {
                        Console.WriteLine(msg.ToString());
                        Message msgToClient = new Message("server", "message delivered", DateTime.Now);
                        string js = msgToClient.ConvertToJSON();
                        byte[] bytes = Encoding.UTF8.GetBytes(js);
                        client.Send(bytes, endPoint);
                    }
                    else
                    {
                        Console.WriteLine("Huston, we have a problem!!!");
                    }
                });
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
