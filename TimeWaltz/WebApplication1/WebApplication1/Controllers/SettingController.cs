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

    [TimeWaltzAuthorize]
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

        return View();
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
        ViewBag.userId = id;
        return View(model);
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