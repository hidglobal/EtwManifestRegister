using System;
using System.Diagnostics;

namespace RegisterManifest
{
    public class WEvtUtil
    {
        private const string APP_NAME = @"C:\Windows\System32\wevtutil.exe";

        public static void Uninstall(string manifestFile)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = "um " + manifestFile;
            psi.Verb = "runas";
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.FileName = APP_NAME;

            string errors;
            string output;

            using (Process p = Process.Start(psi))
            {
                output = p.StandardOutput.ReadToEnd();
                errors = p.StandardError.ReadToEnd();
            }

            if (errors.Trim() != string.Empty)
                throw new Exception("Failure: " + errors);

            Console.Write(output);
        }

        public static void Install(string manifestFile)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = "im " + manifestFile;
            psi.Verb = "runas";
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.FileName = APP_NAME;

            string errors;
            string output;

            using (Process p = Process.Start(psi))
            {
                output = p.StandardOutput.ReadToEnd();
                errors = p.StandardError.ReadToEnd();
            }

            if (errors.Trim() != string.Empty)
                throw new Exception("Failure: " + errors);

            Console.Write(output);
        }

        public static void GetStatus(string appSource)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = "gp " + appSource;
            psi.Verb = "runas";
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.FileName = APP_NAME;

            string errors;
            string output;

            using (Process p = Process.Start(psi))
            {
                output = p.StandardOutput.ReadToEnd();
                errors = p.StandardError.ReadToEnd();
            }

            if (errors.Trim() != string.Empty)
                throw new Exception("Failure: " + errors);

            //Console.Write(output);
        }
    }
}
