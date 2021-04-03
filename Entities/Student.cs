using System;
using WorkCloudTest.IEntities;

namespace WorkCloudTest.Entities
{
    public class Student: IStudent
    {
        public Guid ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; }
        public int Edad { get; set; }
        public int Casa { get; set; }
    }
}
