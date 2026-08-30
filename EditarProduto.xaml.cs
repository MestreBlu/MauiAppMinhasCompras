using System.Globalization;
using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Models;
using Microsoft.Maui.Controls;

namespace MauiAppMinhasCompras.Views
{
    public partial class EditarProduto : ContentPage
    {
        private SQLiteDatabaseHelper _db;
        private Produto _produto;

        public EditarProduto(Produto produto)
        {
            InitializeComponent();

            _produto = produto;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "minhascompras.db3");
            _db = new SQLiteDatabaseHelper(dbPath);

            txtDescricao.Text = _produto.Descricao;
            txtQuantidade.Text = _produto.Quantidade.ToString();
            txtPreco.Text = _produto.Preco.ToString();
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

            _produto.Descricao = txtDescricao.Text;
            _produto.Quantidade = ConverterParaDouble(txtQuantidade.Text);
            _produto.Preco = ConverterParaDouble(txtPreco.Text);

            await _db.Update(_produto);
            await DisplayAlert("Sucesso", "Produto atualizado com sucesso!", "OK");
            await Navigation.PopAsync();
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            bool confirmar = await DisplayAlert("Excluir",
                $"Excluir o produto \"{_produto.Descricao}\"?", "Sim", "Não");

            if (confirmar)
            {
                await _db.Delete(_produto.Id);
                await Navigation.PopAsync();
            }
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

