using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams;
using Microsoft.Bot.Connector.Teams.Models;
using Microsoft.Teams.Samples.HelloWorld.Web.Helper;
using ProactiveMessageTest.Helper;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            using (var connector = new ConnectorClient(new Uri(activity.ServiceUrl)))
            {
                if (activity.IsComposeExtensionQuery())
                {
                    var response = MessageExtension.HandleMessageExtensionQuery(connector, activity);
                    return response != null
                        ? Request.CreateResponse<ComposeExtensionResponse>(response)
                        : new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    if (activity.Type == ActivityTypes.Message)
                    {
                        await EchoBot.EchoMessage(connector, activity);
                    }
                    else
                    {
                        HandleSystemMessage(activity);
                    }
                    return new HttpResponseMessage(HttpStatusCode.Accepted);
                }
            }
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.InstallationUpdate)
            {
                // Handle add/remove from contact lists
                if (message.Action == "add")
                {
                    ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));
                    var data = message.GetChannelData<TeamsChannelData>();

                    // Save this so that message can be sent.
                    TempStorage.ServiceUrl = message.ServiceUrl;
                    TempStorage.ChannelId = data.Channel.Id;
                    Activity reply = message.CreateReply("We will posting a message when you ping on /postmessage endpoint.");
                    connector.Conversations.ReplyToActivity(reply);
                    
                }
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}
