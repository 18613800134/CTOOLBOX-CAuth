
namespace CAuth.Business.Aggregate
{
    using CAM.Core.Business.Interface;
    using CAM.Core.Business.Aggregate;
    using DBContext;

    public partial class Aggregate : BaseAggregate, IBaseInterfaceCommand<DBContextCAuth>
    {
        public Aggregate()
        {
            this.dbContext = new DBContextCAuth();
        }

        public DBContextCAuth DBContext
        {
            get { return (DBContextCAuth)this.dbContext; }
        }
    }
}
