using System;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using DapperLib;

namespace DapperConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = @"Data Source=ADAMGARNER-PC\sqlexpress;Initial Catalog=GenBase;Integrated Security=True";
            IEnumerable<Person> persons = null;

            using (IDbConnection db = new SqlConnection(connString))
            {
                persons = db.Query<Person>("SELECT * FROM Person WHERE Gender = 'M' ORDER BY Surname, Forenames", null);
            }

            foreach (var person in persons)
            {
                Console.WriteLine(person.ToString());
            }

            using (IDbConnection db = new SqlConnection(connString))
            {
                var joined = db.Query("select details, title from citation inner join source on sourceid = source.id where details <> ''", null);

                foreach (Dictionary<string, object> dict in joined)
                {
                    Console.WriteLine(dict["details"]);
                    Console.WriteLine(dict["title"]);
                }
            }
        }
    }
}
