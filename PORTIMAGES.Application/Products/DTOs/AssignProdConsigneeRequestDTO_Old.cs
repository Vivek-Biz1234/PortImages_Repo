namespace PORTIMAGES.Application.Products.DTOs
{
    public class AssignProdConsigneeRequestDTO_Old
    {
        public List<ProductIdDTO> ProductIds { get; set; }
        public int ConsigneeId { get; set; }
        public int? CreatedBy { get; set; }
    }
    public class ProductIdDTO
    {
        public int ProductId { get; set; }
    }
}
