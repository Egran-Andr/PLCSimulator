using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCSimulator.VirtualPR.LogicOperators
{
    public class OrBlock : LogicBlock
    {
        public override bool Execute()
        {
            Output = Inputs.Any(input => input);
            return Output;
        }
    }
}
