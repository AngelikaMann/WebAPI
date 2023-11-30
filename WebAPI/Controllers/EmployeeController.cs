using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;


namespace WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
            select EmployeeId,EmployeeName,Department,
                convert(varchar(10),DateOfJoining,120) as DateOfJoining,
                PhotoFileName
                from dbo.Employee ";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);

        }


        public string Post(Employee employee)
        {
            try
            {
                string query = @"
                    insert into dbo.Employee values
                    (
                '" + employee.EmployeeName + @"'
                ,'" + employee.Department + @"'
                ,'" + employee.DateOfJoining + @"'
                ,'" + employee.PhotoFileName + @"'
                    )";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successfully!!!";

            }
            catch (System.Exception)
            {

                return "Failed to Add!";
            }
        }

        public string Put(Employee employee)
        {
            try
            {
                string query = @"
                     update dbo.Employee set 
                     EmployeeName='" + employee.EmployeeName + @"'
                     ,Department='" + employee.Department + @"'
                     ,DateOfJoining='" + employee.DateOfJoining + @"'
                     ,PhotoFileName='" + employee.PhotoFileName + @"'

                    where EmployeeId=" + employee.EmployeeId + @"";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully!!!";

            }
            catch (System.Exception)
            {

                return "Failed to Update!";
            }
        }


        public string Delete(int id)
        {
            try
            {
                string query = @"delete from dbo.Employee 
                    where EmployeeId=" + id + @"";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Successfully!!!";

            }
            catch (System.Exception)
            {

                return "Failed to Delete!";
            }
        }

        [Route("api/Employee/GetAllDepartmentNames")]
        [HttpGet]

        public HttpResponseMessage GetAllDepartmentNames()
        {
            string query = @"
                select DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/Employee/SaveFile")]

        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);
                postedFile.SaveAs(physicalPath);
                return filename;
            }
            catch (System.Exception)
            {

                return "anonymous.png";
            }
        }
    }
}
