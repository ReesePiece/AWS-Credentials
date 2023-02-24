using Amazon;
using Amazon.Runtime.CredentialManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS_Credentials
{
    public static class GetCredentials
    {
        //Pair of methods that write the key and the region into the profile
        public static void WriteProfile(string profileName, string keyId, string secret)
        {
            Console.WriteLine($"Create the [{profileName}] profile...");
            var options = new CredentialProfileOptions
            {
                AccessKey = keyId,
                SecretKey = secret
            };
            var profile = new CredentialProfile(profileName, options);
            var netSdkStore = new NetSDKCredentialsFile();
            netSdkStore.RegisterProfile(profile);
        }

        public static void AddRegion(string profileName, RegionEndpoint region)
        {
            var netSdkStore = new NetSDKCredentialsFile();
            CredentialProfile profile;
            if (netSdkStore.TryGetProfile(profileName, out profile))
            {
                profile.Region = region;
                netSdkStore.RegisterProfile(profile);
            }
        }
    }
}
