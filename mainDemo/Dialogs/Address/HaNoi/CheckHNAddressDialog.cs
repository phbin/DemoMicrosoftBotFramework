using Microsoft.Bot.Builder.Dialogs;

namespace mainDemo.Dialogs.Address.HaNoi
{
    public class CheckHNAddressDialog:WaterfallDialog
    {
        public CheckHNAddressDialog(string dialogId, IEnumerable<WaterfallStep> steps = null) : base(dialogId, steps)
        {
            AddStep(async (stepContext, cancellationToken) =>
            {
                await stepContext.Context.SendActivityAsync("Các chi nhánh tại Hà Nội:  \n" +
                    "CN1: 103 Hồ Đắc Di - Đống Đa - Hà Nội  \n" +
                    "CN2: 41 ngõ 157, Pháo Đài Láng, Hà Nội"); return await stepContext.EndDialogAsync();
            });
        }

        public static string Id => "checkHNAddressDialog";
        public static CheckHNAddressDialog Instance { get; } = new CheckHNAddressDialog(Id);
    }
}
