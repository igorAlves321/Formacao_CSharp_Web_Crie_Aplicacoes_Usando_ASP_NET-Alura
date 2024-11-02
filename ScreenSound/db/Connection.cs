using MySql.Data.MySqlClient;

namespace ScreenSound.db;

public class Connection
{
    // String de conexão com o banco de dados MySQL
    private string connectionString = "server=localhost;database=ScreenSound;user=root;password=;port=3306";

    // Método para obter a conexão
    public MySqlConnection GetConnection()
    {
        try
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Conexão com o banco de dados estabelecida com sucesso!");
            return connection;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
            return null;
        }
    }

    // Método para fechar a conexão
    public void CloseConnection(MySqlConnection connection)
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            Console.WriteLine("Conexão fechada.");
        }
    }
}
