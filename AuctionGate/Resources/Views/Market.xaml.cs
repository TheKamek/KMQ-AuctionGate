using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AuctionGate.Resources.Views
{
    public partial class MarketPage : ContentPage
    {
        public ObservableCollection<AuctionItem> Auctions { get; private set; }
        public ICommand PlaceBidCommand { get; private set; }

        public MarketPage()
        {
            InitializeComponent();
            InitializeAuctions();
            InitializeCommands();
            BindingContext = this;
        }

        private void InitializeCommands()
        {
            PlaceBidCommand = new Command<AuctionItem>(async (item) =>
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "Item", item }
                };
                await Shell.Current.GoToAsync("ItemDetails", navigationParameter);
            });
        }

        private void InitializeAuctions()
        {
            Auctions = new ObservableCollection<AuctionItem>
            {
                new AuctionItem
                {
                    Name = "Swaggy Shirt",
                    CurrentBid = 299,
                    TimeLeft = "2h 15m",
                    Description = "Elegant vintage timepiece in excellent condition",
                    ImageSource = "item1.jpg"
                },
                new AuctionItem
                {
                    Name = "Art Deco Mug",
                    CurrentBid = 150,
                    TimeLeft = "4h 30m",
                    Description = "Beautiful art deco mug from the 1920s",
                    ImageSource = "item2.png"
                },
                new AuctionItem
                {
                    Name = "Medication Plan B",
                    CurrentBid = 450,
                    TimeLeft = "1h 45m",
                    Description = "Latest generation, barely used",
                    ImageSource = "item3.png"
                },
                new AuctionItem
                {
                    Name = "Neues Produkt",
                    CurrentBid = 450,
                    TimeLeft = "1h 45m",
                    Description = "Latest generation, barely used",
                    ImageSource = "item3.png"
                },
            };
        }

        private async void CreateItemPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///CreateAuctionPage");
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
}