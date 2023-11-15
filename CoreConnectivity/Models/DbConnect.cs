using System.Data;
using Microsoft.Data.SqlClient;

namespace CoreConnectivity.Models
{
    public class DbConnect
    {
        private readonly IConfiguration _configuration;
        public  DbConnect(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Employee> GetEmployees()
        {
            string dbConn = _configuration.GetConnectionString("DefaultConnection").ToString();
            List<Employee> empList = new List<Employee>();
            string sql = "select * from tbl_Employee where is_Del=0";
            SqlConnection con = new SqlConnection(dbConn);
            SqlCommand cmd = new SqlCommand(sql, con);
            if(con.State==ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                Employee emp = new Employee();
                emp.Eid = (int)dr["eid"];
                emp.Name = dr["ename"].ToString();
                emp.Age = (int)dr["eage"];
                emp.Address = dr["eaddress"].ToString();
                emp.Mobileno = dr["emobileno"].ToString();
                emp.Salary = (decimal)dr["esalary"];
                empList.Add(emp);
            }
            con.Close();
            return empList;
        }
        public bool addEmployee(Employee empObj)
        {
            string dbConn = _configuration.GetConnectionString("DefaultConnection").ToString();
            SqlConnection con = new SqlConnection(dbConn);
            if(con.State==ConnectionState.Closed)
            {
                con.Open();
            }
            string sql = "insert into tbl_Employee(ename,eage,eaddress,emobileno,esalary) values(@name,@age,@address,@mobile,@salary)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", empObj.Name);
            cmd.Parameters.AddWithValue("@age", empObj.Age);
            cmd.Parameters.AddWithValue("@address", empObj.Address);
            cmd.Parameters.AddWithValue("@mobile", empObj.Mobileno);
            cmd.Parameters.AddWithValue("@salary", empObj.Salary);
            int n=cmd.ExecuteNonQuery();
            con.Close();
            if (n!=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Employee GetEmployee(int id)
        {
            string dbConn = _configuration.GetConnectionString("DefaultConnection").ToString();
            string sql = "select * from tbl_Employee where eid=@eid";
            SqlConnection con = new SqlConnection(dbConn);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@eid", id);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            Employee emp = new Employee();
            if (dr.Read())
            {
                emp.Eid = (int)dr["eid"];
                emp.Name = dr["ename"].ToString();
                emp.Age = (int)dr["eage"];
                emp.Address = dr["eaddress"].ToString();
                emp.Mobileno = dr["emobileno"].ToString();
                emp.Salary = (decimal)dr["esalary"];
            }
            con.Close();
            return emp;
        }
        public bool updateEmployee(Employee empObj)
        {
            string dbConn = _configuration.GetConnectionString("DefaultConnection").ToString();
            SqlConnection con = new SqlConnection(dbConn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string sql = "update tbl_Employee set ename=@name,eage=@age,eaddress=@address,emobileno=@mobile,esalary=@salary where eid=@eid";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", empObj.Name);
            cmd.Parameters.AddWithValue("@age", empObj.Age);
            cmd.Parameters.AddWithValue("@address", empObj.Address);
            cmd.Parameters.AddWithValue("@mobile", empObj.Mobileno);
            cmd.Parameters.AddWithValue("@salary", empObj.Salary);
            cmd.Parameters.AddWithValue("@eid", empObj.Eid);
            int n = cmd.ExecuteNonQuery();
            con.Close();
            if (n != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool deleteEmployee(int id)
        {
            string dbConn = _configuration.GetConnectionString("DefaultConnection").ToString();
            SqlConnection con = new SqlConnection(dbConn);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string sql = "update tbl_Employee set is_Del=@del where eid=@eid";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@eid",id);
            cmd.Parameters.AddWithValue("@del",true);
            int n = cmd.ExecuteNonQuery();
            con.Close();
            if (n != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}





