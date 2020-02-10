using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRCorePracticeServer.Hubs
{
    public class MySignalRHub : Hub
    {
        /// <summary>
        /// クライアントから任意の個数のデータを受け取ることができる。今回は1個。
        /// クライアントはこのメソッドを呼び出す。
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendToAllAsync(string message)
        {
            //Client側のReceiveMessageメソッドを呼ぶ
            //今回は受信したメッセージにタイムスタンプをつけてべてのクライアントに送信するだけ。
            await Clients.All.SendAsync("ReceiveMessage", AppendTimeStamp(message));
        }

        private string AppendTimeStamp(string message)
        {
            return $"{message} : {DateTimeOffset.Now.ToString("yyyy/MM/dd/HH:mm:ss.fff")}";
        }
    }
}