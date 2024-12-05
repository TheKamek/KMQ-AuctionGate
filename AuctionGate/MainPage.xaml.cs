namespace AuctionGate
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartClick(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("Market");
        }

    }

}
