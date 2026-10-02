using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StocklyMobi
{
    internal static class DatabaseBootstrapper
    {
        public static void EnsureDatabaseAndSchema()
        {
            string configured = ConfigurationManager.ConnectionStrings["POSDB"].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(configured);
            builder.InitialCatalog = "master";

            string scriptPath = Path.Combine(Application.StartupPath, "Database_Full.sql");
            if (!File.Exists(scriptPath))
                throw new FileNotFoundException("Database_Full.sql was not found beside the application.", scriptPath);

            string script = File.ReadAllText(scriptPath);
            string[] batches = Regex.Split(script, @"^\s*GO\s*(?:--.*)?$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                int batchNumber = 0;
                foreach (string rawBatch in batches)
                {
                    string batch = rawBatch.Trim();
                    if (batch.Length == 0) continue;
                    batchNumber++;

                    try
                    {
                        using (SqlCommand command = new SqlCommand(batch, connection))
                        {
                            command.CommandTimeout = 120;
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException ex)
                    {
                        string firstLine = batch.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                        if (firstLine.Length > 100) firstLine = firstLine.Substring(0, 100) + "...";
                        throw new InvalidOperationException(
                            "Database upgrade failed in SQL batch " + batchNumber +
                            ", line " + ex.LineNumber + ".\n" + ex.Message +
                            "\nBatch starts with: " + firstLine, ex);
                    }
                }
            }
        }
    }
}
