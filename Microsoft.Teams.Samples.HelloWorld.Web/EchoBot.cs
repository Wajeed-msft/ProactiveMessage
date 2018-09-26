﻿using System.Threading.Tasks;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;

namespace Microsoft.Teams.Samples.HelloWorld.Web
{
    public class EchoBot
    {
        public static async Task EchoMessage(ConnectorClient connector, Activity activity)
        {
            var reply = activity.CreateReply("You said: " + activity.GetTextWithoutMentions());
            var message = activity.GetTextWithoutMentions();
            if (message == "search")
            {
                await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
                // Read and write..
            }
            else
            {
                await connector.Conversations.ReplyToActivityWithRetriesAsync(reply);
            }
            
        }
    }
}
