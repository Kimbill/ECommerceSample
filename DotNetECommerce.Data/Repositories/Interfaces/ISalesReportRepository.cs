﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Data.Repositories.Interfaces
{
    public interface ISalesReportRepository
    {
        List<Dictionary<string, object>> GenerateSalesReports();
    }
}
