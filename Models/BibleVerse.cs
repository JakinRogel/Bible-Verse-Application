namespace BibleApplication.Models
{
    /// <summary>
    /// Represents a Bible verse.
    /// </summary>
    public class BibleVerse
    {
        /// <summary>
        /// Gets or sets the name of the book containing the verse.
        /// </summary>
        public string Book { get; set; }

        /// <summary>
        /// Gets or sets the chapter number containing the verse.
        /// </summary>
        public int Chapter { get; set; }

        /// <summary>
        /// Gets or sets the verse number.
        /// </summary>
        public int Verse { get; set; }

        /// <summary>
        /// Gets or sets the text of the verse.
        /// </summary>
        public string Text { get; set; }
    }
}
//ALL WORK DONE BY JAKIN ROGEL