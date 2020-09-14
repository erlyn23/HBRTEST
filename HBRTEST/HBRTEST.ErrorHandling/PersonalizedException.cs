using System;

namespace HBRTEST.ErrorHandling
{
    public class PersonalizedException: Exception
    {
        public PersonalizedException(string errorMessage): base(errorMessage)
        {

        }
    }
}
