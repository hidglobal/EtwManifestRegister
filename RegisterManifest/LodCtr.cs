using System;
using System.IO;
using System.Diagnostics;

namespace RegisterManifest
{
    public class LodCtr
    {
        private const string INST_APP_NAME = @"C:\Windows\System32\lodctr.exe";
        private const string UNINST_APP_NAME = @"C:\Windows\System32\unlodctr.exe";

        public static void Uninstall(string manifestFile, string targetDirectory)
        {
            string targetPath = Path.Combine(targetDirectory, Path.GetFileName(manifestFile));
            File.Copy(manifestFile, targetPath);

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = "/m:" + targetPath;
            psi.Verb = "runas";
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.FileName = UNINST_APP_NAME;

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

            File.Delete(targetPath);
        }

        public static void Install(string manifestFile, string targetDirectory)
        {
            string targetPath = Path.Combine(targetDirectory, Path.GetFileName(manifestFile));
            File.Copy(manifestFile, targetPath);

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = "/m:" + targetPath;
            psi.Verb = "runas";
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.FileName = INST_APP_NAME;

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

            File.Delete(targetPath);
        }        
    }
}
