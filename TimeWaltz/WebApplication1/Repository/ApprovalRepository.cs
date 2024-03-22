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
    public class FlextimesRepository
    {
        private readonly TimeWaltzContext db;

        public FlextimesRepository(TimeWaltzContext db)
        {
            this.db = db;
        }

        public Flextime GetSetting()
        {
            return db.Flextimes.AsNoTracking().First();
        }
    }
}
