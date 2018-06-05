using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace OutlookService.Authorization
{
    //class CustomPrincipal : IPrincipal
    //{
    //    private ILogger _logger;
    //    readonly IIdentity _identity;
    //    string[] _roles;

    //    public CustomPrincipal(IIdentity identity)
    //    {
    //        _identity = identity;
    //    }

    //    public static CustomPrincipal Current => Thread.CurrentPrincipal as CustomPrincipal;

    //    public IIdentity Identity => _identity;

    //    public string[] Roles
    //    {
    //        get
    //        {
    //            EnsureRoles();
    //            return _roles;
    //        }
    //    }

    //    public bool IsInRole(string role)
    //    {
    //        EnsureRoles();
    //        return _roles.Contains(role);
    //    }

    //    protected virtual void EnsureRoles()
    //    {
    //        _roles = _identity.Name == "testName" ? new[] { "ADMIN" } : new [] { "USER" };
    //        _logger = LogManager.GetCurrentClassLogger();
    //        _logger.Info($"Check role - {_roles[0]}");
    //    }
    //}
}