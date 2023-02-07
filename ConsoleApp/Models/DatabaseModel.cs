using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Models
{
    class DatabaseModel : ImportedModelBase
    {
        public int NumberOfChildren => Tables?.Count() ?? 0;
        public IEnumerable<TableModel> Tables;

        public override string ToString() =>
            $"Database '{Name}' ({NumberOfChildren} tables)";
    }
}
