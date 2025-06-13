using Microsoft.Maui.Controls;
using MauiAppGestorMovil.ViewModels;

namespace MauiAppGestorMovil.Views
{
    public partial class FrameDashboard : ContentPage
    {
        public FrameDashboard()
        {
            InitializeComponent();
            BindingContext = new FrameDashboardViewModel(); // Enlaza el ViewModel
        }
    }
}
