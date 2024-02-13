using DeleeRefreshMonkey.ViewModels;

namespace DeleeRefreshMonkey.Views;

public partial class MonkeysDetailsView : ContentPage
{
	public MonkeysDetailsView(MonkeyDetailsViewModel vm)
	{
		InitializeComponent();
        this.BindingContext = vm;
    }
}