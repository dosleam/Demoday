using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public class DatabaseManager : Monobehaviour
{
    private string connectionString = "Server=localhost;Database=echo_shot_vr;User ID=root;Pooling=true;";

    private void Start()
    {
        TestConnection();
    }

    private void TestConnection()
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Debug.Log("Connexion à MySQL locale réussie");
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Erreur de connexion à MySQL : " + ex.Message); 
            }
        }
    }

    public void SaveScore(int score)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var command = new MySqlCommand("INSERT INTO scores (score) VALUES (@score);", connection))
            {
                command.Parameters.AddWithValue("@score", score);
                command.ExecuteNonQuery();
            }
        }
        Debug.Log("Score Enregistré : " + score);
    }

    public List<int> GetTopScores(int limit = 10)
    {
       List<int> scores = new List<int>();
       using (var connection = new MySqlConnection(connectionString))
       {
        connection.Open();
        using (var command = new MySqlCommand("SELECT score FROM scores ORDER BY score DESC LIMIT @limit;", connection)
        {
            command.Parameters.AddWithValue("@limit", limit);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    scores.Add(reader.GetInt32(0));
                }
            }
        }
       }
       return scores;
    }
}
