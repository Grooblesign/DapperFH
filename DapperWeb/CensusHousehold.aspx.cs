using System;
using System.Data;
using System.Web.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DapperWeb
{
    public partial class CensusHousehold : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnStringDb"].ToString()))
            {
                try
                {
                    var household = db.Query("SELECT * FROM CensusHousehold WHERE Id = @Id", new { Id = Request.QueryString["id"] });

                    foreach (Dictionary<string, object> dict in household)
                    {
                        mainDiv.InnerHtml += dict["Address"] + "<br />";
                    }

                    mainDiv.InnerHtml += "<br />";

                    var people = db.Query("SELECT * FROM CensusHouseholdPerson WHERE CensusHouseholdId = @Id", new { Id = Request.QueryString["id"] });

                    foreach (Dictionary<string, object> dict in people)
                    {
                        mainDiv.InnerHtml += dict["Name"] + "<br />";
                    }
                }
                catch (Exception ex)
                {
                    exception.InnerText = ex.Message;
                }
            }
        }
    }
}