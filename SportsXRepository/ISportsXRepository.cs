using System.Threading.Tasks;
using SportsXDomain;

namespace SportsXRepository
{
    public interface ISportsXRepository
    {
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveChangesAsync();

        Task<Cliente[]> GetAllClientesAsync(bool includeTelefone);
        Task<Cliente[]> GetAllClientesAsyncByName(string nome);
        Task<Cliente> GetClientesById(int ClienteId);
    }
}