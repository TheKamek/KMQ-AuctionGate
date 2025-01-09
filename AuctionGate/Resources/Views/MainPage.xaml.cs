namespace AuctionGate
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartClick(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MarketPage");
        }

    }

}
