using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthRestAPI.Models
{
    public class ResponseData
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public DateTime ResponseTime { get; set; } = DateTime.Now;
    }
}