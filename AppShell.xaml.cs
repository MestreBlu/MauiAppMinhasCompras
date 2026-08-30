using MauiAppMinhasCompras.Views;
using Microsoft.Maui.Controls;

namespace MauiAppMinhasCompras
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NovoProduto), typeof(NovoProduto));
            Routing.RegisterRoute(nameof(EditarProduto), typeof(EditarProduto));
        }
    }
}



