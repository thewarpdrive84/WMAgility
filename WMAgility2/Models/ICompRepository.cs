using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMAgility2.Models
{
    public interface ICompRepository
    {
        IEnumerable<Competition> AllComps { get; }
        Competition GetCompById(int Id);

        //Unit Testing
        Competition CreateComp(Competition comp);

        Competition UpdateComp(Competition comp);

        Competition DeleteComp(Competition comp);
    }
}
