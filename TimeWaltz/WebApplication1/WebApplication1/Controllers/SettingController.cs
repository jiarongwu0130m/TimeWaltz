using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Models.Entity;

namespace WebApplication1.Controllers
{
    public class SettingController : Controller
    {
        private readonly TimeWaltzContext _db;

        public SettingController(TimeWaltzContext db)
        {
            _db = db;
        }
        /// <summary>
        /// 雜湊SHA256
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Get_SHA256_Hash(string value)
        {
            using var hash = SHA256.Create(); 
            return hash.ComputeHash(Encoding.UTF8.GetBytes(value)).ToString();
        }
        /// <summary>
        /// 隨機鹽，長度12碼
        /// </summary>
        /// <returns></returns>
        private string GenerateSalt()
        {
            StringBuilder sb = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < 13; i++)
            {
                char[] chars = (Get_Salt(random.Next(3))).ToCharArray();

                char aChar = chars[random.Next(chars.Length)];
                sb.Append(aChar);
            }
            return sb.ToString();
        }

        private string Get_Salt(int i)
        {
            return i switch { 0 => "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                1 => "abcdefghijklmnopqrstuvwxyz",
                2 => "1234567890",
                3=> "!@#$%^&*()_+"
            };
        }

        #region 帳號設定 
        /// <summary>
        /// 帳號查詢select
        /// </summary>
        /// <returns></returns>
        public IActionResult AccountS()
        {
            return View();
        }
        /// <summary>
        /// 帳號空白新增頁
        /// </summary>
        /// <returns></returns>
        public IActionResult Account()
        {
            ViewBag.GenderEnum = _db.Departments.Select(se => new SelectListItem
            {
                Text = se.DepartmentName.ToString(),
                Value = se.DepartmentId.ToString()
            });

            return View();
        }

        /// <summary>
        /// 帳號新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Account(AccountViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //密碼加鹽
            string Salts = GenerateSalt();

            //密碼雜湊
            model.Password = Get_SHA256_Hash(model.Password+ Salts);


            _db.Users.Add(new User
            {
                Account = model.Account,
                Password = model.Password,
                Stop = model.Stop,
                Salt = Salts,
                DepartmentId = Convert.ToInt32(model.DepartmentName),
                PasswordDate = DateTime.Now
            }) ;
            _db.SaveChanges();

            //未定義
            return RedirectToAction("Setting", "AccountS");
        }


        ///// <summary>
        ///// 帳號修改update
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public IActionResult Account(string id, AccountViewModel model)
        //{

        //    //_db.Users.Add(new User
        //    //{
        //    //    Account = model.Account,
        //    //    Password = model.Password,
        //    //    Stop = model.Stop,
        //    //    Salt = Salts,
        //    //    DepartmentId = Convert.ToInt32(model.DepartmentName),
        //    //    PasswordDate = DateTime.Now
        //    //});
        //    _db.SaveChanges();
        //    return View();
        //}
        ///// <summary>
        ///// 帳號修改 依照ID給予資料
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public IActionResult Account(string id)
        //{
        //    //AccountViewModel data;


        //    //return View(data);
        //    return View();
        //}

        #endregion

        #region 權限設定 
        /// <summary>
        /// ●權限設定
        /// </summary>
        /// <returns></returns>
        public IActionResult Access()
        {
            return View();
        }

        #endregion
        #region 角色設定 
        /// <summary>
        /// ●角色設定
        /// </summary>
        /// <returns></returns>
        public IActionResult Role()
        {
            return View();
        }

        #endregion
        #region 公佈欄 
        /// <summary>
        /// ●公佈欄
        /// </summary>
        /// <returns></returns>
        public IActionResult BillBoard()
        {
            return View();
        }

        #endregion
    }
}
