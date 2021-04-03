using System;

namespace WorkCloudTest.IEntities
{
    interface IStudent
    {
        Guid ID { get; }
        string Nombre { get; }
        string Apellido { get; }
        string Identificacion { get; }
        int Edad { get; }
        int Casa { get; }
    }
}
