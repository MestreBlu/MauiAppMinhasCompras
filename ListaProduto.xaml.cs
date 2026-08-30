using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using Microsoft.Maui.Controls;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        private SQLiteDatabaseHelper _db;

        public ListaProduto()
        {
            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "minhascompras.db3");
            _db = new SQLiteDatabaseHelper(dbPath);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CarregarProdutos();
        }

        private async Task CarregarProdutos()
        {
            var produtos = await _db.GetAll();
            listaProdutos.ItemsSource = produtos;
        }

        private async void OnNovoProdutoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoProduto());
        }

        private async void OnSearchButtonPressed(object sender, EventArgs e)
        {
            var produtos = await _db.Search(txtBusca.Text);
            listaProdutos.ItemsSource = produtos;
        }

        private async void OnEditarClicked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var produto = swipeItem?.BindingContext as Produto;

            if (produto != null)
            {
                await Navigation.PushAsync(new EditarProduto(produto));
            }
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            var swipeItem = sender as SwipeItem;
            var produto = swipeItem?.BindingContext as Produto;

            if (produto == null) return;

            bool confirmar = await DisplayAlert("Excluir",
                $"Excluir o produto \"{produto.Descricao}\"?", "Sim", "Não");

            if (confirmar)
            {
                await _db.Delete(produto.Id);
                await CarregarProdutos();
            }
        }
    }
}

