using System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Roulette1.Client
{
    class Program
    {
        static List<HitChecker> hitCheckers = HitChecker.MakeHitChecker();

        static string PickOne()
        {
            Random rnd = new Random();
            int cursor = rnd.Next(0, hitCheckers.Count);
            return hitCheckers[cursor].ToString();
        }
        static string url = "http://localhost:10080/roullete";
        static void Main(string[] args)
        {
            List<NetworkClient> clientList = new List<NetworkClient>();
            int userCount = 500;

            Console.WriteLine("anykey to start");
            Console.ReadKey(false);
            for(int i =0;i<userCount; i++)
            {
                NetworkClient client = new NetworkClient(url);
                clientList.Add(client);
            }
            var master = new MonitorNetworkClient(url);
            clientList.Add(master); //모니터링할 대장클라이언트

            Parallel.ForEach(clientList, client =>
            {
                client.Connect();
                Console.Write('.');
            });
            Console.Clear();

            Stopwatch framewatch = new Stopwatch();
            framewatch.Start();
            Stopwatch workerWatch = new Stopwatch();
            workerWatch.Start();

            while (master.gameState == null)
            {
                Thread.Sleep(200);
            }
            while (true)
            {
                

                if (workerWatch.ElapsedMilliseconds > 3000)
                {
                    foreach (var client in clientList)
                    {
                        if (client.Connected == false)
                            client.Connect();

                        if (client.User == null)
                        {
                            Console.Write('-');
                            continue;
                        }


                        if (framewatch.ElapsedMilliseconds > 1000)
                        {
                            int frame = 0;
                            long totalMoney = 0;
                            foreach (var client2 in clientList)
                            {
                                frame += client2.Frame;
                                client2.Frame = 0;
                                if (client2.User != null)
                                    totalMoney += (long)client2.User.Money;

                                //Console.ForegroundColor = ConsoleColor.Black;
                            }

                            Console.Clear();
                            Console.WriteLine("frame : {0}, total money : {1} >> {2}", frame, totalMoney, framewatch.ElapsedMilliseconds);
                            framewatch.Restart();
                        }

                        string randomBetting = PickOne();
                        client.Betting(randomBetting, 1);
                    }
                    workerWatch.Restart();
                }
          

                
                
            }
        }
        
    }
}
