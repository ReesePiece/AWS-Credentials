using Amazon;
using Amazon.Runtime.CredentialManagement;
using Microsoft.Win32.SafeHandles;
using System.Linq;

namespace AWS_Credentials
{
    public class Program
    {
        static void Main(string[] args)
        {
            //No one wants to accidentally ship AWS credentials and keys to GitHub
            //This set of methods gets the credentials from the home computer at runtime
            //avoiding the need to place anything in the code itself.

            //REQUIRED:  Obtain an AWS key and secret key. Place them in the .NET
            //SDK Store on your computer which stores them encrypted in a standard
            //place for Visual Studio Community and Visual Studio Code to find and use.
            //The following program will place them in the SDK Store which is located at:
            // %USERPROFILE%\AppData\Local\AWSToolkit\RegisteredAccounts.json

            //1. Use NuGet to add the following packages to the project:
            //      AWSSDK.Core
            //
            //2. Add "using Amazon;" and "using Amazon.Runtime.CredentialManagement;"
            //   to your libraries at the top of the code.
            //
            //3. The program will ask for the key values on the command line and push 
            //   them into the SDK Store. That way they don't end up in the source code.


            //Ask user for AWS Access Key for push into SDK Store
            var accessKeyGood = false;
            var keyId = "";
            do
            {
                Console.WriteLine("Enter your AWS Access Key value and hit Return/Enter:  ");
                keyId = Console.ReadLine().Trim();
                //Check the AWS Access Key string validity
                accessKeyGood = CheckKeys.CheckAccessKey(keyId);
                Console.WriteLine();
                if (accessKeyGood) 
                { 
                    Console.WriteLine("AWS Access Key appears to be good."); 
                }
                else
                {
                    Console.WriteLine("The AWS Access Key is not in a valid format.");
                    Console.WriteLine($"The AWS Access Key entered is: '{keyId}'");
                }
            } while (!accessKeyGood);


            //Ask user for AWS Secret Key for push into SDK Store
            var secretKey = "";
            var secretKeyGood = false;
            do
            {
                Console.WriteLine("Enter your AWS Secret Key value and hit Return/Enter:  ");
                secretKey = Console.ReadLine().Trim();
                //Check the AWS Secret Key string validity
                secretKeyGood = CheckKeys.CheckSecretKey(secretKey);
                Console.WriteLine();
                if (secretKeyGood)
                {
                    Console.WriteLine("AWS Secret Key appears to be good.");
                }
                else
                {
                    Console.WriteLine("The AWS Secret Key is not in a valid format.");
                    Console.WriteLine($"The AWS Secret Key entered is: '{secretKey}'");
                }
            } while (!secretKeyGood);

            //Ask user for profile name
            Console.WriteLine("Enter the profile name for these keys and hit Return/Enter:  ");
            var profileName = Console.ReadLine().Trim();
            Console.WriteLine($"Profile name is: '{profileName}'\n");

            //If the keys are good, write them to the store
            if ((accessKeyGood) && (secretKeyGood))
            {
                Console.WriteLine("Writing Keys into the SDK Store");
                GetCredentials.WriteProfile(profileName, keyId, secretKey);
                GetCredentials.AddRegion(profileName, RegionEndpoint.USEast1);
            }

            //This can also be done manually, but the keys are stored in plain text on
            //your computer, which is not optimal. (The keys listed here are no longer
            //valid keys)
            // In C:\Users\YourAccount\.aws, place them  with the following names:
            // "credentials" with the following content:
            //
            //[default]
            //aws_access_key_id=AWS-ACCESS-KEY-VALUE
            //aws_secret_access_key=AWS-SECRET-KEY-VALUE
            //
            // and "config" with the following content using US-East-Northern-Virginia
            // as the region:
            //
            //[default]
            //region = us-east-1 

        }
    }
}