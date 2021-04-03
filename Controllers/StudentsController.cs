using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WorkCloudTest.Contexts;
using WorkCloudTest.Entities;
using WorkCloudTest.Enums;
using WorkCloudTest.IRepositories;
using WorkCloudTest.Models;

namespace WorkCloudTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger Logger;
        private readonly IRepository Repository;
        private readonly IMapper Mapper;

        public StudentsController(PgsqlContext context, ILogger iLogger, IMapper mapper, IRepository repository)
        {
            Repository = repository;
            Logger = iLogger;
            Mapper = mapper;
        }

        /// GET: api/Students
        /// <summary>
        /// List Students
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            Object meta;
            try
            {
                var model = await Repository.SelectAll<Student>();
                return model;
            }
            catch (Exception exception)
            {
                Logger.Fatal(exception.Message);
                Logger.Fatal(exception.StackTrace);
                meta = new { Error = new { Description = "Error Interno Del Servidor." } };
                return StatusCode(500, new { Meta = meta });
            }
        }

        /// GET: api/Students/f5f2813a-6108-4975-8ea5-41a62eb2a77f
        /// <summary>
        /// Gets a specific Student.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]

        public async Task<ActionResult<Student>> GetStudent(Guid id)
        {
            Object meta;
            try
            {
                var model = await Repository.SelectById<Student>(id);

                if (model == null)
                {
                    return NotFound();
                }

                return model;
            }
            catch (Exception exception)
            {
                Logger.Fatal(exception.Message);
                Logger.Fatal(exception.StackTrace);
                meta = new { Error = new { Description = "Error Interno Del Servidor." } };
                return StatusCode(500, new { Meta = meta });
            }
        }

        /// PUT: api/Students/f5f2813a-6108-4975-8ea5-41a62eb2a77f
        /// <summary>
        /// Updates a specific Student.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Students/f5f2813a-6108-4975-8ea5-41a62eb2a77f
        ///     {
        ///         "ID": "f5f2813a-6108-4975-8ea5-41a62eb2a77f",
        ///         "Nombre": "Daniela",
        ///         "Apellido": "Moran",
        ///         "Identificacion": "1234568s517",
        ///         "Edad": 14,
        ///         "Casa": "Slytherin"
        ///     }
        ///
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, StudentModel model)
        {
            Object meta;
            try
            {
                if (id != model.ID)
                {
                    return BadRequest();
                }
                if (!Enumeration.ExistName<HouseType>(model.Casa))
                {
                    meta = new
                    {
                        Error = new { Description = "Error en los datos del estudiante", Data = "La casa seleccionada no es valida" }
                    };
                    return BadRequest(new { Meta = meta });
                }

                var student = await Repository.SelectById<Student>(id);

                if (student == null)
                {
                    return NotFound();
                }

                student.Nombre = model.Nombre;
                student.Apellido = model.Apellido;
                student.Identificacion = model.Identificacion;
                student.Edad = model.Edad;
                student.Casa = HouseType.FromName<HouseType>(model.Casa).Value;

                await Repository.UpdateAsync<Student>(student);

                return NoContent();
            }
            catch (Exception exception)
            {
                Logger.Fatal(exception.Message);
                Logger.Fatal(exception.StackTrace);
                meta = new { Error = new { Description = "Error Interno Del Servidor." } };
                return StatusCode(500, new { Meta = meta });
            }
        }

        /// POST: api/Students
        /// <summary>
        /// Store a specific Student.
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Students
        ///     {
        ///         "Nombre": "Daniela",
        ///         "Apellido": "Moran",
        ///         "Identificacion": "1234568s517",
        ///         "Edad": 14,
        ///         "Casa": "Slytherin"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentModel model)
        {
            Object meta;
            try
            {
                if (!Enumeration.ExistName<HouseType>(model.Casa))
                {
                    meta = new
                    {
                        Error = new { Description = "Error en los datos del estudiante", Data = "La casa seleccionada no es valida" }
                    };
                    return BadRequest(new { Meta = meta });
                }

                Student student = Mapper.Map<Student>(model);

                await Repository.CreateAsync<Student>(student);
                return CreatedAtAction("GetStudent", new { id = student.ID }, student);
            }
            catch (Exception exception)
            {
                Logger.Fatal(exception.Message);
                Logger.Fatal(exception.StackTrace);
                meta = new { Error = new { Description = "Error Interno Del Servidor." } };
                return StatusCode(500, new { Meta = meta });
            }
        }

        /// DELETE: api/Students/f5f2813a-6108-4975-8ea5-41a62eb2a77f
        /// <summary>
        /// Deletes a specific Student.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(Guid id)
        {
            Object meta;
            try
            {
                var model = await Repository.SelectById<Student>(id);

                if (model == null)
                {
                    return NotFound();
                }

                await Repository.DeleteAsync<Student>(model);

                return model;
            }
            catch (Exception exception)
            {
                Logger.Fatal(exception.Message);
                Logger.Fatal(exception.StackTrace);
                meta = new { Error = new { Description = "Error Interno Del Servidor." } };
                return StatusCode(500, new { Meta = meta });
            }
        }
    }
}
