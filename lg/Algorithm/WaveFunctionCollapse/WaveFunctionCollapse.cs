using System.Collections.Generic;

namespace LostGen {
    public static class WFC {
        /*
            defn Run():
                PatternsFromSample()
                BuildPropagator()
                Loop until finished:
                    Observe()
                    Propagate()
                OutputObservations()

            defn Observe(coefficient_matrix):
                FindLowestEntropy()
                If there is a contradiction, throw an error and quit
                If all cells are at entropy 0, processing is complete:
                    Return CollapsedObservations()
                Else:
                    Choose a pattern by a random sample, weighted by the
                        pattern frequency in the source data
                    Set the boolean array in this cell to false, except
                        for the chosen pattern

            defn FindLowestEntropy(coefficient_matrix):
                Return the cell that has the lowest greater-than-zero
                    entropy, defined as:
                    A cell with one valid pattern has 0 entropy
                    A cell with no valid patterns is a contradiction
                    Else: the entropy is based on the sum of the frequency
                        that the patterns appear in the source data, plus
                        Use some random noise to break ties and
                            near-ties.
            
            defn OutputObservations(coefficient_matrix):
                For each cell:
                    Set observed value to the average of the color value
                        of this cell in the pattern for the remaining
                        valid patterns
                    Return the observed values as an output image

         */
        
        public static IEnumerable<int[,]> PatternsFromSample(int[,] sample, int size) {
            // Sweep across the sample in increments of size * size
            for (int y = 0; y < sample.GetLength(0); y += size) {
                for (int x = 0; x < sample.GetLength(1); x += size) {
                    // Create a size * size pattern array
                    int[,] pattern = new int[size, size];
                    for (int py = 0; py < size; py++) {
                        for (int px = 0; px < size; px++) {
                            pattern[py, px] = sample[y + py, x + px];
                        }
                    }
                    yield return pattern;
                }
            }
        }

        
    }
}