namespace PORTIMAGES.Common.Enums
{
    public enum ResultStatus:short
    {
        Success=1,
        EmailExists = 2,
        MobileExists = 3,
        NotFound = -1,
        Failed = -2,
        Error = -99
    }
}
