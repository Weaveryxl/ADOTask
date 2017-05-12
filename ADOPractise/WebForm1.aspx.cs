using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADOPractise
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "SELECT * FROM People WHERE LastName like @NameForm";
                cmd.CommandText = "spADOPractise";
                cmd.Connection = con;

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NameForm", "%an%");

                SqlParameter outputParam = new SqlParameter();
                outputParam.ParameterName = "@discrimination";
                outputParam.SqlDbType = System.Data.SqlDbType.VarChar;
                outputParam.Direction = System.Data.ParameterDirection.Output;
                outputParam.Size = 50;
                cmd.Parameters.Add(outputParam);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                //GridView1.DataSource = rdr;
                //GridView1.DataBind();

                string discriminator = outputParam.Value.ToString();
                message.Text = "User class: " + discriminator;
                GridView1.DataSource = rdr;
                GridView1.DataBind();
                //con.Close(); In using, we don't have to close 
            }
                
        }
    }
}