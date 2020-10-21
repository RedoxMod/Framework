using System.Collections.Generic;
using System.Threading.Tasks;
using Redox.API.Data;

namespace Redox.API.Configuration
{
    public interface IConfiguration
    {
        void Load();
    }
}