namespace DataOperations.TypeClasses
{
    /// <summary>
    /// Container for stored procedure parameter definitions
    /// </summary>
    public class StoredProcedureDetail
    {
        public string Name { get; set; }
        public byte SystemType { get; set; }
        public int MaxLength { get; set; }
        public byte Precision { get; set; }
        public byte Scale { get; set; }

        public string[] ItemArray()
        {
            return new[]
            {
                Name,
                SystemType.ToString(),
                MaxLength.ToString(),
                Precision.ToString(),
                Scale.ToString()
            };
        }
    }
}
