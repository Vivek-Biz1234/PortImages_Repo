using PORTIMAGES.Common.Helpers;
using PORTIMAGES.Common.Interfaces; 
namespace PORTIMAGES.Common.Extensions
{
    public static class EncryptionExtensions
    {
        public static void EncryptIds<T>(this IEnumerable<T> items)where T:IEncrypTableDTO
        {
            foreach (var item in items) 
            {
                item.EncID= CryptoHelper.Encrypt(item.ID.ToString());
            }

        }
    }
}
