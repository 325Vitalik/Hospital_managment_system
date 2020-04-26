using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
    }
}
