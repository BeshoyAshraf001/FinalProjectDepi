namespace FinalProjectDepi.DAL.Repositories.Classes;

using Contexts;
using Entities;
using Interfaces;

public class ClientRepo : GenericRepo<Client>, IClientRepo
{
    public ClientRepo(AppDbContext context)
        : base(context) { }
}
