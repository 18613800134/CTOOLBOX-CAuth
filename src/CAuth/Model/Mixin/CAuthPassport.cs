
namespace CAuth.Model.Mixin
{
    using System;

    public class CPassportMixin
    {
        public long Id { get; set; }
        public string LoginName { get; set; }
        public bool Locker_IsLocked { get; set; }
        public DateTime Locker_LockTime { get; set; }
        public string Locker_LockReason { get; set; }
    }
}
