
namespace CAuth.Business.DBContext
{
    using System.Data.Entity;
    using CAM.Common.Data;
    using CAuth.Model.Entity;

    public class DBContextCAuth : BaseDBContext<DBContextCAuth>
    {
        public IDbSet<CAuthPassport> CAuthPassport { get; set; }
    }
}
