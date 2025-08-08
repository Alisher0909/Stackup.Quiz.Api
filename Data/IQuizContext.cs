using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Stackup.Quiz.Api.Data;

public interface IQuizContext
{
    DbSet<Entities.Quiz> Quizes { get; set; }
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}