using SMS.DATA.Infrastructure;
using SMS.DATA.Models.NonDbContextModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SMS.DATA.Implementation
{
    public class StoredProcCaller : IStoredProcCaller
    {
        private readonly SqlConnection _connection;
        private readonly SqlConnection _Requestconnection;
        public StoredProcCaller()
        {
            var connStr = ConfigurationManager.ConnectionStrings["SmsConnection"].ConnectionString;
            _connection = new SqlConnection(connStr);

        }

        public List<EmployeeFinanceInfo> GetEmployeeFinance(Guid? schoolId, Guid? DesignationId, string SalaryMonth)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("GetEmployeeFinances", _connection);
            cmd.Parameters.AddWithValue("@School", schoolId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Designation", DesignationId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SalaryMonth", SalaryMonth ?? (object)DBNull.Value);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            var empFinanceList = new List<EmployeeFinanceInfo>();
            while (rdr.Read())
            {
                var IsSalaryTransferred = Convert.ToBoolean(rdr["IsSalaryTransferred"].ToString() == "" ? "false" : rdr["IsSalaryTransferred"].ToString());
                var empfDetailsId = rdr["EmpFinanceDetailsId"].ToString() == "" ? Guid.Empty : Guid.Parse(rdr["EmpFinanceDetailsId"].ToString());
                var studentFinance = new EmployeeFinanceInfo
                {
                    FirstName = rdr["FirstName"].ToString(),
                    LastName = rdr["LastName"].ToString(),
                    Designation = rdr["designation"].ToString(),
                    SchoolName = rdr["SchoolName"].ToString(),
                    SalaryMonth = rdr["SalaryMonth"].ToString(),
                    IsSalaryTransferred = IsSalaryTransferred,
                    SalaryYear = rdr["SalaryYear"].ToString(),
                    EmpFinanceDetailsId = empfDetailsId,
                    EmployeeId = Guid.Parse(rdr["EmployeeId"].ToString()),
                };
                empFinanceList.Add(studentFinance);
            }
            _connection.Close();
            return empFinanceList;
        }

        public List<EmployeeFinanceInfo> GetEmployeeFinanceDetail(Guid? schoolId, Guid? DesignationId)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("GetEmployeeFinancesDetail", _connection);
            cmd.Parameters.AddWithValue("@School", schoolId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Designation", DesignationId ?? (object)DBNull.Value);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            var empFinanceList = new List<EmployeeFinanceInfo>();
            while (rdr.Read())
            {
                var empfDetailsId = rdr["EmpFinanceDetailsId"].ToString() == "" ? Guid.Empty : Guid.Parse(rdr["EmpFinanceDetailsId"].ToString());
                var studentFinance = new EmployeeFinanceInfo
                {
                    FirstName = rdr["FirstName"].ToString(),
                    LastName = rdr["LastName"].ToString(),
                    Designation = rdr["designation"].ToString(),
                    SchoolName = rdr["SchoolName"].ToString(),
                    EmpFinanceDetailsId = empfDetailsId,
                    EmployeeId = Guid.Parse(rdr["EmployeeId"].ToString()),
                };
                empFinanceList.Add(studentFinance);
            }
            _connection.Close();
            return empFinanceList;
        }

        public List<StudentFinanceInfo> GetStudentFinanceDetail(Guid? schoolId, Guid? ClassId, int? Regno, string Month, string Year)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("GetStudentFinancesDetailNew", _connection);
            cmd.Parameters.AddWithValue("@SchoolId", schoolId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ClassId", ClassId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Regno", Regno ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Month", Month ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Year", Year ?? (object)DBNull.Value);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            var stdFinanceList = new List<StudentFinanceInfo>();
            while (rdr.Read())
            {
                var stdfDetailsId = rdr["StudentFinanceDetailsId"].ToString() == "" ? Guid.Empty : Guid.Parse(rdr["StudentFinanceDetailsId"].ToString());
                var studentFinance = new StudentFinanceInfo();
                studentFinance.RegistrationNumber = !string.IsNullOrWhiteSpace(rdr["RegistrationNumber"].ToString()) ? Convert.ToInt32(rdr["RegistrationNumber"]) : 0;
                studentFinance.FirstName = rdr["FirstName"].ToString();
                studentFinance.LastName = rdr["LastName"].ToString();
                studentFinance.ClassName = rdr["ClassName"].ToString();
                studentFinance.SchoolName = rdr["SchoolName"].ToString();
                studentFinance.FeeMonth = rdr["FeeMonth"].ToString();
                studentFinance.FeeYear = rdr["FeeYear"].ToString();
                studentFinance.StudentFinanceDetailsId = stdfDetailsId;
                studentFinance.StudentId = Guid.Parse(rdr["StudentId"].ToString());
                studentFinance.Type = rdr["Type"].ToString();
                studentFinance.Fee = !string.IsNullOrWhiteSpace(rdr["Fee"].ToString()) ? Convert.ToDecimal(rdr["Fee"]) : new decimal();
                studentFinance.Arears = !string.IsNullOrWhiteSpace(rdr["Arears"].ToString()) ? Convert.ToDecimal(rdr["Arears"]) : new decimal();
                studentFinance.FeeSubmitted = !string.IsNullOrWhiteSpace(rdr["FeeSubmitted"].ToString()) && Convert.ToBoolean(rdr["FeeSubmitted"]);

                stdFinanceList.Add(studentFinance);
            }
            _connection.Close();
            return stdFinanceList;
        }

        public List<StudentFinanceInfo> GetStudentFinance(Guid? School, Guid? Class, Guid? StudentId, string FeeMonth)
        {
            _connection.Open();
            SqlCommand cmd = new SqlCommand("GetStudentFinances", _connection);
            cmd.Parameters.AddWithValue("@School", School ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Class", Class ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@StudentId", StudentId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FeeMonth", FeeMonth ?? (object)DBNull.Value);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            var studentFinanceList = new List<StudentFinanceInfo>();
            while (rdr.Read())
            {
                var isFeeSubmitted = Convert.ToBoolean(rdr["FeeSubmitted"].ToString() == "" ? "false" : rdr["FeeSubmitted"].ToString());
                var sfDetailsId = rdr["StudentFinanceDetailsId"].ToString() == "" ? Guid.Empty : Guid.Parse(rdr["StudentFinanceDetailsId"].ToString());
                var studentFinance = new StudentFinanceInfo
                {
                    FirstName = rdr["FirstName"].ToString(),
                    LastName = rdr["LastName"].ToString(),
                    ClassName = rdr["ClassName"].ToString(),
                    SchoolName = rdr["SchoolName"].ToString(),
                    FeeMonth = rdr["FeeMonth"].ToString(),
                    FeeSubmitted = isFeeSubmitted,
                    FeeYear = rdr["FeeYear"].ToString(),
                    StudentFinanceDetailsId = sfDetailsId,
                    StudentId = Guid.Parse(rdr["StudentId"].ToString()),
                    Type = rdr["Type"].ToString()
                };
                studentFinanceList.Add(studentFinance);
            }
            _connection.Close();
            return studentFinanceList;
        }

        public UserInfo GetUserInfo(string UserName)
        {

            _connection.Open();
            SqlCommand cmd = new SqlCommand("GetUserInfobyusername", _connection);
            cmd.Parameters.AddWithValue("UserName", UserName);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var imgSize = rdr["ImageSize"].ToString();
                var imageId = rdr["ImageId"].ToString() == "" ? Guid.Empty : Guid.Parse(rdr["ImageId"].ToString());
                return new UserInfo
                {
                    UserName = rdr["Username"].ToString(),
                    Email = rdr["Email"].ToString(),
                    FirstName = rdr["FirstName"].ToString(),
                    LastName = rdr["LastName"].ToString(),
                    CreationDate = (DateTime)rdr["CreatedDate"],
                    PermanentAddress = rdr["PermanentAddress"].ToString(),
                    Phone = rdr["Phone"].ToString(),
                    ImageName = rdr["ImageName"].ToString(),
                    ImagePath = rdr["ImagePath"].ToString(),
                    ImageSize = Convert.ToInt32(imgSize == "" ? "0" : imgSize),
                    ImageId = imageId,
                    PersonId = Guid.Parse(rdr["PersonId"].ToString()),
                    ImageExtension = rdr["ImageExtension"].ToString()
                };
            }
            _connection.Close();
            return null;
        }


        public List<EmployeeFinanceInfo> RequestGetEmployeeFinance(Guid? schoolId, Guid? DesignationId, string SalaryMonth)
        {
            _Requestconnection.Open();
            SqlCommand cmd = new SqlCommand("GetEmployeeFinances", _Requestconnection);
            cmd.Parameters.AddWithValue("@School", schoolId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Designation", DesignationId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@SalaryMonth", SalaryMonth ?? (object)DBNull.Value);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            var empFinanceList = new List<EmployeeFinanceInfo>();
            while (rdr.Read())
            {
                var IsSalaryTransferred = Convert.ToBoolean(rdr["IsSalaryTransferred"].ToString() == "" ? "false" : rdr["IsSalaryTransferred"].ToString());
                var empfDetailsId = rdr["EmpFinanceDetailsId"].ToString() == "" ? Guid.Empty : Guid.Parse(rdr["EmpFinanceDetailsId"].ToString());
                var studentFinance = new EmployeeFinanceInfo
                {
                    FirstName = rdr["FirstName"].ToString(),
                    LastName = rdr["LastName"].ToString(),
                    Designation = rdr["designation"].ToString(),
                    SchoolName = rdr["SchoolName"].ToString(),
                    SalaryMonth = rdr["SalaryMonth"].ToString(),
                    IsSalaryTransferred = IsSalaryTransferred,
                    SalaryYear = rdr["SalaryYear"].ToString(),
                    EmpFinanceDetailsId = empfDetailsId,
                    EmployeeId = Guid.Parse(rdr["EmployeeId"].ToString()),
                };
                empFinanceList.Add(studentFinance);
            }
            _Requestconnection.Close();
            return empFinanceList;
        }

        public List<EmployeeFinanceInfo> RequestGetEmployeeFinanceDetail(Guid? schoolId, Guid? DesignationId)
        {
            _Requestconnection.Open();
            SqlCommand cmd = new SqlCommand("GetEmployeeFinancesDetail", _Requestconnection);
            cmd.Parameters.AddWithValue("@School", schoolId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Designation", DesignationId ?? (object)DBNull.Value);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            var empFinanceList = new List<EmployeeFinanceInfo>();
            while (rdr.Read())
            {
                var empfDetailsId = rdr["EmpFinanceDetailsId"].ToString() == "" ? Guid.Empty : Guid.Parse(rdr["EmpFinanceDetailsId"].ToString());
                var studentFinance = new EmployeeFinanceInfo
                {
                    FirstName = rdr["FirstName"].ToString(),
                    LastName = rdr["LastName"].ToString(),
                    Designation = rdr["designation"].ToString(),
                    SchoolName = rdr["SchoolName"].ToString(),
                    EmpFinanceDetailsId = empfDetailsId,
                    EmployeeId = Guid.Parse(rdr["EmployeeId"].ToString()),
                };
                empFinanceList.Add(studentFinance);
            }
            _Requestconnection.Close();
            return empFinanceList;
        }

        public List<StudentFinanceInfo> RequestGetStudentFinanceDetail(Guid? schoolId, Guid? ClassId, Guid? StudentId)
        {
            _Requestconnection.Open();
            SqlCommand cmd = new SqlCommand("GetStudentFinancesDetail", _Requestconnection);
            cmd.Parameters.AddWithValue("@SchoolId", schoolId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ClassId", ClassId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@StudentId", StudentId ?? (object)DBNull.Value);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            var stdFinanceList = new List<StudentFinanceInfo>();
            while (rdr.Read())
            {
                var stdfDetailsId = rdr["StudentFinanceDetailsId"].ToString() == "" ? Guid.Empty : Guid.Parse(rdr["StudentFinanceDetailsId"].ToString());
                var studentFinance = new StudentFinanceInfo
                {
                    FirstName = rdr["FirstName"].ToString(),
                    LastName = rdr["LastName"].ToString(),
                    ClassName = rdr["ClassName"].ToString(),
                    SchoolName = rdr["SchoolName"].ToString(),
                    StudentFinanceDetailsId = stdfDetailsId,
                    StudentId = Guid.Parse(rdr["StudentId"].ToString()),
                    Type = rdr["Type"].ToString()
                };
                stdFinanceList.Add(studentFinance);
            }
            _Requestconnection.Close();
            return stdFinanceList;
        }

        public List<StudentFinanceInfo> RequestGetStudentFinance(Guid? School, Guid? Class, Guid? StudentId, string FeeMonth)
        {
            _Requestconnection.Open();
            SqlCommand cmd = new SqlCommand("GetStudentFinances", _Requestconnection);
            cmd.Parameters.AddWithValue("@School", School ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Class", Class ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@StudentId", StudentId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FeeMonth", FeeMonth ?? (object)DBNull.Value);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            var studentFinanceList = new List<StudentFinanceInfo>();
            while (rdr.Read())
            {
                var isFeeSubmitted = Convert.ToBoolean(rdr["FeeSubmitted"].ToString() == "" ? "false" : rdr["FeeSubmitted"].ToString());
                var sfDetailsId = rdr["StudentFinanceDetailsId"].ToString() == "" ? Guid.Empty : Guid.Parse(rdr["StudentFinanceDetailsId"].ToString());
                var studentFinance = new StudentFinanceInfo
                {
                    FirstName = rdr["FirstName"].ToString(),
                    LastName = rdr["LastName"].ToString(),
                    ClassName = rdr["ClassName"].ToString(),
                    SchoolName = rdr["SchoolName"].ToString(),
                    FeeMonth = rdr["FeeMonth"].ToString(),
                    FeeSubmitted = isFeeSubmitted,
                    FeeYear = rdr["FeeYear"].ToString(),
                    StudentFinanceDetailsId = sfDetailsId,
                    StudentId = Guid.Parse(rdr["StudentId"].ToString()),
                    Type = rdr["Type"].ToString()
                };
                studentFinanceList.Add(studentFinance);
            }
            _Requestconnection.Close();
            return studentFinanceList;
        }

        public UserInfo RequestGetUserInfo(string UserName)
        {

            _Requestconnection.Open();
            SqlCommand cmd = new SqlCommand("GetUserInfobyusername", _Requestconnection);
            cmd.Parameters.AddWithValue("RequestUserName", UserName);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var imgSize = rdr["ImageSize"].ToString();
                var imageId = rdr["ImageId"].ToString() == "" ? Guid.Empty : Guid.Parse(rdr["ImageId"].ToString());
                return new UserInfo
                {
                    UserName = rdr["Username"].ToString(),
                    Email = rdr["Email"].ToString(),
                    FirstName = rdr["FirstName"].ToString(),
                    LastName = rdr["LastName"].ToString(),
                    CreationDate = (DateTime)rdr["CreatedDate"],
                    PermanentAddress = rdr["PermanentAddress"].ToString(),
                    Phone = rdr["Phone"].ToString(),
                    ImageName = rdr["ImageName"].ToString(),
                    ImagePath = rdr["ImagePath"].ToString(),
                    ImageSize = Convert.ToInt32(imgSize == "" ? "0" : imgSize),
                    ImageId = imageId,
                    PersonId = Guid.Parse(rdr["PersonId"].ToString()),
                    ImageExtension = rdr["ImageExtension"].ToString()
                };
            }
            _Requestconnection.Close();
            return null;
        }
    }
}
