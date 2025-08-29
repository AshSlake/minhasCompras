using Microsoft.Maui.Controls;
using minhasCompras.models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace minhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    private readonly ObservableCollection<Produto> lista = [];

    public ListaProduto()
	{
		InitializeComponent();

        list_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        List<Produto> product_temp = await App.Db.GetAllProdutos();

        lista.Clear();

        product_temp.ForEach( i => lista.Add(i) );
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
             Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("OPS", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string serchText = e.NewTextValue;

        lista.Clear();

        List<Produto> product_temp = await App.Db.SearchProduto(serchText);

        product_temp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = string.Format("Total da compra: {0:C2}", soma);

        DisplayAlert("Valor da Compra", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
       int idSelecionado = (int)((MenuItem)sender).CommandParameter;

        await App.Db.Delete(idSelecionado);

        lista.Clear();

        List<Produto> product_temp = await App.Db.GetAllProdutos();

        lista.Clear();

        product_temp.ForEach(i => lista.Add(i));
    }
}