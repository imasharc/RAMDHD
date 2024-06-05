using RAMDHD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.Database.Flashcards
{
    class SampleChallengesFlashcardsRepository
    {
        public static List<Flashcard> GetFlashcards()
        {
            return new List<Flashcard>
            {
                new Flashcard { Question = "I struggle to stay focused...", Answer = "Set a timer for 5 minutes and work on a task without distraction.\n\nReward yourself afterwards!" },
                new Flashcard { Question = "I often feel restless...", Answer = "Get up and do 60 seconds of push-ups/squats/jumping-jacks" },
                new Flashcard { Question = "I have difficulty starting tasks...", Answer = "Try performing a task for just 2 minutes. Observe yourself afterwards." },
                new Flashcard { Question = "I stuggle to start a conversation...", Answer = "Go to a local bakery and make a small talk with cashier eg. comment on the weather." },
                new Flashcard { Question = "I struggle with remembering what people have asked me for...", Answer = "As soon as you get asked to do something, write it down in the notes." },
                new Flashcard { Question = "I struggle with biting my nails...", Answer = "Find a simple fidget toy eg. a ruber band or a keychain. You can buy one as well." },
                new Flashcard { Question = "I feel down when I am not productive during the day...", Answer = "Whatever happens during a day, perform some leisure physical activity in the afternoon. Try going for a bike or a walk to wind out after a hard day." },
                // Add more flashcards as needed
            };
        }
    }
}
