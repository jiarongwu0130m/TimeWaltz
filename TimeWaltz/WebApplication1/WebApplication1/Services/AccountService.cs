using WebApplication1.Models.Entity;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class AccountService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public AccountService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }
        public int CreateAccess(User entity)
        {
            _timeWaltzContext.Users.Add(entity);
            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }

        public List<User> GetAccessOrNull()
        {
            return _timeWaltzContext.Users.ToList();
        }

        public User? GetAccessOrNull(int id)
        {
            return _timeWaltzContext.Users.FirstOrDefault(x => x.Id == id);

        }

        public int updateVacationType(AccountViewModel model, string Salt)
        {
            var entity = _timeWaltzContext.Users.FirstOrDefault(x => x.Id == model.Id);

            entity.PasswordDate = DateTime.Now;
            entity.EmployeesId = model.EmployeesID.Value;
            entity.DepartmentId = model.DepartmentID.Value;
            entity.Stop = model.Stop;

            _timeWaltzContext.SaveChanges();
            return model.Id;
        }

        public void DeleteVacationType(Access entity)
        {
            if (entity != null)
            {
                _timeWaltzContext.Accesses.Remove(entity);
                _timeWaltzContext.SaveChanges();
            }
        }

    }

}
