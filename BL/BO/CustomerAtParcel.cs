using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace BO
    {

        public class CustomerAtParcel
        {

            public CustomerAtParcel()
            {

            }
            public int Id { get; set; }
            public string Name { get; set; }


            public override string ToString()
            {
                return $" id: {Id}, " +
                    $" Name: {Name} ";

                ;
            }

        }
    }

