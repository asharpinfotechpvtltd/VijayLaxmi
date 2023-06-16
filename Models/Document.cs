namespace VijayLaxmi.Models
{
    public class Document
    {
        public int Id { get; set; }
        public Int64 AadharNo { get; set; }
        public string DocumentFrontFileName { get; set; } = null;
        public string DocumentBackFileName { get; set; } = null;
        public string PancardFileName { get; set; } = null;
        public string TicFileName { get; set; } = null;
        public string SignedDocumentName { get; set; } = null;
    }
}
