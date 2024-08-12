using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCSimulator.VirtualPR.LogicOperators
{
    public class XorBlock : LogicBlock
    {
        public override bool Execute()
        {
            if (Inputs.Count < 2)
            {
                throw new InvalidOperationException("XOR block requires at least two inputs.");
            }
            bool result = Inputs[0];
            for (int i = 1; i < Inputs.Count; i++)
            {
                result ^= Inputs[i];
            }
            return result;
        }
    }
}
