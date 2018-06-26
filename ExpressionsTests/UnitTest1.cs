using System;
using System.Linq.Expressions;
using BLL.BLLService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Entities;

namespace ExpressionsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ParameterExpression pe = Expression.Parameter(typeof(Appointment), "Appointment");

            Expression left = Expression.Property(pe, typeof(Appointment).GetProperty("AppointmentId") ?? throw new InvalidOperationException());
            Expression right = Expression.Constant(15000, typeof(int));
            Expression e1 = Expression.GreaterThanOrEqual(left, right);

            left = Expression.Property(pe, typeof(Appointment).GetProperty("AppointmentId") ?? throw new InvalidOperationException());
            right = Expression.Constant(19000, typeof(int));
            Expression e2 = Expression.LessThanOrEqual(left, right);

            Expression predicateBody = Expression.And(e1, e2);

            new CustomExpressionVisitor().Visit(predicateBody);
        }
    }
}
