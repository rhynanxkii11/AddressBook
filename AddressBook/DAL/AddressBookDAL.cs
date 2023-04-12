using AddressBook.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace AddressBook.DAL
{
    public class AddressBookDAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }
        public List<AddressBookM> GetAll()
        {
            List<AddressBookM> abook = new List<AddressBookM>();
            using(_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[uspGetAddressBook]";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    AddressBookM addressB = new AddressBookM();
                    addressB.Id = Convert.ToInt32(dr["Id"]);
                    addressB.FirstName = dr["FirstName"].ToString();
                    addressB.LastName = dr["LastName"].ToString();
                    addressB.Address = dr["Address"].ToString();
                    addressB.City = dr["City"].ToString();
                    addressB.Province = dr["Province"].ToString();
                    addressB.ContactNumber = dr["ContactNumber"].ToString();
                    addressB.Email = dr["Email"].ToString();
                    abook.Add(addressB);
                }
                _connection.Close();
            }
            return abook;
        }

        public bool Insert(AddressBookM model)
        {
            int id = 0;
            using(_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[uspInsertAddressBook]";
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@Address", model.Address);
                _command.Parameters.AddWithValue("@City", model.City);
                _command.Parameters.AddWithValue("@Province", model.Province);
                _command.Parameters.AddWithValue("@ContactNumber", model.ContactNumber);
                _command.Parameters.AddWithValue("@Email", model.Email);

                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }

        public AddressBookM GetById(int id)
        {
            AddressBookM newAddressBook = new AddressBookM();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[uspGetAddressBookById]";
                _command.Parameters.AddWithValue("@Id", id);
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while (dr.Read())
                {
                    newAddressBook.Id = Convert.ToInt32(dr["Id"]);
                    newAddressBook.FirstName = dr["FirstName"].ToString();
                    newAddressBook.LastName = dr["LastName"].ToString();
                    newAddressBook.Address = dr["Address"].ToString();
                    newAddressBook.City = dr["City"].ToString();
                    newAddressBook.Province = dr["Province"].ToString();
                    newAddressBook.ContactNumber = dr["ContactNumber"].ToString();
                    newAddressBook.Email = dr["Email"].ToString();
                }
                _connection.Close();
            }
            return newAddressBook;
        }

        public bool Update(AddressBookM model)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[uspUpdateAddressBook]";
                _command.Parameters.AddWithValue("@Id", model.Id);
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@Address", model.Address);
                _command.Parameters.AddWithValue("@City", model.City);
                _command.Parameters.AddWithValue("@Province", model.Province);
                _command.Parameters.AddWithValue("@ContactNumber", model.ContactNumber);
                _command.Parameters.AddWithValue("@Email", model.Email);

                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            int idd = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[uspDeleteAddressBook]";
                _command.Parameters.AddWithValue("@Id", id);
                _connection.Open();
                idd = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return idd > 0 ? true : false;
        }
    }
}
