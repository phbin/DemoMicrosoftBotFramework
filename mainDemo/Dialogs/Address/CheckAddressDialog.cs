using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using mainDemo.Dialogs.Address.HaNoi;
using mainDemo.Dialogs.Address.HoChiMinh;

namespace mainDemo.Dialogs.Address
{
    public class CheckAddressDialog:WaterfallDialog
    {
        public CheckAddressDialog(string dialogId, IEnumerable<WaterfallStep> steps = null) : base(dialogId, steps)
        {
            AddStep(async (stepContext, cancellationToken) =>
            {
                return await stepContext.PromptAsync("choicePrompt",
                    new PromptOptions
                    {
                        Prompt = stepContext.Context.Activity.CreateReply($"Chúng tôi có các chi nhánh khác nhau tại Hồ Chí Minh và Hà Nội, bạn muốn xem địa chỉ ở đâu?"),
                        Choices = new[] { new Choice { Value = "Hà Nội" }, new Choice { Value = "Hồ Chí Minh" } }.ToList()
                    });
            });

            AddStep(async (stepContext, cancellationToken) =>
            {
                var response = stepContext.Result as FoundChoice;

                if (response.Value == "Hà Nội")
                {
                    return await stepContext.BeginDialogAsync(CheckHNAddressDialog.Id);
                }

                if (response.Value == "Hồ Chí Minh")
                {
                    return await stepContext.BeginDialogAsync(CheckHCMAddressDialog.Id);
                }

                return await stepContext.NextAsync();
            });
        }

        public static string Id => "checkAddressDialog";
        public static CheckAddressDialog Instance { get; } = new CheckAddressDialog(Id);
    }
}
