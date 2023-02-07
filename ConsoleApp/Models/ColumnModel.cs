namespace ConsoleApp.Models
{
    class ColumnModel : ImportedModelBase
    {
        public string ParentName { get; set; }
        public string DataType { get; set; }
        public string ParentType { get; set; }
        public bool IsNullable { get; set; }

        public override string ToString() =>
            $"\t\tColumn '{Name}' with {DataType} data type {(IsNullable ? "accepts nulls" : "with no nulls")}";
    }
}
