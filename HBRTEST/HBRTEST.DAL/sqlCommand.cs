using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace HBRTEST.DAL
{
    class sqlCommand
    {
        #region Definiciones
        private static SqlCommand command = null;
        #endregion

        #region Constructor
        private sqlCommand()
        {

        }
        #endregion

        #region Métodos
        #region Métodos Públicos
        public static SqlCommand InstanceCommand()
        {
            if (command == null)
            {
                command = new SqlCommand();
                return command;
            }
            return command;
        }
        #endregion
        #endregion
    }
}
