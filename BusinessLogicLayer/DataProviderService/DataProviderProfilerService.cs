using BusinessLogicLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.DataProviderService
{
    public class DataProviderProfilerService : IDataProviderProfilerService
    {
        private readonly IApplicationDbContext _dbContext;
        public DataProviderProfilerService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ResultTime ComparePerformance()
        {
            Stopwatch timeSQL = new Stopwatch();
            Stopwatch timeLinq = new Stopwatch();

            ResultTime result = new ResultTime();

            timeLinq.Start();
            
           _dbContext.Employees.Include(h => h.HiringHistories).ThenInclude(a => a.Achievements).ToList();

            timeLinq.Stop();
            result.TimeLinq = timeLinq.ElapsedMilliseconds.ToString();
           
            timeSQL.Start();
            //var sql = string.Format(@"SELECT E.Id, E.Name, E.Surname,
            //        H.Id, H.Name, H.EmployeesId,
            //        A.Id, A.Description, A.HiringHistoriesId
            //    FROM Employees as E
            //    JOIN HiringHistories as H ON H.EmployeesId = E.Id
            //    JOIN Achievements as A ON A.HiringHistoriesId = H.Id
            //    ");
            _dbContext.Employees.FromSqlRaw("SELECT * FROM Employees").ToList();
            
            /* 
             Я не пойму почему этот запрос работает в Microsoft SQL Server Management Studio, а тут выбивает ошибку 
              SELECT E.Id, E.Name, E.Surname, 
		            H.Id, H.Name, H.EmployeesId,
		            A.Id, A.Description, A.HiringHistoriesId
                FROM Employees as E
                JOIN HiringHistories as H ON H.EmployeesId = E.Id
                JOIN Achievements as A ON A.HiringHistoriesId = H.Id
             */

            timeSQL.Stop();
            result.TimeSql = timeSQL.ElapsedMilliseconds.ToString();

            return result;
        }
    }
}
