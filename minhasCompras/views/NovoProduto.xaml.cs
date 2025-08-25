using minhasCompras.models;
using System.Threading.Tasks;
using minhasCompras.helpers;
using System.Globalization;

namespace minhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txt_descricao.Text) ||
                string.IsNullOrWhiteSpace(txt_quantidade.Text) ||
                string.IsNullOrWhiteSpace(txt_preco.Text))
            {

                await DisplayAlert("OPS", "INSIRA TODOS OS CAMPOS", "OK");
                return;
            }

            Produto produto = new Produto
            {
                Descricao = Convert.ToString(txt_descricao.Text),
                quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            await App.Db.Insert(produto);
            await DisplayAlert("SUCESSO", "REGISTRO INSERIDO", "OK");

            // após salvar com sucesso o novo produto o ususario é direcionado a lista de produtos novamente
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        try
        {
            if (sender is Button button)
            {
                await helpers.AnimationHelpers.AnimateClickAsync(button);
            }

            await Navigation.PopAsync();

        }catch(Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }

    }
}