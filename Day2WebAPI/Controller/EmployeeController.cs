using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day2WebAPI.Models;

namespace Day2WebAPI.Controller
{

    public class EmployeeController : ApiController
    {

        static List<Employee> employees = new List<Employee>();

        [Route("api/employee")]
        [HttpGet]
        public List<Employee> GetAllEmployees() {
            return employees;
        }
        [Route("api/employee/{id}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeById(int id) {
            try {
                Employee emp = employees[id];
                return Request.CreateResponse(HttpStatusCode.OK, emp);
            }
            catch (System.ArgumentOutOfRangeException e){
                return Request.CreateResponse(HttpStatusCode.NotFound, id);

            }

        }
        [Route("api/employee/")]
        [HttpGet]
        public HttpResponseMessage GetEmployeesByPosition(string position) {
            List<Employee> empByPosition = new List<Employee>();
            foreach (var emp in employees) {
                if (emp.position == position)
                    empByPosition.Add(emp);
            }
     
            if (!empByPosition.Any()) Request.CreateResponse(HttpStatusCode.NotFound, position);
            return Request.CreateResponse(HttpStatusCode.OK, empByPosition);
        }
        [Route("api/employee")]
        [HttpPost]
        public void AddEmployee(Employee employee) {
            employees.Add(employee);
        }
        [Route("api/employee")]
        [HttpDelete]
        public IHttpActionResult RemoveEmployee([FromBody]string name) {

            int success = employees.RemoveAll(x => x.firstName + " " + x.lastName == name);
            if (success == 0) return NotFound();
            return Ok("Success");
        }

    }
}
