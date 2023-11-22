using Firebase.Database;
using Microsoft.Maui.Storage;
using MoneyTransferApp.Model;
using MoneyTransferApp.ViewModel;

namespace MoneyTransferApp.View;

public partial class TransferMoneyPage : ContentPage
{
    FirebaseClient firebase;

    public TransferMoneyPage()
	{
		InitializeComponent();
        BindingContext = new MoneyTransferViewModel();

        firebase = new FirebaseClient("https://moneytransferapp-4fdc2-default-rtdb.asia-southeast1.firebasedatabase.app/");
        LoadPaymentDetails();
    }

    async void LoadPaymentDetails()
    {
        // Fetch payment details from Firebase Realtime Database
        var paymentDetails = await firebase
            .Child("Payments")
            .OnceAsync<PaymentModel>();

        // Display the latest payment details
        var latestPayment = paymentDetails.LastOrDefault();

        if (latestPayment != null)
        {
            // Set the binding context to the latest payment details
            BindingContext = new MoneyTransferViewModel
            {
                LatestPayment = latestPayment.Object
            };
        }
    }
}