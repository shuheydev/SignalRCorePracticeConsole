using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalRCorePracticeClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //HubConnectionオブジェクトを使ってサーバーのSignalRハブと通信する
            HubConnection connection;

            //SignalRハブとの通信を担うオブジェクトを生成
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44304/mysignalrhub")
                .WithAutomaticReconnect()
                .Build();

            //クライアント側の受信時の処理を行うメソッドを登録する。
            //文字列で指定した名前でサーバー側から呼び出される。
            connection.On<string>("ReceiveMessage", ReceiveMessageClient);

            try
            {
                //サーバー側のSignalRハブと接続
                await connection.StartAsync();
                Console.WriteLine("接続できました");
            }
            catch (Exception ex)
            {
                Console.WriteLine("接続できませんでした");
            }

            while (true)
            {
                Console.Write("メッセージを入力: ");

                string input = Console.ReadLine();

                try
                {
                    //メッセージ送信
                    await connection.InvokeAsync<string>("SendToAllAsync", input);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("メッセージの送信に失敗しました");
                }

                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// メッセージ受信時に実行される
        /// </summary>
        /// <param name="messageFromServer"></param>
        private static void ReceiveMessageClient(string messageFromServer)
        {
            Console.WriteLine($"サーバーから{DateTimeOffset.Now.ToString("yyyy/MM/dd/HH:mm:ss.fff")}に受信:\n {messageFromServer}");
        }
    }
}