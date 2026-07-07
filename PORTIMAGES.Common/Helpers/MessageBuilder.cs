using PORTIMAGES.Common.Enums;

namespace PORTIMAGES.Common.Helpers
{
    public class MessageBuilder
    {
        public static string Success(string entity, CrudAction action)
            => $"{entity} {action.ToString().ToLower()} successfully !!";
        public static string EmailExists()
            => "Email id already exists !!";

        public static string MobileExists()
         => "Mobile number already exists !!";

        public static string NotFound(string entity)
            => $"{entity} not found !!";

        public static string Error()
            => "Something went wrong !!";
    }
}
