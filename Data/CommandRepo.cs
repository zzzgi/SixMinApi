using Microsoft.EntityFrameworkCore;
using SixMinApi.Models;

namespace SixMinApi.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public async void DeleteCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Command>> GetAllCommands()
        {
            return await _context.Commands.ToListAsync();
        }

        public async Task<Command?> GetCommandById(int id)
        {
            return await _context.Commands.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task SafeChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}