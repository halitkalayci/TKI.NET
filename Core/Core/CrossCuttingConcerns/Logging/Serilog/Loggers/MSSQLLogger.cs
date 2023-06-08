using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class MSSQLLogger : LoggerServiceBase
    {
        public MSSQLLogger()
        {
            //TODO : IConfiguration
            var sinkOptions = new MSSqlServerSinkOptions()
            {
                AutoCreateSqlDatabase = false,
                AutoCreateSqlTable = true,
                TableName="Logs",
            };

            var columnOptions = new ColumnOptions();
            columnOptions.Store.Remove(StandardColumn.Message);
            columnOptions.Store.Remove(StandardColumn.Properties);

            var seriLogConfig = new LoggerConfiguration().WriteTo.MSSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RentACarTKI;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False", sinkOptions:sinkOptions, columnOptions:columnOptions).CreateLogger();

            Logger = seriLogConfig;
        }
    }
}
