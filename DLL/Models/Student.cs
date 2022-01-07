using System;
using System.Collections.Generic;
using System.Text;
using DLL.Models.Interfaces;

namespace DLL.Models
{
    public class Student: ISoftDelete,ITrackable
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTimeOffset createdAt { get; set ; }
        public string createdBy { get ; set ; }
        public DateTimeOffset lastUpdated { get ; set; }
        public string lastUpdatedBy { get ; set ; }
    }
}
