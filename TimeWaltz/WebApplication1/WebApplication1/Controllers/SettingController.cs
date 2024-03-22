using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using WebApplication1.Filters;
using WebApplication1.Helpers;
using WebApplication1.Models.SettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

public class SettingController : Controller
{
    private readonly TimeWaltzContext _db;
    private readonly DropDownBasicSettingService _dropDownBasicSettingService;
    private readonly UserService _UserService;

    public SettingController(TimeWaltzContext db, UserService UserService,
        DropDownBasicSettingService dropDownBasicSettingService)
    {
        _db = db;
        _UserService = UserService;
        _dropDownBasicSettingService = dropDownBasicSettingService;
    }

    #region 權限設定

    /// <summary>
    ///     ●權限設定
    /// </summary>
    /// <returns></returns>
    public IActionResult Access()
    {
        return View();
    }

    #endregion

    #region 角色設定

    /// <summary>
    ///     ●角色設定
    /// </summary>
    /// <returns></returns>
    public IActionResult Role()
    {
        return View();
    }

    #endregion


    #region 帳號設定

    /// <summary>
    ///     帳號查詢select
    /// </summary>
    /// <returns></returns>
        [TimeWaltzAuthorize]

    public IActionResult Account()
    {
        return View();
    }

    /// <summary>
    ///     帳號查詢select
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Account(UserViewModel models)
    {
        var model = _UserService.GetUserViewModel(models);

        ViewBag.GenderEnumDepart = DropDownHelper
            .GetDepartmentNameDropDownList(_dropDownBasicSettingService.GetDropDownData());
        ViewBag.GenderEnumEmployees = DropDownHelper
            .GetEmployeeNameDropDownList(_dropDownBasicSettingService.GetEmployeeDropDownData());
        return View(model);
    }


    /// <summary>
    ///     新增帳號頁面
    /// </summary>
    /// <returns></returns>
    public IActionResult AccountCreate()
    {
        var model = new UserCreateViewModel
        {
            EmployeesNameSelectList = DropDownHelper
                .GetEmployeeNameDropDownList(_dropDownBasicSettingService.GetEmployeeDropDownData()),
            DepartmentNameSelectList = DropDownHelper
                .GetDepartmentNameDropDownList(_dropDownBasicSettingService.GetDropDownData())
        };
        return View(model);
    }

    /// <summary>
    ///     新增帳號
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AccountCreate(UserCreateModel model)
    {
        var u = _db.Users.FirstOrDefault(x => x.Account == model.Account);
        //帳號是否重複
        if (u != null)
        {
            ModelState.AddModelError(string.Empty, "帳號重複");
            return View(model);
        }

        //密碼鹽
        var Salts = _UserService.GenerateSalt();
        //密碼雜湊
        model.Password = _UserService.SHA256EncryptString(model.Password + Salts);

        _db.Users.Add(new User
        {
            Account = model.Account,
            Password = model.Password,
            Stop = model.Stop,
            Salt = Salts,
            RoleId = 2,
            Employee = new Employee()
            {
                DepartmentId = model.DepartmentName,
                HireDate = DateTime.Now,
                Name = model.EmployeesName,
                EmployeesNo = DateTime.Now.Ticks.ToString()
            },
            PasswordDate = DateTime.Now
        });
        _db.SaveChanges();

        return RedirectToAction("Account", "Setting");
    }



    [HttpGet]
    public IActionResult AccountEdit(int id)
    {
        var user = _db.Users.Include(x => x.Employee).FirstOrDefault(x => x.Id == id);
        var model = new UserEditViewModel()
        {
            Id = user.Id,
            Stop = Convert.ToInt32(user.Stop),
            EmployeesName = user.Employee.Name,
            DepartmentName = user.Employee.DepartmentId,
            EmployeesNameSelectList = DropDownHelper
                .GetEmployeeNameDropDownList(_dropDownBasicSettingService.GetEmployeeDropDownData()),
            DepartmentNameSelectList = DropDownHelper
                .GetDepartmentNameDropDownList(_dropDownBasicSettingService.GetDropDownData()),
            StopSelectList = new List<SelectListItem>()
            {
                new("停用","1"),
                new("啟用","0"),
            }
        };
        return View(model);
    }

    /// <summary>
    ///     修改帳號頁
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult AccountEdit(UserEditModel model)
    {
        var user = _db.Users.Include(x=>x.Employee).ThenInclude(x=>x.Department).FirstOrDefault(x => x.Id == model.Id);
        if (user == null) return RedirectToAction("Account", "Setting");

        try
        {
            if (model.Password != null)
            {
                var salts = _UserService.GenerateSalt();
                user.Password = _UserService.SHA256EncryptString(model.Password + salts);
            }

            user.Employee.Name = model.EmployeesName;
            user.Stop = Convert.ToBoolean(model.Stop);
            user.Employee.DepartmentId = model.DepartmentName;
            _db.SaveChanges();
            return RedirectToAction("AccountEdit", "Setting",new {id = model.Id});
        }
        catch (Exception e)
        {
            return RedirectToAction("Account", "Setting");
        }
    }


    public IActionResult DepartmentDelete(int id)
    {
        var entity = _UserService.GetUserOrNull(id);
        _UserService.DeleteUserType(entity);
        return RedirectToAction("Account", "Setting");
    }

    #endregion

    #region 公佈欄

    /// <summary>
    ///     公佈欄-瀏覽頁面
    /// </summary>
    /// <returns></returns>
        [TimeWaltzAuthorize]
    public IActionResult BillBoard()
    {
        return View();
    }

    /// <summary>
    ///     公佈欄-新增頁面
    /// </summary>
    /// <returns></returns>
    public IActionResult BillBoardCreate()
    {
        return View();
    }

    /// <summary>
    ///     公佈欄-編輯頁面
    /// </summary>
    /// <returns></returns>
    public IActionResult BillBoardEdit(int id)
    {
        return View();
    }

    #endregion
}