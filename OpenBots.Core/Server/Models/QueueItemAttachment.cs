﻿using System;

namespace OpenBots.Core.Server.Models
{
    public class QueueItemAttachment : Entity
    {
        public Guid QueueItemId { get; set; }
        public Guid FileId { get; set; }
    }
}
