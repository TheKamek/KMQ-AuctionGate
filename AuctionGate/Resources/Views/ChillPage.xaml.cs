namespace AuctionGate.Resources.Views;

public partial class ChillPage : ContentPage
{
	public ChillPage()
	{
		InitializeComponent();
	}

    private async void OnStartClick(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MarketPage");
    }
}