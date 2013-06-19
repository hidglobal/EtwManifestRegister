using System;
using System.Configuration;
using System.IO;

namespace RegisterManifest
{
    class Program
    {
        static void Main(string[] args)
        {

            string eventsManifestFile = ConfigurationManager.AppSettings["eventsManifestFile"];
            string countersManifestFile = ConfigurationManager.AppSettings["counterManifestFile"];
            string sourceEvents = ConfigurationManager.AppSettings["eventsAppSource"];
            string sourceCounters = ConfigurationManager.AppSettings["countersAppSource"];
            string targetApp = ConfigurationManager.AppSettings["targetApplication"];

            for (int i = 0; i < args.Length; i += 2)
            {
                switch (args[i])
                {
                    case "-eventsManifest":
                    case "-em":
                        eventsManifestFile = args[i + 1];
                        break;
                    case "-countersManifest":
                    case "-cm":
                        countersManifestFile = args[i + 1];
                        break;
                    case "-sourceEvents":
                    case "-se":
                        sourceEvents = args[i + 1];
                        break;
                    case "-sourceCounters":
                    case "-sc":
                        sourceCounters = args[i + 1];
                        break;
                    case "-targetPath":
                    case "-tp":
                        targetApp = args[i + 1];
                        break;
                    default:
                        Console.WriteLine(string.Format("Unknown arg: {0} = {1}", args[i], args[i + 1]));
                        break;
                }
            }

            try
            {
                string originalMessagesFilename;
                string originalResourcesFilename;

                Console.WriteLine("Uninstalling events...");
                WEvtUtil.Uninstall(eventsManifestFile);
                Console.WriteLine("Done.");

                Console.WriteLine("Uninstalling counters...");
                LodCtr.Uninstall(countersManifestFile, Path.GetDirectoryName(targetApp));
                Console.WriteLine("Done.");

                Console.WriteLine("Update events manifest paths...");
                ManifestReader.UpdateEventsManifestFileLoc(eventsManifestFile, targetApp, out originalMessagesFilename, out originalResourcesFilename);
                Console.WriteLine("Done.");

                Console.WriteLine("Installing events...");
                WEvtUtil.Install(eventsManifestFile);
                Console.WriteLine("Done.");

                Console.WriteLine("Installing counters...");
                LodCtr.Install(countersManifestFile, Path.GetDirectoryName(targetApp));
                Console.WriteLine("Done.");

                Console.WriteLine("Restore events manifest paths...");
                ManifestReader.RestoreEventsManifestFileLoc(eventsManifestFile, originalMessagesFilename, originalResourcesFilename);
                Console.WriteLine("Done.");

                /*
                WEvtUtil.GetStatus(appSource1);
                Console.WriteLine("Manifest Events Registration Completed.");
                
                WEvtUtil.GetStatus(appSource2);
                Console.WriteLine("Manifest Counters Registration Completed.");                
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
