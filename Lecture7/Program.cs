using System.Data;
using Microsoft.Data.SqlClient;

namespace BulkInsert
{
    class Program
    {
        static void Main(string[] args)
        {
            long start_time;
            long end_time;

            start_time = DateTime.Now.Ticks;
            using (SqlConnection sqlconn = new SqlConnection("Server=localhost;Initial Catalog=Northwind;Persist Security Info=False;User ID=sa;Password=pass;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;"))
            {
                sqlconn.Open();

                DataTable dt = new DataTable("[Products]");
                DataColumn col = new DataColumn();
                col.ColumnName = "ProductName";
                dt.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "SupplierID";
                dt.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "CategoryID";
                dt.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "QuantityPerUnit";
                dt.Columns.Add(col);
                col = new DataColumn();
                col.ColumnName = "UnitPrice";
                dt.Columns.Add(col);

                for (int i = 0; i < 10000; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["ProductName"] = "Product No." + i;
                    dr["SupplierID"] = 1;
                    dr["CategoryID"] = 1;
                    dr["QuantityPerUnit"] = "1 box";
                    dr["UnitPrice"] = 10;
                    dt.Rows.Add(dr);
                }

                using (SqlBulkCopy s = new SqlBulkCopy(sqlconn))
                {
                    s.DestinationTableName = dt.TableName;
                    s.ColumnMappings.Add("ProductName", "ProductName");
                    s.ColumnMappings.Add("SupplierID", "SupplierID");
                    s.ColumnMappings.Add("CategoryID", "CategoryID");
                    s.ColumnMappings.Add("QuantityPerUnit", "QuantityPerUnit");
                    s.ColumnMappings.Add("UnitPrice", "UnitPrice");
                    s.WriteToServer(dt);
                }
            }

            end_time = DateTime.Now.Ticks;

            TimeSpan timespan = new TimeSpan(end_time - start_time);
            Console.WriteLine(timespan.Seconds.ToString());

            Console.Read();
        }
    }
}
