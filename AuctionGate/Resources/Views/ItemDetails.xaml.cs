using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace AuctionGate.Resources.Views
{
    [QueryProperty(nameof(Item), "Item")]
    public partial class ItemDetails : ContentPage
    {
        private AuctionItem _item;
        public AuctionItem Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                ValidateUserName();
                OnPropertyChanged();
            }
        }

        private string _nameErrorMessage;
        public string NameErrorMessage
        {
            get => _nameErrorMessage;
            set
            {
                _nameErrorMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasNameError));
            }
        }

        private string _bidAmount;
        public string BidAmount
        {
            get => _bidAmount;
            set
            {
                _bidAmount = value;
                ValidateBidAmount();
                OnPropertyChanged();
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasError));
            }
        }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
        public bool HasNameError => !string.IsNullOrEmpty(NameErrorMessage);

        public ICommand SubmitBidCommand { get; private set; }

        public ItemDetails()
        {
            InitializeComponent();
            InitializeCommands();
            BindingContext = this;
        }

        private void ValidateUserName()
        {
            NameErrorMessage = null;

            if (string.IsNullOrWhiteSpace(UserName))
            {
                NameErrorMessage = "Please enter your name";
                return;
            }

            if (UserName.Length < 2)
            {
                NameErrorMessage = "Name must be at least 2 characters long";
                return;
            }

            if (UserName.Length > 25)
            {
                NameErrorMessage = "Name must not exceed 25 characters";
                return;
            }

            if (!UserName.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)))
            {
                NameErrorMessage = "Name can only contain letters, numbers, and spaces";
                return;
            }
        }

        private void ValidateBidAmount()
        {
            ErrorMessage = null;

            if (string.IsNullOrWhiteSpace(BidAmount))
            {
                ErrorMessage = "Please enter a bid amount";
                return;
            }

            if (!decimal.TryParse(BidAmount, out decimal bidAmount))
            {
                ErrorMessage = "Please enter a valid amount";
                return;
            }

            if (bidAmount <= Item.CurrentBid)
            {
                ErrorMessage = "Bid must be higher than the current bid";
                return;
            }
        }

        private void InitializeCommands()
        {
            SubmitBidCommand = new Command(async () =>
            {
                ValidateUserName();
                ValidateBidAmount();

                if (HasError || HasNameError)
                    return;

                decimal bidAmount = decimal.Parse(BidAmount);
                bool confirmed = await DisplayAlert("Confirm Bid",
                    $"Are you sure you want to place a bid of ${bidAmount:F2}?",
                    "Yes", "No");

                if (confirmed)
                {
                    await DisplayAlert("Success", "Your bid has been placed!", "OK");
                    await Shell.Current.GoToAsync("///MarketPage");
                }
            });
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///MarketPage");
        }
    }
}