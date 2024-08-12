using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCSimulator.VirtualPR.LogicOperators
{
    public class NotBlock : LogicBlock
    {
        public override bool Execute()
        {
            if (Inputs.Count != 1)
            {
                throw new InvalidOperationException("NOT block can have only one input");
            }
            Output = !Inputs[0];
            return Output;
        }
    }
}
