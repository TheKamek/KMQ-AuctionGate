using AuctionGate.Resources.Views;

namespace AuctionGate;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("Market", typeof(MarketPage));
        Routing.RegisterRoute("CreateAuction", typeof(CreateAuctionPage));
        Routing.RegisterRoute("AuctionSuccess", typeof(AuctionSuccessPage));
    }
}