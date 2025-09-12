using MrBekoXBlogAppServer.Application.Interfaces.Repositories.ContactInfoRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreContactInfoRepository;

public class EfCoreContactInfoWriteRepository : EfCoreWriteRepository<ContactInfo>, IContactInfoWriteRepository
{
    public EfCoreContactInfoWriteRepository(AppDbContext context) : base(context)
    {
    }
}
