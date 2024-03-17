using System.Text;
using WebApplication1.Models.Entity;
using WebApplication1.Models.SettingViewModels;
using System.Security.Cryptography;


namespace WebApplication1.Services
{
    public class AccessService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public AccessService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }

        public int CreateAccess(Access entity)
        {
            _timeWaltzContext.Accesses.Add(entity);
            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }

        public List<Access> GetAccessOrNull()
        {
            return _timeWaltzContext.Accesses.ToList();
        }

        public Access? GetAccessOrNull(int id)
        {
            return _timeWaltzContext.Accesses.FirstOrDefault(x => x.Id == id);
        }
        public Access? GetUserOrNull(int id)
        {
            return _timeWaltzContext.Accesses.FirstOrDefault(x => x.Id == id);

        }

        public void DeleteUserType(Access entity)
        {
            if (entity != null)
            {
                _timeWaltzContext.Accesses.Remove(entity);
                _timeWaltzContext.SaveChanges();
            }
        }

        public int updateVacationType(AccessViewModel model, string Salt)
        {
            var entity = _timeWaltzContext.Users.FirstOrDefault(x => x.Id == model.Id);

            //entity.PasswordDate = DateTime.Now;
            //entity.EmployeesId = model.EmployeesName.Value;
            //entity.DepartmentId = model.DepartmentName.Value;
            //entity.Stop = model.Stop;

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
