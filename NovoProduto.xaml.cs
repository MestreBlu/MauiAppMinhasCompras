using System.Globalization;
using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using Microsoft.Maui.Controls;

namespace MauiAppMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        private SQLiteDatabaseHelper _db;

        public NovoProduto()
        {
            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "minhascompras.db3");
            _db = new SQLiteDatabaseHelper(dbPath);
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescricao.Text) ||
                string.IsNullOrWhiteSpace(txtQuantidade.Text) ||
                string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                await DisplayAlert("Atenção", "Preencha todos os campos.", "OK");
                return;
            }

            var produto = new Produto
            {
                Descricao = txtDescricao.Text,
                Quantidade = ConverterParaDouble(txtQuantidade.Text),
                Preco = ConverterParaDouble(txtPreco.Text)
            };

            await _db.Insert(produto);
            await DisplayAlert("Sucesso", "Produto cadastrado com sucesso!", "OK");
            await Navigation.PopAsync();
        }

        private double ConverterParaDouble(string valor)
        {
            if (double.TryParse(valor.Replace(',', '.'),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out double resultado))
            {
                return resultado;
            }
            return 0;
        }
    }
}

