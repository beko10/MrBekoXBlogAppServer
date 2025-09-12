using MrBekoXBlogAppServer.Application.Interfaces.Repositories.ContactInfoRepository;
using MrBekoXBlogAppServer.Domain.Entities;
using MrBekoXBlogAppServer.Persistence.Context;

namespace MrBekoXBlogAppServer.Persistence.Repositories.EfCoreContactInfoRepository;

public class EfCoreContactInfoReadRepository : EfCoreReadRepository<ContactInfo>, IContactInfoReadRepository
{
    public EfCoreContactInfoReadRepository(AppDbContext context) : base(context)
    {
    }
}
