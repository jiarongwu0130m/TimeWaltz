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
    public class ApprovalRepository
    {
        private readonly TimeWaltzContext db;

        public ApprovalRepository(TimeWaltzContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// 看三小
        /// </summary>
        /// <param name="requestStatus"></param>
        /// <param name="tableType"></param>
        /// <returns></returns>
        public List<int> GetId(Enum.RequestStatusEnum requestStatus, TableTypeEnum tableType)
        {
            return db.Approvals.AsNoTracking().Where(x => x.Status == requestStatus && x.TableType == (int)tableType).Select(x => x.TableId).ToList();
        }
    }
}
