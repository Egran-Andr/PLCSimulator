using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCSimulator
{
    internal interface IPLC
    {
        bool Connect(string ipAddress, int rack = 0, int slot = 1);
        void Disconnect();
        byte[] ReadData(int dbNumber, int start, int size);
        bool WriteData(int dbNumber, int start, byte[] data);
    }
}
