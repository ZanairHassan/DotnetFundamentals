namespace ASP.NetFundamentals.Configurations
{
    public class SecretKey
    {
        public const string SectionName = "SecretKey";
        public string AppToken { get; set; } = string.Empty;
    }
}
