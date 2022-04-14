using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class TransferLog
    {
        public int Id { get; set; }
        public int LastTransferSkip { get; set; }
        public string ActionType { get; set; }
    }
}
