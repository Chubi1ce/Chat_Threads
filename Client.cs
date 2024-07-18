﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat_Threads;

internal class Client
{
    public static void StartClient(string nickName)
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        UdpClient client = new UdpClient();

        while (true)
        {
            Console.WriteLine("Input massage:");

            string? text = Console.ReadLine();
            if (String.IsNullOrEmpty(text))
            {
                continue;
            }

            Message msg = new Message(nickName, text, DateTime.Now);
            string js = msg.ConvertToJSON();
            byte[] bytes = Encoding.UTF8.GetBytes(js);
            client.Send(bytes, endPoint);

            byte[] buffer = client.Receive(ref endPoint);
            string str = Encoding.UTF8.GetString(buffer);
            Message? msgFromServer = Message.ConvertFromJSON(str);
            Console.WriteLine(msgFromServer);

             if (text.ToLower() == "exit")
            {
                break;
            }
            

        }
    }
}

