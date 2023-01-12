namespace Catstagram.Server.Data.Models.Base
{
    using System.Diagnostics.CodeAnalysis;

    public interface IEntity
    {
         DateTime CreatedOn { get; set; }

        [AllowNull]
        string CreatedBy { get; set; }

         DateTime? ModifiedOn { get; set; }

        [AllowNull]
        string ModifiedBy { get; set; }
    }
}
