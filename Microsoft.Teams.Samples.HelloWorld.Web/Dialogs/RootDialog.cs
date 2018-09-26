using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using System.Net.Http;
using Microsoft.Bot.Connector.Teams.Models;
using AdaptiveCards;
using Microsoft.Bot.Connector.Teams;
using ProactiveMessageTest.Helper;
using System.Threading;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = (Activity)await argument;
            var messageText = message.GetTextWithoutMentions();

            ConnectorClient connector = new ConnectorClient(new Uri(message.ServiceUrl));
            Activity reply = message.CreateReply();
            
            // Create first card
            reply.Attachments.Add(ReminderHelper.GetAdaptiveCard());
            var msgToUpdate = await connector.Conversations.ReplyToActivityAsync(reply);

            Thread.Sleep(5000);

            Activity updatedReply =  message.CreateReply();
            // Put the updated card
            updatedReply.Attachments.Add(ReminderHelper.GetUpdatedCard());
            await connector.Conversations.UpdateActivityAsync(reply.Conversation.Id, msgToUpdate.Id, updatedReply);

            context.Wait(MessageReceivedAsync);
        }
    }
}