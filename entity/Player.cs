namespace MyGameProject
{
    public class Player : Entity
    {
        GamePanel gamePanel;
        KeyHandler keyH;

        public int screenX;
        public int screenY;


        // Initialization
        public Player(GamePanel gameP, KeyHandler keyH)
        {
            this.gamePanel = gameP;
            this.keyH = keyH;

            screenX = Config.screenWidth / 2 - (Config.tileSize / 2);
            screenY = Config.screenHeight / 2 - (Config.tileSize / 2);

            setDefaultValues();
            getPlayerSprite();
        }


        // Setting default player values
        public void setDefaultValues()
        {
            worldX = Config.tileSize*25;
            worldY = Config.tileSize*25;
            baseSpeed = 4;
            direction = "down";
        }


        // Gets player sprite from assets folder
        public void getPlayerSprite()
        {
            try
            {
                up1 = new Bitmap("./assets/player/up1.png");
                up2 = new Bitmap("./assets/player/up2.png");
                down1 = new Bitmap("./assets/player/down1.png");
                down2 = new Bitmap("./assets/player/down2.png");
                left1 = new Bitmap("./assets/player/left1.png");
                left2 = new Bitmap("./assets/player/left2.png");
                right1 = new Bitmap("./assets/player/right1.png");
                right2 = new Bitmap("./assets/player/right2.png");
            }

            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        // Update player
        public void update()
        {
            // Only increase the spriteCounter while the player is moving
            if (keyH.upPressed || keyH.downPressed || keyH.leftPressed || keyH.rightPressed)
            {
                spriteCounter++;
            }


            int speed;
            // Speed calibration for the diagonal movement
            if (keyH.DiagonalMovement())
            {
                speed = (int)Math.Round(baseSpeed * 0.707);
            }
            else
            {
                speed = baseSpeed;
            }


            // Vertical Movement
            if (keyH.upPressed)
            {
                direction = "up";
                worldY -= speed;
            }
            else if (keyH.downPressed)
            {
                direction = "down";
                worldY += speed;
            }


            // Horizontal Movement
            if (keyH.leftPressed)
            {
                direction = "left";
                worldX -= speed;
            }
            else if (keyH.rightPressed)
            {
                direction = "right";
                worldX += speed;
            }


            // Iterate the sprite -> 60 / 12 times per second
            if (spriteCounter > 12)
            {
                spriteNumber = (spriteNumber == 1) ? 2 : 1;
                spriteCounter = 0;
            }
        }

        
        
        // Draw player
        public void draw(Graphics graphics)
        {
            Bitmap? sprite = down1;


            // Change sprite according to direction
            switch (direction)
            {
                case "up":
                    sprite = (spriteNumber == 1) ? up1 : up2;
                    break;

                case "down":
                    sprite = (spriteNumber == 1) ? down1 : down2;
                    break;

                case "left":
                    sprite = (spriteNumber == 1) ? left1 : left2;
                    break;

                case "right":
                    sprite = (spriteNumber == 1) ? right1 : right2;
                    break;
            }

            
            // Draw player sprite
            graphics.DrawImage(sprite, new Rectangle (screenX, screenY, Config.tileSize, Config.tileSize));
        }
    }
}