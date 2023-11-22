using Firebase.Database;
using Firebase.Database.Query;
using MoneyTransferApp.Model;
using MoneyTransferApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace MoneyTransferApp.ViewModel
{
    public class PaymentDeatilViewModel:BaseViewModel
    {
        

        private string _CardholderName;
        public string CardholderName
        {
            get
            { 
                return _CardholderName;
            }
            set
            { 
                _CardholderName = value;
                OnPropertyChanged();
            }
        }

        private string _Price;
        public string Price
        {
            get { return _Price; }
            set { _Price = value; OnPropertyChanged(); }
        }

        private string _CardNumber;
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; OnPropertyChanged(); }
        }

        private string _ExpiryDate;
        public string ExpiryDate
        {
            get { return _ExpiryDate; }
            set { _ExpiryDate = value; OnPropertyChanged(); }
        }

        // Error Label Property
        private string _NameError;          // Name Error Label Property Bind
        private string _PriceError;         // Price Error Label Property Bind 
        private string _ExpiryDateError;    // ExpiryDate Error Label Property Bind 
        private string _CardNumberError;    // CardNumber Error Label Property Bind 

        // Name Error Label Property Bind
        public string NameError
        {
            get
            {
                return _NameError;
            }
            set
            {
                _NameError = value;
                OnPropertyChanged();
            }
        }

        // Price Error Label Property Bind 
        public string PriceError
        {
            get
            {
                return _PriceError;
            }
            set
            {
                _PriceError = value;
                OnPropertyChanged();
            }
        }

        // ExpiryDate Error Label Property Bind 
        public string ExpiryDateError
        {
            get
            {
                return _ExpiryDateError;
            }
            set
            {
                _ExpiryDateError = value;
                OnPropertyChanged();
            }
        }

        // Card Number Error Label Property Bind 
        public string CardNumberError
        {
            get
            {
                return _CardNumberError;
            }
            set
            {
                _CardNumberError = value;
                OnPropertyChanged();
            }
        }
        // ErrorLabel Visibility Property
        private bool _isNameErrorVisible;                 // Name Error Label Visibility Property Bind 
        private bool _isPriceErrorVisible;                // Price Error Label Visibility Property Bind 
        private bool _isExpiryDateErrorVisible;            // ExpiryDate Error Label Visibility Property Bind 
        private bool _isCardNumberErrorVisible;        // Card Number Error Label Visibility Property Bind 


        // Name Error Label Visibility Property Bind 
        public bool IsNameErrorVisible
        {
            get
            {
                return _isNameErrorVisible;
            }
            set
            {
                _isNameErrorVisible = value;
                OnPropertyChanged();
            }
        }

        // Price Error Label Visibility Property Bind 
        public bool IsPriceErrorVisible
        {
            get
            {
                return _isPriceErrorVisible;
            }
            set
            {
                _isPriceErrorVisible = value;
                OnPropertyChanged();
            }
        }

        // ExpiryDate Error Label Visibility Property Bind 
        public bool IsExpiryDateErrorVisible
        {
            get
            {
                return _isExpiryDateErrorVisible;
            }
            set
            {
                _isExpiryDateErrorVisible = value;
                OnPropertyChanged();
            }
        }

        // Card Number  Error Label Visibility Property Bind 
        public bool IsCardNumberErrorVisible
        {
            get
            {
                return _isCardNumberErrorVisible;
            }
            set
            {
                _isCardNumberErrorVisible = value;
                OnPropertyChanged();
            }
        }

        public string HeaderText { get;set; }
        public string CardholderNameLabelText { get;set; }
        public string PriceLabelText {  get;set; }
        public string CardNumberLabelText { get; set; }
        public string ExpiryDateLabelText {  get;set; }
        public string SaveButtonText {  get;set; }

        public string CardholderPlaceholderText {  get;set; }
        public string PricePlaceholderText { get;set; }
        public string CardNumberPlaceholderText { get;set; }

        public ICommand SaveCommand { get; set; }

        public PaymentDeatilViewModel()
        {
            HeaderText = "Payment Detail";
            CardholderNameLabelText = "Cardholder Name";
            PriceLabelText = "Price";
            CardNumberLabelText = "Card Number";
            ExpiryDateLabelText = "Card Expiry Date";
            SaveButtonText = " Save Payment Detail";

            CardholderPlaceholderText = "Enter Cardholder Name ";
            PricePlaceholderText = "Enter Price";
            CardNumberPlaceholderText = "Enter Card Number";

            SaveCommand = new Command(OnSaveClicked);
           
        }

        private async void OnSaveClicked()
        {
            ValidateAllFields();
            if (!IsNameErrorVisible && !IsPriceErrorVisible && !IsExpiryDateErrorVisible && !IsCardNumberErrorVisible)
            {
                try
                {


                    // Save payment details to Firebase Realtime Database
                    var firebase = new FirebaseClient("https://moneytransferapp-4fdc2-default-rtdb.asia-southeast1.firebasedatabase.app/");
                    await firebase
                        .Child("Payments")
                        .PostAsync(new PaymentModel
                        {
                            CardholderName = CardholderName,
                            Price = Price,
                            ExpiryDate = ExpiryDate,
                            CardNumber = CardNumber
                        });

                    // Navigate to UserDashboardPage
                    await App.Current.MainPage.Navigation.PushAsync(new UserDashBoard());

                    // Show a success message
                    await App.Current.MainPage.DisplayAlert("Success", "Payment details saved successfully", "OK");
                }
                catch (Exception ex)
                {
                    // Handle exceptions, e.g., display an error message
                    await App.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
            
        }

        private void ValidateAllFields()
        {
            bool isAnyFieldEmpty = false;

            if (string.IsNullOrEmpty(CardholderName))
            {
                NameError = "Name is required";
                isAnyFieldEmpty = true;

            }
            else
            {
                NameError = string.Empty;
            }

            if (string.IsNullOrEmpty(Price))
            {
                PriceError = "Price is required";
                isAnyFieldEmpty = true;
            }
            else
            {
                PriceError = string.Empty;
            }

            if (string.IsNullOrEmpty(ExpiryDate))
            {
                ExpiryDateError = "ExpiryDate is required";
                isAnyFieldEmpty = true;
            }
            else
            {
                ExpiryDateError = string.Empty;
            }

            if (string.IsNullOrEmpty(CardNumber))
            {
                CardNumberError = "Card Number is required";
                isAnyFieldEmpty = true;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(CardNumber, @"^\d{16}$"))
            {
                CardNumberError = "Enter a valid 16-digit mobile number";
                isAnyFieldEmpty = true;
            }
            else
            {
                CardNumberError = string.Empty;
            }

            // Add more validation for other fields if needed

            if (isAnyFieldEmpty)
            {

                IsNameErrorVisible = true;
                IsPriceErrorVisible = true;
                IsExpiryDateErrorVisible = true;
                IsCardNumberErrorVisible = true;
            }
            else
            {
                IsNameErrorVisible = false;
                IsPriceErrorVisible = false;
                IsExpiryDateErrorVisible = false;
                IsCardNumberErrorVisible = false;
            }
        }

    }
}
