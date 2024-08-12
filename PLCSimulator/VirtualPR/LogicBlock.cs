using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCSimulator.VirtualPR
{
    public abstract class LogicBlock
    {
        public List<bool> Inputs { get; set; } = new List<bool>();
        public bool Output { get; protected set; }
        public List<LogicBlock> Outputs { get; set; } = new List<LogicBlock>();
        public abstract bool Execute();

        public void AddOutput(LogicBlock block)
        {
            Outputs.Add(block);
        }
    }
}
