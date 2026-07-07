namespace PORTIMAGES.Application.Products.DTOs
{
    public class ViewProductsRequestDTO
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
