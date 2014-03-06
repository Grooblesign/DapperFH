using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DapperLib;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace DapperWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IEnumerable<DapperLib.Person> persons = null;
            
            using (IDbConnection db = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnStringDb"].ToString()))
            {
                persons = db.Query<DapperLib.Person>("SELECT * FROM Person WHERE Gender = 'M' ORDER BY Surname, Forenames", null);
            }

            foreach (DapperLib.Person person in persons)
            {
                personList.InnerHtml += String.Format("<a href=\"Person.aspx?id={0}\">", person.Id) + person.Surname + ", " + person.Forenames + "</a>";
                personList.InnerHtml += "<br />";
            }
        }
    }
}