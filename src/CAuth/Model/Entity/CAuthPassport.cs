
namespace CAuth.Model.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using CAM.Core.Model.Entity;
    using CAM.General.ComplexStruct;


    public class CAuthPassport : BaseEntityNormal, IEntityDataLocker
    {
        [Required]
        [Index(IsClustered = false, IsUnique = false)]
        [StringLength(50, ErrorMessage = "登陆账号：[{0}] 的长度为 {2} 到 {1} 个字符。", MinimumLength = 1)]
        public string LoginName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public Token Token { get; set; }

        /// <summary>
        /// 用户账户的第三方扩展插件接口属性
        /// </summary>
        public EXTPlugInForPassport EXTPlugIn { get; set; }
        public DataLock Locker { get; set; }
    }

}
