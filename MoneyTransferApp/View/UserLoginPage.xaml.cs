using MoneyTransferApp.ViewModel;

namespace MoneyTransferApp.View;

public partial class UserLoginPage : ContentPage
{
	public UserLoginPage()
	{
		InitializeComponent();
        BindingContext = new UserLoginViewModel();
    }
}