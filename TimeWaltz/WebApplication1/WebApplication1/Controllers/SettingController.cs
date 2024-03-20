using Microsoft.AspNetCore.Mvc;
using Repository.Models;
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
    public IActionResult Account()
    {
        var model = _UserService.GetUserViewModel();

        ViewBag.GenderEnumDepart = DropDownHelper
            .GetDepartmentNameDropDownList(_dropDownBasicSettingService.GetDropDownData());
        ViewBag.GenderEnumEmployees = DropDownHelper
            .GetEmployeeNameDropDownList(_dropDownBasicSettingService.GetEmployeeDropDownData());

        return View(model);
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
    public IActionResult AccountCreate(UserCreateViewModel model)
    {
        model.EmployeesNameSelectList = DropDownHelper
            .GetEmployeeNameDropDownList(_dropDownBasicSettingService.GetEmployeeDropDownData());
        model.DepartmentNameSelectList = DropDownHelper
            .GetDepartmentNameDropDownList(_dropDownBasicSettingService.GetDropDownData());


        //帳號是否重複
        if (_UserService.GetUserCreateOrNull(model).Count != 0)
        {
            ModelState.AddModelError(string.Empty, "帳號重複");
            return View(model);
        }

        if (!ModelState.IsValid) return View(model);

        //密碼鹽
        var Salts = _UserService.GenerateSalt();
        //密碼雜湊
        model.Password = _UserService.SHA256EncryptString(model.Password + Salts);

        _db.Users.Add(new User
        {
            Account = model.Account,
            Password = model.Password,
            Stop = model.Stop == 1 ? true : false,
            Salt = Salts,
            //DepartmentId = Convert.ToInt32(model.DepartmentName),//todo
            //EmployeesId = Convert.ToInt32(model.EmployeesName),//todo
            PasswordDate = DateTime.Now
        });
        _db.SaveChanges();

        return RedirectToAction("Account", "Setting");
    }

    /// <summary>
    ///     修改帳號頁
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IActionResult AccountEdit(int id)
    {
        var employeeNameDropDownData = _dropDownBasicSettingService.GetEmployeeDropDownData();
        var entity = _UserService.GetUserOrNull(id);
        var Salts = "";
        if (employeeNameDropDownData != null)
        {
            var model = EntityHelper.ToEditViewModel(entity);
            model.EmployeesNameSelectList = DropDownHelper
                .GetEmployeeNameDropDownList(_dropDownBasicSettingService.GetEmployeeDropDownData());
            model.DepartmentNameSelectList = DropDownHelper
                .GetDepartmentNameDropDownList(_dropDownBasicSettingService.GetDropDownData());

            if (model.Password != null)
            {
                Salts = _UserService.GenerateSalt();

                model.Password = _UserService.SHA256EncryptString(model.Password + Salts);

            }
            //密碼雜湊

            _UserService.EditUserType(model, Salts);
            return RedirectToAction("Account", "Setting");

        }
        //密碼雜湊


        //_UserService.EditUserType(model, Salts);
        return RedirectToAction("Account", "Setting");
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