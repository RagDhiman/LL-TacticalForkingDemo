namespace Shop_Web_UI.DTOs
{
    public class Account: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public string EmailAddress { get; set; }
    }
}