using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanClient
{
    class HangmanFigure
    {
        enum HangmanState { First, Second, Third, Fourth, Fifth, Sixth, Final }

        HangmanState state;

        /// <summary>
        /// Constructor.
        /// </summary>
        public HangmanFigure()
        {
            state = HangmanState.First;
        }

        /// <summary>
        /// Draws the hangman figure, according to its state, in the console window.
        /// </summary>
        public void Draw()
        {
            Console.Clear();

            switch (state)
            {
                case HangmanState.First:
                    Console.WriteLine("o------o");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("o---------o");
                    break;
                case HangmanState.Second:
                    Console.WriteLine("o------o");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      o");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("o---------o");
                    break;
                case HangmanState.Third:
                    Console.WriteLine("o------o");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      o");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("o---------o");
                    break;
                case HangmanState.Fourth:
                    Console.WriteLine("o------o");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      o");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|     /|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("o---------o");
                    break;
                case HangmanState.Fifth:
                    Console.WriteLine("o------o");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      o");
                    Console.WriteLine("|      |/");
                    Console.WriteLine("|     /|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("o---------o");
                    break;
                case HangmanState.Sixth:
                    Console.WriteLine("o------o");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      o");
                    Console.WriteLine("|      |/");
                    Console.WriteLine("|     /|");
                    Console.WriteLine("|     /");
                    Console.WriteLine("|");
                    Console.WriteLine("o---------o");
                    break;
                case HangmanState.Final:
                    Console.WriteLine("o------o");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      |");
                    Console.WriteLine("|      o");
                    Console.WriteLine("|      |/");
                    Console.WriteLine("|     /|");
                    Console.WriteLine("|     / \\");
                    Console.WriteLine("|");
                    Console.WriteLine("o---------o");
                    break;
            }
        }

        /// <summary>
        /// Sets the hangman figure's state according to the number of wrong guesses.
        /// </summary>
        /// <param name="wrongGuesses">The number of times player has guessed wrong.</param>
        public void SetState(int wrongGuesses)
        {
            switch (wrongGuesses)
            {
                case 0:
                    state = HangmanState.First;
                    break;
                case 1:
                    state = HangmanState.Second;
                    break;
                case 2:
                    state = HangmanState.Third;
                    break;
                case 3:
                    state = HangmanState.Fourth;
                    break;
                case 4:
                    state = HangmanState.Fifth;
                    break;
                case 5:
                    state = HangmanState.Sixth;
                    break;
                case 6:
                    state = HangmanState.Final;
                    break;
                default:
                    break;
            }
        }
    }
}
