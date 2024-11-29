using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Repositories.Enums
{
    public enum ResponseResultType
    {     
        Success,
        NotFound,
        AlreadyExists,
        BlankInput,
        InvalidFormat,
        InvalidValue
        
    }
}
