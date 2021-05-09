using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project_Krestiki_noliki_Mambetniyazov
{
    struct Point
    {
        public int x;
        public int y;
        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public static bool operator ==(Point lhs, Point rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }
        public static bool operator !=(Point lhs, Point rhs)
        {
            return lhs.x != rhs.x && lhs.y != rhs.y;
        }
    }

    public partial class Form1 : Form
    {
        int [,]board;
        int AI_stone;
        Point false_turn; 
        string game_result;
        int human_stone;
        int pc_score;
        int hm_score;
        public Form1()
        {
            InitializeComponent();
            board = new int[3, 3];
            AI_stone = 2;
            human_stone = 1;
            hm_score = 0;
            pc_score = 0;
            false_turn = new Point(-1, -1);
            game_result = "";
        }

        public void applyChanges()
        {
            if (board[0, 0] == 0)
            {
                button1.Text = "";
            }
            if (board[0, 1] == 0)
            {
                button2.Text = "";
            }
            if (board[0, 2] == 0)
            {
                button3.Text = "";
            }
            if (board[1, 0] == 0)
            {
                button4.Text = "";
            }
            if (board[1, 1] == 0)
            {
                button5.Text = "";
            }
            if (board[1, 2] == 0)
            {
                button6.Text = "";
            }
            if (board[2, 0] == 0)
            {
                button7.Text = "";
            }
            if (board[2, 1] == 0)
            {
                button8.Text = "";
            }
            if (board[2, 2] == 0)
            {
                button9.Text = "";
            }
            //
            if (board[0, 0] == 1)
            {
                button1.Text = "X";
            }
            if (board[0, 1] == 1)
            {
                button2.Text = "X";
            }
            if (board[0, 2] == 1)
            {
                button3.Text = "X";
            }
            if (board[1, 0] == 1)
            {
                button4.Text = "X";
            }
            if (board[1, 1] == 1)
            {
                button5.Text = "X";
            }
            if (board[1, 2] == 1)
            {
                button6.Text = "X";
            }
            if (board[2, 0] == 1)
            {
                button7.Text = "X";
            }
            if (board[2, 1] == 1)
            {
                button8.Text = "X";
            }
            if (board[2, 2] == 1)
            {
                button9.Text = "X";
            }
            //
            if (board[0, 0] == 2)
            {
                button1.Text = "O";
            }
            if (board[0, 1] == 2)
            {
                button2.Text = "O";
            }
            if (board[0, 2] == 2)
            {
                button3.Text = "O";
            }
            if (board[1, 0] == 2)
            {
                button4.Text = "O";
            }
            if (board[1, 1] == 2)
            {
                button5.Text = "O";
            }
            if (board[1, 2] == 2)
            {
                button6.Text = "O";
            }
            if (board[2, 0] == 2)
            {
                button7.Text = "O";
            }
            if (board[2, 1] == 2)
            {
                button8.Text = "O";
            }
            if (board[2, 2] == 2)
            {
                button9.Text = "O";
            }
        }

        public void setDefault()
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    board[i, j] = 0;
                }
            }
            applyChanges();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            setDefault();
        }
        Point checkVerticals(int stone)
        {
            for(int j = 0; j < 3; j += 1)
            {
                int empty = 0, already = 0;
                for(int i = 0; i < 3; i += 1)
                {
                   if (board[i, j] == stone)
                    {
                        already += 1;
                    } else 
                    if (board[i, j] == 0)
                    {
                        empty += 1;
                    }
                }
                if (empty == 1 && already == 2)
                {
                    for(int i = 0; i < 3; i += 1)
                    {
                        if (board[i, j] == 0)
                        {
                            return new Point(i, j);
                        }
                    }
                }
            }
            return false_turn;
        }
        Point checkHorizontals(int stone)
        {
            for(int i = 0; i < 3; i += 1)
            {
                int empty = 0, already = 0;
                for(int j = 0; j < 3; j += 1)
                {
                    if (board[i, j] == stone)
                    {
                        already += 1;
                    } else 
                    if (board[i, j] == 0)
                    {
                        empty += 1;
                    }
                }
                if (already == 2 && empty == 1)
                {
                    for(int j = 0; j < 3; j += 1)
                    {
                        if (board[i, j] == 0)
                        {
                            return new Point(i, j);
                        }
                    }
                }
            }
            return false_turn;
        }
        Point checkDiagonals(int stone)
        {
            int empty = 0, already = 0;
            for(int i = 0; i < 3; i++)
            {
                if (board[i, i] == stone)
                {
                    already += 1;
                } else 
                if (board[i, i] == 0)
                {
                    empty += 1;
                }
            }
            if (empty == 1 && already == 2)
            {
                for(int i = 0; i < 3; i++)
                {
                    if (board[i, i] == 0)
                    {
                        return new Point(i, i);
                    }
                }
            }
            empty = 0;
            already = 0;
            for(int i = 0; i < 3; i++)
            {
                if (board[i, 2 - i] == stone)
                {
                    already += 1;
                } else
                {
                    empty += 1;
                }
            }
            if (already == 2 && empty == 1)
            {
                for(int i = 0; i < 3; i++)
                {
                    if (board[i, 2 - i] == 0)
                    {
                        return new Point(i, 2 - i);
                    }
                }
            }
            return false_turn;
        }

        public bool can_win(int stone)
        {
            if (checkVerticals(stone) != false_turn)
            {
                return true;
            }
            if (checkHorizontals(stone) != false_turn)
            {
                return true;
            }
            if (checkDiagonals(stone) != false_turn)
            {
                return true;
            }
            return false;
        }
        void make_winning_turn()
        {
            if (checkHorizontals(AI_stone) != false_turn)
            {
                Point turn = checkHorizontals(AI_stone);
                board[turn.x, turn.y] = 2;
                return;
            }
            if (checkVerticals(AI_stone) != false_turn)
            {
                Point turn = checkVerticals(AI_stone);
                board[turn.x, turn.y] = 2;
                return;
            }
            if (checkDiagonals(AI_stone) != false_turn)
            {
                Point turn = checkDiagonals(AI_stone);
                board[turn.x, turn.y] = 2;
                return;
            }
        }
        void make_blocking_turn()
        {
            if (checkHorizontals(human_stone) != false_turn)
            {
                Point turn = checkHorizontals(human_stone);
                board[turn.x, turn.y] = 2;
                return;
            }
            if (checkVerticals(human_stone) != false_turn)
            {
                Point turn = checkVerticals(human_stone);
                board[turn.x, turn.y] = 2;
                return;
            }
            if (checkDiagonals(human_stone) != false_turn)
            {
                Point turn = checkDiagonals(human_stone);
                board[turn.x, turn.y] = 2;
                return;
            }
        }
        void make_middle_turn()
        {
            Point[] potential = { new Point(1, 1), new Point(0, 0), new Point(0, 2), new Point(2, 0), new Point(2, 2)};
            Point[] mid = { new Point(0, 1), new Point(1, 0), new Point(1, 2), new Point(2, 1)};
            if (board[0, 1] == human_stone && board[1, 0] == human_stone)
            {
                if (board[0, 0] == 0)
                {
                    board[0, 0] = AI_stone;
                    return;
                }
            }
            if (board[0, 1] == human_stone && board[1, 2] == human_stone)
            {
                if (board[0, 2] == 0)
                {
                    board[0, 2] = AI_stone;
                    return;
                }
            }
            if (board[1, 0] == human_stone && board[2, 1] == human_stone)
            {
                if (board[2, 0] == 0)
                {
                    board[2, 0] = AI_stone;
                    return;
                }
            }
            if (board[1, 2] == human_stone && board[2, 1] == human_stone)
            {
                if (board[2, 2] == 0)
                {
                    board[2, 2] = AI_stone;
                    return;
                }
            }

            if (board[1, 1] == 2)
            {
                foreach(Point pt in mid)
                {
                    if (board[pt.x, pt.y] == 0)
                    {
                        board[pt.x, pt.y] = AI_stone;
                        return;
                    }
                }
            }
            foreach(Point pt in potential)
            {
                if (board[pt.x, pt.y] == 0)
                {
                    board[pt.x, pt.y] = AI_stone;
                    return;
                }
            }
            for(int i = 0; i < 3; i += 1)
            {
                for(int j = 0; j < 3; j += 1)
                {
                    if (board[i, j] == 0)
                    {
                        board[i, j] = AI_stone;
                        return;
                    }
                }
            }
        }
        public void AITurn()
        {
            if (can_win(AI_stone))
            {
                make_winning_turn();
            } else 
            if (can_win(human_stone))
            {
                make_blocking_turn();
            } else
            {
                make_middle_turn();
            }
        }
        bool game_over()
        {
            int already = 0;
            for (int j = 0; j < 3; j += 1)
            {
                already = 0;
                for (int i = 0; i < 3; i += 1)
                {
                    if (board[i, j] == AI_stone)
                    {
                        already += 1;
                    }
                }
                if (already == 3)
                {
                    game_result = "You lose!";
                    pc_score += 1;
                    return true;
                }
            }
            for (int i = 0; i < 3; i += 1)
            {
                already = 0;
                for (int j = 0; j < 3; j += 1)
                {
                    if (board[i, j] == AI_stone)
                    {
                        already += 1;
                    }
                }
                if (already == 3)
                {
                    game_result = "You lose!";
                    pc_score += 1;
                    return true;
                }
            }
            already = 0;
            for (int i = 0; i < 3; i++)
            {
                if (board[i, i] == AI_stone)
                {
                    already += 1;
                }
            }
            if (already == 3)
            {
                game_result = "You lose!";
                pc_score += 1;
                return true;
            }
            already = 0;
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 2 - i] == AI_stone)
                {
                    already += 1;
                }
            }
            if (already == 3)
            {
                game_result = "You lose!";
                pc_score += 1;
                return true;
            }
            already = 0;
            for (int j = 0; j < 3; j += 1)
            {
                already = 0;
                for (int i = 0; i < 3; i += 1)
                {
                    if (board[i, j] == human_stone)
                    {
                        already += 1;
                    }
                }
                if (already == 3)
                {
                    game_result = "You win!";
                    hm_score += 1;
                    return true;
                }
            }
            for (int i = 0; i < 3; i += 1)
            {
                already = 0;
                for (int j = 0; j < 3; j += 1)
                {
                    if (board[i, j] == human_stone)
                    {
                        already += 1;
                    }
                }
                if (already == 3)
                {
                    game_result = "You win!";
                    hm_score += 1;
                    return true;
                }
            }
            already = 0;
            for (int i = 0; i < 3; i++)
            {
                if (board[i, i] == human_stone)
                {
                    already += 1;
                }
            }
            if (already == 3)
            {
                game_result = "You win!";
                hm_score += 1;
                return true;
            }
            already = 0;
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 2 - i] == human_stone)
                {
                    already += 1;
                }
            }
            if (already == 3)
            {
                game_result = "You win!";
                hm_score += 1;
                return true;
            }
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                        return false;
                }
            }
            game_result = "Draw!";
            return true;
        }
        void update_game_result()
        {
            AI_score.Text = pc_score.ToString();
            human_score.Text = hm_score.ToString();
        }
        public void humanTurn(int x, int y)
        {
            if (board[x, y] != 0)
            {
                return;
            }
            board[x, y] = 1;
            applyChanges();
            if (game_over())
            {
                MessageBox.Show("Game over! " + game_result);
                update_game_result();
                setDefault();
                return;
            }
            AITurn();
            applyChanges();
            if (game_over())
            {
                MessageBox.Show("Game over! " + game_result);
                update_game_result();
                setDefault();
                return;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            humanTurn(0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            humanTurn(0, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            humanTurn(0, 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            humanTurn(1, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            humanTurn(1, 1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            humanTurn(1, 2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            humanTurn(2, 0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            humanTurn(2, 1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            humanTurn(2, 2);
        }
    }
}
