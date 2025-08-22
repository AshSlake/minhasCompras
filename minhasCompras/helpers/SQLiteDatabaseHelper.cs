using minhasCompras.models;
using SQLite;

namespace minhasCompras.helpers
{
    public class SQLiteDatabaseHelper
    {
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

        public Task<List<Produto>> Update(Produto produto)
        {
            string sql = "UPDATE Produto SET Descricao = ?, quantidade = ?, Preco = ? WHERE Id = ?";

            return _connection.QueryAsync<Produto>(sql, produto.Descricao, produto.quantidade, produto.Id);

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
           string sql = "SELECT * FROM Produto WHERE Descricao LIKE ?";
            return _connection.QueryAsync<Produto>(sql);
        }
    }
}
