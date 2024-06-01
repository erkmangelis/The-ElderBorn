namespace ElderBorn
{

    public class NPC : Entity
    {

        public NPC(GamePanel gamePanel) : base(gamePanel)
        {
            movingDirection = "down";
            lookingDirection = "down";
            speed = 2;
            worldX = Config.tileSize*22;
            worldY = Config.tileSize*24;
            

            getNPCSprite();
        }


        // Gets NPC sprite from assets folder
        public void getNPCSprite()
        {
            try
            {
                down1 = new Bitmap("./assets/npc/down1.png");
                down2 = new Bitmap("./assets/npc/down2.png");
            }

            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        // Update NPC
        public void update()
        {
            spriteCounter++;
            
            // Iterate the sprite -> 60 / 12 times per second
            if (spriteCounter > 12)
            {
                spriteNumber = (spriteNumber == 1) ? 2 : 1;
                spriteCounter = 0;
            }
        }


        // Draw NPC
        public void draw(Graphics graphics)
        {
            Bitmap? sprite = down1;
            int screenX = worldX - gamePanel.player.worldX + gamePanel.player.screenX;
            int screenY = worldY - gamePanel.player.worldY + gamePanel.player.screenY;


            // Render the objects that only inside of the screen
            if (worldX + Config.tileSize > gamePanel.player.worldX - gamePanel.player.screenX && 
                worldX - Config.tileSize < gamePanel.player.worldX + gamePanel.player.screenX &&
                worldY + Config.tileSize > gamePanel.player.worldY - gamePanel.player.screenY &&
                worldY - Config.tileSize < gamePanel.player.worldY + gamePanel.player.screenY)
            {
                sprite = (spriteNumber == 1) ? down1 : down2;
                
                graphics.DrawImage(sprite, new Rectangle (screenX, screenY, Config.tileSize*2, Config.tileSize*2));
            }   
        }
    }
}