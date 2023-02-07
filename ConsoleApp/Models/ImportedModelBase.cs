using System;
using System.Data.SqlTypes;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    abstract class ImportedModelBase
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
