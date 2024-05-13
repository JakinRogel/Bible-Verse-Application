using Microsoft.AspNetCore.Mvc;
using BibleApplication.Models;

namespace BibleApplication.Controllers
{
    /// <summary>
    /// Controller for handling Bible-related actions.
    /// </summary>
    public class BibleController : Controller
    {
        private readonly BibleService bibleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BibleController"/> class.
        /// </summary>
        /// <param name="bibleService">The Bible service for handling Bible-related operations.</param>
        public BibleController(BibleService bibleService)
        {
            this.bibleService = bibleService;
        }

        /// <summary>
        /// Displays the index view.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handles the search action.
        /// </summary>
        /// <param name="searchText">The text to search for.</param>
        /// <param name="testament">The selected testament ("OldTestament", "NewTestament", or "Both").</param>
        /// <returns>The index view with search results or error message.</returns>
        [HttpPost]
        public IActionResult Search(string searchText, string testament)
        {
            // Validate the search text
            string validationMessage = bibleService.ValidateSearchText(searchText);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                ViewData["ErrorMessage"] = validationMessage;
                return View("Index");
            }

            // Perform search based on the selected testament
            List<BibleVerse> results;
            if (testament == "OldTestament")
            {
                results = bibleService.SearchOldTestament(searchText);
            }
            else if (testament == "NewTestament")
            {
                results = bibleService.SearchNewTestament(searchText);
            }
            else
            {
                results = bibleService.Search(searchText);
            }

            ModelState.Clear(); // Clear model state after successful submission
            return View("Index", results);
        }
    }
}
//ALL WORK DONE BY JAKIN ROGEL