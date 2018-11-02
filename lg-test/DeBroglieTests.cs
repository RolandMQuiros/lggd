using NUnit.Framework;
using System;

using DeBroglie;
using DeBroglie.Models;
using DeBroglie.Topo;

namespace LostGen.Tests {
    public class DeBroglieTest {
        [Test]
        public void QuickStartTest() {
            ITopoArray<char> sample = TopoArray.Create(new[,]
            {
                { '_', '_', '_'},
                { '_', '*', '_'},
                { '_', '_', '_'},
            }, periodic: false);
            // Specify the model used for generation
            var model = new AdjacentModel(sample.ToTiles());
            // Set the output dimensions
            var topology = new Topology(10, 10, periodic: false);
            // Acturally run the algorithm
            var propagator = new TilePropagator(model, topology);
            var status = propagator.Run();
            if (status != Resolution.Decided) throw new Exception("Undecided");
            var output = propagator.ToValueArray<char>();
            // Display the results
            for (var y = 0; y < 10; y++)
            {
                for (var x = 0; x < 10; x++)
                {
                    System.Console.Write(output.Get(x, y));
                }
                System.Console.WriteLine();
            }
        }
    }
}