using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCSimulator
{
    public class PlcSimulator : IPLC
    {
        private readonly Dictionary<int, byte[]> _dataStore = new Dictionary<int, byte[]>();
        private bool _isConnected;
        public PLCProgram Program { get; private set; }

        public bool Connect(string ipAddress, int rack = 0, int slot = 1)
        {
            _isConnected = true;
            Console.WriteLine($"Connected to {ipAddress} on Rack {rack}, Slot {slot}");
            return true;
        }

        public void Disconnect()
        {
            _isConnected = false;
            Console.WriteLine("Disconnected.");
        }

        public virtual byte[] ReadData(int dbNumber, int start, int size)
        {
            if (_dataStore.TryGetValue(dbNumber, out var dbData))
            {
                byte[] data = new byte[size];
                Array.Copy(dbData, start, data, 0, size);
                return data;
            }
            return null;
        }

        public virtual bool WriteData(int dbNumber, int start, byte[] data)
        {
            if (!_dataStore.ContainsKey(dbNumber))
            {
                _dataStore[dbNumber] = new byte[1024]; // Assume memory size
            }
            Array.Copy(data, 0, _dataStore[dbNumber], start, data.Length);
            return true;
        }

        public void LoadProgram(PLCProgram program)
        {
            Program = program;
            Console.WriteLine("Program loaded into the PLC simulator.");
        }


        public void ExecuteProgram()
        {
            if (Program != null)
            {
                Console.WriteLine("Executing the loaded PLC program...");
                Program.Execute();
            }
            else
            {
                Console.WriteLine("No program loaded to execute.");
            }
        }
    }
}
