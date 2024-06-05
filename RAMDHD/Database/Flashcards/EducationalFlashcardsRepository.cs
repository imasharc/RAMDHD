using RAMDHD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Database.Flashcards
{
    class EducationalFlashcardsRepository
    {
        public static List<Flashcard> GetFlashcards()
        {
            return new List<Flashcard>
            {
                new Flashcard { Question = "What is procrastination?", Answer = "Procrastination is NOT laziness!\n\n" +
                "Procrastination is the intentional delay of important tasks in favor of less significant, often more enjoyable activities, often due to feeling overwhelmed." },
                new Flashcard { Question = "How to cooperate with procrastination?", Answer = "The way to deal with this is to divide tasks into smaller, more manageable steps.\n" +
                "Thanks to such clearly defined activity boundaries, it is easier to see the progress of the task." },
                new Flashcard { Question = "What does ADHD stand for?", Answer = "Attention Deficit Hyperactivity Disorder" },
                new Flashcard { Question = "What are the 3 established ADHD types?", Answer = "1. Predominantly Inattentive\n" +
                "2. Predominantly Hyperactive-Impulsive\n" +
                "3. Combined" },
                new Flashcard { Question = "What are some common symptoms of inattentive ADHD?", Answer = "- Difficulty sustaining attention in tasks or play activities\n" +
                "- Difficulty organizing tasks and activities\n" +
                "- Frequent careless mistakes in schoolwork, work, or other activities" },
                new Flashcard { Question = "What are some common symptoms of hyperactive-impulsive ADHD?", Answer = "- Often fidgets with or taps hands or feet, or squirms in seat\n" +
                "- Often has difficulty waiting their turn\n" +
                "- Often unable to play or engage in leisure activities quietly" },
                new Flashcard { Question = "How can combined presentation of ADHD be described?", Answer = "Combined ADHD type is often described as including a mix of symptoms from both the inattentive and hyperactive-impulsive categories." },
                // Add more flashcards as needed
            };
        }
    }
}
