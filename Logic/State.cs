using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNo.Logic
{
    class State
    {
        public bool callBanker;
        public int stage;
        public int boxesToOpen;

        public State(bool callBanker, int stage, int boxesToOpen)
        {
            this.callBanker = callBanker;
            this.stage = stage;
            this.boxesToOpen = boxesToOpen;
        }
    }
}
