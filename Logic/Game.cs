using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DealOrNo.Data;
using System.Drawing;

namespace DealOrNo.Logic
{
    class Game
    {
        private Rules rules;
        private BoxGeneration boxes;
        private int openedBoxes;
        private int stage;
        private String firstBox;
        private bool openFirstBoxEnabled;
        private bool callBanker;

        public Game(Rules rules, BoxGeneration boxes)
        {
            this.boxes = boxes;
            this.rules = rules;
            openedBoxes = 0;
            stage = 0;
            firstBox = "0";
            openFirstBoxEnabled = false;
            callBanker = false;
        }

        public State nextStep() {
            openedBoxes++;
            if (stage >= rules.getOpenSequence().Length-1)
            {
                openFirstBoxEnabled = true;
                return null;
            }
            
            

            if(rules.getOpenSequence()[stage] == openedBoxes) {
                openedBoxes = 0;
                if (stage > 0)
                {
                    callBanker = true;
                }
                stage++;
                return new State(callBanker, stage,rules.getOpenSequence()[stage]);
            }
            else
            {
                callBanker = false;
                return new State(callBanker, stage, rules.getOpenSequence()[stage] - openedBoxes);

            }

            
        }
        public void setFirstBox(String boxNum)
        {
            firstBox = boxNum;
        }

        public String getFirstBox()
        {
            return firstBox;
        }

        public int getStage()
        {
            return stage;
        }

        public bool firstBoxEnabled()
        {
            return openFirstBoxEnabled;
        }
        public bool getCallBanker()
        {
            return callBanker;
        }
        public void setStage(int stage)
        {
            this.stage = stage;
        }
    }
 }

