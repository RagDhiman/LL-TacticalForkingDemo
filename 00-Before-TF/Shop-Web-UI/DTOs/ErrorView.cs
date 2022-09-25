namespace Shop_Web_UI.DTOs
{
    public class ErrorView
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}