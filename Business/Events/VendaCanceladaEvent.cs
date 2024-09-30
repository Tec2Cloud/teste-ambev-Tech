namespace Business.Events
{
    public class VendaCanceladaEvent
    {
        public Guid VendaId { get; set; }
        public DateTime Data { get; set; }
    }
}
