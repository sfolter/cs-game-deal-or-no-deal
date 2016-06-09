using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealOrNo.Data;


namespace DealOrNo.Logic
{
    class BoxGeneration
    {
        
        private String[] shuffledBoxes;
        

        public BoxGeneration(Rules rules) {
            shuffledBoxes = new String[rules.getBoxCount()];
            
            shuffledBoxes = generateBoxes(rules.getPrizes());
        }

        private String[] generateBoxes(List<String> prizes)
        {
            List<String> shuffledPrizes = new List<String>(prizes);
            shuffledPrizes.Shuffle();

            return shuffledPrizes.ToArray();
        }

        public String[] getBoxes()
        {
            return shuffledBoxes;
        }
        

    }
}
