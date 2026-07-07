namespace PORTIMAGES.Common.DTOs.Import
{
    public class ImportValidationResultDTO
    {
        public int TotalRows { get; set; }

        public int ValidRows { get; set; }

        public int InvalidRows { get; set; }

        public int DuplicateRows { get; set; }

        public bool IsValid => InvalidRows == 0;

        public List<ImportValidationErrorDTO> Errors { get; set; } = new();
    }
}
