using PLCSimulator.VirtualPR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCSimulator
{
    public class PLCProgram
    {
        private Dictionary<string, LogicBlock> _blocks = new Dictionary<string, LogicBlock>();
        private Dictionary<LogicBlock, List<LogicBlock>> _outputs = new Dictionary<LogicBlock, List<LogicBlock>>();

        public PLCProgram AddBlock<T>(string name, Action<T> configureBlock) where T : LogicBlock, new()
        {
            var block = new T();
            configureBlock(block);
            _blocks[name] = block;
            return this;
        }

        public PLCProgram SetOutputs(string fromBlockName, params string[] toBlockNames)
        {
            if (!_blocks.TryGetValue(fromBlockName, out var fromBlock))
            {
                throw new ArgumentException($"Block with name {fromBlockName} does not exist.");
            }

            if (!_outputs.ContainsKey(fromBlock))
            {
                _outputs[fromBlock] = new List<LogicBlock>();
            }

            foreach (var toBlockName in toBlockNames)
            {
                if (!_blocks.TryGetValue(toBlockName, out var toBlock))
                {
                    throw new ArgumentException($"Block with name {toBlockName} does not exist.");
                }
                _outputs[fromBlock].Add(toBlock);
                fromBlock.Outputs.Add(toBlock);
            }
            return this;
        }

        public void Execute()
        {
            var queue = new Queue<LogicBlock>(_blocks.Values);

            while (queue.Count > 0)
            {
                var currentBlock = queue.Dequeue();
                ExecuteBlock(currentBlock);

                if (_outputs.ContainsKey(currentBlock))
                {
                    foreach (var outputBlock in _outputs[currentBlock])
                    {
                        if (!queue.Contains(outputBlock))
                        {
                            queue.Enqueue(outputBlock);
                        }
                    }
                }
            }
        }

        private void ExecuteBlock(LogicBlock block)
        {
            bool result = block.Execute();
            Console.WriteLine($"Block executed with result: {result}");

            foreach (var outputBlock in block.Outputs)
            {
                outputBlock.Inputs.Add(result);
            }
        }

    }
}
