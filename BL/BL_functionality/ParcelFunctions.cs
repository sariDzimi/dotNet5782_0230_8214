using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace BL
{
    partial class BL
    {

        /// <summary>
        /// add Parcel To DL
        /// </summary>
        /// <param name="parcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addParcelToDL(Parcel parcel)
        {
            DO.Parcel parcelDL = new DO.Parcel()
            {
                Id = parcel.Id,
                SenderId = parcel.customerAtParcelSender.Id,
                TargetId = parcel.customerAtParcelReciver.Id,
                Weight = (DO.WeightCategories)parcel.Weight,
                Pritority = (DO.Pritorities)parcel.Pritority,
                Requested = parcel.Requested == null ? null : parcel.Requested,
                DroneId = parcel.droneAtParcel == null ? 0 : parcel.droneAtParcel.Id,
                Scheduled = parcel.Scheduled == null ? null : parcel.Scheduled,
                Delivered = parcel.Delivered == null ? null : parcel.Delivered,
                PickedUp = parcel.PickedUp == null ? null : parcel.PickedUp
            };
            try
            {
                lock (dal)
                {
                    dal.AddParcel(parcelDL);
                }
            }
            catch (DalApi.IdAlreadyExistException)
            {
                throw new IdAlreadyExist(parcel.Id);
            }

        }

        /// <summary>
        /// convert To Parcel BL
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Parcel convertToParcelBL(DO.Parcel p)
        {
            Drone droneBL = new Drone();
            DroneAtParcel droneAtParcel;
            if (p.DroneId == 0)
            {
                droneAtParcel = null;
            }
            else
            {
                droneAtParcel = new DroneAtParcel() { Id = p.DroneId };
            }
            //DroneAtParcel droneAtParcel = new DroneAtParcel() { Id = p.DroneId, Battery = droneBL.Battery, Location = droneBL.Location };
            CustomerAtParcel customerAtParcelsender1 = new CustomerAtParcel() { Id = p.SenderId, Name = dal.GetCustomerById(p.SenderId).Name };
            CustomerAtParcel customerAtParcelreciver1 = new CustomerAtParcel() { Id = p.TargetId, Name = dal.GetCustomerById(p.TargetId).Name };

            Parcel ParcelBL = new Parcel() { Id = p.Id, Delivered = p.Delivered, PickedUp = p.PickedUp, droneAtParcel = droneAtParcel, Pritority = (BO.Pritorities)p.Pritority, Requested = p.Requested, Scheduled = p.Scheduled, customerAtParcelSender = customerAtParcelsender1, customerAtParcelReciver = customerAtParcelreciver1, Weight = (BO.WeightCategories)p.Weight };
            return ParcelBL;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleateParcel(int id)
        {
            lock (dal)
            {
                dal.DeleteParcel(id);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateParcel(Parcel parcel)
        {
            lock (dal)
            {
                dal.UpdateParcel(new DO.Parcel()
                {
                    Id = parcel.Id,
                    Delivered = parcel.Delivered,
                    DroneId = parcel.droneAtParcel == null ? 0 : parcel.droneAtParcel.Id,
                    PickedUp = parcel.PickedUp,
                    Pritority = (DO.Pritorities)parcel.Pritority,
                    Requested = parcel.Requested,
                    Scheduled = parcel.Scheduled,
                    SenderId = parcel.customerAtParcelSender.Id,
                    TargetId = parcel.customerAtParcelReciver.Id,
                    Weight = (DO.WeightCategories)parcel.Weight,
                    IsActive = true
                });
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelsBy(Predicate<Parcel> findBy)
        {
            return from parcel in GetParcels()
                   where findBy(parcel)
                   select parcel;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcelById(int id)
        {
            try
            {
                return GetParcelsBy(c => c.Id == id).First();
            }
            catch
            {
                throw new NotFound($"couldn't find Parcel ${id}");
            }
        }

        /// <summary>
        /// Get Parcels
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels()
        {
            lock (dal)
            {
                return from parcel in dal.GetParcels()
                       select convertToParcelBL(parcel);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public ParcelToList convertParcelToParcelToList(Parcel parcel)
        {
            return new ParcelToList()
            {
                ID = parcel.Id,
                NameOfCustomerReciver = parcel.customerAtParcelReciver.Name,
                NameOfCustomerSended = parcel.customerAtParcelSender.Name,
                parcelStatus = ParcelsStatus(parcel),
                pritorities = parcel.Pritority,
                weightCategories = parcel.Weight
                
            };
        }
    }
}

