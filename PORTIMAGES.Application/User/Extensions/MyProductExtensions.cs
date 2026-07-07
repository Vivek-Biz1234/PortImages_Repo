using PORTIMAGES.Application.User.DTOs;
using PORTIMAGES.Common.Helpers;

namespace PORTIMAGES.Application.User.Extensions
{
    public static class MyProductExtensions
    {
        public static void EncryptIds(this IEnumerable<MyProductResponseDTOs> products)
        {
            foreach (var product in products)
            {
                product.EncID=CryptoHelper.Encrypt(product.ID.ToString());
            }
        }
    }
}
