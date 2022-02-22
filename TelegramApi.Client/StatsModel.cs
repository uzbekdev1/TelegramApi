using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramApi.Client
{
    internal class StatsModel
    {

        public int Chats { get; set; }

        public int Users { get; set; }

        public IDictionary<int, string> Messages { get; set; }

        public IDictionary<long, string> Participants { get; set; }

    }
}
