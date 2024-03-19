using Repository.Models;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Models.SettingViewModels;

namespace WebApplication1.Services
{
    public class UserService
    {
        private readonly TimeWaltzContext _timeWaltzContext;

        public UserService(TimeWaltzContext timeWaltzContext)
        {
            _timeWaltzContext = timeWaltzContext;
        }

        public int CreateUser(User entity)
        {
            _timeWaltzContext.Users.Add(entity);
            _timeWaltzContext.SaveChanges();
            return entity.Id;
        }

        public List<User> GetUserOrNull()
        {
            return _timeWaltzContext.Users.ToList();
        }

        public List<UserViewModel> GetUserViewModel()
        {

            var query = _timeWaltzContext.Users
                .Join(_timeWaltzContext.Employees, x => x.Id, d => d.Id, (x, d) => new { x, d })
                 .Join(_timeWaltzContext.Departments, x => x.x.DepartmentId, y => y.Id, (xd, y) => new { xd, y })
                 .Select(x => new UserViewModel
                 {
                     Id = x.xd.x.Id,
                     Account = x.xd.x.Account,
                     EmployeesID = x.xd.x.EmployeesId,
                     DepartmentID = x.xd.x.DepartmentId,
                     EmployeesName = x.xd.d.Name,
                     DepartmentName = x.y.DepartmentName,
                     StopName = (x.xd.x.Stop ? "啟用" : "停用"),

                 }).ToList();

            return query;
        }
        public List<UserViewModel> GetUserViewModel(UserViewModel entity)
        {

            var query = _timeWaltzContext.Users
                .Join(_timeWaltzContext.Employees, x => x.Id, d => d.Id, (x, d) => new { x, d })
                 .Join(_timeWaltzContext.Departments, x => x, y => y.Id, (xd, y) => new { xd, y });
            if (!string.IsNullOrEmpty(entity.EmployeesName))
            {
                query = query.Where(x => x.xd.d.Name.Contains(entity.EmployeesName.ToString()));
            }

            if (!string.IsNullOrEmpty(entity.DepartmentName))
            {
                query = query.Where(x => x.xd.x.DepartmentId.ToString() == entity.DepartmentName);
            }

            if (!string.IsNullOrEmpty(entity.Account))
            {
                query = query.Where(x => x.xd.x.Account.Contains(entity.Account));
            }
            var query1= query.Select(x => new UserViewModel
                 {
                     Id = x.xd.x.Id,
                     Account = x.xd.x.Account,
                     EmployeesID = x.xd.x.EmployeesId,
                     DepartmentID = x.xd.x.DepartmentId,
                     EmployeesName = x.xd.d.Name,
                     DepartmentName = x.y.DepartmentName,
                     StopName = (x.xd.x.Stop ? "啟用" : "停用"),

                 }).ToList();

            return query1;
        }
        public User? GetUserOrNull(int id)
        {
            return _timeWaltzContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetUserOrNull(UserViewModel entity)
        {
            var query = _timeWaltzContext.Users
                .Join(_timeWaltzContext.Employees, x => x.Id, d => d.Id, (x, d) => new { x, d });

            if (!string.IsNullOrEmpty(entity.EmployeesName))
            {
                query = query.Where(x => x.d.Name.Contains(entity.EmployeesName.ToString()));
            }

            if (!string.IsNullOrEmpty(entity.DepartmentName.ToString()))
            {
                query = query.Where(x => x.x.DepartmentId.ToString() == entity.DepartmentName);
            }

            if (!string.IsNullOrEmpty(entity.Account))
            {
                query = query.Where(x => x.x.Account.Contains(entity.Account));
            }

            return query.Select(x => x.x).ToList(); 
        }
        public List<User> GetUserCreateOrNull(UserCreateViewModel entity)
        {
            List<User> entities = _timeWaltzContext.Users.Where(x => x.Account == entity.Account).ToList();
                return entities;
        }

        public void DeleteeUserType(User entity)
        {
            if (entity != null)
            {
                _timeWaltzContext.Users.Remove(entity);
                _timeWaltzContext.SaveChanges();
            }
        }

        public int EditUserType(UserEditViewModel model, string Salt)
        {
            var entity = _timeWaltzContext.Users.FirstOrDefault(x => x.Id == model.Id);

            if (entity != null)
            {
                if (Salt != "")
                {
                    entity.Salt = Salt;
                    entity.Password = model.Password;
                }
                entity.PasswordDate = DateTime.Now;
                entity.EmployeesId = Convert.ToInt32(model.EmployeesName);
                entity.DepartmentId = Convert.ToInt32(model.DepartmentName);
                entity.Stop = model.Stop==1?true:false;

                _timeWaltzContext.SaveChanges();
            }
            return model.Id;
        }

        public void DeleteUserType(User entity)
        {
            if (entity != null)
            {
                _timeWaltzContext.Users.Remove(entity);
                _timeWaltzContext.SaveChanges();
            }
        }



        /// <summary>
        /// SHA256加密，返回字串
        /// </summary>
        /// <param name="deseninstr">待加密字串</param>
        /// <param name="isupper">false:小寫 true:大寫</param>
        /// <returns></returns>
        public string SHA256EncryptString(string deseninstr, bool isupper = false)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(deseninstr);
            using (var mySHA256 = SHA256.Create())
            {
                byte[] hash = mySHA256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString(isupper ? "X2" : "x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// 隨機鹽，長度12碼
        /// </summary>
        /// <returns></returns>
        public string GenerateSalt()
        {
            StringBuilder sb = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < 13; i++)
            {
                char[] chars = (Get_Salt(random.Next(4))).ToCharArray();

                char aChar = chars[random.Next(chars.Length)];
                sb.Append(aChar);
            }
            return sb.ToString();
        }

        public string Get_Salt(int i)
        {
            return i switch
            {
                0 => "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                1 => "abcdefghijklmnopqrstuvwxyz",
                2 => "1234567890",
                3 => "!@#$%^&*()_+",
            };
        }



    }
}
