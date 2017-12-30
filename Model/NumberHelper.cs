using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace RunNo_MVC.Models
{

    public class Product
    {
        public string GUID { get; set; }
        public string Name { get; set; }
    }
    public static class UsedList
    {

        public static List<Product> UsedProductList = new List<Product>();


        public static void ClearAllUsedList()
        {
            UsedProductList.Clear();
        }
    }
    public static class NumberObject
    {

        public static int LimitNo = 0;
        public static int CountIndex = 0;
        public static string PickNumber(string identityUser)
        {
            Random random = new Random();

            string newnumber = "";

            if (UsedList.UsedProductList.Exists(n => n.GUID == identityUser))
            {
                newnumber = UsedList.UsedProductList.Find(n => n.GUID == identityUser).Name;
            }
            else
            {
                newnumber = NextCountNumber();
                UsedList.UsedProductList.Add(new Product() { GUID = identityUser, Name = newnumber });
            }

            return newnumber;
        }

        public static void SetLimitNo(string newLimitNo)
        {
            UsedList.ClearAllUsedList();
            CountIndex = 0;
            Int32.TryParse(newLimitNo, out LimitNo);
        }


        private static string NextCountNumber()
        {
            CountIndex++;

            if (CountIndex > LimitNo)
                return "号已取完！请等待下一局！";

            return CountIndex.ToString();
        }

        /// <summary>
        /// method to generate a MD5 hash of a string
        /// </summary>
        /// <param name="strToHash">string to hash</param>
        /// <returns>hashed string</returns>
        public static string GenerateMD5(string str)
        {
            //  MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //new 

            var timeflag = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

            MD5 md5 = MD5.Create();

            str += timeflag;

            byte[] byteArray = Encoding.ASCII.GetBytes(str);
            byteArray = md5.ComputeHash(byteArray);
            string hashedValue = "";
            foreach (byte b in byteArray)
            {
                hashedValue += b.ToString("x2");
            }
            return hashedValue;
        }


    }
}