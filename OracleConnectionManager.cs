using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMenagement
{
    public class OracleConnectionManager
    {
        public OracleConnectionManager() { 
            Oracle.ManagedDataAccess.Client.OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection("Data Source=xe;User ID=system;Unicode=True");
        }
    }
}
