using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace RaspberryTelegramBot
{
    public class TelegramBotClient
    {
        HttpClient client = new HttpClient();
        string startUrl = "http://api.telegram.org/bot1352703675:AAFI4rsxffY8AylyWrj5UwkNYZzeV4qzviA/"; //test

        double update_id = 0;
        public event Action<MessageUser> OnMessage;
        public async void Start()
        {
            while (true)
            {
                string url = $"{startUrl}getUpdates?offset={update_id}";

                var json = client.GetStringAsync(url).Result;

                var msgs = JsonObject.Parse(json).GetNamedArray("result");

                for (int i = 0; i < msgs.Count; i++)
                {
                    #region args

                    
                    string msgText = msgs[i].GetObject().GetNamedValue("message")
                                            .GetObject().GetNamedValue("text").GetString();

                    double msgChatId = msgs[i].GetObject().GetNamedValue("message")
                                            .GetObject().GetNamedValue("chat")
                                            .GetObject().GetNamedValue("id").GetNumber();

                    string msgFirstName = msgs[i].GetObject().GetNamedValue("message")
                                             .GetObject().GetNamedValue("chat")
                                             .GetObject().GetNamedValue("first_name").GetString();

                    update_id = msgs[i].GetObject().GetNamedValue("update_id").GetNumber() + 1;

                    #endregion

                    var args = new MessageUser()
                    {
                        ChatId = msgChatId,
                        MessageText = msgText,
                        FirstName = msgFirstName
                    };

                    OnMessage(args);

                   // Debug.WriteLine($"msgText = {msgText} msgChatId = {msgChatId}  update_id = {update_id}");
                }

                await Task.Delay(5000);
            }


        }

        public void SendTextMessage(double ChaId, string TextMsg)
        {
            string url = $"{startUrl}sendMessage?chat_id={ChaId}&text={TextMsg}";
            var r = client.GetStringAsync(url).Result;

        }
    }
}
