using System;
using System.Windows.Forms;

namespace ElderBorn
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // Create new form as gameWindow
            Form gameWindow = new Form();


            // Initializing  the KeyHandler
            KeyHandler? keyH = new KeyHandler();


            // These values were found through trial and error.
            // By passing these values along with the screen size to the window size, the panel and window compatibility issue is resolved.
            int borderWidth = 16;
            int borderHeight = 39;


            // Window Settings
            gameWindow.Size = new Size(Config.screenWidth + borderWidth, Config.screenHeight + borderHeight); // Window Size
            gameWindow.Text = "The ElderBorn"; // Window Title

            // Temp
            string windowLog = "";

            gameWindow.StartPosition = FormStartPosition.CenterScreen; // Start Position
            gameWindow.FormBorderStyle = FormBorderStyle.FixedSingle; // Window Style 
            gameWindow.MaximizeBox = false; // Disable the Maximize Button on window
            gameWindow.KeyDown += KeyDownEvent; // Call KeyDownEvent Function When KeyDown Event
            gameWindow.KeyUp += KeyUpEvent; // Call KeyDownEvent Function When KeyUp Event


            // Resize the window to panel size
            //gameWindow.AutoSize = true;
            //gameWindow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            

            // Initializing  the GamePanel
            GamePanel? gamePanel = new GamePanel(keyH); // Create new GamePanel
            //gamePanel.Dock = DockStyle.Fill; // Cover window with gamePanel
            gameWindow.Controls.Add(gamePanel);
            
            
            // Create new objects from classes
            TileHandler? tileH = new TileHandler(gamePanel);
            Player? player = new Player(gamePanel, keyH);
            CollisionDetector? collisionD = new CollisionDetector(gamePanel);
            ObjectHandler? objectH = new ObjectHandler(gamePanel);


            Point windowOffset = gameWindow.PointToScreen(gamePanel.Location);


            // Sets created objects to gamePanel
            gamePanel.setComponents(player, tileH, collisionD, objectH, windowOffset);


            // Thread Start-Stop
            gamePanel.StartGameThread();
            gameWindow.FormClosing += (sender, e) => gamePanel.StopGameThread();


            // Listen Key Down Events and Send Info to KeyHandler Class
            void KeyDownEvent(object? sender, KeyEventArgs e)
            {
                keyH.HandleKeyPress(e.KeyCode, true);
            }


            // Listen Key Up Event and Send Info to KeyHandler Class
            void KeyUpEvent(object? sender, KeyEventArgs e)
            {
                keyH.HandleKeyRelease(e.KeyCode, false);
            }


            // Run the gameWindow
            Application.Run(gameWindow);
        }
    }
}