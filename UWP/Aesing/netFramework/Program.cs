using OneView.Utility.Security;
using System;
using System.Linq;

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

            var decrypted = ClientEncryption.DecryptIdentity(StringToByteArray("3266B2AA6097FD72E8D098253DFA34D44D36B996E6E63BBE5FB5D19C42759C5B361202AA482C74693442A25168194D54878A7C923C146B640152D14DE5C08E4F661E1AD7D12B32644FFE4AA41BABE0A5"));
            //var decrypted = ClientEncryption.DecryptIdentity(StringToByteArray("6AEB5D337B939B57318F7B9D7EB58D780510DEBF3F4DDF4F1C4700765292846827AA82F1EA8184FFD4C4DA2E6A897B2240A2E4C6274322D272B399DBD71CF112"));
            var stop = "here";
        }

        static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
