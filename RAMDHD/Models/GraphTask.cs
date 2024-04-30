using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Models
{
    public class GraphTask
    {
        public int Id { get; set; }
        private string title;
        public string Title
        {
            get => title;
            set => title = value ?? "Add new graph task"; // Set default if null is assigned
        }
        public string Activity1 { get; set; }
        public string Activity2 { get; set; }
        public string Activity3 { get; set; }
        public string Activity4 { get; set; }
        public int ProcrastinationActivity { get; set; } // Stores the activity number marked as procrastination

        public GraphTask()
        {
            Title = "Add new graph task"; // Default value for Title
        }
    }

}
