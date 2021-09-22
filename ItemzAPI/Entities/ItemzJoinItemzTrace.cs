// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace ItemzApp.API.Entities
{
    public class ItemzJoinItemzTrace
    {
        public Guid FromItemzId { get; set; }
        public virtual Itemz? FromItemz { get; set; }

        public Guid ToItemzId { get; set; }
        public virtual Itemz? ToItemz { get; set; }
    }
}
