using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCSimulator.VirtualPR.LogicOperators
{
    public class AndBlock : LogicBlock
    {
        public override bool Execute()
        {
            Output = Inputs.All(input => input);
            return Output;
        }
    }
}
