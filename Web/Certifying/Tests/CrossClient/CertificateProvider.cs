//using System;
//using System.IO;
//using System.Reflection;
//using System.Security.Cryptography.X509Certificates;

//namespace CrossClient
//{
//    public static class CertificateProvider
//    {
//        private const string certificateFileName = "BigsbyClientCert.cer";

//        public static X509Certificate2 GetCertificate()
//        {
//            var rawData = ReadResource(typeof(CertificateProvider).Namespace + "." + certificateFileName);

//            return new X509Certificate2(rawData);
//        }

//        private static byte[] ReadResource(string resourceName)
//        {
//            var assembly = Assembly.GetExecutingAssembly();

//            using (var input = assembly.GetManifestResourceStream(resourceName))
//            using (var result = new MemoryStream())
//            {
//                if (input == null)
//                    throw new ArgumentException($"Resource {resourceName} is not found", nameof(resourceName));
//                input.CopyTo(result);

//                return result.ToArray();
//            }
//        }
//    }
//}
