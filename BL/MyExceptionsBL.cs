using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{

    /// <summary>
    ///  exception that object is out of range
    /// </summary>
    public class OutOfRange : Exception
    {
        public OutOfRange(string obj) : base($"{obj} out of range") { }
    }

    /// <summary>
    /// exception that the drone is not in a correct status for a certain action
    /// </summary>
    public class DroneIsNotInCorrectStatus : Exception
    {
        public DroneIsNotInCorrectStatus(string statusOfDrone) : base($"drone is in status: {statusOfDrone}") {}
    }

    /// <summary>
    /// exception that the drone doesn't have enough battery for a certain action
    /// </summary>
    public class DroneDoesNotHaveEnoughBattery : Exception
    {
        public DroneDoesNotHaveEnoughBattery(int id) : base($"Drone with id: {id}, does not have enough battery") { }

    }

    /// <summary>
    /// exception that a certain object is not found
    /// </summary>
    public class NotFound : Exception
    {
        public NotFound(string obj, int id) : base($"{obj} with id: {id} is not found") { }
        public NotFound(string message) : base(message) { }

    }

    /// <summary>
    /// exception that an object with a certaion id aleready exists
    /// </summary>
    public class IdAlreadyExist : Exception
    {
        public IdAlreadyExist(string obj, int id) : base($"{obj} with id: {id}, already exits") { }

    }

}

