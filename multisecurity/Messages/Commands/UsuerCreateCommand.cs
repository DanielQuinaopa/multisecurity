using Cordillera.Distribuidas.Event.Commands;
using Cordillera.Distribuidas.Event.Events;

namespace multitrabajo_retiro.Messages.Commands
{
    public class UsuerCreateCommand : Command
    {
        public int IdCustomer { get; }
        public string FullName { get; }
        public string Email { get; }

        // Constructor que requiere los tres parámetros
        public UsuerCreateCommand(int idCustomer, string fullName, string email)
        {
            IdCustomer = idCustomer;
            FullName = fullName;
            Email = email;
        }
    }
}
