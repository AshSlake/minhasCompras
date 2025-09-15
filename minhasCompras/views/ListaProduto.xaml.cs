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
        try
        {
            InitializeComponent();

            list_produtos.ItemsSource = lista;
        }
        catch
        {
            DisplayAlert("OPS", "ERRO INESPERADO", "OK");
        }

    }

    protected async override void OnAppearing()
    {
        try
        {
            List<Produto> product_temp = await App.Db.GetAllProdutos();

            lista.Clear();

            product_temp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }

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
        try
        {
            string serchText = e.NewTextValue;  

            list_produtos.IsRefreshing = true;

            lista.Clear();

            List<Produto> product_temp = await App.Db.SearchProduto(serchText);

            product_temp.ForEach(i => lista.Add(i));

        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
        finally
        {
            list_produtos.IsRefreshing = false;
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            double soma = lista.Sum(i => i.Total);

            string msg = string.Format("Total da compra: {0:C2}", soma);

            DisplayAlert("Valor da Compra", msg, "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("OPS", ex.Message, "OK");
        }
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem? selecionado = sender as MenuItem;
            Produto? produto = selecionado?.BindingContext as Produto;

            bool readyFromExclude = await DisplayAlert("Atenção", "Confirma a exclusão do produto?", "SIM", "NÃO");

            if (!readyFromExclude)
            {
                return;
            }

            if (produto == null)
            {
                await DisplayAlert("OPS", "Nenhum produto selecionado", "OK");
                return;
            }
            await App.Db.Delete(produto.Id);

            lista.Clear();

            List<Produto> product_temp = await App.Db.GetAllProdutos();

            product_temp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }

    private void list_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto produto = e.SelectedItem as Produto;
            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = produto
            });
               
        }
        catch (Exception ex)
        {
            DisplayAlert("OPS", ex.Message, "OK");
        }
    }

    private async void list_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            List<Produto> product_temp = await App.Db.GetAllProdutos();

            lista.Clear();

            product_temp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
        finally
        {
            list_produtos.IsRefreshing = false;
        }
    }
}