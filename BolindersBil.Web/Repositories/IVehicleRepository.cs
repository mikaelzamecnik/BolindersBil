using BolindersBil.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BolindersBil.Web.Repositories
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> Vehicles { get; }
        IEnumerable<Brand> Brands {get;}
        IEnumerable<Dealership> Dealerships { get; }
        IEnumerable<FileUpload> FileUploads { get; }

        void SaveVehicle(Vehicle v);
        Vehicle DeleteVehicle(int vehicleId);



    }
}
