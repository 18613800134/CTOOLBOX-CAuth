
namespace CAuth.Business.Rule
{
    using System.ComponentModel.DataAnnotations;
    using CAM.Core.Business.Rule;
    using CAM.Core.Model.Validation;
    using CAM.Common.Data;
    using Model.Entity;

    public class PassportCannotExistsSameLoginNameRule : BaseRule<CAuthPassport>
    {
        public PassportCannotExistsSameLoginNameRule(IRepository<CAuthPassport> res, CAuthPassport checkObj)
            : base(res, checkObj)
        {

        }

        public override ValidationResult validate()
        {
            ValidationResult result = ValidationResult.Success;
            if (_res.exists(m => m.Id != _checkObj.Id && m.System.DeleteFlag == false && m.LoginName == _checkObj.LoginName))
            {
                result = createValidationResult("LoginName", string.Format("账号【{0}】已经被注册！", _checkObj.LoginName));
            }
            return result;
        }
    }
}
