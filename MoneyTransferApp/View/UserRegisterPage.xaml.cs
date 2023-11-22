using MoneyTransferApp.ViewModel;

namespace MoneyTransferApp.View;

public partial class UserRegisterPage : ContentPage
{
	public UserRegisterPage()
	{
		InitializeComponent();
		BindingContext = new UserRegisterViewModel();
	}
}