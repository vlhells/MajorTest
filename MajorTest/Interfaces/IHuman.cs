namespace MajorTest.Interfaces
{
    public interface IHuman
    {
        public string FirstName { get; set; }
        public string? SecondName { get; set; } // Второе имя (отчество).
        public string LastName { get; set; }
    }
}
