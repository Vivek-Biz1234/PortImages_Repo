using PORTIMAGES.Application.Products.DTOs;
using PORTIMAGES.Common.Helpers; 

namespace PORTIMAGES.Application.Products.Extensions
{
    public static class ProductExtensions
    {
        public static void EncryptIds(this IEnumerable<ProductResponseDTO> products)
        {
            foreach (var item in products)
            {
                item.EncID = CryptoHelper.Encrypt(item.ID.ToString());
            }
        }

        public static void EncryptId(this ProductResponseDTO product)
        {
            product.EncID = CryptoHelper.Encrypt(product.ID.ToString());            
        }
    }
}
