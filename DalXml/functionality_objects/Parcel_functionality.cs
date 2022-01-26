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
        #region Parcel
        public void AddParcel(Parcel parcel)
        {
            List<DO.Parcel> parcelList = GetParcels().ToList();
            if (parcelList.Any(p => p.Id == parcel.Id))
            {
                throw new IdAlreadyExist(parcel.Id);
            }

            parcelList.Add(parcel);

            XMLTools.SaveListToXMLSerializer<DO.Parcel>(parcelList, dir + parcelFilePath);
        }
        public void DeleteParcel(int id)
        {
            Parcel parcel;
            try
            {
                parcel = GetParcelById(id);
            }
            catch
            {
                throw new NotFoundException("parcel");
            }
            parcel.IsActive = false;
            UpdateParcel(parcel);
        }
        public void UpdateParcel(Parcel parcel)
        {
            List<DO.Parcel> parcelList = GetParcels().ToList();

            int index = parcelList.FindIndex(d => d.Id == parcel.Id);

            if (index == -1)
                throw new NotFoundException("parcel");

            parcelList[index] = parcel;

            XMLTools.SaveListToXMLSerializer<DO.Parcel>(parcelList, dir + parcelFilePath);
        }
        public DO.Parcel GetParcelById(int id)
        {
            try
            {
                return GetParcels(p => p.Id == id).First();
            }
            catch (Exception)
            {
                throw new NotFoundException("parcel");
            }

        }
        public IEnumerable<DO.Parcel> GetParcels(Predicate<DO.Parcel> predicat = null)
        {
            IEnumerable<DO.Parcel> parcelList = XMLTools.LoadListFromXMLSerializer<DO.Parcel>(dir + parcelFilePath);

            predicat ??= ((st) => true);
            return from parcel in parcelList
                   where predicat(parcel)
                   orderby parcel.Id
                   select parcel;
        }


        #endregion
    }
}
