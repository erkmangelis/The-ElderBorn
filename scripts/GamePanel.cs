using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace ElderBorn
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
        KeyHandler? keyH = new KeyHandler();
        Thread? gameThread;
        public Player? player;
        public TileHandler? tileH;
        public CollisionDetector? collisionD;
        public ObjectHandler? objectH;
        public SoundBox? soundBox;
        public MusicBox? musicBox;
        

        // Temp
        string FPSCounter = "";
        string log = "";
        public Point windowOffset;


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
        public void setComponents(Player p, TileHandler th, CollisionDetector cd, ObjectHandler oh, Point woff, SoundBox sb, MusicBox mb)
        {
            player = p;
            tileH = th;
            collisionD = cd;
            objectH = oh;
            windowOffset = woff;
            soundBox = sb;
            musicBox = mb;
        }


        // Play sound effect
        public void playSE(string sound, bool loop)
        {
            if (!loop)
            {
                soundBox.play(sound);
            }
            else
            {
                soundBox.loop(sound);
            }
            
        }

        // Stop sound effect
        public void stopSE()
        {
            soundBox.stop();
        }


        // Play music
        public void playMusic(string sound, bool loop)
        {
            if (loop)
            {
                musicBox.Loop(sound);
            }
            else
            {
                musicBox.Play(sound);
            }
        }

        // Stop music
        public void stopMusic(string sound)
        {
            musicBox.Stop(sound);
        }


        // Prepare the components that must be ready before the game start
        public void setupGame()
        {

        }


        // Start the game thread
        public void StartGameThread()
        {
            if (!isRunning)
            {
                isRunning = true;
                gameThread = new Thread(Run);
                gameThread.Start();
                playMusic("theme", true); // TEMP
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
                    FPSCounter = "FPS: " + Convert.ToString(drawCount);

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
            // Tile
            tileH.draw(graphics);

            // Object
            objectH.draw(graphics);

            // Player
            player.draw(graphics);

            Font myFont = new Font("Arial", 12);
            graphics.DrawString(FPSCounter, myFont, Brushes.White, new PointF(5,5));
            graphics.DrawString(log, myFont, Brushes.White, new PointF(700,5));
        }
        
    }
}
