//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DalApi;
//using DalObject;

//namespace DAL
//{
//    public static class DalFactory
//    {
//        public static IDal GetDal(string typeDal)
//        {
//            switch (typeDal)
//            {
//                case "DalObject":
//                    return DalObject.DalObject.GetInstance;
//                case "DalXml":
//                    //return DalObject.DalXml.GetInstance;
//                    return DalObject.DalObject.GetInstance;
//                default:
//                    throw new NoSuchInstance();
//            }
//        }
//    }
//}
