using AdaptiveCards;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ProactiveMessageTest.Helper
{
    /// <summary>
    ///  Helper class which posts to the saved channel every 20 seconds.
    /// </summary>
    public static class ReminderHelper
    {
        public static void PostMessageToChannel(string serviceUrl, string channelId)
        {
            try
            {
                var connector = new ConnectorClient(new Uri(serviceUrl));
                var channelData = new Dictionary<string, string>();
                channelData["teamsChannelId"] = channelId;

                // Create a new reply.
                IMessageActivity newMessage = Activity.CreateMessageActivity();
                newMessage.Type = ActivityTypes.Message;

                // var card = GetHeroCard(); 
                var card = GetAdaptiveCard();
                newMessage.Attachments.Add(card);

                ConversationParameters conversationParams = new ConversationParameters(
                    isGroup: true,
                    bot: null,
                    members: null,
                    topicName: "Test Conversation",
                    activity: (Activity)newMessage,
                    channelData: channelData);
                MicrosoftAppCredentials.TrustServiceUrl(serviceUrl, DateTime.MaxValue);
                connector.Conversations.CreateConversationAsync(conversationParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static Attachment GetAdaptiveCard()
        {
            var card2 = new AdaptiveCard()
            {

                Body = new List<AdaptiveElement>()
                {
                    new AdaptiveTextBlock(){Text="Adaptive Card",Weight=AdaptiveTextWeight.Bolder,Size=AdaptiveTextSize.ExtraLarge},new AdaptiveTextBlock(){Text="Your bots — wherever your users are talking",Weight=AdaptiveTextWeight.Bolder,Size=AdaptiveTextSize.Small},
                    new AdaptiveImage(){Size=AdaptiveImageSize.Auto,Url=new System.Uri("https://pbs.twimg.com/profile_images/3647943215/d7f12830b3c17a5a9e4afcc370e3a37e_400x400.jpeg"), HorizontalAlignment=AdaptiveHorizontalAlignment.Left},
                    new AdaptiveTextBlock(){Text="Build and connect intelligent bots to interact with your users naturally wherever they are, from text/sms to Skype, Slack, Office 365 mail and other popular services.",HorizontalAlignment=AdaptiveHorizontalAlignment.Left, MaxLines=10, Wrap=true }
                },

                Actions = new List<AdaptiveAction>()
                {
                    new AdaptiveSubmitAction()
                    {
                        Title="Test Submit Action"
                    },
                    new AdaptiveShowCardAction()
                    {
                        Title="Test Show Card Action",
                         Card=new AdaptiveCard()
                       {
                          Body=new List<AdaptiveElement>()
                          {
                              new AdaptiveTextBlock(){Text="Show Card", Weight=AdaptiveTextWeight.Bolder,Size=AdaptiveTextSize.Medium},
                              new AdaptiveDateInput(){}
                          }
                       }
                    },
                    new AdaptiveOpenUrlAction()
                    {
                        Title="Test Open URL Action",
                        Url=new System.Uri("http://adaptivecards.io")
                    }
                },
            };
            return new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card2,

            };
        }
    }
}