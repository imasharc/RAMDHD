using RAMDHD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Database.Flashcards
{
    class AffirmationsRepository
    {
        public static List<Flashcard> GetFlashcards()
        {
            return new List<Flashcard>
            {
                new Flashcard { Question = "I embrace my unique way of thinking and know that it is a strength." },
                new Flashcard { Question = "I am patient with myself as I navigate through my tasks and challenges." },
                new Flashcard { Question = "I have the power to focus and accomplish my goals, one step at a time." },
                new Flashcard { Question = "I forgive myself for past mistakes and celebrate my progress." },
                new Flashcard { Question = "I am creative and capable, and my ADHD is a part of what makes me special." },
                new Flashcard { Question = "I am resilient and can overcome any obstacles that come my way." },
                new Flashcard { Question = "I celebrate small victories and acknowledge my efforts each day." },
                new Flashcard { Question = "I am in control of my actions and can direct my energy positively." },
                new Flashcard { Question = "I am worthy of understanding, patience, and compassion from myself and others." },

                // Add more flashcards as needed
            };
        }
    }
}
