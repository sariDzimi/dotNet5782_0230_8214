using System;
using System.Collections.Generic;
using System.IO;
using DO;
using System.Linq;
using System.Xml.Linq;
using DalApi;

namespace Dal
{
    public partial class DalXml : IDal
    {
        #region Add Parcel
        /// <summary>
        /// adds parcel to parcels xml file
        /// </summary>
        /// <param name="parcel">parcel</param>
        public void AddParcel(Parcel parcel)
        {
            List<DO.Parcel> parcelList = GetParcels().ToList();
            if (parcelList.Any(p => p.Id == parcel.Id))
            {
                throw new IdAlreadyExistException("parcel", parcel.Id);
            }

            parcelList.Add(parcel);

            XMLTools.SaveListToXMLSerializer<DO.Parcel>(parcelList, dir + parcelFilePath);
        }

        #endregion

        #region Get Parcel

        /// <summary>
        /// returns parcles form parcels xml file
        /// </summary>
        /// <param name="getBy">condition</param>
        /// <returns>parcels that full-fill the conditon</returns>
        public IEnumerable<DO.Parcel> GetParcels(Predicate<DO.Parcel> getBy = null)
        {
            IEnumerable<DO.Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(dir + parcelFilePath);

            getBy ??= ((st) => true);
            return from parcel in parcelList
                   where getBy(parcel) && parcel.IsActive == true
                   orderby parcel.Id
                   select parcel;
        }

        /// <summary>
        /// finds a parcel by id
        /// </summary>
        /// <param name="id">id of parcel</param>
        /// <returns>parcel with the given id</returns>
        public DO.Parcel GetParcelById(int id)
        {
            try
            {
                return GetParcels(p => p.Id == id).First();
            }
            catch (Exception)
            {
                throw new NotFoundException("parcel", id);
            }

        }

        #endregion

        #region UpdateParcel

        /// <summary>
        /// updates the parcel in the parcels xml file
        /// </summary>
        /// <param name="parcel">parcel with updated details</param>
        public void UpdateParcel(Parcel parcel)
        {
            List<DO.Parcel> parcelList = GetParcels().ToList();

            int index = parcelList.FindIndex(d => d.Id == parcel.Id);

            if (index == -1)
                throw new NotFoundException("parcel", parcel.Id);

            parcelList[index] = parcel;

            XMLTools.SaveListToXMLSerializer<DO.Parcel>(parcelList, dir + parcelFilePath);
        }

        #endregion

        #region Delete Parcel

        /// <summary>
        /// deletes parcel from p xarcelsml file
        /// </summary>
        /// <param name="id">id of parcel</param>
        public void DeleteParcel(int id)
        {
            Parcel parcel;
            try
            {
                parcel = GetParcelById(id);
            }
            catch
            {
                throw new NotFoundException("parcel", id);
            }
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }

        #endregion
    }
}
