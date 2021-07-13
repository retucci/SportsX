using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsXDomain;
using SportsXRepository;

namespace SportsXRespository
{
    public class SportsXRepo : ISportsXRepository
    {
        private SportsXContext _context { get; set; }

        public SportsXRepo(SportsXContext context)
        {
            _context = context;
            // Adicionando NoTracking para não travar o recurso do EF ao realizar as querys
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Cliente[]> GetAllClientesAsync(bool includeTelefone)
        {
            IQueryable<Cliente> query = _context.Clientes.Include(i => i.Telefones);
            query = query.OrderByDescending(o => o.Nome);
            return await query.ToArrayAsync();
        }

        public async Task<Cliente[]> GetAllClientesAsyncByName(string nome)
        {
            IQueryable<Cliente> query = _context.Clientes.Include(i => i.Telefones);
            query = query.Where(w=> w.Nome.ToLower().Contains(nome.ToLower())).OrderByDescending(o => o.Nome);
            return await query.ToArrayAsync();
        }

        public async Task<Cliente> GetClientesById(int ClienteId)
        {
            IQueryable<Cliente> query = _context.Clientes.Include(i => i.Telefones);
            query = query.Where(w=> w.Id == ClienteId).OrderByDescending(o => o.Nome);
            return await query.FirstOrDefaultAsync();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}