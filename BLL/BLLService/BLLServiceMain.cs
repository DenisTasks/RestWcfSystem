using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Linq;
using AutoMapper;
using BLL.EntitesDTO;
using BLL.Interfaces;
using Dapper;
using Dapper.FastCrud;
using ExpressionVisualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Model;
using Model.Entities;
using Model.Interfaces;
using Model.ModelService;

namespace BLL.BLLService
{
    public class CustomExpressionVisitor : ExpressionVisitor
    {
        public int indent = 0;

        public override Expression Visit(Expression node)
        {
            if (node == null)
            {
                return base.Visit(node);
            }

            Trace.WriteLine($"FROM DEFAULT {new String(' ', indent * 4)}{node.NodeType} - {node.GetType()}");

            indent++;
            Expression result = base.Visit(node);
            indent--;

            return result;
        }

        //protected override Expression VisitBinary(BinaryExpression node)
        //{
        //    if (node.Right.NodeType == ExpressionType.Constant && node.Right.ToString() == "15000")
        //    {
        //        //ParameterExpression pe = Expression.Parameter(typeof(Appointment), "Appointment");
        //        //Expression left = Expression.Property(pe, typeof(Appointment).GetProperty("AppointmentId") ?? throw new InvalidOperationException());
        //        Expression right = Expression.Constant(16000, typeof(int));
        //        Expression e1 = Expression.GreaterThanOrEqual(node.Left, right);
        //        return e1;
        //    }

        //    if (node.Right.NodeType == ExpressionType.Constant && node.Right.ToString() == "19000")
        //    {
        //        //ParameterExpression pe = Expression.Parameter(typeof(Appointment), "Appointment");
        //        //Expression left = Expression.Property(pe, typeof(Appointment).GetProperty("AppointmentId") ?? throw new InvalidOperationException());
        //        Expression right = Expression.Constant(18000, typeof(int));
        //        Expression e1 = Expression.LessThanOrEqual(node.Left, right);
        //        return e1;
        //    }

        //    Trace.WriteLine($"FROM BINARY {new String(' ', indent * 4)}{node.NodeType} - {node.GetType()}");
        //    Expression result = base.VisitBinary(node);
        //    return result;
        //}

        //protected override Expression VisitConstant(ConstantExpression node)
        //{
        //    Trace.WriteLine($"FROM CONSTANT {new String(' ', indent * 4)}{node.NodeType} - {node.GetType()}");
        //    Expression result = base.VisitConstant(node);
        //    return result;
        //}

        //protected override Expression VisitParameter(ParameterExpression node)
        //{
        //    Trace.WriteLine($"FROM PARAMETER {new String(' ', indent * 4)}{node.NodeType} - {node.GetType()}");
        //    Expression result = base.VisitParameter(node);
        //    return result;
        //}

        //protected override Expression VisitMember(MemberExpression node)
        //{
        //    Trace.WriteLine($"FROM MEMBER {new String(' ', indent * 4)}{node.NodeType} - {node.GetType()} {node.Member.Name}");
        //    Expression result = base.VisitMember(node);
        //    return result;
        //}
    }

    public class BllServiceMain : IBllServiceMain
    {
        private readonly WPFOutlookContext _context;
        private readonly IGenericRepository<Appointment> _appointments;
        private readonly IGenericRepository<User> _users;
        private readonly IGenericRepository<Location> _locations;
        public BllServiceMain(IGenericRepository<Appointment> appointments, IGenericRepository<User> users, IGenericRepository<Location> locations, WPFOutlookContext context)
        {
            _appointments = appointments;
            _users = users;
            _locations = locations;
            _context = context;
            //_context.Configuration.AutoDetectChangesEnabled = true;
        }

        public IEnumerable<AppointmentDTO> GetAppointmentsByUserId(int id)
        {
            IQueryable<Appointment> queryableData = _context.Appointments;

            ParameterExpression pe = Expression.Parameter(typeof(Appointment));
            Expression left = Expression.Property(pe, "AppointmentId");
            Expression right = Expression.Constant(15000, typeof(int));
            Expression e1 = Expression.GreaterThanOrEqual(left, right);

            left = Expression.Property(pe, "AppointmentId");
            right = Expression.Constant(19000, typeof(int));
            Expression e2 = Expression.LessThanOrEqual(left, right);

            Expression predicateBody = Expression.And(e1, e2);




            Appointment a = new Appointment { AppointmentId = 17000 };
            //Trace.WriteLine(Expression.Lambda<Func<Appointment, bool>>(predicateBody, pe).Compile());
            //Trace.WriteLine(Expression.Lambda<Func<Appointment, bool>>(predicateBody, pe).Compile()(a));

            //MethodCallExpression whereCallExpression = Expression.Call(
            //    typeof(Queryable),
            //    "Where",
            //    new[] { queryableData.ElementType },
            //    queryableData.Expression,
            //    Expression.Lambda<Func<Appointment, bool>>(predicateBody, pe));
            //var results = queryableData.Provider.CreateQuery<Appointment>(whereCallExpression).ToList();


            Expression resultExp = new CustomExpressionVisitor().Visit(predicateBody);
            Trace.WriteLine(resultExp);

            //VisualizerDevelopmentHost host = new VisualizerDevelopmentHost(predicateBody,
            //    typeof(ExpressionTreeVisualizer),
            //    typeof(ExpressionTreeVisualizerObjectSource));
            //host.ShowVisualizer();

            //VisualizerDevelopmentHost host2 = new VisualizerDevelopmentHost(resultExp,
            //    typeof(ExpressionTreeVisualizer),
            //    typeof(ExpressionTreeVisualizerObjectSource));
            //host2.ShowVisualizer();

            // Appointment => ((Appointment.AppointmentId >= 16000) And (Appointment.AppointmentId <= 18000))
            var wherePredicate = Expression.Lambda<Func<Appointment, bool>>(resultExp, pe);
            //queryableData.Where(Appointment => ((Appointment.AppointmentId >= 16000) And(Appointment.AppointmentId <= 18000)))
            MethodCallExpression testWhere = Expression.Call(typeof(Queryable), "Where", new[] { typeof(Appointment) }, queryableData.Expression, wherePredicate);
            Trace.WriteLine(Expression.Lambda<Func<Appointment, bool>>(resultExp, pe).ToString());
            Trace.WriteLine(Expression.Lambda<Func<Appointment, bool>>(resultExp, pe).Compile()(a));
            Trace.WriteLine(testWhere);
            var results2 = queryableData.Provider.CreateQuery<Appointment>(testWhere).ToList();


            // короткая запись
            Expression<Func<int, int, int>> expression = (aaz, bbz) => aaz + bbz;
            var body = expression.Body;
            Trace.WriteLine($"body is {body}");
            foreach (var item in expression.Parameters)
            {
                Trace.WriteLine($"parameters are: {item.Name} {item.NodeType} {item.Type}");
            }
            Trace.WriteLine(expression.ToString());
            Trace.WriteLine(expression.Compile()(1, 2));

            //Expression lefted = Expression.Constant(5, typeof(int));
            //Expression righted = Expression.Constant(6, typeof(int));

            // длинная запись
            ParameterExpression az = Expression.Parameter(typeof(int));
            ParameterExpression bz = Expression.Parameter(typeof(int));
            Expression total = Expression.Add(az, bz);
            Trace.WriteLine($"body is {Expression.Lambda<Func<int, int, int>>(total, az, bz).Body}");
            foreach (var item in Expression.Lambda<Func<int, int, int>>(total, az, bz).Parameters)
            {
                Trace.WriteLine($"parameters are: {item.Name} {item.NodeType} {item.Type}");
            }
            Trace.WriteLine(Expression.Lambda<Func<int, int, int>>(total, az, bz).ToString());
            Trace.WriteLine(Expression.Lambda<Func<int, int, int>>(total, az, bz).Compile()(1, 2));



            //using (_appointments.BeginTransaction())
            //{
            //collection = _appointments.Get(x => x.Users.Any(s => s.UserId == id)).ToList();

            // with lazy loading. Initialize field Users will be at mapping step (below)
            //collection = _appointments.Get().ToList();

            // with eager loading
            //var collection = _context.Appointments.Include(x => x.Users).AsNoTracking().ToList();

            //AppointmentRepository<Appointment, AppointmentDTO> newRepo = new AppointmentRepository<Appointment, AppointmentDTO>(_context);

            //Trace.WriteLine("IEnumerable start : " + DateTime.Now.Second + DateTime.Now.Millisecond);
            //IEnumerable<Appointment> people = _context.Appointments;
            //var person = people.Where(x => x.AppointmentId > 5000).Where(a => a.AppointmentId < 34000).Take(27000).OrderBy(s => s.AppointmentId).ToList();
            //Trace.WriteLine("IEnumerable after where-where-take : " + DateTime.Now.Second + DateTime.Now.Millisecond);

            //Trace.WriteLine("IQueryable start : " + DateTime.Now.Second + DateTime.Now.Millisecond);
            //IQueryable<Appointment> people2 = _context.Appointments;
            //var person2 = people2.Where(x => x.AppointmentId > 5000).Where(a => a.AppointmentId < 34000).Take(27000).OrderBy(s => s.AppointmentId).ToList();
            //Trace.WriteLine("IQueryable after where-where-take : " + DateTime.Now.Second + DateTime.Now.Millisecond);

            // LINQ TO XML
            //var testXml = new XElement("Root",
            //    from a in person
            //    select new XElement("AppointmentForXML", new XElement("Id", a.AppointmentId), new XElement("Subject", a.Subject), new XElement("Location", a.LocationId)));
            //string filPath = @"C:/Test.xml";
            //var root = XElement.Load(filPath);
            //root.Add(testXml);
            //root.Save(filPath);

            // overlapping dates
            //DateTime second = DateTime.Now.AddHours(10);
            //DateTime fourth = DateTime.Now.AddHours(11);
            //var test = _context.Appointments.Where(x => x.BeginningDate < fourth && x.EndingDate > second).ToList();
            //Trace.WriteLine(test.Count);
            //}

            List<Appointment> test;
            string connectionString = ConfigurationManager.ConnectionStrings["WPFOutlookContext"].ConnectionString;
            var sql =
                "SELECT * FROM Appointments AS A LEFT JOIN UserAppointments AS UA ON A.AppointmentId = UA.AppointmentId LEFT JOIN Users AS U ON UA.UserId = U.UserId";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                test = db.Query<Appointment, User, Appointment>(sql, (appointment, user) =>
                      {
                          appointment.Users = new List<User>();
                          appointment.Users.Add(user);
                          return appointment;
                      }, splitOn: "UserId").ToList();
                Trace.WriteLine(test.Count + " DAPPER");
                Trace.WriteLine(test.Find(s => s.AppointmentId == 34968).Users.ElementAt(0).Name + " DAPPER");


                //    db.Execute("INSERT INTO Appointments (Subject,BeginningDate,EndingDate,OrganizerId,LocationId) VALUES (@Subject,@BeginningDate,@EndingDate,@OrganizerId,@LocationId)",
                //        newApp);
                //db.Insert(newApp);

            }
            Appointment newApp = new Appointment
            {
                Subject = "Hello!",
                BeginningDate = DateTime.Now,
                EndingDate = DateTime.Now.AddHours(1),
                LocationId = 1,
                OrganizerId = 1,
            };

            var attachingApp = _context.Appointments.AsNoTracking().First(s => s.AppointmentId == 34976);
            //_context.Appointments.Attach(attachingApp);
            attachingApp.Subject = $"{DateTime.Now}";
            _context.Entry(attachingApp).State = EntityState.Modified;
            _context.SaveChanges();

            var mappingItem = Mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentDTO>>(test);

            //foreach (var item in collection)
            //{
            //    using (IDbConnection db = new SqlConnection(connectionString))
            //    {
            //        var users = db.Query<User>($"SELECT * FROM UserAppointments AS UA INNER JOIN Users AS U ON UA.UserId = U.UserId WHERE UA.AppointmentId = {item.AppointmentId}").ToList();
            //        if (users.Count > 0)
            //        {
            //            foreach (var item2 in users)
            //            {
            //                item.Users.Add(item2);
            //            }
            //        }
            //    }
            //}
            //var test = newRepo.Get();

            //foreach (var item in mappingCollection)
            //{
            //    //using (_locations.BeginTransaction())
            //    //{
            //        item.Room = _locations.FindById(item.LocationId).Room;
            //    //}
            //}
            //var mappingItem = Mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentDTO>>(collection);

            return mappingItem.Take(100);
        }

        public AppointmentDTO GetAppointmentById(int id)
        {
            Appointment appointment;
            using (_appointments.BeginTransaction())
            {
                appointment = _appointments.FindById(id);
            }
            var mappingItem = Mapper.Map<Appointment, AppointmentDTO>(appointment);
            return mappingItem;
        }

        public IEnumerable<AppointmentDTO> GetAppointmentsByUserIdSqlText(int id, int itemsToSkip, int pageSize)
        {
            List<Appointment> collection = new List<Appointment>();
            string connectionString = ConfigurationManager.ConnectionStrings["WPFOutlookContext"].ConnectionString;
            using (var cnn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("SELECT * FROM Appointments", cnn))
                {
                    {
                        cnn.Open();
                        using (SqlDataReader appointmentReader = cmd.ExecuteReader())
                        {
                            while (appointmentReader.Read())
                            {
                                var getApp = new Appointment
                                {
                                    AppointmentId = appointmentReader.GetInt32(0),
                                    Subject = appointmentReader.GetString(1),
                                    BeginningDate = appointmentReader.GetDateTime(2),
                                    EndingDate = appointmentReader.GetDateTime(3)
                                    //,OrganizerId = appointmentReader.GetInt32(4),
                                    //LocationId = appointmentReader.GetInt32(5)
                                };


                                collection.Add(getApp);
                            }
                        }
                    }
                }
            }
            var mappingCollection = Mapper.Map<IEnumerable<Appointment>, IEnumerable<AppointmentDTO>>(collection).ToList();
            return mappingCollection.OrderBy(a => a.AppointmentId).Skip(itemsToSkip).Take(pageSize).ToList();
        }

        public AppointmentDTO UpdateAppointment(AppointmentDTO appointment)
        {
            var updateApp = _appointments.FindById(appointment.AppointmentId);
            if (updateApp != null)
            {
                updateApp.Subject = appointment.Subject;
                _appointments.Update(updateApp);
                _context.SaveChanges();
                var outputApp = Mapper.Map<Appointment, AppointmentDTO>(updateApp);
                return outputApp;
            }
            return null;
        }

        public AppointmentDTO AddAppointment(AppointmentDTO appointment, int id)
        {
            var appointmentItem = Mapper.Map<AppointmentDTO, Appointment>(appointment);
            //appointmentItem.Organizer = _users.FindById(id);
            //appointmentItem.Location = _locations.FindById(appointmentItem.LocationId);
            //appointmentItem.OrganizerId = id;
            //appointmentItem.Users = new List<User>();
            //var convert = Mapper.Map<IEnumerable<UserDTO>, IEnumerable<User>>(appointment.Users);
            //foreach (var item in convert)
            //{
            //    if (_users.FindById(item.UserId) != null)
            //    {
            //        //appointmentItem.Users.Add(_users.FindById(item.UserId));
            //    }
            //}

            using (var transaction = _appointments.BeginTransaction())
            {
                try
                {
                    _appointments.Create(appointmentItem);
                    _appointments.Save();
                    var outputItem = _appointments.Get().LastOrDefault();
                    transaction.Commit();
                    return Mapper.Map<Appointment, AppointmentDTO>(outputItem);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.StackTrace);
                }
            }
        }

        public AppointmentDTO RemoveAppointment(int appointmentId)
        {
            var appointment = _appointments.FindById(appointmentId);
            if (appointment != null)
            {
                using (var transaction = _appointments.BeginTransaction())
                {
                    try
                    {
                        _appointments.Remove(appointment);
                        _appointments.Save();
                        transaction.Commit();
                        return Mapper.Map<Appointment, AppointmentDTO>(appointment);
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception(e + " from BLL");
                    }
                }
            }
            return null;
        }
    }
}