using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.Simulator
{
    public enum LineState
    {
        /// <summary>
        /// Error condition, too many attempts to drive this input.
        /// </summary>
        TooManyDrivers = -1,

        /// <summary>
        /// Set if this input is currently floating.
        /// </summary>
        Floating = 0,

        /// <summary>
        /// Set if the input is currently high.
        /// </summary>
        High = 1,

        /// <summary>
        /// Set if the input is currently low.
        /// </summary>
        Low = 2
    }

    public static class LineStateExt
    {
        public static bool IsError(this LineState state)
        {
            return (int)state < 0;
        }
    }
}
