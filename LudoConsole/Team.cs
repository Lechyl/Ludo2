using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoConsole
{
    interface Team
    {

        Board[] Revive(Board[] boards);
        Board[] Move(Board[] boards);
    }
}
