using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        #region Parcel
        /// <summary>
        /// adds the parcel to the parcels list in the DataSource
        /// If the ID alredy exist the function will throw exception
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel)
        {
            if (DataSource.parcels.Any(ps => ps.Id == parcel.Id))
            {
                throw new IdAlreadyExist(parcel.Id);
            }
            DataSource.parcels.Add(parcel);
        }

        /// <summary>
        /// returns parcel form datasource
        /// </summary>
        /// <returns>DataSource.customers</returns>
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> getBy = null)
        {
            getBy ??= (parcel => true);
            return (from parcel in DataSource.parcels
                    where (getBy(parcel)/*parcel.IsActive*/)
                    select parcel);
        }
        public Parcel GetParcelById(int id)
        {
            try
            {

                return GetParcels(p => p.Id == id).First();
            }
            catch
            {
                throw new NotFoundException("parcel");
            }
        }

        /// <summary>
        /// updates the drones list in the database
        /// </summary>
        /// <param name="parcel"></param>
        public void UpdateParcel(Parcel parcel)
        {
            int index = DataSource.parcels.FindIndex(p => p.Id == parcel.Id);
            if (index == -1)
                throw new NotFoundException("parcel");
            DataSource.parcels[index] = parcel;

        }
        public void DeleteParcel(int id)
        {
            Parcel parcel = GetParcelById(id);
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }

        #endregion
    }
}