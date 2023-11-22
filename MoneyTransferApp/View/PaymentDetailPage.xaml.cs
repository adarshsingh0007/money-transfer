using MoneyTransferApp.ViewModel;

namespace MoneyTransferApp.View;

public partial class PaymentDetailPage : ContentPage
{
	public PaymentDetailPage()
	{
		InitializeComponent();
        BindingContext = new PaymentDeatilViewModel();
        
    }
}

