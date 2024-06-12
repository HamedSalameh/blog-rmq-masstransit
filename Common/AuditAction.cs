namespace Common
{
   public readonly struct AuditAction
    {
        public string value { get; }

        private AuditAction(string value)
        {
            this.value = value;
        }

        public static AuditAction Processed => new AuditAction("Processed");
        public static AuditAction Failed => new AuditAction("Failed");


        public override string ToString() => value;

        public static bool operator ==(AuditAction left, AuditAction right) => left.value == right.value;
        public static bool operator !=(AuditAction left, AuditAction right) => left.value != right.value;

        public override bool Equals(object obj) => obj is AuditAction other && this == other;
        public override int GetHashCode() => value.GetHashCode();
    }
}
