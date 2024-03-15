using System.Security.Cryptography;
using System.Text;
using WebApplication1.Models.Entity;
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

        public User? GetUserOrNull(int id)
        {
            return _timeWaltzContext.Users.FirstOrDefault(x => x.Id == id);
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
                entity.PasswordDate = DateTime.Now;
                entity.EmployeesId = Convert.ToInt32(model.DepartmentName);
                entity.DepartmentId = Convert.ToInt32(model.EmployeesName);
                entity.Salt = Salt;
                entity.Stop = model.Stop;

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
