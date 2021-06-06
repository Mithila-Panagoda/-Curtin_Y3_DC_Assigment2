using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Teir
{
    class User
    {
        public User(uint ID, string fName, string lName)
        {
            this.ID = ID;
            this.fName = fName;
            this.lName = lName;
        }

        public uint ID { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
    }
}
