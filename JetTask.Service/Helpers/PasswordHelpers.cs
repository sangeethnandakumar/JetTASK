using ExpressEncription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetTask.Service.Helpers
{
    public static class PasswordHelpers
    {
        public static string EncryptPassword(string password)
        {
            return BlowFishHashing.HashString(password, BlowFishHashing.GenerateSalt(13));
        }
    }
}