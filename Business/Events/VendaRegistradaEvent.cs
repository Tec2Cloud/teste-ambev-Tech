namespace Business.Events
{
    public class VendaRegistradaEvent
    {
        public Guid VendaId { get; set; }
        public DateTime Data { get; set; }
        public string Cliente { get; set; }
    }
}
