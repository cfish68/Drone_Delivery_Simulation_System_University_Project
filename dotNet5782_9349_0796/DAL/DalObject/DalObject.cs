﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static IDAL.IDALInterface;


namespace DalObject
{
    public partial class DalObject : IDAL.IDAL
    {
        //creates a DAL object by intializing values accordign to Initialize
        public DalObject()
        {
            DataSource.Initialize();//constructor for DalObjects
        }

        /// <summary>
        /// Returns an array of the power consumptions { Free, Light, Medium, Heavy, ChargingRate }
        /// </summary>
        /// <returns></returns>
        public static double[] GetPowerConsumptions()
        {
            double[] array = new double[5];
            array[0] = DataSource.GetFree();
            array[1] = DataSource.GetLightWeight();
            array[2] = DataSource.GetMediumWeight();
            array[3] = DataSource.GetHeavyWeight();
            array[4] = DataSource.GetChargingRate();

            return array;
        }
    }
}




