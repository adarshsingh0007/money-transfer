using Firebase.Database;
using Firebase.Database.Query;
using MoneyTransferApp.Model;
using MoneyTransferApp.Repository;
using MoneyTransferApp.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTransferApp.ViewModel
{
    public class MoneyTransferViewModel:BaseViewModel
    {      
        private PaymentModel latestPayment;
        public PaymentModel LatestPayment
        {
            get { return latestPayment; }
            set { latestPayment = value; OnPropertyChanged(); }
        }

        private string selectedUserDetails;
        public string SelectedUserDetails
        {
            get { return selectedUserDetails; }
            set { selectedUserDetails = value; OnPropertyChanged(); }
        }

        private string amount;
        public string Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged(); }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(); }
        }

        public ObservableCollection<User> UserNames { get; set; }
        public User SelectedUser { get; set; }
        
        public ObservableCollection<TransactionCategory> TransactionCategories { get; set; }
        public TransactionCategory SelectedCategory { get; set; }
        

        public Command SearchCommand { get; }
        public Command TransferCommand { get; }

       

        FirebaseClient firebase;
        public MoneyTransferViewModel()
        {
            UserNames = new ObservableCollection<User>();
            TransactionCategories = new ObservableCollection<TransactionCategory>();
            

            TransferCommand = new Command(async () => await TransferMoney());
            firebase = new FirebaseClient("https://moneytransferapp-4fdc2-default-rtdb.asia-southeast1.firebasedatabase.app/");

            LoadUserNames();
            LoadTransactionCategories();
        }

        private void LoadUserNames()
        {
           
            UserNames.Add(new User { UserName = "Mohit" });
            UserNames.Add(new User { UserName = "Rahul" });
            UserNames.Add(new User { UserName = "Sunil" });
            UserNames.Add(new User { UserName = "Vikky" });
            UserNames.Add(new User { UserName = "Vinnit" });
        }

        private void LoadTransactionCategories()
        {
            
            var entertainmentCategory = new TransactionCategory
            {
                CategoryName = "Entertainment",
                
            };

            var foodCategory = new TransactionCategory
            {
                CategoryName = "Food",              
                
            };

            var clothsCategory = new TransactionCategory
            {
                CategoryName = "Cloths",            
               
            };

            var travelCategory = new TransactionCategory
            {
                CategoryName = "Travel",             
                
            };

            TransactionCategories.Add(entertainmentCategory);
            TransactionCategories.Add(foodCategory);
            TransactionCategories.Add(clothsCategory);
            TransactionCategories.Add(travelCategory);
        }

      private async Task TransferMoney()
{
      try
      {
       
        if (SelectedUser == null)
        {
            
            await Application.Current.MainPage.DisplayAlert("Error", "Please select a user.", "OK");
            return;
        }

        if (SelectedCategory == null)
        {
           
            await Application.Current.MainPage.DisplayAlert("Error", "Please select a category.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(Amount))
        {
           
            await Application.Current.MainPage.DisplayAlert("Error", "Please enter the amount.", "OK");
            return;
        }

        
        if (!decimal.TryParse(Amount, out decimal amount))
        {
           
            await Application.Current.MainPage.DisplayAlert("Error", "Invalid amount format.", "OK");
            return;
        }

       
        var transaction = new Transaction
        {
            UserName = SelectedUser.UserName,
            Category = SelectedCategory.CategoryName,
            Amount = Amount,
            Message = Message,
            TransactionTime = DateTime.Now
        };

        
        await firebase
            .Child("Transactions")
            .PostAsync(transaction);

       
        await UserDashBoardViewModel.Instance.LoadLatestTransaction();

       
        await Application.Current.MainPage.Navigation.PushAsync(new TransactionSuccessNotifiPage());
       }
       catch (Exception ex)
       {
      
        Console.WriteLine($"Error transferring money: {ex.Message}");

       
        await Application.Current.MainPage.DisplayAlert("Error", $"Error transferring money: {ex.Message}", "OK");
       }
      }




    }
}
