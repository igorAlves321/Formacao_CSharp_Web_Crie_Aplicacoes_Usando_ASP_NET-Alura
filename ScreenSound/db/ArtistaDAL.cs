using MySql.Data.MySqlClient;
using ScreenSound.Modelos;
using ScreenSound.db;

namespace ScreenSound.db
{
    public class ArtistaDAL
    {
        // Método para adicionar um novo artista ao banco de dados
        public void Adicionar(Artista artista)
        {
            using var connection = new Connection().GetConnection(); // Obter a conexão e abrir automaticamente

            string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @fotoPerfil, @bio)";
            MySqlCommand command = new MySqlCommand(sql, connection);

            // Adiciona parâmetros para evitar SQL Injection
            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@fotoPerfil", artista.FotoPerfil);
            command.Parameters.AddWithValue("@bio", artista.Bio);

            int linhasAfetadas = command.ExecuteNonQuery(); // Executa o comando
            Console.WriteLine($"Linhas afetadas: {linhasAfetadas}");
        }

        // Método para listar todos os artistas
        public IEnumerable<Artista> Listar()
        {
            var lista = new List<Artista>();

            using var connection = new Connection().GetConnection();
            if (connection != null)
            {
                string sql = "SELECT Nome, FotoPerfil, Bio FROM Artistas";
                MySqlCommand command = new MySqlCommand(sql, connection);

                using MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string nome = dataReader["Nome"].ToString();
                    string bio = dataReader["Bio"].ToString();
                    string fotoPerfil = dataReader["FotoPerfil"].ToString();

                    var artista = new Artista(nome, bio)
                    {
                        FotoPerfil = fotoPerfil
                    };

                    lista.Add(artista);
                }
            }

            return lista;
        }
    }
}
