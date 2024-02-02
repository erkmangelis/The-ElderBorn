using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace MyGameProject
{
    // Define the IRunnable interface
    public interface IRunnable
    {
        void Run();
    }


    public class GamePanel : Panel, IRunnable
    {
        // Thread Status
        private volatile bool isRunning = false;

        // FPS
        int FPS;

        // Declaring Classes
        KeyHandler keyH = new KeyHandler();
        Thread? gameThread;
        public Player? player;
        TileHandler? tileH;
        

        // Temp
        string text = "";


        // Initialization
        public GamePanel(KeyHandler keyHandler)
        {
            this.Size = new Size(Config.screenWidth, Config.screenHeight);
            this.FPS = Config.FPS;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;
            this.Focus();
            this.Paint += Render;
            this.keyH = keyHandler;
        }


        // Setting Classes
        public void setComponents(Player p, TileHandler th)
        {
            player = p;
            tileH = th;
        }


        // Start the game thread
        public void StartGameThread()
        {
            if (!isRunning)
            {
                isRunning = true;
                gameThread = new Thread(Run);
                gameThread.Start();
            }
        }
        

        // Stop the game thread
        public void StopGameThread()
        {
            isRunning = false;
            if (gameThread != null && gameThread.IsAlive)
            {
                gameThread.Join();
            }
        }


        // Main game loop function
        public void Run()
        {

            // Creating Stopwatch
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Calculate the draw interval in miliseconds
            long drawInterval = (long)(1000000.0 / FPS); // 16666.66 times per milisecond

            // Setting the values of  "timer" and "drawCount"
            long timer = 0;
            int drawCount = 0;


            // Game loop
            while (isRunning)
            {
                
                // Update the components
                update();

                // Draw the screen with updated information
                Invalidate();
                

                // Calculate the remaining time
                long remainingTime = drawInterval - stopwatch.ElapsedTicks;

                // Increase the drawCount every iteration
                drawCount++;
                
                // Add passed time to the timer every iteration before the sleep
                // If there's remaining time, sleep the thread
                if (remainingTime > 0)
                {
                    timer += stopwatch.ElapsedTicks + remainingTime;
                    Thread.Sleep(TimeSpan.FromTicks(remainingTime));    
                }
                else if (remainingTime < 0)
                {
                    timer += stopwatch.ElapsedTicks;
                }
                
                // If 1 second passed
                if (timer >= 1000000)
                {
                    // Print out the FPS
                    text = "FPS: " + Convert.ToString(drawCount);

                    // Reset the timer and drawCount
                    drawCount = 0;
                    timer = 0;
                }


                // Restart the stopwatch for the next iteration
                stopwatch.Restart();
            }
        }


        // Updates components
        public void update()
        {
            player.update();
        }


        // Renders components
        public void Render(object? sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;


            // Components that must be rendered
            tileH.draw(graphics);
            player.draw(graphics);

            Font myFont = new Font("Arial", 16);
            graphics.DrawString(text, myFont, Brushes.White, new PointF(10,10));
        }
        
    }
}
