using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public EmployeesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<Dictionary<int, List<Employee>>>>   GetEmployee()
        {
            var filePath = _hostingEnvironment.ContentRootPath + "\\Employees.json";
            var fileContent = await System.IO.File.ReadAllTextAsync(filePath);

            if (string.IsNullOrEmpty(fileContent))
            {
                var jsonContent = JsonConvert.SerializeObject(InitSetEmployees());
                System.IO.File.WriteAllText(filePath, jsonContent);
            }

            var employeeList = JsonConvert.DeserializeObject<List<Employee>>(fileContent);
           return  EmployeeGroupByDate(employeeList);
 
        }

        private Dictionary<int, List<Employee>> EmployeeGroupByDate(List<Employee> employeeList)
        {


            Dictionary<int, List<Employee>> dic = new Dictionary<int, List<Employee>>();

            var stratFromMonth = 1;
            for (int i = 1; i <= 4; i++)
            {
                dic.Add(i, employeeList.Where(x => GroupByQuerte(x, stratFromMonth)).OrderBy(x=> x.Name.Split(' ')[0] ).ToList());
                stratFromMonth += 3;
            }
            return dic;
        }

        private bool GroupByQuerte(Employee x, int stratFromMonth)
        {
            var date = DateTime.ParseExact(x.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture).Month;

            if (date >= stratFromMonth && date < stratFromMonth + 3)
            {
                return true;
             }

            return false;
        }

        private List<Employee> InitSetEmployees()
        {
            return new List<Employee> {
                 new Employee (){  Name ="john due", Age =23, Date = "01/01/2013",salery =3000},
                 new Employee (){  Name ="kind david", Age =23, Date="01/02/2020",salery =3000},
                 new Employee (){  Name ="Neil  Irani ", Age =23, Date="01/02/2018",salery =3000},
                 new Employee (){  Name ="Tom Hanks", Age =23, Date="01/02/2015",salery =3000},

                 new Employee (){  Name ="yona yona", Age =23, Date="01/06/2017",salery =3000},
                 new Employee (){  Name ="birsdy song", Age =23, Date="02/06/2009",salery =3000},
                 new Employee (){  Name ="dog  cat", Age =23, Date="01/07/2003",salery =3000},
                 new Employee (){  Name ="Tom Hanks", Age =23, Date="01/07/2015",salery =3000},

                     new Employee (){  Name ="men men", Age =23, Date="01/09/2015",salery =3000},
                 new Employee (){  Name ="Larri king", Age =23, Date="01/09/2002",salery =3000},
                 new Employee (){  Name ="Foo Bar", Age =23, Date="01/10/2015",salery =3000},
                 new Employee (){  Name ="Morgan little", Age =23, Date="01/10/2015",salery =3000},

                 new Employee (){  Name ="men men", Age =23, Date="01/12/2015",salery =3000},
                 new Employee (){  Name ="Larri king", Age =23, Date="01/12/2015",salery =3000},
                 new Employee (){  Name ="Foo Bar", Age =23, Date="01/12/2015",salery =3000},
                 new Employee (){  Name ="Morgan little", Age =23, Date="01/02/2015",salery =3000},

             };

        }


        // GET api/values/5

    }
}
