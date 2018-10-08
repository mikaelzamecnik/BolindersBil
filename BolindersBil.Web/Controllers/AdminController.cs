using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.Web.Infrastructure;
using BolindersBil.Web.Models;
using BolindersBil.Web.Repositories;
using BolindersBil.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BolindersBil.Web.Controllers
{
    public class AdminController : Controller
    {
        private IVehicleRepository vehicleRepo;
        public int PageLimit = 8;
        private IHostingEnvironment _appEnvironment;

        public AdminController(IVehicleRepository vehicleRepository, IHostingEnvironment appEnvironment)
        {
            vehicleRepo = vehicleRepository;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index(int page = 1)
        {
            var vehicles = vehicleRepo.Vehicles;

            var toSkip = (page - 1) * PageLimit;
            var vehiclesInPageLimit = vehicles
                .OrderBy(x => x.Id)
                .Skip(toSkip)
                .Take(PageLimit);

            var totalNumberOfVehicles = vehicles.Count();

            var showButton = true;

            if (page * PageLimit > totalNumberOfVehicles)
            {
                showButton = false;
            }

            var vm = new VehicleListViewModel
            {
                Vehicles = vehiclesInPageLimit,
                ShowButton = showButton,
                NextPage = ++page
            };

            return View(vm);
        }

        // Redigera fordon
        [HttpGet]
        public IActionResult Edit(int vehicleId)
        {

            var vehicle = vehicleRepo.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
            var vm = new EditVehicleViewModel
            {
                DealerShips = vehicleRepo.Dealerships.ToSelectList(vehicle),
                Brands = vehicleRepo.Brands.ToSelectList(vehicle),
                Vehicle = vehicle,
            };
            return View(vm);

        }


        [HttpPost]
        public IActionResult Edit(EditVehicleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Vehicle.DateUpdated = DateTime.Now;
                vehicleRepo.SaveVehicle(vm.Vehicle);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(vm);
            }
        }


        [HttpGet]

        public IActionResult Create(int vehicleId)
        {
            var vehicle = vehicleRepo.Vehicles.FirstOrDefault(x => x.Id.Equals(vehicleId));
            var vm = new EditVehicleViewModel
            {
                DealerShips = vehicleRepo.Dealerships.ToSelectList(vehicle),
                Brands = vehicleRepo.Brands.ToSelectList(vehicle),
                Vehicle = vehicle,
            };
            return View(vm);
        }

        // Skapa nytt fordon
        [HttpPost]
        public async Task<IActionResult> Create(EditVehicleViewModel vm, ICollection<IFormFile> images)
        {

            if (ModelState.IsValid && vm != null)
            {
                /* Paths */
                // to wwwroot
                string rootPath = _appEnvironment.WebRootPath;
                // to Images folder
                string imageFolderPath = rootPath + "\\Images";
                // to Registration folder
                string targetFolder = imageFolderPath + "\\" + vm.Vehicle.RegistrationNumber;
                /* Create Registration folder*/
                Directory.CreateDirectory(targetFolder);
                // Array to store each image
                List<FileUpload> gallery = new List<FileUpload>();
                foreach (var image in images)
                {
                    Guid uniqueGuid = Guid.NewGuid();
                    string targetFileName = uniqueGuid + image.FileName;
                    string finalTargetFilePath = targetFolder + "\\" + targetFileName;
                    //Save images into RegNr folder
                    if (image.Length > 0)
                    {
                        using (var stream = new FileStream(finalTargetFilePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                    }
                    var imageProperty = new FileUpload
                    {
                        FileTitle = uniqueGuid,
                        FilePath = finalTargetFilePath
                    };
                    gallery.Add(imageProperty);
                }

                // Save information to database
                vm.Vehicle.FileUpload = gallery;
                vm.Vehicle.DateAdded = DateTime.Now;
                vehicleRepo.SaveVehicle(vm.Vehicle);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(vm);
            }
        }
        private void ImageResize(string inputImagePath, string outputImagePath, int newWidth)
        {
            const long quality = 50L;
            Bitmap sourceBitmap = new Bitmap(inputImagePath);
            double dblWidthOriginal = sourceBitmap.Width;
            double dblHeigthOriginal = sourceBitmap.Height;
            double heightWidthRelation = dblHeigthOriginal / dblWidthOriginal;
            int newHeight = (int)(newWidth * heightWidthRelation);
            // Create empty draw area.
            var newDrawArea = new Bitmap(newWidth, newHeight);
            using (var graphic_of_DrawArea = Graphics.FromImage(newDrawArea))
            {
                graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;
                graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;
                // Draw into placeholder.
                // Imports the image into the drawarea.
                graphic_of_DrawArea.DrawImage(sourceBitmap, 0, 0, newWidth, newHeight);
                // Output as .Jpg
                using (var output = System.IO.File.Open(outputImagePath, FileMode.Create))
                {
                    // Setup jpg 
                    var qualityParamId = Encoder.Quality;
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                    // Save Bitmap as Jpg 
                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    newDrawArea.Save(output, codec, encoderParameters);
                    output.Close();
                }
                graphic_of_DrawArea.Dispose();
            }
            sourceBitmap.Dispose();
        }
        // Ta bort fordon
        [HttpPost]
        public IActionResult DeleteBulk(string vehiclesIdToDelete)
        {
            if (vehiclesIdToDelete != null)
            {
                int[] vehiclesIdToDeleteArray = Array.ConvertAll(vehiclesIdToDelete.Split(','), int.Parse);

                foreach (var item in vehiclesIdToDeleteArray)
                {
                    Delete(item);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // Ta bort fordon
        [HttpPost]
        public IActionResult Delete(int vehicleId)
        {
            var deleted = vehicleRepo.DeleteVehicle(vehicleId);
            if (deleted != null)
            {
                //product was found and deleted
            }
            else
            {
                //TODO
                //product was not found - show error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}