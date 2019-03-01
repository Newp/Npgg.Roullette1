using System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;
using System.Collections.Generic;

namespace Roulette1.Client
{
    class Program
    {
        static void Log(Color color, string msg)
        {
            Console.WriteLine(msg);
        }
        static List<HitChecker> hitCheckers = HitChecker.MakeHitChecker();

        static string PickOne()
        {
            Random rnd = new Random();
            int cursor = rnd.Next(0, hitCheckers.Count);
            return hitCheckers[cursor].ToString();
        }

        static void Main(string[] args)
        {
            string url = "http://localhost:10080/roullete";

            NetworkClient client = new NetworkClient(url);


            client.Connect("test");

            while(true)
            {
                string msg = Console.ReadLine();

                string randomBetting = PickOne();
                client.Betting(randomBetting, 1);

                client.Send(msg);
            }
        }
    }
}
