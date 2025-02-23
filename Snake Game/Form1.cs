using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        private List<Rectangle> snake;
        private Rectangle food;
        private int direction;
        private int score;
        private Random random;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            snake = new List<Rectangle>();
            snake.Add(new Rectangle(70, 70, 10, 10)); 
            direction = 0; 
            score = 0;
            random = new Random();
            GenerateFood();
            timer1.Start();
        }

        private void GenerateFood()
        {
            int x = random.Next(0, this.ClientSize.Width / 10) * 10;
            int y = random.Next(0, this.ClientSize.Height / 10) * 10;
            food = new Rectangle(x, y, 10, 10);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (direction != 2) direction = 0;
                    break;
                case Keys.Down:
                    if (direction != 3) direction = 1;
                    break;
                case Keys.Left:
                    if (direction != 0) direction = 2;
                    break;
                case Keys.Up:
                    if (direction != 1) direction = 3;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            CheckCollision();
            Invalidate();
        }
        private void MoveSnake()
        {
            Rectangle head = snake[0];
            switch (direction)
            {
                case 0: head.X += 10; break;
                case 1: head.Y += 10; break;
                case 2: head.X -= 10; break;
                case 3: head.Y -= 10; break;
            }

            snake.Insert(0, head);
            if (head.IntersectsWith(food))
            {
                score += 10;
                GenerateFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void CheckCollision()
        {
            Rectangle head = snake[0];

            if (head.X < 0 || head.X >= this.ClientSize.Width || head.Y < 0 || head.Y >= this.ClientSize.Height)
            {
                GameOver();
            }

            for (int i = 1; i < snake.Count; i++)
            {
                if (head.IntersectsWith(snake[i]))
                {
                    GameOver();
                }
            }
        }

        private void GameOver()
        {
            timer1.Stop();
            string message = "Miss mo?";
            string title = "Sagott!!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("pabili po coke mismo");
            }
            else
            {
                MessageBox.Show("Ayyy");
            }
            //InitializeGame();
            Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Random random = new Random();
            Color randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            base.OnPaint(e);
            Graphics g = e.Graphics;
            foreach (Rectangle segment in snake)
            {
                g.FillRectangle(Brushes.Black, segment);
            }
            g.FillRectangle(new SolidBrush(randomColor), food);
            g.DrawString($"Score: {score}", this.Font, Brushes.Black, new PointF(10, 10));
        }
    }
}
