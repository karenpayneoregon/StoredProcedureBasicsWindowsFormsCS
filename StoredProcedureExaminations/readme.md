# About
This project provides code to view stored procedures in a specific database using Transactional SQL (T-SQL). 

For those who might like an alternate method the following code sample using [SMO](https://docs.microsoft.com/en-us/sql/relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide?view=sql-server-2017).

> When adding references to a project for SMO note that each version of SQL-Server has it's own version of SMO which needs to be known when attempting to add references.

- Create a new class project name the project StoredProcedureScripter
- Add references for SMO (see using statements below)
- Add the following class.

```csharp
using System.Collections.Generic;
using System.Data;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;

namespace ScriptingLibrary
{
    public class StoredProcedureScripter
    {
        /// <summary>
        /// Script stored procedures from a specific database residing in a specific
        /// SQL-Server instance
        /// </summary>
        /// <param name="pServerName">Name of SQL-Server</param>
        /// <param name="pCatalogName">Catalog to traverse Stored Procedures on</param>
        /// <param name="pFileName">File name and path to write Stored Procedures too</param>
        /// <remarks>
        /// Exception handling intentionally left out. At least there should be a try/catch
        /// around this method from the caller of this method.
        /// </remarks>
        public void Execute(string pServerName, string pCatalogName, string pFileName)
        {
            Server server = new Server(pServerName);
            Database database = server.Databases[pCatalogName];

            var sqlSmoObjectList = new List<SqlSmoObject>();
            DataTable dataTable = database.EnumObjects(DatabaseObjectTypes.StoredProcedure);

            foreach (DataRow row in dataTable.Rows)
            {
                var currentSchema = (string)row["Schema"];

                if (currentSchema == "sys" || currentSchema == "INFORMATION_SCHEMA")
                {
                    continue;
                }

                var sp = (StoredProcedure)server.GetSmoObject(new Urn((string)row["Urn"]));

                if (!sp.IsSystemObject)
                {
                    sqlSmoObjectList.Add(sp);
                }

            }

            var scriptWriter = new Scripter 
            {
                Server = server, Options =
                {
                    IncludeHeaders = true,
                    SchemaQualify = true,
                    ToFileOnly = true,
                    FileName = pFileName
                }
            };

            scriptWriter.Script(sqlSmoObjectList.ToArray());

        }
    }
}
```
- Create a Console project to test the code above
- Add a reference to the class project above
- Replace Main method with the following (change the namespace to your namespace)
- Change the first parameter to Execute to your server name e.g. .\SQLEXPRESS
- Change the second parameter to Execute to the database which has stored procedures.

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptingLibrary;

namespace ScriptConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new StoredProcedureScripter();
            test.Execute(
                "KARENS-PC", 
                "CustomerDatabase", 
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "results.txt"));
        }
    }
}
```
