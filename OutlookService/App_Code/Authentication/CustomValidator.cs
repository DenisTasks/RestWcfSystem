using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace OutlookService.Authentication
{
    //public class CustomValidator : UserNamePasswordValidator
    //{
    //    private ILogger _logger;
        
    //    public override void Validate(string userName, string password)
    //    {
    //        _logger = LogManager.GetCurrentClassLogger();
    //        if (userName == "testName" && password == "testPassword")
    //        {
    //            _logger.Info("Password is correct!");
    //            return;
    //        }
    //        _logger.Info("Password is wrong!");
    //        throw new SecurityTokenException("Username or password is not valid!");
    //    }
    //}
}
