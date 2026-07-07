namespace PORTIMAGES.Application.Products.DTOs
{
    public class AssignProdBrokerRequestDTO
    {
        public List<ProductIdDTO> ProductIds { get; set; }
        public int BrokerId { get; set; }
        public int? CreatedBy { get; set; }
    }
}
