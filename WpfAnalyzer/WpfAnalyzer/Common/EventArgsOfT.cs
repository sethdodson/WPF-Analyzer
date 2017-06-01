using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAnalyzer.Common
{
    public class EventArgs<T> : EventArgs
    {
        public T Data { get; }

        public EventArgs(T data)
        {
            Data = data;
        }
    }
}
