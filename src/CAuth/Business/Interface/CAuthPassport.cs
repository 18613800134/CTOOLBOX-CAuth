
namespace CAuth.Business.Interface
{
    using System;
    using System.Linq;
    using CAM.General.ComplexStruct;
    using CAM.Core.Business.Interface;
    using Model.Entity;
    using DBContext;

    public interface ICAuthPassportCommand : IBaseInterfaceCommand<DBContextCAuth>
    {
        long loginFromPC(string LoginName, string Password, ref string oldToken, ref string newToken);
        long loginFromMobileIOS(string LoginName, string Password, ref string oldToken, ref string newToken);
        long loginFromMobileAndroid(string LoginName, string Password, ref string oldToken, ref string newToken);
        long loginFromPadIOS(string LoginName, string Password, ref string oldToken, ref string newToken);
        long loginFromPadAndroid(string LoginName, string Password, ref string oldToken, ref string newToken);

        long loginByToken(string Token);

        long addPassport(string LoginName, string Password, ref Token token);
        void updatePassword(long passportId, string newPassword);
        void updateLoginName(long passportId, string newLoginName);

        void lockPassport(long passportId, string lockReason);
        void unLockPassport(long passportId);

        CAuthPassport readPassport(long Id);
    }
}
