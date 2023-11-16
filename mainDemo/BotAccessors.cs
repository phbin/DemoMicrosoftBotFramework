using mainDemo.Bots;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder;

namespace mainDemo
{
    public class BotAccessors
    {
        public BotAccessors(ConversationState conversationState)
        {
            ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
        }

        public static string FlowerShopBotStateAccessorName { get; } = $"{nameof(BotAccessors)}.FlowerShopState";
        public IStatePropertyAccessor<FlowerShopState> FlowerShopStateStateAccessor { get; internal set; }

        public static string DialogStateAccessorName { get; } = $"{nameof(BotAccessors)}.DialogState";
        public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; internal set; }
        public ConversationState ConversationState { get; }
    }
}
