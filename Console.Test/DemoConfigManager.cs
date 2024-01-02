using Common.Infrastructure.Data.Interfaces;
using Common.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.Test
{
    public class DemoConfigManager : IConfigManager
    {
        IRequestStatusTemplate[] IConfigManager.GetRequestStatusTemplate(string[] activeTasks)
        {
            throw new NotImplementedException();
        }
    }
}
