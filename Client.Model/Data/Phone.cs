using System;
using System.Collections.Generic;

namespace Client.Model.Data
{
    public class Phone
    {
        public int Id { get; set; }
        public string NumberPhone { get; set; }
        public int ClientId { get; set; }
        public AClient Client { get; set; }

    }
}