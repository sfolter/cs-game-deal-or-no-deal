using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace DealOrNo.Logic
{
    class Offer
    {
        private Label[] prizes;
        public int offer;
        int activePrizes;
        public Offer(Label[] prizes)
        {
            this.prizes = prizes;
            activePrizes = 0;
            offer = 0;
            foreach (var item in prizes)
            {
                if (item.BackColor == Color.Red)
                {
                    activePrizes++;
                    offer += Convert.ToInt32(item.Text);
                }
            }
            if (activePrizes < 13)
            {
                activePrizes *= 4;
            }
            activePrizes *= 2;
            offer /= activePrizes;
        }

    }
}
