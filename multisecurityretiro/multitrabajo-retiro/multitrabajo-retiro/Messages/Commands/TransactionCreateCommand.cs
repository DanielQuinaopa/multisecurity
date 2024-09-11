using Cordillera.Distribuidas.Event.Commands;

namespace multitrabajo_retiro.Messages.Commands
{
    public class UsuerCreateCommand : Command
    {
        public int IdUsuer { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = null!;
        public string CreationDate { get; set; } = null!;
        public int AccountId { get; set; }

        public UsuerCreateCommand(int idUsuer, decimal amount, string type, string creationDate, int accountId)
        {
            IdUsuer = idUsuer;
            Amount = amount;
            Type = type;
            CreationDate = creationDate;
            AccountId = accountId;
        }
    }
}
