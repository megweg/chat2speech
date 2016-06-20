using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebSocket4Net;

namespace BroChatCatch.WebSocketProcessor
{
    public class BroChatListenner
    {
        public delegate void AllData(BroChatMessage msg);
        public event AllData OnAll;

        public delegate void OnlyMessage(BCMessage msg);
        public event OnlyMessage OnMessage;

        public delegate void OnlyStatistic(BCStatistic msg);
        public event OnlyStatistic OnStatistic;

        public BroChatListenner(int port = 15619)
        {
            ws = new WebSocket($"ws://localhost:{port}/", "", WebSocketVersion.Rfc6455);

            ws.MessageReceived += WsOnMessageReceived;

            ws.Open();
        }

        private void WsOnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            BroChatMessage bcm = JsonConvert.DeserializeObject<BroChatMessage>(e.Message);

            OnAll?.Invoke(bcm);

            if(bcm.Type== "statistic") OnStatistic?.Invoke(bcm.Statistic);
            else OnMessage?.Invoke(bcm.Message);
        }

        private WebSocket ws;
    }
}
