using MosqueMateServices.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MosqueMateServices.Helper
{
    public class IslamicHelper
    {
        public static double CalculateQiblaDirection(double userLatitude, double userLongitude)
        {
            double kaabaLatitude = 21.3891; // Kaaba's latitude in Mecca
            double kaabaLongitude = 39.8579; // Kaaba's longitude in Mecca

            double deltaLongitude = kaabaLongitude - userLongitude;

            double y = Math.Sin(deltaLongitude);
            double x = Math.Cos(userLatitude) * Math.Tan(kaabaLatitude) - Math.Sin(userLatitude) * Math.Cos(deltaLongitude);

            double qiblaDirection = Math.Atan2(y, x);
            qiblaDirection = qiblaDirection * (180.0 / Math.PI);

            // Adjust to get a positive value
            qiblaDirection = (qiblaDirection + 360) % 360;
            return qiblaDirection;
        }
        public static Directions QiblaDirection(double Latitude, double Longitude)
        {
            var result = (int)CalculateQiblaDirection(Latitude, Longitude);

            switch (result)
            {
                case 0:
                    return Directions.North;
                case 90:
                    return Directions.East;
                case 180:
                    return Directions.South;
                case 270:
                    return Directions.West;
            }

            #region dgree_between_cases
            if (result >= (int)Directions.North && result <= (int)Directions.East)
            {
                return Directions.NorthEast;
            }
            else if (result >= (int)Directions.East && result <= (int)Directions.South)
            {
                return Directions.SouthEast;
            }
            else if (result >= (int)Directions.South && result <= (int)Directions.West)
            {
                return Directions.SouthWest;
            }
            else
            {
                return Directions.NorthWest;
            } 
            #endregion




        }
    }
}
