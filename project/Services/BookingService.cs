using project.Models;
using project.Repositories;

namespace project.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<Booking>> GetUserBookingsAsync(string userId)
        {
            return await _bookingRepository.GetBookingsByUserAsync(userId);
        }

        public async Task<Booking?> GetUserBookingByIdAsync(int id, string userId)
        {
            return await _bookingRepository.GetBookingByIdAsync(id, userId);
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            await _bookingRepository.AddBookingAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await _bookingRepository.UpdateBookingAsync(booking);
        }

        public async Task DeleteBookingAsync(int id, string userId)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id, userId);
            if (booking != null)
            {
                await _bookingRepository.DeleteBookingAsync(booking);
            }
        }
    }
}
