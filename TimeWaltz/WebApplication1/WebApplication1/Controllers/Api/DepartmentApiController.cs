﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Filters;
using WebApplication1.Helpers;
using WebApplication1.Models.BasicSettingViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers.Api
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DepartmentApiController : ControllerBase
    {
        private readonly TimeWaltzContext _db;
        private readonly DepartmentService _departmentService;

        public DepartmentApiController(TimeWaltzContext timeWaltzContext, DepartmentService departmentService)
        {
            _db = timeWaltzContext;
            _departmentService = departmentService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var result =  _db.Departments.Select(x => new SelectListItem(x.DepartmentName, x.Id.ToString())).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult<DepartmentCreateShowDto> GetDropDownList()
        {
            try
            {
                var empList = _db.Employees.ToList();
                var model = new DepartmentCreateShowDto
                {
                    EmployeeNameSelectList = DropDownHelper
                           .GetEmployeeNameDropDownList(empList),
                };
                return model;
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
        [HttpPost]
        public ActionResult Create(DepartmentCreateDto dto)
        {
            try
            {
                var entity = new Department
                {
                    DepartmentName = dto.DepartmentName,
                };
                _departmentService.CreateDepartment(entity);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<DepartmentEditDto> GetEditData(int id)
        {
            try
            {
                var entity = _departmentService.GetDepartmentOrNull(id);
                if (entity != null)
                {
                    var model = new DepartmentEditDto
                    {
                        Id = id,
                        DepartmentName = entity.DepartmentName,
                        //EmployeesId = entity.EmployeeId,//todo 部門主管
                    };
                    return model;
                }
                throw new Exception("資料庫錯誤");
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }
        [HttpPost]
        [Route("{id}")]
        public ActionResult Edit(DepartmentEditDto dto)
        {
            try
            {
                var model = new DepartmentEditViewModel
                {
                    EmployeesId = dto.EmployeesId,
                    DepartmentName = dto.DepartmentName,
                    Id = dto.Id
                };
                _departmentService.EditDepartment(model);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }
        }

        [HttpGet]
        public ActionResult<List<DepartmentDto>> GetListData()
        {
            try
            {
                var entities = _departmentService.GetDepartment();
                var emp = _db.Employees.ToList();
                var models = new List<DepartmentDto>();
                foreach (var entity in entities)
                {
                    models.Add(new DepartmentDto
                    {
                        Id = entity.Id,
                        DepartmentName = entity.DepartmentName,
                        //EmployeesName = emp.FirstOrDefault(x=>x.Id == entity.EmployeeId).Name,//todo 部門主管
                    });
                }
                return models;
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }            
        }
        [HttpPost]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var entity = _departmentService.GetDepartmentOrNull(id);
                _departmentService.DeleteDepartment(entity);
                return Ok(new { status = true });
            }
            catch (Exception ex)
            {
                return Ok(new { status = false });
            }

        }
    }
}
