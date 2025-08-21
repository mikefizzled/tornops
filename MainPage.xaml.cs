using TornOps.Models;
using TornOps.ViewModels;

namespace TornOps
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new DashboardViewModel();
        }
    }

}
