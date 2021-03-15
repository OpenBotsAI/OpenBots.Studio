﻿using System;

namespace OpenBots.NetCore.Core.Server.Models
{
    public class Agent : NamedEntity
    {
        public string MachineName { get; set; }
        public string MacAddresses { get; set; }
        public string IPAddresses { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? LastReportedOn { get; set; }
        public string LastReportedStatus { get; set; }
        public string LastReportedWork { get; set; }
        public string LastReportedMessage { get; set; }
        public bool? IsHealthy { get; set; }
        public bool IsConnected { get; set; }
        public Guid? CredentialId { get; set; }
    }
}
