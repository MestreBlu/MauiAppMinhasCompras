using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class EditarProduto : ContentPage
    {
        Produto _produto;

        public EditarProduto(Produto produto)
        {
            InitializeComponent();
            _produto = produto;
            txtDescricao.Text = produto.Descricao;
            txtQuantidade.Text = produto.Quantidade.ToString();
            txtPreco.Text = produto.Preco.ToString();
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            try
            {
                _produto.Descricao = txtDescricao.Text;
                _produto.Quantidade = Convert.ToDouble(txtQuantidade.Text.Replace(',', '.'),
                                                       System.Globalization.CultureInfo.InvariantCulture);
                _produto.Preco = Convert.ToDouble(txtPreco.Text.Replace(',', '.'),
                                                  System.Globalization.CultureInfo.InvariantCulture);

                await App.Db.Update(_produto);
                await DisplayAlert("Sucesso", "Produto atualizado!", "OK");
                await Navigation.PopAsync();
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
                bool confirm = await DisplayAlert("Excluir",
                    $"Excluir {_produto.Descricao}?", "Sim", "Não");
                if (confirm)
                {
                    await App.Db.Delete(_produto.Id);
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}

