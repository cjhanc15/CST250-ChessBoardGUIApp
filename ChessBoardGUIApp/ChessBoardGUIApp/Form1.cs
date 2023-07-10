using ChessBoardConsoleApp;

namespace ChessBoardGUIApp
{
    public partial class Form1 : Form
    {
        static public Board myBoard = new Board(8);
        public Button[,] btnGrid = new Button[myBoard.Size, myBoard.Size];
        public Form1()
        {
            InitializeComponent();
            populateGrid();
        }

        public void populateGrid()
        {
            //fill panel with buttons
            int buttonSize = panel1.Width / myBoard.Size;
            panel1.Height = panel1.Width;

            //create buttons and place in panel
            for(int i = 0; i < myBoard.Size; i++)
            {
                for(int j = 0; j < myBoard.Size; j++)
                {
                    btnGrid[i, j] = new Button();

                    //make each button square
                    btnGrid[i, j].Width = buttonSize;
                    btnGrid[i, j].Height = buttonSize;

                    btnGrid[i, j].Click += Button_Grid_Click; //add click event to each button
                    panel1.Controls.Add(btnGrid[i, j]);
                    btnGrid[i,j].Location = new Point(buttonSize * i, buttonSize * j);

                    //for testing
                    btnGrid[i,j].Text = i.ToString() + "|" + j.ToString();
                    //tag holds row & column
                    btnGrid[i, j].Tag = i.ToString() + "|" + j.ToString();
                }
            }
        }

        public void Button_Grid_Click(object? sender, EventArgs e)
        {
            //get row & col num of button clicked
            string[] stringArr = (sender as Button).Tag.ToString().Split('|');
            int i = int.Parse(stringArr[0]);
            int j = int.Parse(stringArr[1]);

            //label all legal moves for click
            Cell currentCell = myBoard.theGrid[i, j];
            string piece = comboBox1.Text;
            myBoard.MarkNextLegalMoves(currentCell, piece);
            updateButtonLabels();

            //reset background color of all buttons to default
            for (int k = 0; k < myBoard.Size; k++)
            {
                for (int l = 0; l < myBoard.Size; l++)
                {
                    btnGrid[k, l].BackColor = default(Color);
                }
            }

            //set background color of clicked
            (sender as Button).BackColor = Color.Cornsilk;
        }

        public void updateButtonLabels()
        {
            for(int i = 0; i < myBoard.Size; i ++)
            {
                for(int j = 0; j < myBoard.Size; j++)
                {
                    btnGrid[i, j].Text = "";
                    if (myBoard.theGrid[i, j].CurrentlyOccupied) btnGrid[i, j].Text = comboBox1.Text;
                    if (myBoard.theGrid[i, j].LegalNextMove) btnGrid[i, j].Text = "Legal";
                }
            }
        }
    }
}