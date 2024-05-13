using BibleApplication.Models;
using System;
using System.Collections.Generic;

/// <summary>
/// Service for handling Bible-related operations.
/// </summary>
public class BibleService
{
    private readonly BibleDAO bibleDAO;

    /// <summary>
    /// Initializes a new instance of the <see cref="BibleService"/> class.
    /// </summary>
    /// <param name="bibleDAO">The data access object for Bible operations.</param>
    public BibleService(BibleDAO bibleDAO)
    {
        this.bibleDAO = bibleDAO;
    }

    /// <summary>
    /// Validates the search text.
    /// </summary>
    /// <param name="searchText">The text to search for.</param>
    /// <returns>A validation message if validation fails, otherwise null.</returns>
    /// <exception cref="ArgumentNullException">Thrown when searchText is null.</exception>
    public string ValidateSearchText(string searchText)
    { 

        // Check if searchText is empty or contains only white space
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return "Search text cannot be empty or contain only white space.";
        }

        // Check if searchText exceeds the maximum word count
        if (searchText.Split(' ').Length > 90)
        {
            return "Maximum 90 words allowed.";
        }

        // Check if searchText exceeds the maximum character count
        if (searchText.Length > 1000)
        {
            return "Maximum 1000 characters allowed.";
        }

        // Check if searchText contains numeral numbers
        if (searchText.Any(char.IsDigit))
        {
            return "Search text cannot contain numeral numbers, only numbers as words.";
        }

        // Validation passed
        return null;
    }

    /// <summary>
    /// Searches for Bible verses in the Old Testament containing the specified text.
    /// </summary>
    /// <param name="searchText">The text to search for.</param>
    /// <returns>A list of Bible verses matching the search criteria.</returns>
    /// <exception cref="ArgumentNullException">Thrown when searchText is null.</exception>
    public List<BibleVerse> SearchOldTestament(string searchText)
    {
        // Delegate the search operation to the BibleDAO for the Old Testament
        return bibleDAO.SearchInOldTestament(searchText);
    }

    /// <summary>
    /// Searches for Bible verses in the New Testament containing the specified text.
    /// </summary>
    /// <param name="searchText">The text to search for.</param>
    /// <returns>A list of Bible verses matching the search criteria.</returns>
    /// <exception cref="ArgumentNullException">Thrown when searchText is null.</exception>
    public List<BibleVerse> SearchNewTestament(string searchText)
    {
        // Delegate the search operation to the BibleDAO for the New Testament
        return bibleDAO.SearchInNewTestament(searchText);
    }

    /// <summary>
    /// Searches for Bible verses containing the specified text.
    /// </summary>
    /// <param name="searchText">The text to search for.</param>
    /// <returns>A list of Bible verses matching the search criteria.</returns>
    /// <exception cref="ArgumentNullException">Thrown when searchText is null.</exception>
    public List<BibleVerse> Search(string searchText)
    {
        // Delegate the search operation to the BibleDAO
        return bibleDAO.Search(searchText);
    }
}
//ALL WORK DONE BY JAKIN ROGEL