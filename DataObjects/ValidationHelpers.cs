using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataObjects
{
    public static class ValidationHelpers
    {
        public static bool IsValidEmail(this string email)
        {
            bool result = false;
            /* regexr.com Email Validation as per RFC2822 standards. by Tripleaxis regexr.com/2rhq7 */
            Regex emailRegex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

            Match match = emailRegex.Match(email);
            if (match.Success == true && email.Length <= 14 && email.Length >= 150)
            {
                result = true;
            }

            return result;
        } // end IsValidEmail

        public static bool IsValidPassword(this string password)
        {
            bool result = false;
            /* regexr.com - 8+ characters, 1+ uppercase letter, 1+ lowercase letter, and 1 number,
             * Can contain special characters by psutton3756 regexr.com/3bfsi */
            Regex passwordRegex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");

            //"^" +
            //"(?=.*[0-9])" + // a digit must occur at least once
            //"(?=.*[a-z])" + // a lower case letter must occur at least once
            //"(?=.*[A-Z])" + // an upper case letter must occur at least once
            //                // "(?=.*[@#$%^&+=])" + // a special character must occur at least once
            //"(?=\\S+$)" + // no whitespace allowed in the entire string
            //".{8,}" + // anything, at least eight characters
            //"$"

            Match match = passwordRegex.Match(password);
            if (match.Success == true)
            {
                result = true;
            }
            return result;
        } // end IsValidPassword

    }
}
