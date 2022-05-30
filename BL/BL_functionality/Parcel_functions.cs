using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace BL
{
    partial class BL
    {
        #region Add Parcel
        /// <summary>
        /// adds parcel to Dal
        /// </summary>
        /// <param name="parcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddParcel(Parcel parcel)
        {
            DO.Parcel parcelDL = convertParcelFromBLToDal(parcel);
            int idParcel;
            try
            {
                lock (dal)
                {
                    idParcel= dal.AddParcel(parcelDL);
                }
            }
            catch (DalApi.IdAlreadyExistException)
            {
                throw new IdAlreadyExist("parcel", parcel.Id);
            }
            return idParcel;
        }

       ////
        #endregion

        #region Get Parcel

        /// <summary>
        /// gets parcels that full-fill the condition
        /// </summary>
        /// <param name="findBy">condition</param>
        /// <returns>parcels that full-fill the condition</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelsBy(Predicate<Parcel> findBy)
        {
            return from parcel in GetParcels()
                   where findBy(parcel)
                   select parcel;
        }

        /// <summary>
        /// find a certain parcel
        /// </summary>
        /// <param name="id">id of parcel</param>
        /// <returns>parcel</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcelById(int id)
        {
            try
            {
                return GetParcelsBy(c => c.Id == id).First();
            }
            catch
            {
                throw new NotFound("parcel", id);
            }
        }

        /// <summary>
        /// gets parcel from Dal
        /// </summary>
        /// <returns>parcels</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels()
        {
            lock (dal)
            {
                return from parcel in dal.GetParcels()
                       select convertParcelfromDalToBL(parcel);
            }
        }

        /// <summary>
        /// gets all parcels as ParcelToList type
        /// </summary>
        /// <returns>parcels as ParcelToList type</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcelToLists()
        {
            return from parcel in GetParcels()
                   select convertParcelToTypeOfParcelToList(parcel);

        }

        /// <summary>
        /// returns parcels that full-fill the condition as ParcelToList
        /// </summary>
        /// <param name="findBy">condition</param>
        /// <returns>parcels that full-fill the condition as ParcelToList</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcelsToListBy(Predicate<ParcelToList> findBy)
        {
            return from parcel in GetParcelToLists()
                   where findBy(parcel)
                   select parcel;
        }

        #endregion

        #region Update Parcel

        /// <summary>
        /// updates parcel in Dal
        /// </summary>
        /// <param name="parcel">updated parcel</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel)
        {
            lock (dal)
            {
                try
                {
                    lock (dal)
                    {
                        dal.UpdateParcel(convertParcelFromBLToDal(parcel));
                    }
                }
                catch (DalApi.NotFoundException)
                {
                    throw new NotFound("parcel", parcel.Id);
                }

            }
        }

        #endregion

        #region Delete Parcel

        /// <summary>
        /// deletes parcel from Dal
        /// </summary>
        /// <param name="id">id of parcel</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleateParcel(int id)
        {

            try
            {
                lock (dal)
                {
                    dal.DeleteParcel(id);
                }
            }
            catch (DalApi.NotFoundException)
            {
                throw new NotFound("parcel", id);
            }
        }

        #endregion

        #region Convert Parcel
        /// <summary>
        /// converts parcel from type Bl to type Dal
        /// </summary>
        /// <param name="parcel">parcel type BL</param>
        /// <returns>parcel type Dal</returns>
        private DO.Parcel convertParcelFromBLToDal(Parcel parcel)
        {
            return new DO.Parcel()
            {
                Id = parcel.Id,
                DroneId = parcel.droneAtParcel == null ? 0 : parcel.droneAtParcel.Id,
                SenderId = parcel.customerAtParcelSender.Id,
                TargetId = parcel.customerAtParcelReciver.Id,
                Weight = (DO.WeightCategories)parcel.Weight,
                Pritority = (DO.Pritorities)parcel.Pritority,
                Requested = parcel.Requested == null ? null : parcel.Requested,
                Scheduled = parcel.Scheduled == null ? null : parcel.Scheduled,
                Delivered = parcel.Delivered == null ? null : parcel.Delivered,
                PickedUp = parcel.PickedUp == null ? null : parcel.PickedUp,
                IsActive = true
            };
        }

        /// <summary>
        /// converts parcel of type Dal to type BL
        /// </summary>
        /// <param name="p">parcel of type Dal</param>
        /// <returns>parcel of </returns>
        private Parcel convertParcelfromDalToBL(DO.Parcel p)
        {

            DroneAtParcel droneAtParcel = (p.DroneId == 0) ? null : new DroneAtParcel() { Id = p.DroneId };

            CustomerAtParcel customerAtParcelsender1 = new CustomerAtParcel()
            {
                Id = p.SenderId,
                Name = dal.GetCustomerById(p.SenderId).Name
            };
            CustomerAtParcel customerAtParcelreciver1 = new CustomerAtParcel()
            {
                Id = p.TargetId,
                Name = dal.GetCustomerById(p.TargetId).Name
            };

            Parcel ParcelBL = new Parcel()
            {
                Id = p.Id,
                Delivered = p.Delivered,
                PickedUp = p.PickedUp,
                droneAtParcel = droneAtParcel,
                Pritority = (BO.Pritorities)p.Pritority,
                Requested = p.Requested,
                Scheduled = p.Scheduled,
                customerAtParcelSender = customerAtParcelsender1,
                customerAtParcelReciver = customerAtParcelreciver1,
                Weight = (BO.WeightCategories)p.Weight
            };
            return ParcelBL;
        }

        /// <summary>
        /// converts ParcelToList to Parcel type
        /// </summary>
        /// <param name="parcelToList">ParcelToList</param>
        /// <returns>parcel</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel ConvertParcelToTypeOfListToParcel(ParcelToList parcelToList)
        {
            return GetParcelById(parcelToList.Id);
        }

        /// <summary>
        /// covnerts a parcel to type of parcelToList
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public ParcelToList convertParcelToTypeOfParcelToList(Parcel parcel)
        {
            return new ParcelToList()
            {
                Id = parcel.Id,
                NameOfCustomerReciver = parcel.customerAtParcelReciver.Name,
                NameOfCustomerSended = parcel.customerAtParcelSender.Name,
                parcelStatus = calculateParcelsStatus(parcel),
                pritorities = parcel.Pritority,
                weightCategories = parcel.Weight
            };
        }

        /// <summary>
        /// convert parcel to type of ParcelInDelivery
        /// </summary>
        /// <param name="parcel">parcle</param>
        /// <param name="isParclelWaiting">bool isWaiting</param>
        /// <returns>parcelInDelivery</returns>
        private ParcelInDelivery convertParcelToTypeOfParcelInDelivery(Parcel parcel, bool isParclelWaiting)
        {
            Location SenderLocation = GetCustomerById(parcel.customerAtParcelSender.Id).Location;
            Location ReciverLocation = GetCustomerById(parcel.customerAtParcelReciver.Id).Location;

            return new ParcelInDelivery()
            {
                customerAtParcelTheReciver = parcel.customerAtParcelReciver,
                customerAtParcelTheSender = parcel.customerAtParcelSender,
                Id = parcel.Id,
                IsWating = isParclelWaiting,
                locationCollect = SenderLocation,
                locationTarget = ReciverLocation,
                distance = calculateDistanceBetweenTwoLocationds(SenderLocation, ReciverLocation),
                pritorities = parcel.Pritority,
                weightCategories = parcel.Weight
            };
        }

        #endregion

    }
}

