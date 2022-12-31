using System.Data;
using System.Data.SqlClient;

public class DatabaseAccess
{
    private static string connectionString { get; }

    static DatabaseAccess()
    {
        connectionString = "Data Source=.;Initial Catalog=TallerMecanico;Integrated Security=True";
    }

    public static DataTableReader executeStoredProcedure(string spName, Dictionary<string, object>? parameters = null)
    {
        if (spName == null || spName.Length == 0) throw new ArgumentNullException("spName");

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand cmd = new SqlCommand(spName, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> kvp in parameters)
                        cmd.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                }

                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                if (sqlDataReader != null)
                {
                    dt.Load(sqlDataReader);
                }

                connection.Close();

                return new DataTableReader(dt);
            }
        }
    }
}