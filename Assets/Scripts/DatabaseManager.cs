using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class DatabaseManager : MonoBehaviour
{
    private string connectionString = "Server=localhost;Database=echo_shot_vr;User ID=root;Pooling=true;";

    // Ceci va enregistrer le score le plus haut quand le joueur quitte la scène principale
    public void SaveHighScore(int score)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO scores (score) VALUES (@score)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@score", score);
                command.ExecuteNonQuery();
                Debug.Log("Score enregistré avec succès");
            }
            catch (Exception ex)
            {
                Debug.LogError("Erreur lors de l'enregistrement du score : " + ex.Message);
            }
        }
    }

    // Ceci va récupèrer les 10 meilleurs scores pour les afficher dans le menu principal
    public List<int> GetTopScores()
    {
        List<int> topScores = new List<int>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "SELECT score FROM scores ORDER BY score DESC LIMIT 10";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    topScores.Add(reader.GetInt32("score"));
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Erreur lors de la récupération des scores : " + ex.Message);
            }
        }

        return topScores;
    }
}

