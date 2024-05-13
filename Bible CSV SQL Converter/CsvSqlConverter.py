import csv

# Open the CSV file
with open('ESVBible_Database.csv', newline='', encoding='utf-8') as csvfile:
    # Create a CSV reader
    reader = csv.reader(csvfile)
    
    # Open SQL file for writing
    with open('bible.sql', 'w', encoding='utf-8') as sqlfile:
        # Iterate over each row in the CSV
        for row in reader:
            # Extract values from the row
            book_index, chapter, verse, text = row
            
            # Define book names corresponding to indexes
            books = [
                "Genesis", "Exodus", "Leviticus", "Numbers", "Deuteronomy",
                "Joshua", "Judges", "Ruth", "1 Samuel", "2 Samuel", "1 Kings",
                "2 Kings", "1 Chronicles", "2 Chronicles", "Ezra", "Nehemiah",
                "Esther", "Job", "Psalm", "Proverbs", "Ecclesiastes", "Song of Solomon",
                "Isaiah", "Jeremiah", "Lamentations", "Ezekiel", "Daniel", "Hosea",
                "Joel", "Amos", "Obadiah", "Jonah", "Micah", "Nahum", "Habakkuk",
                "Zephaniah", "Haggai", "Zechariah", "Malachi", "Matthew", "Mark",
                "Luke", "John", "Acts", "Romans", "1 Corinthians", "2 Corinthians",
                "Galatians", "Ephesians", "Philippians", "Colossians", "1 Thessalonians",
                "2 Thessalonians", "1 Timothy", "2 Timothy", "Titus", "Philemon",
                "Hebrews", "James", "1 Peter", "2 Peter", "1 John", "2 John",
                "3 John", "Jude", "Revelation"
            ]
            
            # Get the book name
            book_name = books[int(book_index)]
            
            # Escape inner quotes in the text
            text = text.replace("'", "''")
            
            # Construct the SQL insert statement
            sql_insert = f"INSERT INTO dbo.Bible (book, chapter, verse, text) VALUES ('{book_name}', {chapter}, {verse}, '{text}');\n"
            
            # Write the SQL insert statement to the file
            sqlfile.write(sql_insert)
