using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace AuctionGate.Resources.Views;

public partial class MarketPage : ContentPage
{
    public ObservableCollection<AuctionItem> Auctions { get; private set; }

    public MarketPage()
    {
        InitializeComponent();
        InitializeAuctions();
        BindingContext = this;
    }

    private void InitializeAuctions()
    {
        Auctions = new ObservableCollection<AuctionItem>
        {
            new AuctionItem
            {
                Name = "Swaggy Rolex",
                CurrentBid = 299,
                TimeLeft = "2h 15m",
                Description = "Elegant vintage timepiece in excellent condition",
                ImageSource = "item1.jpg"
            },
            new AuctionItem
            {
                Name = "Art Deco Lamp",
                CurrentBid = 150,
                TimeLeft = "4h 30m",
                Description = "Beautiful art deco lamp from the 1920s",
                ImageSource = "item2.png"
            },
            new AuctionItem
            {
                Name = "Gaming Console",
                CurrentBid = 450,
                TimeLeft = "1h 45m",
                Description = "Latest generation gaming console, barely used",
                ImageSource = "item3.png"
            },
        };
    }
    private async void CreateItemPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CreateAuctionPage");
    }
}

public class AuctionItem
{
    public string Name { get; set; }
    public decimal CurrentBid { get; set; }
    public string TimeLeft { get; set; }
    public string Description { get; set; }
    public string ImageSource { get; set; }
}