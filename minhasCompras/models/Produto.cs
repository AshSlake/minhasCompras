using SQLite;

namespace minhasCompras.models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double quantidade { get; set; }
        public double Preco { get; set; }

    }
}
