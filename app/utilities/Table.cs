namespace lms.Utilities
{
    public class Table
    {
        /// <summary>
        /// Calculates the maximum column widths based on the headers and the data in the progress objects.
        /// </summary>
        /// <param name="headers">The array of headers for the progress summary table.</param>
        /// <param name="rows">The list of progress objects.</param>
        /// <returns>An array of integers representing the maximum column widths.</returns>
        public int[] GetMaxColumnWidths(string[] headers, IEnumerable<Lms.Models.Progress> rows)
        {
            var maxWidths = new int[headers.Length];

            // Determine maximum widths based on headers
            for (int i = 0; i < headers.Length; i++)
            {
                maxWidths[i] = headers[i].Length;
            }

            // Determine maximum widths based on the row contents
            foreach (var row in rows)
            {
                maxWidths[0] = Math.Max(maxWidths[0], $"p{row.Id}".Length);  // Add 'p' to the Id length
                maxWidths[1] = Math.Max(maxWidths[1], row.Description?.Length ?? 0);
                maxWidths[2] = Math.Max(maxWidths[2], row.WorkItem?.Title?.Length ?? 0);
                maxWidths[3] = Math.Max(maxWidths[3], row.CreatedAt.ToString("yyyy-MM-dd").Length);
            }

            return maxWidths;
        }

        /// <summary>
        /// Prints a row of data in the progress summary table.
        /// </summary>
        /// <param name="row">The array of data for the row.</param>
        /// <param name="maxWidths">The array of maximum column widths.</param>
        /// <param name="isHeader">A flag indicating whether the row is a header row.</param>
        /// <returns>A formatted string representing the row.</returns>
        public string PrintRow(string[] row, int[] maxWidths, bool isHeader = false)
        {
            var formattedRow = new string[row.Length];
            for (int i = 0; i < row.Length; i++)
            {
                if (isHeader)
                {
                    // Center header by padding manually
                    formattedRow[i] = CenterText(row[i], maxWidths[i]);
                }
                else
                {
                    // Left-align for data rows
                    formattedRow[i] = string.Format($"{{0,-{maxWidths[i]}}}", row[i]);
                }
            }
            return "| " + string.Join(" | ", formattedRow) + " |";
        }

        /// <summary>
        /// Prints a separator line in the progress summary table.
        /// </summary>
        /// <param name="maxWidths">The array of maximum column widths.</param>
        /// <returns>A formatted string representing the separator line.</returns>
        public string PrintSeparator(int[] maxWidths)
        {
            // Use String.Format to simplify separator generation
            var separatorParts = new string[maxWidths.Length];
            for (int i = 0; i < maxWidths.Length; i++)
            {
                separatorParts[i] = new string('-', maxWidths[i] + 2);
            }
            return "+" + string.Join("+", separatorParts) + "+";
        }

        /// <summary>
        /// Centers the text within a given width.
        /// </summary>
        /// <param name="text">The text to be centered.</param>
        /// <param name="width">The width of the text.</param>
        /// <returns>A string representing the centered text.</returns>
        public string CenterText(string text, int width)
        {
            int padding = (width - text.Length) / 2;
            return text.PadLeft(text.Length + padding).PadRight(width);
        }
    }
}
