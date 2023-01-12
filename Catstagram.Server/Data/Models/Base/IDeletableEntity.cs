﻿namespace Catstagram.Server.Data.Models.Base
{
    public interface IDeletableEntity : IEntity
    {
         DateTime? DelateOn { get; set; }

         string DeletedBy { get; set; }

         bool IsDeleted { get; set; }
    }
}
