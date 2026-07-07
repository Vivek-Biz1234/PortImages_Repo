namespace PORTIMAGES.Application.Products.DTOs
{
    public class CommonAssignmentRequestDTO
    {
        public string EncId { get; set; }
        public int ProductId { get; set; }
        public int ReferenceId { get; set; }
        public int? CreatedBy { get; set; }
    }
}
