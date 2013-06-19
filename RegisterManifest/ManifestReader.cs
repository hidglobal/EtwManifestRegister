using System;
using System.IO;
using System.Xml;

namespace RegisterManifest
{
    public class ManifestReader
    {
        public static void UpdateEventsManifestFileLoc(string manifestFile, string newFilePath, out string originalMessageFilename, out string originalResourceFilename)
        {
            FileInfo newFile = new FileInfo(newFilePath);

            if (!newFile.Exists)
                throw new FileNotFoundException("Unable to find file.", newFile.FullName);

            var xml = new XmlDocument();
            xml.Load(manifestFile);

            var providers = xml.GetElementsByTagName("provider");

            if (providers.Count == 0)
                throw new InvalidDataException("Unable to find provider node in manifest file.");

            var provider = providers[0];

            originalMessageFilename = provider.Attributes["resourceFileName"].Value.ToLower();
            originalResourceFilename = provider.Attributes["messageFileName"].Value.ToLower();

            bool changes = false;

            if (originalMessageFilename != newFile.FullName.ToLower())
            {
                provider.Attributes["messageFileName"].Value = Path.Combine(Path.GetDirectoryName(newFile.FullName), Path.GetFileName(originalMessageFilename));
                changes = true;
            }

            if (originalResourceFilename != newFile.FullName.ToLower())
            {
                provider.Attributes["resourceFileName"].Value = Path.Combine(Path.GetDirectoryName(newFile.FullName), Path.GetFileName(originalResourceFilename));
                changes = true;
            }

            if (changes)
            {
                Console.WriteLine("Saving manifest updates....");
                xml.Save(manifestFile);                
            }
        }

        public static void RestoreEventsManifestFileLoc(string manifestFile, string originalMessageFilename, string originalResourceFilename)
        {
            var xml = new XmlDocument();
            xml.Load(manifestFile);

            var providers = xml.GetElementsByTagName("provider");

            if (providers.Count == 0)
                throw new InvalidDataException("Unable to find provider node in manifest file.");

            var provider = providers[0];

            string currentMessageFilename = provider.Attributes["resourceFileName"].Value.ToLower();
            string currentResourceFilename = provider.Attributes["messageFileName"].Value.ToLower();

            bool changes = false;

            if (currentMessageFilename.ToLower() != originalMessageFilename.ToLower())
            {
                string newMessageFilename = originalMessageFilename;
                provider.Attributes["messageFileName"].Value = originalMessageFilename;
                changes = true;
            }

            if (currentResourceFilename.ToLower() != originalResourceFilename.ToLower())
            {
                provider.Attributes["resourceFileName"].Value = originalResourceFilename;
                changes = true;
            }

            if (changes)
            {
                Console.WriteLine("Saving manifest updates....");
                xml.Save(manifestFile);
            }
        }

    }
}
