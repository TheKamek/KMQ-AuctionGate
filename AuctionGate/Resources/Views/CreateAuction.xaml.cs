using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace AuctionGate.Resources.Views;

public partial class CreateAuctionPage : ContentPage
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> Categories { get; } = new()
    {
        "Electronics",
        "Collectibles",
        "Fashion",
        "Home & Garden",
        "Vehicles"
    };
    public string SelectedCategory { get; set; }
    public string StartPrice { get; set; }
    public string ReservePrice { get; set; }
    public List<string> Durations { get; } = new()
    {
        "3 Days",
        "5 Days",
        "7 Days",
        "10 Days"
    };
    public string SelectedDuration { get; set; }
    public string MinimumBidIncrement { get; set; }
    public ObservableCollection<string> UploadedImages { get; } = new();
    public bool BuyNowOption { get; set; }
    public bool FeaturedListing { get; set; }
    public bool PrivateAuction { get; set; }
    public bool InternationalShipping { get; set; }

    public CreateAuctionPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void OpenMenu_Clicked(object sender, EventArgs e)
    {
        // Implement menu functionality
    }

    private async void OnUploadImages(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickMultipleAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select Images"
            });

            if (result != null)
            {
                foreach (var image in result.Take(5))
                {
                    UploadedImages.Add(image.FullPath);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Could not upload images", "OK");
        }
    }

    private async void OnCreateAuction(object sender, EventArgs e)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(Title) ||
            string.IsNullOrWhiteSpace(Description) ||
            string.IsNullOrWhiteSpace(SelectedCategory))
        {
            await DisplayAlert("Error", "Please fill in all required fields", "OK");
            return;
        }

        // TODO: Create auction logic
        await DisplayAlert("Success", "Auction created successfully", "OK");
        await Shell.Current.GoToAsync("AuctionSuccess");
    }

    private async void OnSaveDraft(object sender, EventArgs e)
    {
        // TODO: Save draft logic
        await DisplayAlert("Success", "Draft saved successfully", "OK");
    }
}