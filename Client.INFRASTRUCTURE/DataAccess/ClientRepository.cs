using ClientSi.Domain.Abstractions;
using ClientSi.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ClientSi.INFRASTRUCTURE.DataAccess;

public class ClientRepository : IClientRepository
{
    private readonly ClientContext _context;

    public ClientRepository(ClientContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Client client, CancellationToken cancellationToken = default)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Clients
                       .AnyAsync(c => c.Email == email);

    }
}
