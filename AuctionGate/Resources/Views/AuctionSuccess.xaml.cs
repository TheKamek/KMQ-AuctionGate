using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace AuctionGate.Resources.Views;

public partial class AuctionSuccessPage : ContentPage
{
    private bool isSideNavOpen = false;

    public AuctionSuccessPage(CreateAuctionPage.AuctionCreationData auctionCreationData)
    {
        InitializeComponent();
        moneyLabel.Text = (auctionCreationData.StartPrice.ToString() + "$");
        durationLabel.Text = (auctionCreationData.Duration.ToString() + " Days");
        titleLabel.Text = (auctionCreationData.Title);
    }

    private async void OnMenuClicked(object sender, EventArgs e)
    {
        var translation = isSideNavOpen ? -250 : 0;
        await SideNav.TranslateTo(translation, 0, 250, Easing.CubicOut);
        SideNav.InputTransparent = isSideNavOpen;
        isSideNavOpen = !isSideNavOpen;
    }

    private async void OnViewAuctionClicked(object sender, EventArgs e)
    {
        // Navigate to auction details page
    }

    private async void OnCreateAnotherClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateAuctionPage());
    }
}