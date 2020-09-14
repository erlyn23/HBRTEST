using System;
using System.Collections.Generic;
using System.Text;

namespace HBRTEST.ErrorHandlings
{
    public class ErrorsControls: Exception
    {
        public ErrorsControls(string errorMessage): base (errorMessage)
        {

        }
    }
}
