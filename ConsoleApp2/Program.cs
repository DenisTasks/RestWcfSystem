using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExpressionVisualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<int, int, int>> expression = (a, b) => a + b;
            Console.WriteLine(expression);

            Expression<Func<int, bool>> expr = x => x == 10;

            VisualizerDevelopmentHost host = new VisualizerDevelopmentHost(expr,
                typeof(ExpressionTreeVisualizer),
                typeof(ExpressionTreeVisualizerObjectSource));
            host.ShowVisualizer();

            Console.ReadKey();
        }
    }
}
