using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project.Areas.Identity.Data;
using project.Models;
using project.Services;

namespace project.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IVehicleService _vehicleService;
        private readonly UserManager<AppUser> _userManager;

        public BookingController(IBookingService bookingService, IVehicleService vehicleService, UserManager<AppUser> userManager)
        {
            _bookingService = bookingService;
            _userManager = userManager;
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var bookings = await _bookingService.GetUserBookingsAsync(userId);
            return View(bookings);
        }

        public async Task<IActionResult> Create(int vehicleId)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(vehicleId);
            if (vehicle == null)
                return NotFound();
            var booking = new Booking { Vehicle = vehicle, VehicleId = vehicleId };
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            booking.UserId = _userManager.GetUserId(User);
            ModelState.Remove(nameof(Booking.UserId));

            if (!TryValidateModel(booking))
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Log errors to console
                }
                return View(booking);
            }

            var vehicle = _vehicleService.GetVehicleByIdAsync(booking.VehicleId);
            if (vehicle == null)
                NotFound();

            await _bookingService.CreateBookingAsync(booking);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            await _bookingService.DeleteBookingAsync(id, userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
