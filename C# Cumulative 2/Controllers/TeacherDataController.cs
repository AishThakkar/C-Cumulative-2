using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace C__Cumulative_2.Controllers
{
    public class TeacherDataController : ApiController
    {
     
        .
        private CumulativeProjectDb Teachers = new CumulativeProjectDb();
        
        /// GET api/Teachers/List
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            
            MySqlConnection Conn = Teachers.AccessDatabase();

           
            Conn.Open();

            //Establish a new command(query) 
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat (teacherfname, ' ', teacherlname)) like lower(@key)";

           
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Results of the query 
            MySqlDataReader Results = cmd.ExecuteReader();

            
            List<Teacher> TeachersInfo = new List<Teacher> { };


           
            while (Results.Read())
            {
                //Access Column information
                DateTime HireDate = (DateTime)Results["hiredate"];
                decimal Salary = (decimal)Results["salary"];
                string EmployeeNumber = (string)Results["employeenumber"];
                int TeacherId = (int)Results["teacherid"];
                string TeacherFname = (string)Results["teacherfname"];
                string TeacherLname = (string)Results["teacherlname"];

                
                Teacher NewTeacher = new Teacher();

                
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                TeachersInfo.Add(NewTeacher);
            }

            //Close the connection 
            Conn.Close();


            //returns final list of teachers names.
            return TeachersInfo;


        }


       [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open the connection 
            Conn.Open();

            //Establish a new command(query) 
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = " + id;

            //Results of the query 
            MySqlDataReader Results = cmd.ExecuteReader();

            
            while (Results.Read())
            {
                //Access Column information 
                DateTime HireDate = (DateTime)Results["hiredate"];
                decimal Salary = (decimal)Results["salary"];
                string EmployeeNumber = (string)Results["employeenumber"];
                int TeacherId = (int)Results["teacherid"];
                string TeacherFname = (string)Results["teacherfname"];
                string TeacherLname = (string)Results["teacherlname"];

                
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }

            //returns final details of teachers.
            return NewTeacher;
        }

        
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open the connection 
            Conn.Open();

            //Establish a new command(query) 
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// This function allows user to add teacher
        /// </summary>
        /// <param name="NewTeacher"></param>
        [HttpPost]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open the connection 
            Conn.Open();

            //Establish a new command(query) 
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate,salary) values (@TeacherFname,@TeacherLname,@EmployeeNumber,CURRENT_DATE(),@Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();


            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            MySqlConnection Conn = Teachers.AccessDatabase();

            //Open the connection 
            Conn.Open();

            //Establish a new command(query) 
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber,salary=@Salary where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();


            cmd.ExecuteNonQuery();

            Conn.Close();

        }

    }
}
