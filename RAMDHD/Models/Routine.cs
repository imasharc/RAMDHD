namespace RAMDHD.Models
{
    public class Routine
    {
        public int Id { get; set; }
        private string title;
        public string Title
        {
            get => title;
            set => title = value ?? "Add new routine"; // Set default if null is assigned
        }
        public string Description { get; set; }
        public string Step1 { get; set; }
        public string Step2 { get; set; }
        public string Step3 { get; set; }
        public string Step4 { get; set; }


        public Routine()
        {
            Title = "Add new routine"; // Default value for Title
        }
    }
}
