using System;
using System.Threading.Tasks;
using WorkCloudTest.Entities;
using WorkCloudTest.Enums;
using WorkCloudTest.IRepositories;

namespace WorkCloudTest.Seeds
{
	public class StudentSeed
	{
        private readonly IRepository Repository;
		public StudentSeed(IRepository repository)
        {
            Repository = repository;
		}

		public async Task Run()
		{
            Student student = new Student();

            student.ID = Guid.NewGuid();
            student.Nombre = "Harry";
            student.Apellido = "Potter";
            student.Identificacion = "357241689";
            student.Edad = 12;
            student.Casa = HouseType.FromName<HouseType>("Gryffindor").Value;
            await Repository.CreateAsync<Student>(student);

            student.ID = Guid.NewGuid();
            student.Nombre = "Hermione";
            student.Apellido = "Granger";
            student.Identificacion = "1472583690";
            student.Edad = 12;
            student.Casa = HouseType.FromName<HouseType>("Gryffindor").Value;
            await Repository.CreateAsync<Student>(student);

            student.ID = Guid.NewGuid();
            student.Nombre = "Ron";
            student.Apellido = "Weasley";
            student.Identificacion = "3692581475";
            student.Edad = 12;
            student.Casa = HouseType.FromName<HouseType>("Gryffindor").Value;
            await Repository.CreateAsync<Student>(student);

			student.ID = Guid.Parse("12d99f00-93ca-4ce4-af7b-b31fbcd729bc");
			student.Nombre = "Javier";
			student.Apellido = "Duarte";
			student.Identificacion = "1234567891";
			student.Edad = 12;
			student.Casa = HouseType.FromName<HouseType>("Gryffindor").Value;
            await Repository.CreateAsync<Student>(student);

            student = new Student();
            student.ID = Guid.Parse("06cc894b-7dc3-41ef-9559-1c565770d33c");
            student.Nombre = "Daniela";
            student.Apellido = "Moran";
            student.Identificacion = "123456857";
            student.Edad = 14;
            student.Casa = HouseType.FromName<HouseType>("Slytherin").Value;
            await Repository.CreateAsync<Student>(student);

            student = new Student();
            student.ID = Guid.Parse("f5f2813a-6108-4975-8ea5-41a62eb2a77f");
            student.Nombre = "Samantha";
            student.Apellido = "Robles";
            student.Identificacion = "9876543210";
            student.Edad = 13;
            student.Casa = HouseType.FromName<HouseType>("Hufflepuff").Value;
            await Repository.CreateAsync<Student>(student);
		}
	}
}
