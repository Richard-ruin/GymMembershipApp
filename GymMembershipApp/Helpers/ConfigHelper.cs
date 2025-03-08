using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace GymMembershipApp.Helpers
{
    public static class ConfigHelper
    {
        private static readonly string ConnectionString = "Data Source=DESKTOP-2I89EEK;Initial Catalog=GymMemberDB;Integrated Security=True";

        public static string GetConnectionString(string name)
        {
            // For simplicity in a MAUI app, we're hardcoding the connection string
            // In a production app, you might want to use a secure storage solution
            return ConnectionString;
        }
    }
}