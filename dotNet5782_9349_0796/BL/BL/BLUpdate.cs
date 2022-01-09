﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        /// <summary>
        /// If drone Id given exists, the Drone Model will be updated and a Drone will be returned
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Model"></param>
        public  Drone UpdateDrone(int Id, string Model)
        {

            int Dronei = DalObject.DataSource.DroneList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if(Dronei == -1)
            {
                throw new MessageException("Error: Drone not found\n");
            }
            
            DO.Drone Drone = DalObject.DataSource.DroneList[Dronei];
            Drone.Model = Model;
            DalObject.DataSource.DroneList[Dronei] = Drone;

            int Listi = BLObject.BLDroneList.FindIndex(x => x.Id == Id);
            BLObject.BLDroneList[Listi].Model = Model;


            //Create Drone to return
            Drone d = new();
            d.Id = Drone.Id;
            d.Model = Model;
            d.Weight = BLObject.BLDroneList[Listi].Weight;
            d.Location = BLObject.BLDroneList[Listi].Location;
            d.BatteryStatus = BLObject.BLDroneList[Listi].BatteryStatus;
            d.Status = BLObject.BLDroneList[Listi].DroneStatus;

            return d;
        }

        /// <summary>
        /// Updates Station
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="StationName"></param>
        /// <param name="AmountOfChargingStation"></param>
        public  void UpdateStation(int Id, int StationName = -1, int AmountOfChargingStation = -1)
        {
            int Stationi = DalObject.DataSource.StationList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Stationi == -1)
            {
                throw new MessageException("Error: Station not found\n");
            }

            DO.Station Station = DalObject.DataSource.StationList[Stationi];
            if(StationName != -1)
            {
                Station.Name = StationName;
            }
            if(AmountOfChargingStation != -1)
            {
                Station.ChargeSlots = AmountOfChargingStation;
            }
            
            DalObject.DataSource.StationList[Stationi] = Station;

        }

        /// <summary>
        /// updates a customer: options to update are name and or phone.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        public  void UpdateCustomer(int Id, string Name = "-1", string Phone = "-1")
        {
            int Customeri = DalObject.DataSource.CustomerList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Customeri == -1)
            {
                throw new MessageException("Error: Customer not found\n");
            }

            DO.Customer Customer = DalObject.DataSource.CustomerList[Customeri];
            if (Name != "-1")
            {
                Customer.Name = Name;
            }
            if (Phone != "-1")
            {
                Customer.Phone = Phone;
            }

            DalObject.DataSource.CustomerList[Customeri] = Customer;
        }

        /// <summary>
        /// updates a customer: options to update are name and or phone.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        public void UpdatePackage(int Id, int SenderId = 0, int ReceiverId = 0, string Weight = "", string Priority = "")
        {

            int packagei = DalObject.DataSource.PackageList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (packagei == -1)
            {
                throw new MessageException("Error: Package not found\n");
            }

            DO.Package package = DalObject.DataSource.PackageList[packagei];

            if (SenderId != 0) { 
                int Senderi = DalObject.DataSource.CustomerList.FindIndex(x => x.Id == SenderId);
                if (Senderi == -1)
                {
                    throw new MessageException("Error: Sender not found\n");
                }
                package.SenderId = SenderId;
            }

            if (ReceiverId != 0)
            {
                int Receiveri = DalObject.DataSource.CustomerList.FindIndex(x => x.Id == ReceiverId);
                if (Receiveri == -1)
                {
                    throw new MessageException("Error: Receiver not found\n");
                }
                package.ReceiverId = ReceiverId;
            }

            //Check if Weight is valid
            if (Weight != "")
            {
                if (Weight != "light" && Weight != "medium" && Weight != "heavy")
                    throw new MessageException("Error: Weight status invalid\n");
                //If valid, convert to WeightCatagory
                DO.WeightCategory weightCatagory = (DO.WeightCategory)Enum.Parse(typeof(DO.WeightCategory), Weight);

                package.Weight = weightCatagory;
            }

            //Check if Priority is valid
            if (Priority != "")
            {
                if (Priority != "regular" && Priority != "fast" && Priority != "emergency")
                    throw new MessageException("Error: Weight status invalid\n");
                //If valid, convert to WeightCatagory
                DO.Priority priorityCatagory = (DO.Priority)Enum.Parse(typeof(DO.Priority), Priority);

                package.Priority = priorityCatagory;
            }

            DalObject.DataSource.PackageList[packagei] = package;
        }
    }
}
