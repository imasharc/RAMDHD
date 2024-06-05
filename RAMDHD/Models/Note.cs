namespace RAMDHD.Models
{
    public class Note
    {
        public int Id { get; set; }
        private string headline;
        public string Headline
        {
            get => headline;
            set => headline = value ?? "Add new note"; // Set default if null is assigned
        }
        public string Description { get; set; }

        public Note()
        {
            Headline = "Add new note"; // Default value for Headline
        }
    }
}
