using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public interface ISubscribersOrderRegistered
    {
        void OnOrderRegistered(object source, EventArgs e);

    }
}
