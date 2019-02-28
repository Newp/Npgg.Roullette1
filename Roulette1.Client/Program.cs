using System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;

namespace Roulette1.Client
{
    class Program
    {
        static void Log(Color color, string msg)
        {
            Console.WriteLine(msg);
        }
        static void Main(string[] args)
        {
            string url = "http://localhost:10080/roullete";

            NetworkClient client = new NetworkClient(url);

            client.Connect();

            while(true)
            {
                string msg = Console.ReadLine();

                client.Send(msg);
            }
        }
    }
}
