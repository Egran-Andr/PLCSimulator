using PLCSimulator.VirtualPR.LogicOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var simulator = new PlcSimulator();

            simulator.Connect("192.168.0.1");

            var plcProgram = new PLCProgram()
                .AddBlock<AndBlock>("And1", block =>
                {
                    block.Inputs.Add(true);
                    block.Inputs.Add(true);
                    block.Inputs.Add(false);
                })
                .AddBlock<NotBlock>("Not1", block =>
                {
                })
                .SetOutputs("And1", "Not1"); // AND блок рассылает сигнал на OR и NOT блоки

            simulator.LoadProgram(plcProgram);

            simulator.ExecuteProgram();

            simulator.Disconnect();

        }
    }
}
