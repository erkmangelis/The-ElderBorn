using System;
using System.Windows.Forms;

namespace MyGameProject
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // Create new form as window
            Form window = new Form();


            // Initializing  the KeyHandler
            KeyHandler? keyH = new KeyHandler();


            // These values were found through trial and error.
            // By passing these values along with the screen size to the window size, the panel and window compatibility issue is resolved.
            int borderWidth = 16;
            int borderHeight = 39;

            // Window Settings
            window.Size = new Size(Config.screenWidth + borderWidth, Config.screenHeight + borderHeight); // Window Size
            window.Text = "My 2D Game"; // Window Title
            window.StartPosition = FormStartPosition.CenterScreen; // Start Position
            window.FormBorderStyle = FormBorderStyle.FixedSingle; // Window Style 
            window.MaximizeBox = false; // Disable the Maximize Button on window
            window.KeyDown += KeyDownEvent; // Call KeyDownEvent Function When KeyDown Event
            window.KeyUp += KeyUpEvent; // Call KeyDownEvent Function When KeyUp Event

            // Resize the window to panel size
            //window.AutoSize = true;
            //window.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            

            // Initializing  the GamePanel
            GamePanel? gamePanel = new GamePanel(keyH); // Create new GamePanel
            //gamePanel.Dock = DockStyle.Fill; // Cover window with gamePanel
            window.Controls.Add(gamePanel);
            
            
            // Create new objects from classes
            TileHandler? tileH = new TileHandler(gamePanel);
            Player? player = new Player(gamePanel, keyH);
            CollisionDetector? collisionD = new CollisionDetector(gamePanel);


            // Sets created class to gamePanel
            gamePanel.setComponents(player, tileH, collisionD);


            // Thread Start-Stop
            gamePanel.StartGameThread();
            window.FormClosing += (sender, e) => gamePanel.StopGameThread();


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


            // Run the window
            Application.Run(window);
        }
    }
}