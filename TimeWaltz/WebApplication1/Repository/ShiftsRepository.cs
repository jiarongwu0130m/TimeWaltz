using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ShiftsRepository
    {
        private readonly TimeWaltzContext db;

        public ShiftsRepository(TimeWaltzContext db)
        {
            this.db = db;
        }

        public List<Shift> GetEmpShifts(int userId,DateTime? beforeDateTime=null)
        {
            var shifts = db.Shifts.AsNoTracking().Include(x => x.ShiftSchedule);
            return beforeDateTime.HasValue ? shifts.Where(x => x.EmployeesId == userId && x.ShiftsDate.Date <= beforeDateTime.Value).ToList() : shifts.Where(x => x.EmployeesId == userId).ToList();
        }
    }
}
