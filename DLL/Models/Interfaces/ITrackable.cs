using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models.Interfaces
{
    internal interface ITrackable
    {
        DateTimeOffset createdAt { get; set; }
        string createdBy { get; set; }
        DateTimeOffset lastUpdated { get; set; }

    }
}
