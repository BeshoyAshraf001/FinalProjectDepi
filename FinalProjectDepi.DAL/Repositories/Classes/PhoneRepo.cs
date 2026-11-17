namespace FinalProjectDepi.DAL.Repositories.Classes;

using Contexts;
using Entities;
using Interfaces;

public class PhoneRepo : GenericRepo<Phone>, IPhoneRepo
{
    public PhoneRepo(AppDbContext context)
        : base(context) { }
}
