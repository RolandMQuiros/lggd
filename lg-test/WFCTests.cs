using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using LostGen;

namespace LostGen.Tests
{
    public class WFCTests {
        private bool ArrayCompare2D(int[,] first, int[,] second) {
            return first.Rank == second.Rank && // Check if both have same number of dimensions
                Enumerable.Range(0, first.Rank) // Check if both have same length in every dimension
                    .All(dimension => first.GetLength(dimension) == second.GetLength(dimension)) &&
                // If the above are true, we can flatten both into IEnumerables and compare by element
                first.Cast<int>().SequenceEqual(second.Cast<int>());
        }

        [Test]
        public void CreatePatternsFromSample() {
            int[,] sample = new int[,] {
                { 0, 1, 1, 0 },
                { 0, 1, 1, 0 },
                { 0, 1, 1, 1 },
                { 1, 1, 1, 1 }
            };

            var samples = LostGen.WFC.PatternsFromSample(sample, 2);
            
            Assert.IsTrue(
                samples.Any(
                    s => ArrayCompare2D(s, new int[,] { { 0, 1 },
                                                        { 0, 1 } })
                )
            );

            Assert.IsTrue(
                samples.Any(
                    s => ArrayCompare2D(s, new int[,] { { 1, 0 },
                                                        { 1, 0 } })
                )
            );

            Assert.IsTrue(
                samples.Any(
                    s => ArrayCompare2D(s, new int[,] { { 0, 1 },
                                                        { 1, 1 } })
                )
            );

            Assert.IsTrue(
                samples.Any(
                    s => ArrayCompare2D(s, new int[,] { { 1, 1 },
                                                        { 1, 1 } })
                )
            );
        }
    }
}