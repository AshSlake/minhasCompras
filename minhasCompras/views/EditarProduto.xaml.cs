using minhasCompras.models;

namespace minhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

	private async void ToolbarItem_Clicked(object sender, EventArgs e)
	{
		try
		{
            if (
                string.IsNullOrWhiteSpace(txt_descricao.Text) ||
                string.IsNullOrWhiteSpace(txt_quantidade.Text) ||
                string.IsNullOrWhiteSpace(txt_preco.Text))
            {

                await DisplayAlert("OPS", "INSIRA TODOS OS CAMPOS", "OK");
                return;
            }

            Produto produto_anexado = BindingContext as Produto;

            if (produto_anexado == null) 
            {
                await DisplayAlert("OPS", "Produto invalido", "OK");
            }

            Produto produto = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = Convert.ToString(txt_descricao.Text),
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            await App.Db.Update(produto);
            await DisplayAlert("SUCESSO", "REGISTRO ATUALIZADO", "OK");

            // após salvar com sucesso o novo produto o ususario é direcionado a lista de produtos novamente
            await Navigation.PopAsync();
        }
		catch (Exception ex)
		{
			await DisplayAlert("OPS", ex.Message, "OK");
		}
	}
}