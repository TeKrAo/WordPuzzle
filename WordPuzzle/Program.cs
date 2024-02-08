using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPuzzle
{
    using System;

    public class Result
    {
        public bool Contains { get; set; }
        public int? Row { get; set; }
        public int? Column { get; set; }
        public Direction WordDirection { get; set; }

        public Result()
        {
            Contains = false;
            Row = null;
            Column = null;
            WordDirection = Direction.None;
        }
    }

    public enum Direction
    {
        None,
        Horizontal,
        Vertical
    }

    public class WordSearchPuzzle
    {
        private char[,] puzzle;
        private int rows;
        private int columns;

        public WordSearchPuzzle(char[,] puzzle)
        {
            this.puzzle = puzzle;
            this.rows = puzzle.GetLength(0);
            this.columns = puzzle.GetLength(1);
        }

        public Result SearchWord(string word)
        {
            Result result = new Result();

            // Search horizontally
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j <= columns - word.Length; j++)
                {
                    if (CheckHorizontal(i, j, word))
                    {
                        result.Contains = true;
                        result.Row = i;
                        result.Column = j;
                        result.WordDirection = Direction.Horizontal;
                        return result;
                    }
                }
            }

            // Search vertically
            for (int i = 0; i <= rows - word.Length; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (CheckVertical(i, j, word))
                    {
                        result.Contains = true;
                        result.Row = i;
                        result.Column = j;
                        result.WordDirection = Direction.Vertical;
                        return result;
                    }
                }
            }

            return result;
        }

        private bool CheckHorizontal(int row, int col, string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (puzzle[row, col + i] != word[i])
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckVertical(int row, int col, string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (puzzle[row + i, col] != word[i])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            char[,] puzzle = {
            {'s', 't', 'y', 'l', 'e', 'a', 'b', 'c', 'd', 'e', 'f'},
            {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k'},
            {'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p'},
            {'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u'},
            {'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'},
            {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k'},
            {'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v'},
            {'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l'},
            {'m', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w'},
            {'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm'},
            {'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x'}
        };

            WordSearchPuzzle wordSearchPuzzle = new WordSearchPuzzle(puzzle);

            Console.WriteLine("Word Search Puzzle:");
            DisplayPuzzle(puzzle);

            string input;
            do
            {
                Console.WriteLine("Enter a word to search or type 'quit' to exit:");
                input = Console.ReadLine().Trim();
                if (input.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    break;

                Result result = wordSearchPuzzle.SearchWord(input);
                Console.WriteLine($"Word: {input}");
                if (result.Contains)
                {
                    Console.WriteLine($"Found at Row: {result.Row}, Column: {result.Column}, Direction: {result.WordDirection}");
                }
                else
                {
                    Console.WriteLine("Not found.");
                }
            } while (true);
        }

        private static void DisplayPuzzle(char[,] puzzle)
        {
            int rows = puzzle.GetLength(0);
            int columns = puzzle.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(puzzle[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

