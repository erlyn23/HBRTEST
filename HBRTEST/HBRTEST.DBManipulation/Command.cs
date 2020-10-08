using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace HBRTEST.DBUtilities
{
    public class Command
    {
        private static Command commandInstance;
        private static SqlCommand command = new SqlCommand();

        private Command()
        {

        }

        public static Command InstanceCommand()
        {
            if (commandInstance == null)
            {
                commandInstance = new Command();
                return commandInstance;
            }
            return commandInstance;
        }

        public SqlCommand GetSqlCommand()
        {
            return command;
        }
    }
}
