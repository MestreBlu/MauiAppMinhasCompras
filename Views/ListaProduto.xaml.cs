using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views
{
    public partial class ListaProduto : ContentPage
    {
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

        public ListaProduto()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                CarregarLista();
            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private void CarregarLista()
        {
            lista.Clear();
            var produtos = App.Db.GetAll().Result;
            foreach (var p in produtos)
                lista.Add(p);
            listaProdutos.ItemsSource = lista;
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var busca = e.NewTextValue;

                if (string.IsNullOrWhiteSpace(busca))
                {
                    CarregarLista();
                }
                else
                {
                    lista.Clear();
                    var produtos = App.Db.Search(busca).Result;
                    foreach (var p in produtos)
                        lista.Add(p);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void OnProdutoSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem != null)
                {
                    var produto = (Produto)e.SelectedItem;
                    await Navigation.PushAsync(new EditarProduto(produto));
                    ((ListView)sender).SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            try
            {
                var menuItem = (MenuItem)sender;
                var produto = (Produto)menuItem.CommandParameter;

                bool confirm = await DisplayAlert("Excluir",
                    $"Deseja excluir {produto.Descricao}?", "Sim", "Não");

                if (confirm)
                {
                    await App.Db.Delete(produto.Id);
                    CarregarLista();
                    await DisplayAlert("Sucesso", "Produto excluído!", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void OnNovoProdutoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoProduto());
        }
    }
}

