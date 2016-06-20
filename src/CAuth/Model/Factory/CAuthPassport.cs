
namespace CAuth.Model.Factory
{
    using CAM.Core.Model.Entity;
    using CAM.General.ComplexStruct;
    using Entity;
    using System;

    public class CAuthPassportFactory
    {
        public static CAuthPassport createPassport()
        {
            CAuthPassport passport = EntityBuilder.build<CAuthPassport>();
            passport.LoginName = "";
            passport.Password = "";
            passport.Token = new Token();
            passport.EXTPlugIn = new EXTPlugInForPassport();
            passport.Locker = DataLockFactory.createUnLockedLock();
            return passport;
        }
    }
}
