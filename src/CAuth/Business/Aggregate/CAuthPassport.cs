
namespace CAuth.Business.Aggregate
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity.SqlServer;
    using System.Collections.Generic;

    using Model.Entity;
    using Model.Factory;
    //using Model.Filter;

    using CAM.Core.Business.Interface;
    using CAM.Core.Business.Aggregate;

    using Interface;
    using Rule;

    using CAM.Common.Data;
    using CAM.Common.Convert;
    using CAM.Common.Error;

    using CAM.General.ComplexStruct;

    public partial class Aggregate : ICAuthPassportCommand
    {
        private const string _C_ERR_LOGINNAME = "登录名错误";
        private const string _C_ERR_PASSWORD = "登陆密码错误";
        private const string _C_ERR_LOGINEXPIRE = "登陆信息过期，请重新使用账号密码登陆系统";
        private const string _C_ERR_PASSPORTLOCKED = "账号已被锁定，锁定原因：{0}";

        /// <summary>
        /// 检查账户是否允许登陆，并判断是否登陆成功
        /// </summary>
        /// <param name="passport"></param>
        /// <param name="Password"></param>
        private void checkPassportIsValide(CAuthPassport passport, string Password)
        {
            try
            {
                if (passport == null)
                {
                    ErrorHandler.ThrowException(_C_ERR_LOGINNAME);
                }
                if (passport.Password != Password.ToMD5HashCode())
                {
                    ErrorHandler.ThrowException(_C_ERR_PASSWORD);
                }

                if (passport.Locker.IsLocked)
                {
                    ErrorHandler.ThrowException(string.Format(_C_ERR_PASSPORTLOCKED, passport.Locker.LockReason));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }


        #region 利用账号密码实现的五种登陆方式

        public long loginFromPC(string LoginName, string Password, ref string oldToken, ref string newToken)
        {
            try
            {
                IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
                CAuthPassport dbObj = res.read(m => m.LoginName == LoginName && m.System.DeleteFlag == false);

                checkPassportIsValide(dbObj, Password);

                oldToken = dbObj.Token.PC.ToString();
                dbObj.Token.PC = Guid.NewGuid();
                res.update(dbObj);
                commit();

                newToken = dbObj.Token.PC.ToString();

                return dbObj.Id;
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
                return 0;
            }
        }

        public long loginFromMobileIOS(string LoginName, string Password, ref string oldToken, ref string newToken)
        {
            try
            {
                IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
                CAuthPassport dbObj = res.read(m => m.LoginName == LoginName && m.System.DeleteFlag == false);

                checkPassportIsValide(dbObj, Password);

                oldToken = dbObj.Token.MobileIOS.ToString();
                dbObj.Token.MobileIOS = Guid.NewGuid();
                res.update(dbObj);
                commit();

                newToken = dbObj.Token.MobileIOS.ToString();

                return dbObj.Id;
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
                return 0;
            }
        }

        public long loginFromMobileAndroid(string LoginName, string Password, ref string oldToken, ref string newToken)
        {
            try
            {
                IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
                CAuthPassport dbObj = res.read(m => m.LoginName == LoginName && m.System.DeleteFlag == false);

                checkPassportIsValide(dbObj, Password);

                oldToken = dbObj.Token.MobileAndroid.ToString();
                dbObj.Token.MobileAndroid = Guid.NewGuid();
                res.update(dbObj);
                commit();

                newToken = dbObj.Token.MobileAndroid.ToString();

                return dbObj.Id;
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
                return 0;
            }
        }

        public long loginFromPadIOS(string LoginName, string Password, ref string oldToken, ref string newToken)
        {
            try
            {
                IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
                CAuthPassport dbObj = res.read(m => m.LoginName == LoginName && m.System.DeleteFlag == false);

                checkPassportIsValide(dbObj, Password);

                oldToken = dbObj.Token.PadIOS.ToString();
                dbObj.Token.PadIOS = Guid.NewGuid();
                res.update(dbObj);
                commit();

                newToken = dbObj.Token.PadIOS.ToString();

                return dbObj.Id;
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
                return 0;
            }
        }

        public long loginFromPadAndroid(string LoginName, string Password, ref string oldToken, ref string newToken)
        {
            try
            {
                IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
                CAuthPassport dbObj = res.read(m => m.LoginName == LoginName && m.System.DeleteFlag == false);

                checkPassportIsValide(dbObj, Password);

                oldToken = dbObj.Token.PadAndroid.ToString();
                dbObj.Token.PadAndroid = Guid.NewGuid();
                res.update(dbObj);
                commit();

                newToken = dbObj.Token.PadAndroid.ToString();

                return dbObj.Id;
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
                return 0;
            }
        }


        #endregion

        #region 通过cookie实现免登陆尝试

        public long loginByToken(string Token)
        {
            try
            {
                IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
                Guid guidToken = Guid.Parse(Token);

                CAuthPassport dbObj = null;

                dbObj = res.read(m => m.Token.PC == guidToken && m.System.DeleteFlag == false);
                if (dbObj != null)
                {
                    return dbObj.Id;
                }
                dbObj = res.read(m => m.Token.MobileIOS == guidToken && m.System.DeleteFlag == false);
                if (dbObj != null)
                {
                    return dbObj.Id;
                }
                dbObj = res.read(m => m.Token.MobileAndroid == guidToken && m.System.DeleteFlag == false);
                if (dbObj != null)
                {
                    return dbObj.Id;
                }
                dbObj = res.read(m => m.Token.PadIOS == guidToken && m.System.DeleteFlag == false);
                if (dbObj != null)
                {
                    return dbObj.Id;
                }
                dbObj = res.read(m => m.Token.PadAndroid == guidToken && m.System.DeleteFlag == false);
                if (dbObj != null)
                {
                    return dbObj.Id;
                }

                ErrorHandler.ThrowException(_C_ERR_LOGINEXPIRE);
                return 0;
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
                return 0;
            }
        }

        #endregion


        public long addPassport(string LoginName, string Password, ref Token token)
        {
            try
            {
                IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
                CAuthPassport dbObj = CAuthPassportFactory.createPassport();

                dbObj.LoginName = LoginName;
                dbObj.Password = Password.ToMD5HashCode();

                dbObj.addValidationRule(new PassportCannotExistsSameLoginNameRule(res, dbObj));

                dbObj.validate();
                res.add(dbObj);
                commit();

                token = dbObj.Token;
                return dbObj.Id;
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
                return 0;
            }
        }

        public void updatePassword(long passportId, string newPassword)
        {
            try
            {
                IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
                CAuthPassport dbObj = res.read(m => m.Id == passportId);

                dbObj.Password = newPassword.ToMD5HashCode();

                dbObj.validate();
                res.update(dbObj);
                commit();

            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void updateLoginName(long passportId, string newLoginName)
        {
            try
            {
                IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
                CAuthPassport dbObj = res.read(m => m.Id == passportId);

                dbObj.LoginName = newLoginName;

                dbObj.addValidationRule(new PassportCannotExistsSameLoginNameRule(res, dbObj));

                dbObj.validate();
                res.update(dbObj);
                commit();

            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }


        public CAuthPassport readPassport(long Id)
        {
            IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
            return res.read(m => m.Id == Id);
        }


        public void lockPassport(long passportId, string lockReason)
        {
            try
            {
                IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
                CAuthPassport passport = res.read(m => m.Id == passportId);

                passport.Locker.lockIt(lockReason);

                passport.validate();
                res.update(passport);
                commit();
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }

        public void unLockPassport(long passportId)
        {
            try
            {
                IRepository<CAuthPassport> res = createRepository<CAuthPassport>();
                CAuthPassport passport = res.read(m => m.Id == passportId);

                passport.Locker.unLockIt();

                passport.validate();
                res.update(passport);
                commit();
            }
            catch (Exception ex)
            {
                ErrorHandler.ThrowException(ex);
            }
        }


    }
}
