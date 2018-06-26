using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExpressionVisualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace ExpressionTrees
{
    public class ExExpressinVisitor : ExpressionVisitor
    {
        public int indent = 0;

        public override Expression Visit(Expression node)
        {
            if (node == null)
            {
                return base.Visit(node);
            }

            Console.WriteLine($"{new String(' ', indent * 4)}{node.NodeType} - {node.GetType()}");

            indent++;
            Expression result = base.Visit(node);
            indent--;

            return result;
        }
    }

    public class IncrementDecrementExpressinVisitor : ExpressionVisitor
    {
        public int indent = 0;

        public override Expression Visit(Expression node)
        {
            if (node == null)
            {
                return base.Visit(node);
            }

            indent++;
            Expression result = base.Visit(node);
            indent--;

            return result;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            ConstantExpression constant;
            if (node.NodeType == ExpressionType.Add && node.Right.NodeType == ExpressionType.Constant)
            {
                constant = (ConstantExpression)node.Right;
                if ((int)constant.Value == 1)
                {
                    Expression incremented = Expression.Increment(node.Left);
                    return incremented;
                }
            }

            if (node.NodeType == ExpressionType.Subtract && node.Right.NodeType == ExpressionType.Constant)
            {
                constant = (ConstantExpression)node.Right;
                if ((int)constant.Value == 1)
                {
                    Expression decremented = Expression.Decrement(node.Left);
                    return decremented;
                }
            }

            Expression result = base.VisitBinary(node);
            return result;
        }
    }

    public class ReplaceVisitor : ExpressionVisitor
    {
        public int indent = 0;
        private Dictionary<string, Expression> _replaceDictionary;

        public ReplaceVisitor()
        {
            _replaceDictionary = new Dictionary<string, Expression>();
        }

        public Expression Start(Expression incomingNode, Dictionary<string, Expression> incomingDictionary)
        {
            _replaceDictionary = incomingDictionary;
            return Visit(incomingNode);
        }

        public override Expression Visit(Expression node)
        {
            if (node == null)
            {
                return base.Visit(node);
            }
            indent++;
            Expression result = base.Visit(node);
            indent--;

            return result;
        }

        protected override Expression VisitParameter(ParameterExpression incomingParameter)
        {
            if (_replaceDictionary.ContainsKey(incomingParameter.Name))
            {
                Expression replacedParameter = _replaceDictionary[incomingParameter.Name];
                return replacedParameter;
            }

            Expression result = base.VisitParameter(incomingParameter);
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ParameterExpression x = Expression.Parameter(typeof(int), "x");
            Expression incrementedOne = Expression.Constant(1, typeof(int));
            Expression incrementedTotal = Expression.Add(x, incrementedOne);
            Expression lessThan = Expression.Constant(100, typeof(int));
            Expression totalLeft = Expression.LessThan(incrementedTotal, lessThan);

            ParameterExpression y = Expression.Parameter(typeof(int), "y");
            Expression decrementedOne = Expression.Constant(1, typeof(int));
            Expression decrementedTotal = Expression.Subtract(y, decrementedOne);
            Expression totalLeft2 = Expression.GreaterThan(decrementedTotal, lessThan);


            Expression totalAnd = Expression.And(totalLeft, totalLeft2);

            Expression previous = new ExExpressinVisitor().Visit(totalAnd);
            Console.WriteLine(Expression.Lambda<Func<int, int, bool>>(previous, x, y).Body);
            Console.WriteLine(Expression.Lambda<Func<int, int, bool>>(previous, x, y).Compile()(98, 102));
            Console.WriteLine(Expression.Lambda<Func<int, int, bool>>(previous, x, y).Compile()(99, 101));
            Console.WriteLine("______________________________________________________________________");
            //VisualizerDevelopmentHost host = new VisualizerDevelopmentHost(previous,
            //    typeof(ExpressionTreeVisualizer),
            //    typeof(ExpressionTreeVisualizerObjectSource));
            //host.ShowVisualizer();

            // INCREMENT VISITOR
            Expression final = new IncrementDecrementExpressinVisitor().Visit(totalAnd);
            Expression afterIncrement = new ExExpressinVisitor().Visit(final);
            Console.WriteLine(Expression.Lambda<Func<int, int, bool>>(afterIncrement, x, y).Body);
            Console.WriteLine(Expression.Lambda<Func<int, int, bool>>(afterIncrement, x, y).Compile()(98, 102));
            Console.WriteLine(Expression.Lambda<Func<int, int, bool>>(afterIncrement, x, y).Compile()(99, 101));

            Dictionary<string, Expression> replaceDictionary = new Dictionary<string, Expression>();
            Expression replaceConstant = Expression.Constant(102, typeof(int));
            replaceDictionary.Add("y", replaceConstant);
            replaceDictionary.Add("x", replaceConstant);

            // REPLACE VISITOR
            Expression replace = new ReplaceVisitor().Start(totalAnd, replaceDictionary);
            Console.WriteLine("______________________________________________________________________");

            Expression afterReplace = new ExExpressinVisitor().Visit(replace);
            Console.WriteLine(Expression.Lambda<Func<bool>>(afterReplace).Body);
            Console.WriteLine(Expression.Lambda<Func<bool>>(afterReplace).Compile()());
            //VisualizerDevelopmentHost host2 = new VisualizerDevelopmentHost(final,
            //    typeof(ExpressionTreeVisualizer),
            //    typeof(ExpressionTreeVisualizerObjectSource));
            //host2.ShowVisualizer();

            //VisualizerDevelopmentHost host3 = new VisualizerDevelopmentHost(final2,
            //    typeof(ExpressionTreeVisualizer),
            //    typeof(ExpressionTreeVisualizerObjectSource));
            //host3.ShowVisualizer();

            Console.ReadKey();
        }
    }
}