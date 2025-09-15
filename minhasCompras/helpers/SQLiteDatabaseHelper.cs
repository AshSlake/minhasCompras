using minhasCompras.models;
using SQLite;

namespace minhasCompras.helpers
{
    public class SQLiteDatabaseHelper
    {
        // readonly para garantir que a conexão não seja alterada após a inicialização
        readonly SQLiteAsyncConnection _connection;

        public SQLiteDatabaseHelper(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto produto) 
        {
            return _connection.InsertAsync(produto);
        }

        public Task<int> Update (Produto produto)
        {
            if (produto == null || produto.Id <= 0)
            {
                throw new ArgumentException("Produto inválido para atualização.");
            }
            string sql = "UPDATE Produto SET Descricao = ?, Quantidade = ?, Preco = ? , Categoria = ? WHERE Id = ?";
            return _connection.ExecuteAsync(sql, produto.Descricao, produto.Quantidade, produto.Preco, produto.Categoria, produto.Id);
        }

        public Task<int> Delete(int id)
        {
            return _connection.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAllProdutos()
        {
            return _connection.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> SearchProduto(string nome)
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + nome + "%'";
            return _connection.QueryAsync<Produto>(sql);
        }

        public Task<List<Produto>> SearchProductByType(string categoria)
        {
            string sql = "SELECT * FROM Produto WHERE Categoria LIKE '%" + categoria + "%'";
            return _connection.QueryAsync<Produto>(sql);
        }
    }
}
