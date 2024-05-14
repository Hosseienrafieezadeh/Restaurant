using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Contracts.Interface
{
    public interface UnitOfWork
    {
        Task Begin();
        Task Commit();
        Task Complete();
        Task RollBack();
    }
}
