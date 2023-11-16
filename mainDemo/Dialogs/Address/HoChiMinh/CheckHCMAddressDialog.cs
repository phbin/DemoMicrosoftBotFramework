using Microsoft.Bot.Builder.Dialogs;

namespace mainDemo.Dialogs.Address.HoChiMinh
{
    public class CheckHCMAddressDialog:WaterfallDialog
    {
        public CheckHCMAddressDialog(string dialogId, IEnumerable<WaterfallStep> steps = null) : base(dialogId, steps)
        {
            AddStep(async (stepContext, cancellationToken) =>
            {
                await stepContext.Context.SendActivityAsync("Các chi nhánh tại Hồ Chính Minh:  \n" +
                    "CN1: 7/60 Thành Thái, Quận 10, HCM  \n" +
                    "CN2: 46-48 Cô Giang, W2, District Phú Nhuận  \n" +
                    "CN3: 483/4 Lê Văn Sỹ - Phường 12 - Q3 ");
                return await stepContext.EndDialogAsync();
            });
        }

        public static string Id => "checkHCMAddressDialog";
        public static CheckHCMAddressDialog Instance { get; } = new CheckHCMAddressDialog(Id);
    }
}
