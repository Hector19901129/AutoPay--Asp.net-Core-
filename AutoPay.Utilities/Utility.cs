using System;
using System.Collections.Generic;

namespace AutoPay.Utilities
{
    public class Utility
    {
        public static DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public static string GetEncryptionKeyName(string userId)
        {
            return $"enc_key_{userId}";
        }

        public static string GetFormattedAddress(string address, string city, string state, string zip, string country)
        {
            var formattedAddress = $"{address ?? ""},{city ?? ""},{state ?? ""},{zip ?? ""},{country ?? ""}";

            while (formattedAddress.StartsWith(","))
            {
                formattedAddress = formattedAddress.Remove(0, 1);
            }
            while (formattedAddress.EndsWith(","))
            {
                formattedAddress = formattedAddress.Remove(formattedAddress.Length - 1, 1);
            }

            var charIndexesToBeRemoved = new List<int>();

            for (var i = 0; i < formattedAddress.Length; i++)
            {
                if (formattedAddress[i] == ',' && formattedAddress[i + 1] == ',')
                {
                    charIndexesToBeRemoved.Add(i);
                }
            }

            foreach (var i in charIndexesToBeRemoved)
            {
                formattedAddress = formattedAddress.Remove(i, 1);
            }

            return formattedAddress.Replace(",", ", ");
        }

    }
}
