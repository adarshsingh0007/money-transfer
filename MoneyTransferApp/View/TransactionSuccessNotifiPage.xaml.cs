using MoneyTransferApp.ViewModel;

namespace MoneyTransferApp.View;

public partial class TransactionSuccessNotifiPage : ContentPage
{
	public TransactionSuccessNotifiPage()
	{
		InitializeComponent();
        BindingContext = TransactionSuccessNotifiViewModelcs.Instance;

       
    }
}