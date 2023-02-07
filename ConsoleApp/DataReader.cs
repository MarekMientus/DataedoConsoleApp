namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using ConsoleApp.Models;

    internal class DataReader
    {
        internal IEnumerable<DatabaseModel> ImportedObjects { get; private set; }

        public void ImportData(string fileToImport)
        {
            var importedDatabases = new List<DatabaseModel>();
            var importedTabels = new List<TableModel>();
            var importedColumns = new List<ColumnModel>();


            var importedLines = File.ReadLines(fileToImport);

            foreach (var importedLine in importedLines)
            {
                if (string.IsNullOrEmpty(importedLine))
                    continue;

                var values = importedLine.Split(';');
                var type = GetClearedData(values.ElementAtOrDefault(0)).ToUpper();
                if (type == "DATABASE")
                {
                    var database = new DatabaseModel();
                    database.Type = type;
                    database.Name = GetClearedData(values.ElementAtOrDefault(1));
                    importedDatabases.Add(database);

                }
                else if (type == "TABLE")
                {
                    var table = new TableModel();
                    table.Type = type;
                    table.Name = GetClearedData(values.ElementAtOrDefault(1));
                    table.Schema = GetClearedData(values.ElementAtOrDefault(2));
                    table.ParentName = GetClearedData(values.ElementAtOrDefault(3));
                    table.ParentType = GetClearedData(values.ElementAtOrDefault(4)).ToUpper();
                    importedTabels.Add(table);
                }
                else if (type == "COLUMN")
                {
                    var column = new ColumnModel();
                    column.Type = type;
                    column.Name = GetClearedData(values.ElementAtOrDefault(1));
                    column.ParentName = GetClearedData(values.ElementAtOrDefault(3));
                    column.ParentType = GetClearedData(values.ElementAtOrDefault(4)).ToUpper();
                    column.DataType = values.ElementAtOrDefault(5);
                    column.IsNullable = values.ElementAtOrDefault(6) == "1" ? true : false;
                    importedColumns.Add(column);
                }
            }
            ImportedObjects = GetMergedImportedData(importedDatabases, importedTabels, importedColumns);
        }

        private IEnumerable<DatabaseModel> GetMergedImportedData(List<DatabaseModel> importedDatabases, List<TableModel> importedTabels, List<ColumnModel> importedColumns)
        {
            // Fill tabels with columns
            importedTabels.ToList().ForEach(table =>
            {
                table.Columns = importedColumns.Where(column => table.Type == column.ParentType && table.Name == column.ParentName).ToList();
            });
            // Fill databases with tables
            importedDatabases.ToList().ForEach(database =>
            {
                database.Tables = importedTabels.Where(table => database.Type == table.ParentType && database.Name == table.ParentName).ToList();
            });
            return importedDatabases;
        }

        private string GetClearedData(string data)
        {
            if (string.IsNullOrEmpty(data))
                return data;
            return data.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }
    }
}
