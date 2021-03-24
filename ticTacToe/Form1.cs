using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ticTacToe
{
    public partial class mainForm : Form
    {
        //public int [,] buttonValues;   // 1 for an X, 0 for an 0 and -1 for empty
        private string [,] textButtonValues;
        public mainForm()
        {
            InitializeComponent();

            textButtonValues = new string[3, 3] { { " ", " ", " " },
                                                { " ", " ", " " },
                                                { " ", " ", " " }};
        }

        private void button_Click(object sender, EventArgs e)
        {

            // Get the sender as a Button.
            Button myButton = sender as Button;
            

            switch (myButton.Name)
            {
                case "firstLeftButton":
                    textButtonValues[0, 0] = "X";
                    break;
                case "firstMiddleButton":
                    textButtonValues[0, 1] = "X";
                    break;
                case "firstRightButton":
                    textButtonValues[0, 2] = "X";
                    break;
                case "secondLeftButton":
                    textButtonValues[1, 0] = "X";
                    break;
                case "secondMiddleButton":
                    textButtonValues[1, 1] = "X";
                    break;
                case "secondRightButton":
                    textButtonValues[1, 2] = "X";
                    break;
                case "thirdLeftButton":
                    textButtonValues[2, 0] = "X";
                    break;
                case "thirdMiddleButton":
                    textButtonValues[2, 1] = "X";
                    break;
                case "thirdRightButton":
                    textButtonValues[2, 2] = "X";
                    break;
            }

            updateAllButtons();
            //disableAllButtons();
        }

        private void updateAllButtons()
        {
            firstLeftButton.Text = textButtonValues[0,0]; 
            firstMiddleButton.Text = textButtonValues[0,1];
            firstRightButton.Text = textButtonValues[0,2];
            secondLeftButton.Text = textButtonValues[1,0];
            secondMiddleButton.Text = textButtonValues[1,1];
            secondRightButton.Text = textButtonValues[1,2];
            thirdLeftButton.Text = textButtonValues[2,0];
            thirdMiddleButton.Text = textButtonValues[2,1];
            thirdRightButton.Text = textButtonValues[2,2];
            
        }

        private void disableAllButtons()
        {
            firstLeftButton.Enabled = false;
            firstMiddleButton.Enabled = false;
            firstRightButton.Enabled = false;
            secondLeftButton.Enabled = false;
            secondMiddleButton.Enabled = false;
            secondRightButton.Enabled = false;
            thirdLeftButton.Enabled = false;
            thirdMiddleButton.Enabled = false;
            thirdRightButton.Enabled = false;

        }

        private void enableAllButtons()
        {
            firstLeftButton.Enabled = true;
            firstMiddleButton.Enabled = true;
            firstRightButton.Enabled = true;
            secondLeftButton.Enabled = true;
            secondMiddleButton.Enabled = true;
            secondRightButton.Enabled = true;
            thirdLeftButton.Enabled = true;
            thirdMiddleButton.Enabled = true;
            thirdRightButton.Enabled = true;


        }

        private void aiButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Pressed");
            takeAIturn();
            enableAllButtons();
            
        }


        private void takeAIturn()
        {
            Double optimalScore = Double.NegativeInfinity;
            int optimalMoveX = 0;
            int optimalMoveY = 0;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (textButtonValues[x, y] == " ")
                    {
                        textButtonValues[x, y] = "O";
                        double score = checkHeuristic(textButtonValues, 0, false);
                        textButtonValues[x, y] = " ";
                        if (score >= optimalScore)
                        {
                            optimalScore = score;
                            optimalMoveX = x;
                            optimalMoveY = y;

                        }
                    }
                }

            }
            textButtonValues[optimalMoveX, optimalMoveY] = "O";
            checkWin();
            updateAllButtons();

        }

        private Double checkHeuristic(string[,] gameBoard, int depth, bool isMax)
        {
            string win = checkWin();
            if(win != null)
            {
                if(win == "X")
                {
                    return 10;
                }
                else if (win == "Y")
                {
                    return -10;
                }
                else
                {
                    return 0;
                }
            }
            Double optimalScore;
            if (isMax)
            {
                optimalScore = Double.NegativeInfinity;
                ;
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (gameBoard[x, y] == " ")
                        {
                            gameBoard[x, y] = "X";
                            //textButtonValues[x, y] = "X";
                            double score = checkHeuristic(gameBoard, depth + 1, false);
                            gameBoard[x, y] = " ";
                            //textButtonValues[x, y] = " ";
                            optimalScore = Math.Max(score, optimalScore);
                        }
                    }
                }
                return optimalScore;
            }

            else
            {
                optimalScore = Double.PositiveInfinity;
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (gameBoard[x, y] == " ")
                        {
                            gameBoard[x, y] = "X";
                            //textButtonValues[x, y] = "O";
                            Double score = checkHeuristic(gameBoard, depth + 1, true);
                            gameBoard[x, y] = " ";
                            //textButtonValues[x, y] = " ";
                            optimalScore = Math.Min(score, optimalScore);


                        }
                    }
                }
                return optimalScore;
            }
        }
        private string checkWin()
        {
            string winner = null;
            //check left to right
            for (int i = 0; i < 3; i++)
            {
                if (textButtonValues[i, 0] == textButtonValues[i, 1] && textButtonValues[i, 0] == textButtonValues[i, 2] && textButtonValues[i, 0] != " ")
                {
                    winner = textButtonValues[i, 0];
                }
            }

            //check up to down
            for (int i = 0; i < 3; i++)
            {
                if (textButtonValues[0, i] == textButtonValues[1, i] && textButtonValues[0, i] == textButtonValues[2, i] && textButtonValues[0, i] != " ")
                {
                    winner = textButtonValues[0, i];
                }
            }

            //check diagonal
            if (textButtonValues[0, 0] == textButtonValues[1, 1] && textButtonValues[0, 0] == textButtonValues[2, 2] && textButtonValues[0, 0] != " ")
            {
                winner = textButtonValues[0, 0];
            }
            if (textButtonValues[2, 0] == textButtonValues[1, 1] && textButtonValues[2, 0] == textButtonValues[0, 2] && textButtonValues[2, 0] != " ")
            {
                winner = textButtonValues[2, 0];
            }

            Debug.WriteLine("Winner: " + winner);
            return winner;
        }
    }
}
