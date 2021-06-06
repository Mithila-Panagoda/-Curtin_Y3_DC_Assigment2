using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Side
{
    class User
    {
        private User() { }
        private static User instance = null;
        public static User Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new User();
                }
                return instance;
            }
        }
        public uint ID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
    }
}
