using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views
{
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescricao.Text))
                {
                    await DisplayAlert("Atenção", "Informe a descrição.", "OK");
                    return;
                }

                Produto produto = new Produto
                {
                    Descricao = txtDescricao.Text,
                    Quantidade = Convert.ToDouble(txtQuantidade.Text.Replace(',', '.'),
                                                  System.Globalization.CultureInfo.InvariantCulture),
                    Preco = Convert.ToDouble(txtPreco.Text.Replace(',', '.'),
                                             System.Globalization.CultureInfo.InvariantCulture)
                };

                await App.Db.Insert(produto);
                await DisplayAlert("Sucesso", "Produto salvo com sucesso!", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}

