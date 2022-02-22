using System;
using System.Linq;
using System.Threading.Tasks;
using TL;

namespace TelegramApi.Client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.Title="The program will display updates received for the logged-in user.";

            var client = new WTelegram.Client((string what) =>
            {
                switch (what)
                {
                    case "api_id": return "30502";
                    case "api_hash": return "c0e64b710451472c5c36f17686ba658b";
                    case "phone_number": return "+998977111770";
                    case "verification_code": Console.Write("Code: "); return Console.ReadLine();
                    case "first_name": return "Elyor";
                    case "last_name": return "Latipov";
                    case "password": return "Latipov_052989";
                    default: return null;
                }
            });
            var me = await client.LoginUserIfNeeded();
            Console.WriteLine($"We are logged-in as {me.username ?? me.first_name + " " + me.last_name} (id {me.id})");

            var channel = new InputChannel
            {
                access_hash = me.access_hash,
                channel_id = 1
            };
            var messages = await client.Channels_GetMessages(channel, null);
            var participants = await client.Channels_GetAllParticipants(channel, includeKickBan: true);
            var info =await client.Channels_GetFullChannel(channel);
            
            var model = new StatsModel
            {
                Chats = info.chats.Count,
                Users = info.users.Count,
                Messages = messages.Messages.ToDictionary(a=>a.ID,a=>a.ToString()),
                Participants=participants.chats.ToDictionary(a=>a.Key,a=>a.Value.Title)
            };

            Console.WriteLine("Press any key to terminate");
            Console.ReadKey();
        }

    }
}
