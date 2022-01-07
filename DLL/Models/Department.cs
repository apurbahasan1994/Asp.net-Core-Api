using DLL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class Department:ISoftDelete,ITrackable
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTimeOffset createdAt { get; set; }
        public string createdBy { get; set ; }
        public DateTimeOffset lastUpdated { get; set; }
        public string lastUpdatedBy { get; set; }
    }
}
