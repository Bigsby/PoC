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

            //var decrypted = ClientEncryption.DecryptIdentity(StringToByteArray("65281BECE4BECA24F8AA04D64ADB2D31B6EEE4CEB5AD981C78E0E714479A20994073CEA396B8C055AD8C8AA6D39FB4D21A0E48FF148A974BD9289449979AB72C0E75497A539D134B1CCBE6BE9DA703B4"));
            var decrypted = ClientEncryption.DecryptIdentity(StringToByteArray("6AEB5D337B939B57318F7B9D7EB58D780510DEBF3F4DDF4F1C4700765292846827AA82F1EA8184FFD4C4DA2E6A897B2240A2E4C6274322D272B399DBD71CF112"));
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
