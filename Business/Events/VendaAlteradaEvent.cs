namespace Business.Events
{
    public class VendaAlteradaEvent
    {
        public Guid VendaId { get; set; }
        public DateTime Data { get; set; }
    }
}
