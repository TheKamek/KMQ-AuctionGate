using AuctionGate.Resources.Views;

namespace AuctionGate;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("MarketPage", typeof(Resources.Views.MarketPage));
        Routing.RegisterRoute("FAQ", typeof(Resources.Views.FAQ));
        Routing.RegisterRoute("CreateAuctionPage", typeof(Resources.Views.CreateAuctionPage));
        Routing.RegisterRoute("AuctionSuccess", typeof(AuctionSuccessPage));
        Routing.RegisterRoute("ItemDetails", typeof(ItemDetails));
        Routing.RegisterRoute("ChillPage", typeof(ChillPage));
    }

    private async void OnClosedClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert(
            "Logout",
            "Are you sure you want to logout?",
            "Yes", "No");

        if (answer)
        {
            Application.Current?.Quit();
        }
    }
}