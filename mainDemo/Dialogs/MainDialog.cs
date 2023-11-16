using mainDemo.Dialogs.Address;
using mainDemo.Dialogs.Flowers;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;

namespace mainDemo.Dialogs
{
    public class MainDialog : WaterfallDialog
    {
        public MainDialog(string dialogId, IEnumerable<WaterfallStep> steps = null) : base(dialogId, steps)
        {
            AddStep(async (stepContext, cancellationToken) =>
            {
                return await stepContext.PromptAsync("choicePrompt",
                    new PromptOptions
                    {
                        Prompt = stepContext.Context.Activity.CreateReply("FlowerShop xin chào~ Bạn muốn xem thông tin địa chỉ của shop hay cần tư vấn?"),
                        Choices = new[] { new Choice { Value = "Địa chỉ" }, new Choice { Value = "Tư vấn" } }.ToList()
                    });
            });
            AddStep(async (stepContext, cancellationToken) =>
            {
                var response = (stepContext.Result as FoundChoice)?.Value;

                if (response == "Địa chỉ")
                {
                    return await stepContext.BeginDialogAsync(CheckAddressDialog.Id);
                }

                if (response == "Tư vấn")
                {
                    return await stepContext.BeginDialogAsync(FlowerDetailsDialog.Id);
                }

                return await stepContext.NextAsync();
            });

            AddStep(async (stepContext, cancellationToken) => { return await stepContext.ReplaceDialogAsync(Id); });
        }


        public static string Id => "mainDialog";

        public static MainDialog Instance { get; } = new MainDialog(Id);
    }
}