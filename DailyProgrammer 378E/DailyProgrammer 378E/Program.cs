using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace DailyProgrammer_378E
{
    public static class Program
    {
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
        [Fact]
        public static void TestRemoveZeroes()
        {
            // Arrange
            List<uint> test1 = new List<uint> { 5, 3, 0, 2, 6, 2, 0, 7, 2, 5 };
            List<uint> expectedResult1 = new List<uint> { 5, 3, 2, 6, 2, 7, 2, 5 };

            List<uint> test2 = new List<uint> { 4, 0, 0, 1, 3 };
            List<uint> expectedResult2 = new List<uint> { 4, 1, 3 };

            List<uint> test3 = new List<uint> { 1, 2, 3 };
            List<uint> expectedResult3 = test3;     // This one should be the same.

            List<uint> test4 = new List<uint> { 0, 0, 0 };
            List<uint> expectedResult4 = new List<uint> { };

            List<uint> test5 = new List<uint>();    // Empty list.
            List<uint> expectedResult5 = test5;     // This one should be the same.

            // Act
            List<uint> result1 = RemoveZeroes(test1);
            List<uint> result2 = RemoveZeroes(test2);
            List<uint> result3 = RemoveZeroes(test3);
            List<uint> result4 = RemoveZeroes(test4);
            List<uint> result5 = RemoveZeroes(test5);

            // Assert
            Assert.True(result1.SequenceEqual(expectedResult1));
            Assert.True(result2.SequenceEqual(expectedResult2));
            Assert.True(result3.SequenceEqual(expectedResult3));
            Assert.True(result4.SequenceEqual(expectedResult4));
            Assert.True(result5.SequenceEqual(expectedResult5));
        }

        // Run provided test examples for Warmup 2 (SortDescending).
        [Fact]
        public static void TestSortDescending()
        {
            // Arrange
            List<uint> test1 = new List<uint> { 5, 1, 3, 4, 2 };
            List<uint> expectedResult1 = new List<uint> { 5, 4, 3, 2, 1 };

            List<uint> test2 = new List<uint> { 0, 0, 0, 4, 0 };
            List<uint> expectedResult2 = new List<uint> { 4, 0, 0, 0, 0 };

            List<uint> test3 = new List<uint> { 1 };
            List<uint> expectedResult3 = test3;     // This one should be the same.

            List<uint> test4 = new List<uint>();
            List<uint> expectedResult4 = test4;     // This one should be the same.

            // Act
            List<uint> result1 = SortDescending(test1);
            List<uint> result2 = SortDescending(test2);
            List<uint> result3 = SortDescending(test3);
            List<uint> result4 = SortDescending(test4);

            // Assert
            Assert.True(result1.SequenceEqual(expectedResult1));
            Assert.True(result2.SequenceEqual(expectedResult2));
            Assert.True(result3.SequenceEqual(expectedResult3));
            Assert.True(result4.SequenceEqual(expectedResult4));
        }

        // Run provided test examples for Warmup 3 (LengthCheck).
        [Fact]
        public static void TestLengthCheck()
        {
            // Arrange
            List<uint> test1 = new List<uint> { 6, 5, 5, 3, 2, 2, 2 };
            List<uint> test2 = new List<uint> { 5, 5, 5, 5, 5 };
            List<uint> test3 = new List<uint> { 5, 5, 5, 5 };
            List<uint> test4 = new List<uint> { 1, 1 };
            List<uint> test5 = new List<uint>();
            List<uint> test6 = new List<uint>();

            // Act
            bool result1 = LengthCheck(7, test1);
            bool result2 = LengthCheck(5, test2);
            bool result3 = LengthCheck(5, test3);
            bool result4 = LengthCheck(3, test4);
            bool result5 = LengthCheck(1, test5);
            bool result6 = LengthCheck(0, test6);

            // Assert
            Assert.False(result1);
            Assert.False(result2);
            Assert.True(result3);
            Assert.True(result4);
            Assert.True(result5);
            Assert.False(result6);
        }

        // Run provided test examples for Warmup 4 (FrontElimination).
        [Fact]
        public static void TestFrontElimination()
        {
            // Arrange
            List<uint> test1 = new List<uint> { 5, 4, 3, 2, 1 };
            List<uint> expectedResult1 = new List<uint> { 4, 3, 2, 1, 1 };

            List<uint> test2 = new List<uint> { 14, 13, 13, 13, 12, 10, 8, 8, 7, 7, 6, 6, 4, 4, 2 };
            List<uint> expectedResult2 = new List<uint> { 13, 12, 12, 12, 11, 9, 7, 7, 6, 6, 5, 6, 4, 4, 2 };

            List<uint> test3 = new List<uint> { 10, 10, 10 };
            List<uint> expectedResult3 = new List<uint> { 9, 10, 10 };

            List<uint> test4 = new List<uint> { 10, 10, 10 };
            List<uint> expectedResult4 = new List<uint> { 9, 9, 9 };

            List<uint> test5 = new List<uint> { 1 };
            List<uint> expectedResult5 = new List<uint> { 0 };

            // Act
            List<uint> result1 = FrontElimination(4, test1);
            List<uint> result2 = FrontElimination(11, test2);
            List<uint> result3 = FrontElimination(1, test3);
            List<uint> result4 = FrontElimination(3, test4);
            List<uint> result5 = FrontElimination(1, test5);

            // Assert
            Assert.True(result1.SequenceEqual(expectedResult1));
            Assert.True(result2.SequenceEqual(expectedResult2));
            Assert.True(result3.SequenceEqual(expectedResult3));
            Assert.True(result4.SequenceEqual(expectedResult4));
            Assert.True(result5.SequenceEqual(expectedResult5));
        }

        // Run provided test examples for actual exercise (HavelHakimi).
        [Fact]
        public static void TestHavelHakimi()
        {
            // Arrange
            List<uint> test1 = new List<uint> { 5, 3, 0, 2, 6, 2, 0, 7, 2, 5 };
            List<uint> test2 = new List<uint> { 4, 2, 0, 1, 5, 0 };
            List<uint> test3 = new List<uint> { 3, 1, 2, 3, 1, 0 };
            List<uint> test4 = new List<uint> { 16, 9, 9, 15, 9, 7, 9, 11, 17, 11, 4, 9, 12, 14, 14, 12, 17, 0, 3, 16 };
            List<uint> test5 = new List<uint> { 14, 10, 17, 13, 4, 8, 6, 7, 13, 13, 17, 18, 8, 17, 2, 14, 6, 4, 7, 12 };
            List<uint> test6 = new List<uint> { 15, 18, 6, 13, 12, 4, 4, 14, 1, 6, 18, 2, 6, 16, 0, 9, 10, 7, 12, 3 };
            List<uint> test7 = new List<uint> { 6, 0, 10, 10, 10, 5, 8, 3, 0, 14, 16, 2, 13, 1, 2, 13, 6, 15, 5, 1 };
            List<uint> test8 = new List<uint> { 2, 2, 0 };
            List<uint> test9 = new List<uint> { 3, 2, 1 };
            List<uint> test10 = new List<uint> { 1, 1 };
            List<uint> test11 = new List<uint> { 1 };
            List<uint> test12 = new List<uint> { };

            // Act
            bool result1 = HavelHakimi(test1);
            bool result2 = HavelHakimi(test2);
            bool result3 = HavelHakimi(test3);
            bool result4 = HavelHakimi(test4);
            bool result5 = HavelHakimi(test5);
            bool result6 = HavelHakimi(test6);
            bool result7 = HavelHakimi(test7);
            bool result8 = HavelHakimi(test8);
            bool result9 = HavelHakimi(test9);
            bool result10 = HavelHakimi(test10);
            bool result11 = HavelHakimi(test11);
            bool result12 = HavelHakimi(test12);

            // Assert
            Assert.False(result1);
            Assert.False(result2);
            Assert.True(result3);
            Assert.True(result4);
            Assert.True(result5);
            Assert.False(result6);
            Assert.False(result7);
            Assert.False(result8);
            Assert.False(result9);
            Assert.True(result10);
            Assert.False(result11);
            Assert.True(result12);
        }
    }
}
