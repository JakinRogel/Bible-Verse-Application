using BibleApplication.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

/// <summary>
/// Data Access Object for handling database operations related to the Bible.
/// </summary>
public class BibleDAO
{
    private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = Benchmark; Integrated Security = True; Connect Timeout = 30; Encrypt=True;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";

    /// <summary>
    /// Searches for Bible verses in the Old Testament containing the specified text.
    /// </summary>
    /// <param name="searchText">The text to search for.</param>
    /// <returns>A list of Bible verses matching the search criteria.</returns>
    public List<BibleVerse> SearchInOldTestament(string searchText)
    {
        return SearchInTestament(searchText, true);
    }

    /// <summary>
    /// Searches for Bible verses in the New Testament containing the specified text.
    /// </summary>
    /// <param name="searchText">The text to search for.</param>
    /// <returns>A list of Bible verses matching the search criteria.</returns>
    public List<BibleVerse> SearchInNewTestament(string searchText)
    {
        return SearchInTestament(searchText, false);
    }

    /// <summary>
    /// Searches for Bible verses containing the specified text in the specified testament.
    /// </summary>
    /// <param name="searchText">The text to search for.</param>
    /// <param name="oldTestament">True for searching in the Old Testament, false for the New Testament.</param>
    /// <returns>A list of Bible verses matching the search criteria.</returns>
    private List<BibleVerse> SearchInTestament(string searchText, bool oldTestament)
    {
        // Initialize list to store search results
        List<BibleVerse> results = new List<BibleVerse>();

        // Establish database connection
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Determine which testament to search based on parameter
            string testamentCondition = oldTestament ? "Old" : "New";

            // Construct SQL query
            string query = $@"
                    SELECT book, chapter, verse, text 
                    FROM Bible 
                    WHERE book {((oldTestament) ? "NOT" : "")} IN (
                        'Matthew', 'Mark', 'Luke', 'John', 'Acts', 'Romans', '1 Corinthians', '2 Corinthians', 
                        'Galatians', 'Ephesians', 'Philippians', 'Colossians', '1 Thessalonians', '2 Thessalonians', 
                        '1 Timothy', '2 Timothy', 'Titus', 'Philemon', 'Hebrews', 'James', '1 Peter', '2 Peter', 
                        '1 John', '2 John', '3 John', 'Jude', 'Revelation'
                    ) 
                    AND text LIKE @searchText";

            // Execute SQL query
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameter for search text
                command.Parameters.AddWithValue("@searchText", $"%{searchText}%");

                // Read results from database
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Iterate over results and create BibleVerse objects
                    while (reader.Read())
                    {
                        BibleVerse verse = new BibleVerse
                        {
                            Book = reader["book"].ToString(),
                            Chapter = (int)reader["chapter"],
                            Verse = (int)reader["verse"],
                            Text = reader["text"].ToString()
                        };

                        // Add BibleVerse object to results list
                        results.Add(verse);
                    }
                }
            }
        }

        // Return the list of search results
        return results;
    }

    /// <summary>
    /// Searches for Bible verses containing the specified text.
    /// </summary>
    /// <param name="searchText">The text to search for.</param>
    /// <returns>A list of Bible verses matching the search criteria.</returns>
    public List<BibleVerse> Search(string searchText)
    {
        // Initialize list to store search results
        List<BibleVerse> results = new List<BibleVerse>();

        // Establish database connection
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Construct SQL query
            string query = "SELECT book, chapter, verse, text FROM Bible WHERE text LIKE @searchText";

            // Execute SQL query
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Add parameter for search text
                command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                // Read results from database
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Iterate over results and create BibleVerse objects
                    while (reader.Read())
                    {
                        BibleVerse verse = new BibleVerse
                        {
                            Book = reader["book"].ToString(),
                            Chapter = Convert.ToInt32(reader["chapter"]),
                            Verse = Convert.ToInt32(reader["verse"]),
                            Text = reader["text"].ToString()
                        };

                        // Add BibleVerse object to results list
                        results.Add(verse);
                    }
                }
            }
        }

        // Return the list of search results
        return results;
    }
}
//ALL WORK DONE BY JAKIN ROGEL