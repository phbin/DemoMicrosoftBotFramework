using mainDemo.Dialogs.Flowers;
using mainDemo.Dialogs.Address;
using mainDemo.Dialogs;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using mainDemo.Dialogs.Address.HaNoi;
using mainDemo.Dialogs.Address.HoChiMinh;

namespace mainDemo.Bots
{
    public class FlowerShopBot:IBot
    {
        private readonly DialogSet dialogs;

        public FlowerShopBot(BotAccessors botAccessors)
        {
            var dialogState = botAccessors.DialogStateAccessor;
            // compose dialogs
            dialogs = new DialogSet(dialogState);
            dialogs.Add(MainDialog.Instance);
            dialogs.Add(FlowerDetailsDialog.Instance);
            dialogs.Add(CheckAddressDialog.Instance);
            dialogs.Add(CheckHNAddressDialog.Instance);
            dialogs.Add(CheckHCMAddressDialog.Instance);
            dialogs.Add(new ChoicePrompt("choicePrompt"));
            dialogs.Add(new TextPrompt("textPrompt"));
            dialogs.Add(new NumberPrompt<int>("numberPrompt"));
            BotAccessors = botAccessors;
        }

        public BotAccessors BotAccessors { get; }


        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                // initialize state if necessary
                var state = await BotAccessors.FlowerShopStateStateAccessor.GetAsync(turnContext, () => new FlowerShopState(), cancellationToken);

                turnContext.TurnState.Add("BotAccessors", BotAccessors);

                var dialogCtx = await dialogs.CreateContextAsync(turnContext, cancellationToken);

                if (dialogCtx.ActiveDialog == null)
                {
                    await dialogCtx.BeginDialogAsync(MainDialog.Id, cancellationToken: cancellationToken);
                }
                else
                {
                    await dialogCtx.ContinueDialogAsync(cancellationToken);
                }

                await BotAccessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            }
        }
    }
}
