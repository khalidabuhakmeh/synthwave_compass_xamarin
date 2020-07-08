using Compass.ViewModels;
using Xamarin.Forms;

namespace Compass
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((CompassViewModel)BindingContext).StartCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((CompassViewModel)BindingContext).StopCommand.Execute(null);
        }
    }
}