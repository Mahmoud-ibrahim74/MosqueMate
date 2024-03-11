namespace MosqueMate.Security
{
    public class TokenManager
    {
        public static void GetUserInfoCore(string identity)
        {
            //var usrAccount = new userInfo();
            //var handler = new JwtSecurityTokenHandler();
            //var jsonToken = handler.ReadToken(identity);
            //var tokenS = handler.ReadToken(identity) as JwtSecurityToken;
            //List<Claim> claims = tokenS.Claims.ToList();
            //var sub = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            //if (sub != null)
            //{
            //    using (var context = new ProcoorcenteralContext())
            //    {
            //        usrAccount = context.member.Where(x => x.deletedBy == null && x.userName == sub.Value)
            //          .Select(x => new userInfo
            //          {
            //              id = x.id,
            //              contactName = x.contactName,
            //              userName = x.userName,
            //              groupId = (int)(x.groupId ?? -1),
            //          }).FirstOrDefault();

            //        if (usrAccount == null)
            //        {
            //            return new userInfo();
            //        }
            //        return usrAccount;
            //    }
            //}
        }
        public static void GetuserInfoByAccount(string identity)
        {
            //var usrAccount = new userInfo();
            //var handler = new JwtSecurityTokenHandler();
            //var jsonToken = handler.ReadToken(identity);
            //var tokenS = handler.ReadToken(identity) as JwtSecurityToken;
            //List<Claim> claims = tokenS.Claims.ToList();
            //var sub = claims.FirstOrDefault(x => x.Type == ClaimTypes.Version);
            //if (sub != null)
            //{
            //    using (var context = new ProcoorcenteralContext())
            //    {
            //        usrAccount = context.accounts.Where(x => x.id == Convert.ToInt32(sub.Value))
            //          .Select(x => new userInfo
            //          {
            //              id = x.id,
            //              contactName = x.contactName,
            //              userName = x.userName,
            //          }).FirstOrDefault();

            //        if (usrAccount == null)
            //        {
            //            return new userInfo();
            //        }
            //        return usrAccount;
            //    }
            //}
            //return usrAccount;
        }
    }

}
