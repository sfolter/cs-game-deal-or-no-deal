using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNo.Data
{
    class Rules
    {
        private int boxCount;
        private int[] openSequence;
        private List<String> prizes;

        public Rules()
        {
            //Standart rules
            
            openSequence = new int[] {1, 7, 6, 4, 3, 2, 1};
            prizes = new List<string>()
            {
                "1",
                "5",
                "10",
                "15",
                "25",
                "50",
                "75",
                "100",
                "200",
                "300",
                "400",
                "500",
                "750",

                "1000",
                "5000",
                "10000",
                "25000",
                "50000",
                "75000",
                "100000",
                "200000",
                "300000",
                "400000",
                "500000",
                "750000",
                "1000000",
            };
            int counter = 0;
            foreach (var item in prizes)
            {
                counter++;
            }
            boxCount = counter;
        }

        public int getBoxCount() {
            return this.boxCount;
        }

        public int[] getOpenSequence()
        {
            return openSequence;
        }
        public List<String> getPrizes()
        {
            return prizes;
        }
    }
}
