using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Models
{
    class TableModel : ImportedModelBase
    {
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public int NumberOfChildren => Columns?.Count() ?? 0;
        public IEnumerable<ColumnModel> Columns;

        public override string ToString() =>
            $"\tTable '{Schema}.{Name}' ({NumberOfChildren} columns)";
    }
}
