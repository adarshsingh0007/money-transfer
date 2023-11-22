using Firebase.Database;
using Firebase.Database.Query;
using MoneyTransferApp.Model;
using MoneyTransferApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types.Aggregation.Types;

namespace MoneyTransferApp.ViewModel
{
    public class UserDashBoardViewModel:BaseViewModel
    {
        FirebaseClient firebase;

        private static UserDashBoardViewModel instance;

        public static UserDashBoardViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDashBoardViewModel();
                }
                return instance;
            }
        }

        private PaymentModel latestPayment;
        public PaymentModel LatestPayment
        {
           get { return latestPayment; }
            set { latestPayment = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Transaction> transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get { return transactions; }
            set {  transactions = value; OnPropertyChanged(); }
        }

       

       

        private string amount;
        public string Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged(); }
        }


        


        public Command TransferCommand { get; set; }
        public string ButtonText { get; set; }

        public UserDashBoardViewModel()
        {
            ButtonText = "Transfer"; 

            firebase = new FirebaseClient("https://moneytransferapp-4fdc2-default-rtdb.asia-southeast1.firebasedatabase.app/");
          
            TransferCommand = new Command(TransferMoney);
            Transactions = new ObservableCollection<Transaction>();
            LoadLatestTransaction();

        }

        private async void TransferMoney(object obj)
        {
            try
            {
                // Navigate to the TransferMoneyPage
                await Application.Current.MainPage.Navigation.PushAsync(new TransferMoneyPage());
               
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error transferring money: {ex.Message}");
            }
            finally
            {
                // After the transfer, load the latest transactions
                await LoadLatestTransaction();
            }
        }

        // Method to load the latest transactions
        public async Task LoadLatestTransaction()
        {
            try
            {
                // Fetch transactions from the Realtime Database
                var loadedTransactions = await firebase
                    .Child("Transactions")
                    .OrderByKey()
                    .OnceAsync<Transaction>();

                // Clear existing transactions and add the loaded ones
                Transactions.Clear();
                foreach (var transaction in loadedTransactions)
                {
                    Transactions.Add(transaction.Object);
                }
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error loading transactions: {ex.Message}");
            }
        }
    }
}
