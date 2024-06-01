namespace ElderBorn
{
    public class UI
    {
        GamePanel? gamePanel;
        Dictionary<string, Bitmap> components;
        Font arial12 = new Font("Arial", 12);
        Font arial40 = new Font("Arial", 40);
        public bool notificationOn = false;
        public string notification = "";
        int notificationTimer = 0;


        public UI(GamePanel gameP)
        {
            this.gamePanel = gameP;

            getUIComponents();
        }


        // Gets UI components from assets
        void getUIComponents()
        {
            try
            {
                components = new Dictionary<string, Bitmap>
                {
                    {"coin", new Bitmap("./assets/UIcomponents/coinSymbol.png")},
                    {"UI", new Bitmap("./assets/UIcomponents/UI.png")}
                };
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        // Sets notification
        public void showNotification(string text)
        {
            notification = text;
            notificationOn = true;
        }

        int textCenter(string text, Font font, Graphics graphics)
        {
            SizeF textSize = graphics.MeasureString(text, font);
            return (int)textSize.Width/2;
        }


        // Draw UI
        public void draw(Graphics graphics)
        {
            switch(gamePanel.keyH.gameState)
            {
                case 1:
                    drawPlayScreen(graphics);
                    break;

                case 2:
                    drawPauseScreen(graphics);
                    break;
            }
            
        }


        // Draw Pause Screen
        void drawPauseScreen(Graphics graphics)
        {
            string text = "Game Paused";
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, 0, 0, 0)), new Rectangle(0, 0, Config.screenWidth, Config.screenHeight));
            graphics.DrawString(text, arial40, Brushes.White, new PointF(Config.screenWidth/2-textCenter(text, arial40, graphics), Config.screenHeight/2));
        }

        
        // Draw Play Screen
        void drawPlayScreen(Graphics graphics)
        {
            // Draw UI Frame
            graphics.DrawImage(components["UI"], new Rectangle(0, 0, Config.screenWidth, Config.screenHeight));

            // Draw HP Bar and Mana Bar
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 237, 28, 28)), new Rectangle(448, 707, (int)(128*(gamePanel.player.hp/gamePanel.player.maxHp)), 9));
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 99, 151, 230)), new Rectangle(448, 724, 128, 9));

            // Draw Stats Window
            graphics.DrawString("Username1", arial12, Brushes.White, new PointF(30,45));
            graphics.DrawString("_________", arial12, Brushes.White, new PointF(30,47));
            graphics.DrawString("Vitality: " + gamePanel.player.vit, arial12, Brushes.White, new PointF(30,70));
            graphics.DrawString("Intelligence: " + gamePanel.player.intlg, arial12, Brushes.White, new PointF(30,90));
            graphics.DrawString("Strength: " + gamePanel.player.str, arial12, Brushes.White, new PointF(30,110));
            graphics.DrawString("Dexterity: " + gamePanel.player.dex, arial12, Brushes.White, new PointF(30,130));

            // Draw Stage Info
            graphics.DrawString("Stage X", arial12, Brushes.White, new PointF(480, 45));

            // Draw FPS
            graphics.DrawString(gamePanel.FPSCounter, arial12, Brushes.White, new PointF(963,40));

            // Draw Played Time
            graphics.DrawString("Played Time", arial12, Brushes.White, new PointF(34,205));
            graphics.DrawString(($"{gamePanel.playedTime:0.0}"), arial12, Brushes.White, new PointF(63,225));

            // Draw coin amount that player has
            graphics.DrawImage(components["coin"], new Rectangle(670, 0, Config.tileSize, Config.tileSize));
            graphics.DrawString("x" + Convert.ToString(gamePanel.player.coin), arial12, Brushes.White, new PointF(720, 25));

            // Draw Notification
            if (notificationOn)
            {
                graphics.DrawString(notification, arial12, Brushes.White, new PointF(860, 700));
                notificationTimer++;

                if (notificationTimer >= 90)
                {
                    notificationOn = false;
                    notificationTimer = 0;
                }
            }
        }
    }
}