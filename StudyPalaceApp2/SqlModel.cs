using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudyPalaceApp2
{
    internal class SqlModel
    {
        public static DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("STUDY_PALACE");
            SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-OTD56KR;Database=STUDY_PALACE;Integrated Security=True;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}
