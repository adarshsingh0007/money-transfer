using Firebase.Database;
using Firebase.Database.Query;
using MoneyTransferApp.Model;
using MoneyTransferApp.View;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace MoneyTransferApp.ViewModel
{
    public class TransactionSuccessNotifiViewModelcs : BaseViewModel
    {
        FirebaseClient firebase;

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged(); }
        }

        private static TransactionSuccessNotifiViewModelcs instance;

        public static TransactionSuccessNotifiViewModelcs Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TransactionSuccessNotifiViewModelcs();
                }
                return instance;
            }
        }

        private ObservableCollection<Transaction> transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get { return transactions; }
            set { transactions = value; OnPropertyChanged(); }
        }

        public Command DoneCommand { get; set; }
        public string SuccessButton { get; set; }

        public TransactionSuccessNotifiViewModelcs()
        {
            SuccessButton = "Done";

            firebase = new FirebaseClient("https://moneytransferapp-4fdc2-default-rtdb.asia-southeast1.firebasedatabase.app/");

            DoneCommand = new Command(DoneTransaction);
            Transactions = new ObservableCollection<Transaction>();
            LoadLatestTransaction();
        }

        private async void DoneTransaction(object obj)
        {
            try
            {
                // Navigate to the UserDashBoardPage after a successful transfer
                await Application.Current.MainPage.Navigation.PushAsync(new UserDashBoard());
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error transferring money: {ex.Message}");
            }
            finally
            {
                // After the transfer, load the latest transactions
                await LoadLatestTransaction();
            }
        }

        public async Task LoadLatestTransaction()
        {
            try
            {
                // Fetch transactions from the Realtime Database
                var loadedTransactions = await firebase
                    .Child("Transactions")
                    .OrderByKey()
                    .LimitToLast(1) // Limit to the last transaction
                    .OnceAsync<Transaction>();

                // Clear existing transactions and add the loaded ones
                Transactions.Clear();
                foreach (var transaction in loadedTransactions)
                {
                    Transactions.Add(transaction.Object);

                    // Set the UserName and Amount based on the latest transaction
                    UserName = transaction.Object.UserName;

                    // Convert the Amount from string to decimal
                    if (decimal.TryParse(transaction.Object.Amount, out decimal amount))
                    {
                        Amount = amount;
                    }
                    else
                    {
                        // Handle the case where the conversion fails
                        Console.WriteLine("Error converting Amount to decimal.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error loading transactions: {ex.Message}");
            }
        }
    }
}
