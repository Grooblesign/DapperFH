using System;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using Dapper;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Collections.Generic;

namespace DapperWeb
{
    public partial class Person : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnStringDb"].ToString()))
            {
                try
                {
                    var person = db.Query<DapperLib.Person>("SELECT * FROM Person WHERE Id = @Id", new { Id = Request.QueryString["id"] }).Single();

                    Name.InnerText = person.Fullname;

                    DapperLib.Person father = null;
                    if (person.FatherId != 0)
                    {
                        father = db.Query<DapperLib.Person>("SELECT * FROM Person WHERE Id = @Id", new { Id = person.FatherId }).Single();
                    }
                    DapperLib.Person mother = null;
                    if (person.MotherId != 0)
                    {
                        mother = db.Query<DapperLib.Person>("SELECT * FROM Person WHERE Id = @Id", new { Id = person.MotherId }).Single();
                    }

                    parentsDiv.InnerHtml += "<table>";
                    parentsDiv.InnerHtml += "<tr>";
                    parentsDiv.InnerHtml += "<td class=\"parentCell\">Father</td>";
                    parentsDiv.InnerHtml += "<td>";
                    parentsDiv.InnerHtml += father == null ? "" : string.Format("<a href=\"person.aspx?id={0}\">{1}</a>", father.Id, father.Fullname);
                    parentsDiv.InnerHtml += "</td>";
                    parentsDiv.InnerHtml += "</tr>";
                    parentsDiv.InnerHtml += "<tr>";
                    parentsDiv.InnerHtml += "<td class=\"parentCell\">Mother</td>";
                    parentsDiv.InnerHtml += "<td>";
                    parentsDiv.InnerHtml += mother == null ? "" : string.Format("<a href=\"person.aspx?id={0}\">{1}</a>", mother.Id, mother.Fullname);
                    parentsDiv.InnerHtml += "</td>";
                    parentsDiv.InnerHtml += "</tr>";
                    parentsDiv.InnerHtml += "</table>";

                    IEnumerable events = db.Query<DapperLib.Event>("SELECT * FROM Event WHERE PersonId = @Id", new { Id = Request.QueryString["id"] }).OrderBy(p => p.SortDateValue);

                    eventsDiv.InnerHtml += "<table width=\"100%\">";
                    eventsDiv.InnerHtml += "<thead>";
                    eventsDiv.InnerHtml += "<td>Id</td>";
                    eventsDiv.InnerHtml += "<td>Date</td>";
                    eventsDiv.InnerHtml += "<td>Type</td>";
                    eventsDiv.InnerHtml += "<td>Location</td>";
                    eventsDiv.InnerHtml += "<td>Details</td>";
                    eventsDiv.InnerHtml += "</thead>";

                    foreach (DapperLib.Event eventX in events)
                    {
                        if (eventX.IsPrimary)
                        {
                            eventsDiv.InnerHtml += "<tr type=\"primary\">";
                        }
                        else
                        {
                            eventsDiv.InnerHtml += "<tr>";
                        }
                        eventsDiv.InnerHtml += "<td>" + eventX.Id + "</td>";
                        eventsDiv.InnerHtml += "<td>" + eventX.Date + "</td>";
                        eventsDiv.InnerHtml += "<td>" + eventX.Type + "</td>";
                        eventsDiv.InnerHtml += "<td>" + eventX.Location + "</td>";
                        eventsDiv.InnerHtml += "<td>" + eventX.Details + "</td>";
                        eventsDiv.InnerHtml += "</tr>";
                    }
                    eventsDiv.InnerHtml += "</table>";

                    StringBuilder SQLBuilder = new StringBuilder();

                    SQLBuilder.Append("SELECT ");
                    SQLBuilder.Append("chp.Id");
                    SQLBuilder.Append(",c.Title");
                    SQLBuilder.Append(",CensusHouseholdId");
	                SQLBuilder.Append(",ch.Address");
                    SQLBuilder.Append(",PersonId");
                    SQLBuilder.Append(",Name");
                    SQLBuilder.Append(",Age");
                    SQLBuilder.Append(",Occupation");
                    SQLBuilder.Append(",RelToHead");
                    SQLBuilder.Append(",Status");
                    SQLBuilder.Append(",Birthplace ");
                    SQLBuilder.Append("FROM CensusHouseholdPerson AS chp ");
                    SQLBuilder.Append("LEFT JOIN CensusHousehold AS ch ON chp.CensusHouseholdId = ch.id ");
                    SQLBuilder.Append("LEFT JOIN Census AS c ON ch.CensusId = c.id ");
                    SQLBuilder.Append("WHERE PersonId=@Id ");
                    SQLBuilder.Append("ORDER BY c.Title ");

                    var SQL = SQLBuilder.ToString();

                    var joined = db.Query(SQL, new { Id = Request.QueryString["id"] });

                    censusDiv.InnerHtml += "<table width=\"100%\">";

                    censusDiv.InnerHtml += "<thead>";
                    censusDiv.InnerHtml += "<td>Id</td>";
                    censusDiv.InnerHtml += "<td>Census</td>";
                    censusDiv.InnerHtml += "<td>Address</td>";
                    censusDiv.InnerHtml += "<td>Name</td>";
                    censusDiv.InnerHtml += "<td>Relationship</td>";
                    censusDiv.InnerHtml += "<td>Age</td>";
                    censusDiv.InnerHtml += "<td>Occupation</td>";
                    censusDiv.InnerHtml += "<td>Birthplace</td>";
                    censusDiv.InnerHtml += "</thead>";

                    foreach (Dictionary<string, object> dict in joined)
                    {
                        censusDiv.InnerHtml += "<tr>";
                        censusDiv.InnerHtml += string.Format("<td><a href=\"CensusHousehold.aspx?id={0}\">{0}</a></td>", dict["CensusHouseholdId"]);
                        censusDiv.InnerHtml += "<td>" + dict["Title"] + "</td>";
                        censusDiv.InnerHtml += "<td>" + dict["Address"] + "</td>";
                        censusDiv.InnerHtml += "<td>" + dict["Name"] + "</td>";
                        censusDiv.InnerHtml += "<td>" + dict["RelToHead"] + "</td>";
                        censusDiv.InnerHtml += "<td>" + dict["Age"] + "</td>";
                        censusDiv.InnerHtml += "<td>" + dict["Occupation"] + "</td>";
                        censusDiv.InnerHtml += "<td>" + dict["Birthplace"] + "</td>";
                        censusDiv.InnerHtml += "</tr>";
                    }

                    censusDiv.InnerHtml += "</table>";

                    censusDiv.InnerHtml += "<br />";

                    var children = db.Query<DapperLib.Person>("SELECT * FROM Person WHERE FatherId = @Id OR MotherId = @Id", new { Id = Request.QueryString["id"] });

                    childrenDiv.InnerHtml += "<table width=\"100%\">";

                    childrenDiv.InnerHtml += "<thead>";
                    childrenDiv.InnerHtml += "<td>Id</td>";
                    childrenDiv.InnerHtml += "<td>Name</td>";
                    childrenDiv.InnerHtml += "</thead>";

                    foreach (DapperLib.Person child in children)
                    {
                        childrenDiv.InnerHtml += "<tr>";
                        childrenDiv.InnerHtml += "<td>" + child.Id + "</td>";
                        childrenDiv.InnerHtml += "<td>" + child.Fullname + "</td>";
                        childrenDiv.InnerHtml += "</tr>";
                    }

                    childrenDiv.InnerHtml += "</table>";
                }
                catch (Exception ex)
                {
                    exception.InnerText = ex.Message;
                }
            }

        }
    }
}