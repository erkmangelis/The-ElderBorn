namespace ElderBorn
{
    public class Player : Entity
    {
        GamePanel? gamePanel;
        KeyHandler? keyH;

        public int screenX;
        public int screenY;


        // Items
        public int coin = 0;


        // Initialization
        public Player(GamePanel gameP, KeyHandler keyH)
        {
            this.gamePanel = gameP;
            this.keyH = keyH;

            screenX = Config.screenWidth / 2 - (Config.tileSize / 2);
            screenY = Config.screenHeight / 2 - (Config.tileSize / 2);

            // Player collision box size values
            cbX = 15;
            cbY = 24;
            cbDefaultX = cbX;
            cbDefaultY = cbY;
            cbWidth = 14;
            cbHeight = 24;
            collisionBox = new Rectangle(cbX, cbY, cbWidth, cbHeight);

            setDefaultValues();
            getPlayerSprite();
        }


        // Setting default player values
        public void setDefaultValues()
        {
            worldX = Config.tileSize*25;
            worldY = Config.tileSize*25;
            speed = 4;
            lookingDirection = "down";
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


        int mouseX;
        int mouseY;
        int newSpeed;

        // Update player
        public void update()
        {
            // Default collision value
            collisionOn = false;

            // Check any tile has collide with the player
            gamePanel.collisionD.checkTile(this);

            // Check any object has collide with the player
            int objectIndex = gamePanel.collisionD.checkObject(this, true);
            interactWithObject(objectIndex);


            newSpeed = speed;
            // Speed calibration for the diagonal movement
            if (keyH.DiagonalMovement())
            {
                newSpeed = (int)Math.Round(speed * 0.707);
            }
            else
            {
                newSpeed = speed;
            }

            if (movingDirection != lookingDirection)
            {
                newSpeed = (int)Math.Round(newSpeed * (0.75));
            }
            else
            {
                newSpeed = speed;
            }


            
            // Offsetting the mouse position to center of the screen instead top left of the screen
            mouseY = Control.MousePosition.Y - gamePanel.windowOffset.Y - Config.screenHeight/2;
            mouseX = Control.MousePosition.X - gamePanel.windowOffset.X - Config.screenWidth/2;

            // Set player's vertical looking direction
            if (mouseY < 0)
            {
                if (mouseX < (0 - mouseY) && mouseX > (0 + mouseY))
                {
                    lookingDirection = "up";
                }
            }
            else if (mouseY > 0)
            {
                if (mouseX < (0 + mouseY) && mouseX > (0 - mouseY))
                {
                    lookingDirection = "down";
                }
            }

            // Set player's horizontal looking direction
            if (mouseX < 0)
            {
                if (mouseY < (0 - mouseX) && mouseY > (0 + mouseX))
                {
                    lookingDirection = "left";
                }
            }
            else if (mouseX > 0)
            {
                if (mouseY < (0 + mouseX) && mouseY > (0 - mouseX))
                {
                    lookingDirection = "right";
                }
            }


            if (keyH.upPressed)
            {
                movingDirection = "up";
            }
            else if (keyH.downPressed)
            {
                movingDirection = "down";
            }

            // Set player's horizontal direction
            if (keyH.leftPressed)
            {
                movingDirection = "left";
            }
            else if (keyH.rightPressed)
            {
                movingDirection = "right";
            }


            // Move the player
            if (keyH.isMoving())
            {
                // Only increase the spriteCounter while the player is moving
                spriteCounter++;

                // If player not collide any block, can move
                if (!collisionOn)
                {
                    // Vertical Movement
                    if (keyH.upPressed)
                    {
                        worldY -= newSpeed;
                    }
                    else if (keyH.downPressed)
                    {
                        worldY += newSpeed;
                    }

                    // Horizontal Movement
                    if (keyH.leftPressed)
                    {
                        worldX -= newSpeed;
                    }
                    else if (keyH.rightPressed)
                    {
                        worldX += newSpeed;
                    }
                }
            }


            // Iterate the sprite -> 60 / 12 times per second
            if (spriteCounter > 12)
            {
                spriteNumber = (spriteNumber == 1) ? 2 : 1;
                spriteCounter = 0;
            }
        }


        // Player's interaction with the object
        public void interactWithObject(int i)
        {
            if (i != int.MaxValue)
            {
                string objectName = gamePanel.objectH.objects[i].name;

                switch (objectName)
                {
                    case "Coin":
                        coin++;
                        gamePanel.playSE("coin", false);
                        gamePanel.objectH.objects[i] = null;
                        gamePanel.userI.showNotification("+1 Coin");
                        break;

                    case "Pig":
                        if (coin == 3)
                        {
                            coin = 0;
                            gamePanel.objectH.objects[i] = null;
                            gamePanel.userI.showNotification("Coins gathered \n successfully");
                        }
                        else
                        {
                            gamePanel.userI.showNotification("Must have 3x Coin");
                        }
                        break;
                }
            }
        }

        
        // Draw player
        public void draw(Graphics graphics)
        {
            Bitmap? sprite = down1;


            // Change sprite according to direction
            switch (lookingDirection)
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
            
            // Mouse Position and Direction Logs
            string mousePosLog = " Mouse Position: [X: " + Convert.ToString(mouseX) + ", Y: " + Convert.ToString(mouseY) + "]";
            string lookingDirLog = "Looking Direction: " + lookingDirection;
            string movingDirLog = "Moving Direction: " + movingDirection;

            if (keyH.logOn)
            {
                gamePanel.logConsole.logMessage($"-------[ Log Time: {DateTime.Now} ]-------");
                gamePanel.logConsole.logMessage(mousePosLog);
                gamePanel.logConsole.logMessage(lookingDirLog);
                gamePanel.logConsole.logMessage(movingDirLog);
                gamePanel.logConsole.logMessage("Player Speed: " + Convert.ToString(newSpeed));
                gamePanel.logConsole.logMessage($"Coin x{coin}");
            }
            
        }
    }
}