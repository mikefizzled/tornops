using TornOps.Models;
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
