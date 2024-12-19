using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AuctionGate.Resources.Views
{
    public partial class CreateAuctionPage : ContentPage, INotifyPropertyChanged
    {
        private string title;
        private string description;
        private string selectedCategory;
        private double startPriceValue;
        private double reservePriceValue;
        private string selectedDuration;
        private bool buyNowOption;
        private bool featuredListing;
        private bool privateAuction;
        private bool internationalShipping;
        private bool showTitleError;
        private bool showDescriptionError;
        private bool showCategoryError;
        private bool showStartPriceError;
        private bool showDurationError;

        public CreateAuctionPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
        public record AuctionCreationData
        {
            public string Title { get; init; }
            public string Description { get; init; }
            public string Category { get; init; }
            public double StartPrice { get; init; }
            public double ReservePrice { get; init; }
            public string Duration { get; init; }
            public bool BuyNowOption { get; init; }
            public bool FeaturedListing { get; init; }
            public bool PrivateAuction { get; init; }
            public bool InternationalShipping { get; init; }
            public List<string> ImagePaths { get; init; }
        }
        public List<string> Categories { get; } = new()
        {
            "Electronics",
            "Collectibles",
            "Fashion",
            "Home & Garden",
            "Vehicles"
        };

        public List<string> Durations { get; } = new()
        {
            "3 Days",
            "5 Days",
            "7 Days",
            "10 Days"
        };

        public ObservableCollection<string> UploadedImages { get; } = new();

        #region Properties
        public string Title
        {
            get => title;
            set
            {
                title = value;
                ShowTitleError = string.IsNullOrWhiteSpace(value);
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                ShowDescriptionError = string.IsNullOrWhiteSpace(value);
                OnPropertyChanged();
            }
        }

        public string SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                ShowCategoryError = string.IsNullOrWhiteSpace(value);
                OnPropertyChanged();
            }
        }

        public double StartPriceValue
        {
            get => startPriceValue;
            set
            {
                startPriceValue = value;
                ShowStartPriceError = value <= 0;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StartPrice));
            }
        }

        public string StartPrice => $"${StartPriceValue:N2}";

        public double ReservePriceValue
        {
            get => reservePriceValue;
            set
            {
                reservePriceValue = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ReservePrice));
            }
        }

        public string ReservePrice => $"${ReservePriceValue:N2}";

        public string SelectedDuration
        {
            get => selectedDuration;
            set
            {
                selectedDuration = value;
                ShowDurationError = string.IsNullOrWhiteSpace(value);
                OnPropertyChanged();
            }
        }

        public bool BuyNowOption
        {
            get => buyNowOption;
            set
            {
                buyNowOption = value;
                OnPropertyChanged();
            }
        }

        public bool FeaturedListing
        {
            get => featuredListing;
            set
            {
                featuredListing = value;
                OnPropertyChanged();
            }
        }

        public bool PrivateAuction
        {
            get => privateAuction;
            set
            {
                privateAuction = value;
                OnPropertyChanged();
            }
        }

        public bool InternationalShipping
        {
            get => internationalShipping;
            set
            {
                internationalShipping = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Error Properties
        public bool ShowTitleError
        {
            get => showTitleError;
            set
            {
                showTitleError = value;
                OnPropertyChanged();
            }
        }

        public bool ShowDescriptionError
        {
            get => showDescriptionError;
            set
            {
                showDescriptionError = value;
                OnPropertyChanged();
            }
        }

        public bool ShowCategoryError
        {
            get => showCategoryError;
            set
            {
                showCategoryError = value;
                OnPropertyChanged();
            }
        }

        public bool ShowStartPriceError
        {
            get => showStartPriceError;
            set
            {
                showStartPriceError = value;
                OnPropertyChanged();
            }
        }

        public bool ShowDurationError
        {
            get => showDurationError;
            set
            {
                showDurationError = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Validation Methods
        private bool ValidatePrice(double price)
        {
            return price > 0;
        }

        private (bool isValid, List<string> errors) ValidateForm()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Title))
                errors.Add("Title is required");

            if (string.IsNullOrWhiteSpace(Description))
                errors.Add("Description is required");

            if (string.IsNullOrWhiteSpace(SelectedCategory))
                errors.Add("Please select a category");

            if (StartPriceValue <= 0)
                errors.Add("Starting price must be greater than zero");

            if (string.IsNullOrWhiteSpace(SelectedDuration))
                errors.Add("Please select auction duration");

            if (ReservePriceValue > 0 && ReservePriceValue < StartPriceValue)
                errors.Add("Reserve price must be greater than or equal to starting price");

            return (errors.Count == 0, errors);
        }
        #endregion

        #region Event Handlers
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
            catch (Exception)
            {
                await DisplayAlert("Error", "Could not upload images", "OK");
            }
        }

        private void OnDurationRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value && sender is RadioButton radioButton)
            {
                SelectedDuration = radioButton.Content?.ToString();
            }
        }

        private async void OnCreateAuction(object sender, EventArgs e)
        {
            var (isValid, errors) = ValidateForm();

            if (!isValid)
            {
                string message = string.Join("\n", errors);
                await DisplayAlert("Please Fix These Issues", message, "OK");
                return;
            }

            try
            {
                var auctionData = new AuctionCreationData
                {
                    Title = Title,
                    Description = Description,
                    Category = SelectedCategory,
                    StartPrice = StartPriceValue,
                    ReservePrice = ReservePriceValue,
                    Duration = SelectedDuration,
                    BuyNowOption = BuyNowOption,
                    FeaturedListing = FeaturedListing,
                    PrivateAuction = PrivateAuction,
                    InternationalShipping = InternationalShipping,
                    ImagePaths = UploadedImages.ToList()
                };
                await DisplayAlert("Success", "Auction created successfully", "OK");
                await Navigation.PushAsync(new AuctionSuccessPage(auctionData));

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to create auction. Please try again.", "OK");
            }
        }

        private async void OnGoBack(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("MarketPage");
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}