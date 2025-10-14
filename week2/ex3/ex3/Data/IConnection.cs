using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex3.Data
{
    public interface IConnection
    {
        IDbConnection CreateConnection();
    }
}
