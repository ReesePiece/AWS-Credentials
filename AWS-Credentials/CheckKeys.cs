using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS_Credentials
{
    public static class CheckKeys
    {
        public static bool CheckAccessKey(string key)
        {
            //Takes in the string entered by the user and 
            //checks the first five characters and the last 
            //character to make sure it is a token.

            //Null check/Zero length check to get the party started. 
            if (string.IsNullOrEmpty(key)) return false;

            //The first four characters are a prefix id that identifies
            //what it is. There is no known list of all of them, but there
            //are several known and a regular pattern to them. The random
            //characters are A-Z and 2-7 (no 0,1,8,9 digits to confuse 
            //with O and L). 
            var possibles = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            bool prefixIsGood = false;
            bool fifthIsGood = false;
            bool finalIsGood = false;
            bool nothingWeird = false;

            //Make sure all the characters in the Access Key are in the possibles string
            for (var i = 0; i < key.Length; i++)
            {
                if (!possibles.Contains(key[i]))
                {
                    return false;
                }
            }
            nothingWeird = true;

            //Grab the first four char of the string and check it for a 
            //known ID.
            var prefix = key.Substring(0, 4);
            
            if (prefix == "ASIA" || prefix == "AKIA" || prefix == "AROA" || prefix == "AIDA")
            {
                prefixIsGood = true;
            }
            if (prefix[0] == 'A' && prefix[3] == 'A')
            {
                prefixIsGood = true;
            }

            //Check that fifth character to see if it is 'I' or 'J'
            if (key[4] == 'I' || key[4] == 'J')
            {
                fifthIsGood= true;
            }

            //Check the final character in the key string for 'A' or 'Q'
            if (key[key.Length - 1] == 'A' || key[key.Length - 1] == 'Q')
            {
                finalIsGood= true;
            }

            if (prefixIsGood && fifthIsGood && finalIsGood && nothingWeird)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckSecretKey(string secretKey)
        {
            //Secret Keys are roughly 40 characters of any combination of
            //Uppercase Letters, Lowercase Letters, Digits, and '+' and '/'
            //It's Base64 encoded, but appear completely random when decoded

            //Null/Empty check
            if (string.IsNullOrEmpty(secretKey)) return false;

            //Just check to see if the string is one blob of information of 
            //the required character set.
            var secretPossibles = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            
            foreach (var letter in secretKey)
            {
                if (!secretPossibles.Contains(letter))
                {
                    return false;  
                }
            }
            return true;
        }

    }
}
