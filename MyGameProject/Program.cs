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
            KeyHandler keyH = new KeyHandler();


            // Window Settings
            window.Size = new Size(Config.screenWidth, Config.screenHeight); // Window Size
            window.Text = "My 2D Game"; // Window Title
            window.StartPosition = FormStartPosition.CenterScreen; // Start Position
            window.FormBorderStyle = FormBorderStyle.FixedSingle; // Window Style 
            window.MaximizeBox = false; // Disable the Maximize Button on window
            window.KeyDown += KeyDownEvent; // Call KeyDownEvent Function When KeyDown Event
            window.KeyUp += KeyUpEvent; // Call KeyDownEvent Function When KeyUp Event

            // Resize the window to panel size
            window.AutoSize = true;
            window.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            


            // Initializing  the GamePanel
            GamePanel gamePanel = new GamePanel(keyH); // Create new GamePanel
            //gamePanel.Dock = DockStyle.Fill; // Cover window with gamePanel
            window.Controls.Add(gamePanel);
            
            
        


            TileHandler tileH = new TileHandler(gamePanel);
            Player player = new Player(gamePanel, keyH);

            gamePanel.setComponents(player, tileH);

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