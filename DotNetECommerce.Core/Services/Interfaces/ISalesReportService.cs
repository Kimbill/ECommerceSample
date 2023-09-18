using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Core.Services.Interfaces
{
    public interface ISalesReportService
    {
        List<Dictionary<string, object>> GenerateSalesReports();
        List<Dictionary<string, object>> GetTop5Deals();
        // Define more service methods here as needed.
    }

}
