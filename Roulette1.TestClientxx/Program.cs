using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Drawing;
using System.Threading;

namespace TestClient
{

    class Program
    {
        static void Log(Color color , string msg)
        {
            Console.WriteLine(msg);
        }
        static void Main(string[] args)
        {
            HubConnection _connection = new HubConnectionBuilder()

                //.WithUrl("http://localhost:9267/chat")
                .WithUrl("https://localhost:44367/roullete")

                .Build();

            _connection.On<string, string>("broadcastMessage", (name, message)=>{
                Console.WriteLine("[{0}] {1}>>{2}", DateTime.Now, name, message);
            });

            Log(Color.Gray, "Starting connection...");
            try
            {
                _connection.StartAsync().Wait();
            }
            catch (Exception ex)
            {
                Log(Color.Red, ex.ToString());
                return;
            }

            Log(Color.Gray, "Connection established.");

            while(true)
            {
                if(_connection.State == HubConnectionState.Disconnected)
                {
                    Console.WriteLine("reconnecting..");
                    try
                    {
                        _connection.StartAsync().Wait();
                    }
                    catch (Exception ex)
                    {
                        Log(Color.Red, ex.ToString());
                    }
                    continue;
                }
                _connection.InvokeAsync("Send", new object[] { "client1", "check gogo" });
                Thread.Sleep(2000);
            }


            //HubConnection hubConn = new HubConnection("http://localhost:9267/");
            //IHubProxy proxy = null;
            //proxy = hubConn.CreateHubProxy("/chat");
            //proxy.On("broadcast", () =>
            //{
            //    Console.WriteLine("broadcasted");
            //});

            //var connection = hubConn.Start().ContinueWith(task =>
            //{
            //    if (task.IsFaulted)
            //    {
            //        Console.WriteLine("There was an error opening the connection:{0}",
            //                          task.Exception.GetBaseException());
            //    }
            //    else
            //    {
            //        Console.WriteLine("Connected");
            //    }

            //});

            //connection.Wait();



            //while(true)
            //{
            //    proxy.Invoke("Send", "test", "123");
            //    Thread.Sleep(2000);
            //}
        }
    }
}
