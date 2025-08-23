using minhasCompras.models;
using System.Threading.Tasks;

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
            Produto produto = new Produto
            {
                Descricao = Convert.ToString(txt_descricao.Text),
                quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            await App.Db.Insert(produto);
            await DisplayAlert("SUCESSO", "REGISTRO INSERIDO", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }
}