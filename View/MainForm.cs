using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DealOrNo.Data;
using DealOrNo.Logic;

namespace DealOrNo
{
    public partial class MainForm : Form
    {
        Label[] prizes;
        Label[] boxes;
        BoxGeneration generatedBoxes;
        Rules stdRules;
        Game myGame;
        bool gameEnded;
        public MainForm()
        {
            InitializeComponent();
            
            this.FormBorderStyle = FormBorderStyle.FixedSingle;     //Set fixed window size
            
            
            
            init();
            

        }


        private void init()
        {

            

            stdRules = new Rules();
            generatedBoxes = new BoxGeneration(stdRules);
            myGame = new Game(stdRules, generatedBoxes);
            //Init prizes
            prizes = new Label[stdRules.getBoxCount()];
            //Init boxes
            boxes = new Label[stdRules.getBoxCount()];
            addLabels();
            addBoxes();
            lblStatus.Text = "Изберете вашата кутия.";
            gameEnded = false;
        }

        private void addLabels()
        {
            int counter = 0,
                scounter = 0;
            foreach (var item in stdRules.getPrizes())
            {

                prizes[counter] = new Label();
                prizes[counter].Name = "prize" + counter;
                prizes[counter].Text = item;
                prizes[counter].BorderStyle = BorderStyle.Fixed3D;
                prizes[counter].Size = new Size(80, 20);
                prizes[counter].BackColor = Color.Green;
                prizes[counter].ForeColor = Color.White;
                prizes[counter].TextAlign = ContentAlignment.MiddleRight;

                if (counter < stdRules.getBoxCount() / 2)
                {
                    prizes[counter].Location = new Point(15, 30 * counter + 80);
                }
                else
                {
                    prizes[counter].Location = new Point(this.Size.Width - 110, 30 * scounter + 80);
                    scounter++;
                }

                this.Controls.Add(this.prizes[counter]);
                counter++;
            }
        }

        private void addBoxes()
        {
            int counter;
            int lblWidth = 80;
            int lblHeight = 70;
            int marginLeft = 120;
            int marginTop = 60;
            int marginRight = 200;
            int rowCounter = 0;
            int columnCounter = 0;
            
            for (counter = 0; counter < stdRules.getBoxCount(); counter++)
            {
                boxes[counter] = new Label();
                boxes[counter].Name = "box" + counter;
                boxes[counter].Text = (counter + 1).ToString();
                boxes[counter].Font = new Font("Arial Black", 11);
                //boxes[counter].BorderStyle = BorderStyle.Fixed3D;
                boxes[counter].Size = new Size(lblWidth, lblHeight);
                boxes[counter].TextAlign = ContentAlignment.MiddleCenter;
                try
                {
                    boxes[counter].Image = Image.FromFile("briefcase.png");
                    boxes[counter].ImageAlign = ContentAlignment.MiddleCenter;
                }catch(Exception e) {
                    MessageBox.Show("Проблем със зареждането на ресурс " + e.Message, "Грешка!", MessageBoxButtons.OK);
                    return;
                }

                if (counter == 0 || counter == 23)
                {
                    marginLeft += lblWidth;
                }

                if (counter == 3)
                {
                    marginLeft -= lblWidth;
                }

                if (counter/3 == 1 && rowCounter == 0)
                {
                    rowCounter++;
                    columnCounter = 0;
                    
                }
                if (marginLeft + columnCounter * lblWidth > (this.Size.Width - marginRight))
                {
                    columnCounter = 0;
                    rowCounter++;
                }
                
                boxes[counter].Location = new Point(marginLeft + columnCounter*lblWidth, lblHeight * rowCounter + marginTop);

                this.Controls.Add(this.boxes[counter]);

                columnCounter++;
                boxes[counter].Click += new System.EventHandler(this.boxClicked);


            }
        }


        private void boxClicked(object sender, EventArgs e)
        {
            Label lbl = sender as Label;

            if (gameEnded == true)
            {
                return;
            }
            if (lbl.Font.Strikeout == true) //if the box is already opened skip
            {
                return;
            }

            if (myGame.getFirstBox() == "0") //Set the number of the first box
            {
                myGame.setFirstBox(lbl.Text);
                lblStatus.Text = "Отворете " + stdRules.getOpenSequence()[1] + " кутии.";
                myGame.nextStep();

            }

            if (lbl.Text != myGame.getFirstBox())   //If not the first box go to the next step
            {
                try
                {
                    lblStatus.Text = "Отворете " + myGame.nextStep().boxesToOpen + " кутии.";

                }
                catch (Exception ex)
                {

                }
                
            }


            int activeBoxes = 0;
            String tempBoxNum = lbl.Text;
            foreach (var item in boxes)
            {
                if (item.Font.Strikeout == false)
                {
                    activeBoxes++;
                }
            }
            
            if (myGame.firstBoxEnabled() && myGame.getFirstBox() == lbl.Text)   //if this is the last stage the first box can be opened
            {
               
                gameEnded = true;
            }
            

            if (myGame.getFirstBox() != lbl.Text)   //If this is the first box do not show it
            {
                //Deactivate box on click

                int counter;
                for (counter = 0; counter < stdRules.getBoxCount(); counter++)
                {
                    if (generatedBoxes.getBoxes()[Convert.ToInt32(lbl.Text) - 1] == prizes[counter].Text)
                    {
                        prizes[counter].BackColor = Color.Red;
                        prizes[counter].ForeColor = Color.Black;

                    }
                }
                lbl.Text = generatedBoxes.getBoxes()[Convert.ToInt32(lbl.Text) - 1];
                lbl.Font = new Font("Arial", 11, FontStyle.Strikeout);
                try
                {
                    lbl.Image = Image.FromFile("briefcaseBaW.png");
                    lbl.ImageAlign = ContentAlignment.MiddleCenter;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Проблем със зареждането на ресурс " + ex.Message, "Грешка!", MessageBoxButtons.OK);
                    return;
                }

            }
            else
            {
                try
                {
                    lbl.Image = Image.FromFile("briefcaseFB.png");
                    lbl.ImageAlign = ContentAlignment.MiddleCenter;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Проблем със зареждането на ресурс " + ex.Message, "Грешка!", MessageBoxButtons.OK);
                    return;
                }
            }
            

            if (activeBoxes == 2)
            {
                try
                {
                    lblStatus.Text = "Спечелихте " + generatedBoxes.getBoxes()[Convert.ToInt32(tempBoxNum) - 1] + " лева!";

                }
                catch (Exception ex) { }
                gameEnded = true;
                return;
            }

            if (myGame.getCallBanker() == true)
            {
                Offer bankerOffer = new Offer(prizes);

                if (MessageBox.Show("Офертата на банката е " + bankerOffer.offer + " лева! Има ли сделка?", "Сделка или не?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    gameEnded = true;
                    lblStatus.Text = "Спечелихте " + bankerOffer.offer + " лева!";
                    foreach (Label item in boxes)
                    {
                        item.Font = new Font("Arial", 11, FontStyle.Strikeout);
                        try
                        {
                            item.Text = generatedBoxes.getBoxes()[Convert.ToInt32(item.Text) - 1];
                        }
                        catch (Exception ex) { }

                    }


                }
                return;
            }
        }

        

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            removeControls();
            init();
        }

        private void removeControls()
        {
            foreach (var item in prizes)
            {
                this.Controls.Remove(item);
            }
            foreach (var item in boxes)
            {
                this.Controls.Remove(item);
            }
        }

    }
}
