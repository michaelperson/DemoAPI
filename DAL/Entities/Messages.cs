using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Messages
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public int Sender { get; set; }
        public int Recipient { get; set; }
        public int ToGroup   { get; set; }
    }
}
