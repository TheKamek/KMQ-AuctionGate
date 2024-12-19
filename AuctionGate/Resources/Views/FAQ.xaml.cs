using Microsoft.Maui.Controls;

namespace AuctionGate.Resources.Views;

public partial class FAQ : ContentPage
{
    private readonly Dictionary<string, (Label answer, ImageButton button)> faqControls;

    public FAQ()
    {
        InitializeComponent();
        faqControls = new Dictionary<string, (Label answer, ImageButton button)>
        {
            { "1", (answer1, expandButton1) },
            { "2", (answer2, expandButton2) },
            { "3", (answer3, expandButton3) }
        };
    }

    private void OnExpandClicked(object sender, EventArgs e)
    {
        var button = sender as ImageButton;
        if (button == null) return;

        var faqNumber = button.AutomationId?.Replace("expandButton", "")
                       ?? button.StyleId?.Replace("expandButton", "");

        if (faqNumber != null && faqControls.TryGetValue(faqNumber, out var controls))
        {
            controls.answer.IsVisible = !controls.answer.IsVisible;
            button.Rotation = controls.answer.IsVisible ? 180 : 0;
        }
    }

    private async void OnContactSupportClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert(
            "Contact Support",
            "Would you like to send an email to our support team?",
            "Yes", "No");

        if (answer)
        {
            try
            {
                var email = new EmailMessage
                {
                    Subject = "Support Request",
                    To = new List<string> { "support@auctiongate.com" }
                };
                await Email.ComposeAsync(email);
            }
            catch (Exception)
            {
                await DisplayAlert(
                    "Error",
                    "Unable to open email client. Please contact support@auctiongate.com directly.",
                    "OK");
            }
        }
    }
}