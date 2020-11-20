using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace DailyProgrammer_378E
{
    class Program
    {
        static void Main(string[] args)
        {
            TestRemoveZeroes();
            TestSortDescending();
            TestLengthCheck();
            TestFrontElimination();

            TestHavelHakimi();

            Console.WriteLine("Reached end of Main. Press enter to exit.");
            Console.ReadLine();
        }

        // Optional Warmup 1: Eliminating 0's
        // Takes in answers in the form of a List<uint>, removes any zeroes, and returns the result.
        private static List<uint> RemoveZeroes(List<uint> answers)
        {
            return answers.Where(answer => answer != 0).ToList();
        }

        // Optional Warmup 2: Descending Sort
        // Takes in answers in the form of a List<uint> and sorts them from largest to smallest.
        private static List<uint> SortDescending(List<uint> answers)
        {
            return answers.OrderByDescending(answer => answer).ToList();
        }

        // Optional Warmup 3: Length Check
        // Given a number and a sequence of answers, return true if the number is greater than the length of the sequence, otherwise return false.
        private static bool LengthCheck(uint number, List<uint> answers)
        {
            return number > answers.Count;
        }

        // Optional Warmup 4: Front Elimination
        // Given a number N and a sequence of answers, subtract 1 from each of the first N answers in the sequence.
        // You may assume that N is greater than 0 and no greater than the length of the sequence.
        private static List<uint> FrontElimination(uint number, List<uint> answers)
        {
            List<uint> frontEliminatedAnswers = new List<uint>(answers);    // Unsure if we need to leave the original list alone. Better safe than sorry.

            for (int i=0; i<number; i++)
            {
                frontEliminatedAnswers[i] -= 1;
            }

            return frontEliminatedAnswers;
        }

        // ACTUAL EXERCISE: Havel-Hakimi Algorithm
        // Given a sequence of answers, return true if they are consistent or false if the answers are inconsistent.
        private static bool HavelHakimi(List<uint> answers)
        {
            // The code below should be guaranteed to eventually return true or false, so a generic infinite loop should be okay here.
            while (true)
            {
                List<uint> answersWithoutZeroes = RemoveZeroes(answers);

                // Return true if no more answers.
                if (!answersWithoutZeroes.Any())
                    return true;

                List<uint> answersSortedDescending = SortDescending(answersWithoutZeroes);

                // Remove the first answer from the sequence and call it N.
                uint N = answersSortedDescending[0];
                answersSortedDescending.RemoveAt(0);

                // If N is greater than the length of this new sequence, return false.
                if (LengthCheck(N, answersSortedDescending))
                    return false;

                // Subtract 1 from each of the first N elements of the new sequence.
                // Store this value in 'answers' for the next iteration of the loop.
                answers = FrontElimination(N, answersSortedDescending);
            }
        }



        // TEST METHODS

        // Run provided test examples for Warmup 1 (RemoveZeroes).
        private static void TestRemoveZeroes()
        {
            List<uint> test1 = new List<uint> { 5, 3, 0, 2, 6, 2, 0, 7, 2, 5 };
            List<uint> result1 = new List<uint> { 5, 3, 2, 6, 2, 7, 2, 5 };
            Debug.Assert(RemoveZeroes(test1).SequenceEqual(result1));

            List<uint> test2 = new List<uint> { 4, 0, 0, 1, 3 };
            List<uint> result2 = new List<uint> { 4, 1, 3 };
            Debug.Assert(RemoveZeroes(test2).SequenceEqual(result2));

            List<uint> test3 = new List<uint> { 1, 2, 3 };
            Debug.Assert(RemoveZeroes(test3).SequenceEqual(test3)); // This one should be the same.

            List<uint> test4 = new List<uint> { 0, 0, 0 };
            List<uint> result4 = new List<uint> { };
            Debug.Assert(RemoveZeroes(test4).SequenceEqual(result4));

            List<uint> test5 = new List<uint>();                    // Empty list.
            Debug.Assert(RemoveZeroes(test5).SequenceEqual(test5)); // This one should be the same.
        }

        // Run provided test examples for Warmup 2 (SortDescending).
        private static void TestSortDescending()
        {
            List<uint> test1 = new List<uint> { 5, 1, 3, 4, 2 };
            List<uint> result1 = new List<uint> { 5, 4, 3, 2, 1 };
            Debug.Assert(SortDescending(test1).SequenceEqual(result1));

            List<uint> test2 = new List<uint> { 0, 0, 0, 4, 0 };
            List<uint> result2 = new List<uint> { 4, 0, 0, 0, 0 };
            Debug.Assert(SortDescending(test2).SequenceEqual(result2));

            List<uint> test3 = new List<uint> { 1 };
            Debug.Assert(SortDescending(test3).SequenceEqual(test3));   // This one should be the same.

            List<uint> test4 = new List<uint> { };
            Debug.Assert(SortDescending(test4).SequenceEqual(test4));   // This one should be the same.
        }

        // Run provided test examples for Warmup 3 (LengthCheck).
        private static void TestLengthCheck()
        {
            Debug.Assert(false == LengthCheck(7, new List<uint> { 6, 5, 5, 3, 2, 2, 2 }));
            Debug.Assert(false == LengthCheck(5, new List<uint> { 5, 5, 5, 5, 5 }));
            Debug.Assert(true == LengthCheck(5, new List<uint> { 5, 5, 5, 5 }));
            Debug.Assert(true == LengthCheck(3, new List<uint> { 1, 1 }));
            Debug.Assert(true == LengthCheck(1, new List<uint>()));
            Debug.Assert(false == LengthCheck(0, new List<uint>()));
        }

        // Run provided test examples for Warmup 4 (FrontElimination).
        private static void TestFrontElimination()
        {
            List<uint> test1 = new List<uint> { 5, 4, 3, 2, 1 };
            List<uint> result1 = new List<uint> { 4, 3, 2, 1, 1 };
            Debug.Assert(FrontElimination(4, test1).SequenceEqual(result1));

            List<uint> test2 = new List<uint> { 14, 13, 13, 13, 12, 10, 8, 8, 7, 7, 6, 6, 4, 4, 2 };
            List<uint> result2 = new List<uint> { 13, 12, 12, 12, 11, 9, 7, 7, 6, 6, 5, 6, 4, 4, 2 };
            Debug.Assert(FrontElimination(11, test2).SequenceEqual(result2));

            List<uint> test3 = new List<uint> { 10, 10, 10 };
            List<uint> result3 = new List<uint> { 9, 10, 10};
            Debug.Assert(FrontElimination(1, test3).SequenceEqual(result3));

            List<uint> test4 = new List<uint> { 10, 10, 10 };
            List<uint> result4 = new List<uint> { 9, 9, 9 };
            Debug.Assert(FrontElimination(3, test4).SequenceEqual(result4));

            List<uint> test5 = new List<uint> { 1 };
            List<uint> result5 = new List<uint> { 0 };
            Debug.Assert(FrontElimination(1, test5).SequenceEqual(result5));
        }

        // Run provided test examples for actual exercise (HavelHakimi).
        private static void TestHavelHakimi()
        {
            List<uint> test1 = new List<uint> { 5, 3, 0, 2, 6, 2, 0, 7, 2, 5 };
            Debug.Assert(false == HavelHakimi(test1));

            List<uint> test2 = new List<uint> { 4, 2, 0, 1, 5, 0 };
            Debug.Assert(false == HavelHakimi(test2));

            List<uint> test3 = new List<uint> { 3, 1, 2, 3, 1, 0 };
            Debug.Assert(true == HavelHakimi(test3));

            List<uint> test4 = new List<uint> { 16, 9, 9, 15, 9, 7, 9, 11, 17, 11, 4, 9, 12, 14, 14, 12, 17, 0, 3, 16 };
            Debug.Assert(true == HavelHakimi(test4));

            List<uint> test5 = new List<uint> { 14, 10, 17, 13, 4, 8, 6, 7, 13, 13, 17, 18, 8, 17, 2, 14, 6, 4, 7, 12 };
            Debug.Assert(true == HavelHakimi(test5));

            List<uint> test6 = new List<uint> { 15, 18, 6, 13, 12, 4, 4, 14, 1, 6, 18, 2, 6, 16, 0, 9, 10, 7, 12, 3 };
            Debug.Assert(false == HavelHakimi(test6));

            List<uint> test7 = new List<uint> { 6, 0, 10, 10, 10, 5, 8, 3, 0, 14, 16, 2, 13, 1, 2, 13, 6, 15, 5, 1 };
            Debug.Assert(false == HavelHakimi(test7));

            List<uint> test8 = new List<uint> { 2, 2, 0 };
            Debug.Assert(false == HavelHakimi(test8));

            List<uint> test9 = new List<uint> { 3, 2, 1 };
            Debug.Assert(false == HavelHakimi(test9));

            List<uint> test10 = new List<uint> { 1, 1 };
            Debug.Assert(true == HavelHakimi(test10));

            List<uint> test11 = new List<uint> { 1 };
            Debug.Assert(false == HavelHakimi(test11));

            List<uint> test12 = new List<uint> { };
            Debug.Assert(true == HavelHakimi(test12));
        }
    }
}
