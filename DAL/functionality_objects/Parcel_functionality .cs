using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        #region Add Parcel

        /// <summary>
        /// adds droneCharge to DataSource
        /// </summary>
        /// <param name="parcel">parcel</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddParcel(Parcel parcel)
        {
            Random rand = new Random();
            do
            {
                parcel.Id = rand.Next(111, 999);

            } while (DataSource.parcels.Any(ps => ps.Id == parcel.Id));
            DataSource.parcels.Add(parcel);
            return parcel.Id;
        }

        #endregion

        #region Get Parcel

        /// <summary>
        /// returns parcels form datasource
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>parcels that full-fill the conditon</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> getBy = null)
        {
            getBy ??= (parcel => true);
            return (from parcel in DataSource.parcels
                    where (getBy(parcel) & parcel.IsActive == true)
                    select parcel);
        }

        /// <summary>
        /// finds a parcel by id
        /// </summary>
        /// <param name="id">id of parcel</param>
        /// <returns>parcel with the given id</returns>
        public Parcel GetParcelById(int id)
        {
            try
            {

                return GetParcels(p => p.Id == id).First();
            }
            catch
            {
                throw new NotFoundException("parcel", id);
            }
        }

        #endregion

        #region Update Parcel

        /// <summary>
        /// update parcel in the DataSource
        /// </summary>
        /// <param name="parcel">parcel with updated details</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel)
        {
            int index = DataSource.parcels.FindIndex(p => p.Id == parcel.Id);
            if (index == -1)
                throw new NotFoundException("parcel", parcel.Id);
            DataSource.parcels[index] = parcel;

        }

        #endregion

        #region Delete Parcel

        /// <summary>
        /// removing a parcel by marking it as not active
        /// </summary>
        /// <param name="id">id of parcel to remove</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            Parcel parcel = GetParcelById(id);
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }

        #endregion
    }
}