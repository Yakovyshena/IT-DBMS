using System.Diagnostics;

namespace DBMS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (args.Length == 0)
                Application.Run(new Forms.FormDatabase());
            else
            {
                for (int i = 0; i < args.Length - 1; i++)
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Application.ExecutablePath,
                        ArgumentList = { args[i] }
                    });
                try
                {
                    Application.Run(new Forms.FormDatabase(args[^1]));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, $"Failed to open file \"{args[^1]}\"", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}