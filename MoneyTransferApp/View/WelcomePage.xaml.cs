namespace MoneyTransferApp.View;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
        NavigateToRestaurantListPage();
       
    }

    private async void NavigateToRestaurantListPage()
    {
        await Task.Delay(5000); // Delay for 2 seconds (2000 milliseconds)
        await Navigation.PushAsync(new UserLoginPage());
    }

   
}