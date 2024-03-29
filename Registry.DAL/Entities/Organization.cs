﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.DAL.Entities
{
    public class Organization
    {
        public string Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// BIN is a 12-digit number
        /// </summary>
        public string BIN { get; set; }
        public string PhoneNumber { get; set; }
        public int Status { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
    }
}
