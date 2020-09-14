using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace HBRTEST.DAL
{
    class sqlCommand
    {
        private static sqlCommand commandInstance;
        private static SqlCommand command = new SqlCommand();

        private sqlCommand()
        {

        }
        
        public static sqlCommand InstanceCommand()
        {
            if (commandInstance == null)
            {
                commandInstance = new sqlCommand();
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
