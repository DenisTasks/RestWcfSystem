using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExpressionVisualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace MapperWithExpressions
{
    //public class Mapper<TSource, TDestination>
    //{
    //    public TDestination Map(TSource source)
    //    {
    //        ParameterExpression sourceParam = Expression.Parameter(typeof(TSource));
    //        NewExpression newDestionation = Expression.New(typeof(TDestination));

    //        var prop = source.GetType().GetProperties();
    //        var count = prop.Count();
    //        MemberAssignment[] propertiesFrom = new MemberAssignment[count];
    //        for (int i = 0; i < count; i++)
    //        {
    //            Console.WriteLine(prop[i].GetGetMethod().Invoke(new Foo { Name = "Denis", LastName = "Tarasevich", Number = 8666683, IsActive = true }, null));
    //            string elementName = prop[i].Name;
    //            object elementValue = prop[i].GetValue(source);
    //            Type elementType = elementValue.GetType();
    //            ConstantExpression constant = Expression.Constant(elementValue, elementType);
    //            var toAdd = Expression.Bind(typeof(TDestination).GetProperty(elementName), constant);
    //            propertiesFrom[i] = toAdd;
    //        }

    //        MemberInitExpression body = Expression.MemberInit(newDestionation, propertiesFrom);

            //        Expression<Func<TSource, TDestination>> mapFunction =
            //            Expression.Lambda<Func<TSource, TDestination>>(body, sourceParam);
            //        //VisualizerDevelopmentHost host = new VisualizerDevelopmentHost(mapFunction,
            //        //    typeof(ExpressionTreeVisualizer),
            //        //    typeof(ExpressionTreeVisualizerObjectSource));
            //        //host.ShowVisualizer();
            //        return mapFunction.Compile()(source);
            //    }
            //}

            //public class MappingGenerator
            //{
            //    public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
            //    {
            //        return new Mapper<TSource, TDestination>();
            //    }
            //}

    public class Foo
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
    }

    public class Bar
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
    }

    public class Mapper<TSource, TDestination>
    {
        private Func<TSource, TDestination> _mapFunction;
        internal Mapper(Func<TSource, TDestination> func)
        {
            _mapFunction = func;
        }

        public TDestination Map(TSource source)
        {
            return _mapFunction(source);
        }
    }

    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            // left part of lambda
            ParameterExpression sourceParam = Expression.Parameter(typeof(TSource));
            NewExpression newDestionation = Expression.New(typeof(TDestination));

            List<MemberAssignment> propertiesFrom = new List<MemberAssignment>();

            foreach (var item in typeof(TSource).GetProperties())
            {
                PropertyInfo targetProperty = typeof(TDestination).GetProperty(item.Name);
                propertiesFrom.Add(Expression.Bind(targetProperty, Expression.Property(sourceParam, item)));
            }

            MemberInitExpression body = Expression.MemberInit(newDestionation, propertiesFrom);

            Expression<Func<TSource, TDestination>> mapFunction =
                Expression.Lambda<Func<TSource, TDestination>>(body, sourceParam);
            //Console.WriteLine(mapFunction);

            //VisualizerDevelopmentHost host = new VisualizerDevelopmentHost(mapFunction,
            //    typeof(ExpressionTreeVisualizer),
            //    typeof(ExpressionTreeVisualizerObjectSource));
            //host.ShowVisualizer();

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }
    }

    public static class Test
    {
        public static IEnumerable<T> CustomMethod<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Foo start = new Foo {Name = "Denis", LastName = "Tarasevich", Number = 8666683, IsActive = true};

            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();
            var result = mapper.Map(start);
            Console.WriteLine(result.Name + " " + result.LastName + " " + result.Number + " " + result.IsActive + " " + result.GetType());

            //List<int> testList = new List<int>{1,2,3,4,5,6,7,8,9,10};
            //foreach (var item in testList.CustomMethod(s => s > 5))
            //{
            //    Console.WriteLine(item);
            //}
            Console.ReadKey();
        }
    }
}