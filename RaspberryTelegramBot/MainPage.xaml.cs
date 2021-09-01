using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace RaspberryTelegramBot
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        TelegramBotClient tBot = new TelegramBotClient();
        long msgCount = 0;
        public MainPage()
        {
            this.InitializeComponent();
            tBot.OnMessage += TBot_OnMessage;
            tBot.Start();
        }

        private void TBot_OnMessage(MessageUser obj)
        {
            tb.Text = $"Прислано: {++msgCount}";
            var text = $"От {obj.FirstName} пришло: {obj.MessageText}";
            lMsg.Items.Add(text);

            tBot.SendTextMessage(obj.ChatId, text);
        }
    }
}
