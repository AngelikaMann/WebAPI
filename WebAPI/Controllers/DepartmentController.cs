using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
            select DepartmentId,DepartmentName from dbo.Department ";

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


        public string Post(Department department)
        {
            try
            {
                string query = @"
                    insert into dbo.Department values
                    ('" + department.DepartmentName + @"')";
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

        public string Put(Department department)
        {
            try
            {
                string query = @"update dbo.Department set DepartmentName=
                   '" + department.DepartmentName + @"'
                    where DepartmentId=" + department.DepartmentId + @"";

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
                string query = @"delete from dbo.Department 
                    where DepartmentId=" + id + @"";

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
    }
}
