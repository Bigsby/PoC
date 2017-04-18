using OneView.Utility.Security;
using System;

namespace netFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            var ci = new ClientIdentity
            {
                HostName = "host",
                MACAddress = "mac",
                MachineID = "id"
            };

            var encrypted = ClientEncryption.EncryptIdentity(ci);

            var encryptedString = BitConverter.ToString(encrypted).Replace("-", string.Empty);
            var stop = "here";
        }
    }
}
