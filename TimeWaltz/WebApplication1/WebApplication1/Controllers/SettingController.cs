using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Models.Entity;
using WebApplication1.Models.SettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class SettingController : Controller
    {
        private readonly TimeWaltzContext _db;
        private readonly UserService _UserService;
        private readonly DropDownBasicSettingService _dropDownBasicSettingService;

        public SettingController(TimeWaltzContext db, UserService UserService,DropDownBasicSettingService dropDownBasicSettingService)
        {
            _db = db;
            _UserService = UserService;
            _dropDownBasicSettingService = dropDownBasicSettingService;
        }

        
        #region 帳號設定 
        /// <summary>
        /// 帳號查詢select
        /// </summary>
        /// <returns></returns>
        public IActionResult Account()
        {
            var Id = 1;
            var entities = _UserService.GetUserOrNull();

            var model = EntityHelper.ToViewModel(entities);
            ViewBag.GenderEnumDepart = _db.Departments.Select(se => new SelectListItem
            {
                Text = se.DepartmentName.ToString(),
                Value = se.Id.ToString()
            });
            ViewBag.GenderEnumEmployees = _db.Employees.Select(se => new SelectListItem
            {
                Text = se.Name.ToString(),
                Value = se.Id.ToString()
            });

            return View(model);

        }

        /// <summary>
        /// 新增帳號頁面
        /// </summary>
        /// <returns></returns>
        public IActionResult AccountCreate()
        {
            //if (employeeNameDropDownData != null)
            //{
                var model = new UserCreateViewModel
                {
                    EmployeesNameSelectList = DropDownHelper
                       .GetEmployeeNameDropDownList(_dropDownBasicSettingService.GetEmployeeDropDownData()),
                    DepartmentNameSelectList = DropDownHelper
                       .GetDepartmentNameDropDownList(_dropDownBasicSettingService.GetDropDownData()),
                };
                return View(model);
            //}

            return RedirectToAction("PersonalData");

        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AccountCreate(UserCreateViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //密碼加鹽
            string Salts = _UserService.GenerateSalt();

            //密碼雜湊
            model.Password = _UserService.SHA256EncryptString(model.Password + Salts);


            _db.Users.Add(new User
            {
                Account = model.Account,
                Password = model.Password,
                Stop = model.Stop,
                Salt = Salts,
                DepartmentId = Convert.ToInt32(model.DepartmentName),
                EmployeesId = Convert.ToInt32(model.EmployeesName),
                PasswordDate = DateTime.Now
            }) ;
            _db.SaveChanges();

            return RedirectToAction("Setting", "Account");
        }

        /// <summary>
        /// 修改帳號頁 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult AccountEdit(int id)
        {
            var employeeNameDropDownData = _dropDownBasicSettingService.GetEmployeeDropDownData();
            var entity = _UserService.GetUserOrNull(id);
            if (employeeNameDropDownData != null)
            {
                var model = EntityHelper.ToEditViewModel(entity);
                model.EmployeesNameSelectList = DropDownHelper
                        .GetEmployeeNameDropDownList(_dropDownBasicSettingService.GetEmployeeDropDownData());
                model.DepartmentNameSelectList = DropDownHelper
                   .GetDepartmentNameDropDownList(_dropDownBasicSettingService.GetDropDownData());
                model.StopSelectList = DropDownHelper
                    .GetDepartmentNameDropDownList(_dropDownBasicSettingService.GetDropDownData());

                return View(model);
            }
            return RedirectToAction("PersonalData");
        }

        [HttpPost]
        /// <summary>
        /// 修改帳號
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult AccountEdit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //密碼加鹽
            string Salts = _UserService.GenerateSalt();

            //密碼雜湊
            model.Password = _UserService.SHA256EncryptString(model.Password + Salts);


            _UserService.EditUserType(model, Salts);
            return RedirectToAction("Department");
        }



        public IActionResult DepartmentDelete(int id)
        {
            var entity = _UserService.GetUserOrNull(id);
            _UserService.DeleteUserType(entity);
            return RedirectToAction("Department");
        }

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
