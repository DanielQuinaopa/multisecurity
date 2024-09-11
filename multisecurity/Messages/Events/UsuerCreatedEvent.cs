using Cordillera.Distribuidas.Event.Events;

namespace multitrabajo_retiro.Messages.Events
{
    public class UsuerCreatedEvent : Event
    {
        public int IdCustomer { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UsuerCreatedEvent(int idCustomer, string fullName, string email)
        {
            IdCustomer = idCustomer;
            FullName = fullName;
            Email = email;
        }
    }
}
