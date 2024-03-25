using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Models.ScreeningTest
{
    public class Question
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public int SelectedAnswerScore { get; set; } = -1; // -1 means no answer selected
    }
}
