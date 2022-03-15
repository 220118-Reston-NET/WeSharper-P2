using System.Threading.Tasks;
using AutoMapper;
using WeSharper.APIPortal.Interfaces;
using WeSharper.Models;

namespace WeSharper.APIPortal.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;
        private readonly WeSharperContext _context;
        public UnitOfWork(WeSharperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IMessageManagement MessageManagement => new MessageManagement(_context, _mapper);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            _context.ChangeTracker.DetectChanges();
            var changes = _context.ChangeTracker.HasChanges();

            return changes;
        }
    }
}