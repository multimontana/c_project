﻿namespace InfraManager.WebApi.DAL.DTOs.Calls
{
    using System;

    public class CallType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Icon { get; set; }
    }
}
