namespace MajorTest.Interfaces
{
    /// <summary>
    /// An interface determines basic Human properties.
    /// </summary>
    public interface IHuman
    {
        public string FirstName { get; set; }
        public string? SecondName { get; set; } // Второе имя (отчество).
        public string LastName { get; set; }
    }
}
