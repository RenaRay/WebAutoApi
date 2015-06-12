using WebAuto.Backend.Models;
using WebAuto.DataAccess;

namespace WebAuto.Backend.Extensions
{
    public static class CarExtensions
    {
        public static CarModel ToModel(this Car car)
        {
            if (car == null)
            {
                return null;
            }
            return
                new CarModel
                {
                    Id = car.Id,
                    Plate = car.Plate,
                    Vendor = car.Vendor,
                    Model = car.Model
                };
        }

        public static Car ToDataContract(this CarModel car)
        {
            if (car == null)
            {
                return null;
            }
            return
                new Car
                {
                    Id = car.Id,
                    Plate = car.Plate.ToUpper(),
                    Vendor = car.Vendor,
                    Model = car.Model
                };
        }
    }
}