using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class DataPrinter
    {
        internal void PrintData(IEnumerable<DatabaseModel> importedObjects)
        {
            importedObjects.ToList().ForEach(database =>
            {
                Console.WriteLine(database.ToString());
                database.Tables.ToList().ForEach(table =>
                {
                    Console.WriteLine(table.ToString());
                    table.Columns.ToList().ForEach(column =>
                    {
                        Console.WriteLine(column.ToString());
                    });
                });
            });
        }
    }
}
