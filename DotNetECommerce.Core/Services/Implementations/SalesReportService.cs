using DotNetECommerce.Core.Services.Interfaces;
using DotNetECommerce.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNetECommerce.Services.Implementation
{
    public class SalesReportService : ISalesReportService
    {
        private readonly ISalesReportRepository _salesReportRepository;

        public SalesReportService(ISalesReportRepository salesReportRepository)
        {
            _salesReportRepository = salesReportRepository;
        }

        public List<Dictionary<string, object>> GenerateSalesReports()
        {
            try
            {
                // Fetch sales reports from the repository
                var salesReports = _salesReportRepository.GenerateSalesReports();

                // Filter sales reports with at least 10 records
                var filteredSalesReports = salesReports.Where(report => report.Count >= 10).ToList();

                return filteredSalesReports;
            }
            catch (FileNotFoundException ex)
            {
                // Handle file not found exception.
                throw new Exception($"File not found: {ex.FileName}", ex);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid operation exception (e.g., data access error).
                throw new Exception($"Invalid operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions or log errors as needed.
                throw new Exception($"Error in SalesReportService: {ex.Message}", ex);
            }
        }

        public List<Dictionary<string, object>> GetTop5Deals()
        {
            try
            {
                // Fetch sales reports
                var salesReports = GenerateSalesReports();

                // Sort sales reports by the number of records in descending order
                salesReports.Sort((a, b) => b.Count.CompareTo(a.Count));

                // Display the top 5 deals
                var top5Deals = salesReports.Take(5).ToList();

                return top5Deals;
            }
            catch (Exception ex)
            {
                // Handle exceptions or log errors as needed.
                throw new Exception($"Error getting top 5 deals: {ex.Message}", ex);
            }
        }
    }
}