﻿using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal partial class DalObject : DalApi.IDal
    {
        #region Add Parcel

        /// <summary>
        /// adds droneCharge to DataSource
        /// </summary>
        /// <param name="parcel">parcel</param>
        public void AddParcel(Parcel parcel)
        {
            if (DataSource.parcels.Any(ps => ps.Id == parcel.Id))
            {
                throw new IdAlreadyExist(parcel.Id);
            }
            DataSource.parcels.Add(parcel);
        }

        #endregion

        #region Get Parcel

        /// <summary>
        /// returns parcels form datasource
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>parcels that full-fill the conditon</returns>
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> getBy = null)
        {
            getBy ??= (parcel => true);
            return (from parcel in DataSource.parcels
                    where (getBy(parcel)/*parcel.IsActive*/)
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
                throw new NotFoundException("parcel");
            }
        }

        #endregion

        #region Update Parcel

        /// <summary>
        /// update parcel in the DataSource
        /// </summary>
        /// <param name="parcel">parcel with updated details</param>
        public void UpdateParcel(Parcel parcel)
        {
            int index = DataSource.parcels.FindIndex(p => p.Id == parcel.Id);
            if (index == -1)
                throw new NotFoundException("parcel");
            DataSource.parcels[index] = parcel;

        }

        #endregion

        #region Delete Parcel

        /// <summary>
        /// removing a parcel by marking it as not active
        /// </summary>
        /// <param name="id">id of parcel to remove</param>
        public void DeleteParcel(int id)
        {
            Parcel parcel = GetParcelById(id);
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }

        #endregion
    }
}