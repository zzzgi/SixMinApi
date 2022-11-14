

using SixMinApi.Models;

namespace SixMinApi.Data
{
    public interface ICommandRepo
    {
        Task SafeChanges();
        Task<Command?> GetCommandById(int id);
        Task<IEnumerable<Command>> GetAllCommands();
        Task CreateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}