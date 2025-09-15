using minhasCompras.models;
using System.ComponentModel;

namespace minhasCompras.Views;

public partial class NovoProduto : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    // Lista de opções do enum convertidas em string
    public List<string> TipoProdutos { get; } =
        Enum.GetNames(typeof(TipoProduto)).ToList();

    private string _produtoSelecionado;
    public string ProdutoSelecionado
    {
        get => _produtoSelecionado;
        set
        {
            if (_produtoSelecionado != value)
            {
                _produtoSelecionado = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProdutoSelecionado)));
            }
        }
    }

    public NovoProduto()
	{
		InitializeComponent();
        BindingContext = this;
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
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
                Categoria = ProdutoSelecionado

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