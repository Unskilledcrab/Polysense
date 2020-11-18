using System;

namespace PS.Shared.Models.Abstractions
{
    abstract public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDateTime { get; set; }
        public int Age => DateTime.Now.Year - BirthDateTime.Year;
    }
}