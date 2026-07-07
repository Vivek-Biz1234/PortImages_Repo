using PORTIMAGES.Common.Helpers;
using PORTIMAGES.Common.Interfaces;

namespace PORTIMAGES.Common.Extensions
{
    public static class DecryptionExtensions
    {
        public static void DecryptIds<T>(this IEnumerable<T> items)where T: IDecryptTableDTO
        {
            foreach (var item in items)
            {
                item.ID=Convert.ToInt32(CryptoHelper.Decrypt(item.EncID));
            }
        }

        public static List<int> DecryptIds(this List<string> encIds)
        {
            return encIds.Select(x => Convert.ToInt32(CryptoHelper.Decrypt(x))).ToList();
        }
    }
}
