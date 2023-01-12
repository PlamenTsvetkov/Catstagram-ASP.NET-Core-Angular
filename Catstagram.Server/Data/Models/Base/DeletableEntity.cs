namespace Catstagram.Server.Data.Models.Base
{
    public class DeletableEntity : Entity, IDeletableEntity
    {
        public DateTime? DelateOn { get; set; }

        public string DeletedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}