using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class OutOfRange : Exception
        {
            public OutOfRange(string message) : base(message + " out of range") { }
        }


        public class DroneIsNotInCorrectStatus : Exception {
            public DroneIsNotInCorrectStatus(string message) : base(message) { }
        }

        public class DroneDoesNotHaveEnoughBattery : Exception {
            public DroneDoesNotHaveEnoughBattery() : base("Drone does not have enough battery ") { }

        }

        public class NotFound : Exception
        {
            public NotFound(string e) : base($" {e} not found") { }

        }

        public class IdAlreadyExist : Exception{
            public IdAlreadyExist(int id) : base( $"{id}, not valid") { }

        }

    }
}
