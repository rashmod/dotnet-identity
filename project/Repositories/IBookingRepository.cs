using project.Models;

namespace project.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookingsByUserAsync(string userId);
        Task<Booking?> GetBookingByIdAsync(int id, string userId);
        Task AddBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(Booking booking);
    }
}
