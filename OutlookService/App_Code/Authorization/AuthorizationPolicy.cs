using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OutlookService.Authorization
{
    //class AuthorizationPolicy : IAuthorizationPolicy
    //{
    //    Guid _id = Guid.NewGuid();

    //    public bool Evaluate(EvaluationContext evaluationContext, ref object state)
    //    {
    //        IIdentity client = GetClientIdentity(evaluationContext);

    //        evaluationContext.Properties["Principal"] = new CustomPrincipal(client);

    //        return true;
    //    }

    //    private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
    //    {
    //        object obj;
    //        if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
    //            throw new Exception("No Identity found");

    //        IList<IIdentity> identities = obj as IList<IIdentity>;
    //        if (identities == null || identities.Count <= 0)
    //            throw new Exception("No Identity found");

    //        return identities[0];
    //    }

    //    public ClaimSet Issuer => ClaimSet.System;

    //    public string Id => _id.ToString();
    //}
}