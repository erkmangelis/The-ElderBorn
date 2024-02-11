namespace MyGameProject
{
    public class ObjectHandler
    {
        GamePanel gamePanel;
        public SuperObject[] objects;


        // Initialization
        public ObjectHandler(GamePanel gameP)
        {
            this.gamePanel = gameP;
            objects = new SuperObject[10];
            
            setObject();
        }


        // Create objects
        public void setObject()
        {
            objects[0] = new ObjectCoin();
            objects[0].worldX = 22 * Config.tileSize;
            objects[0].worldY = 20 * Config.tileSize;

            objects[1] = new ObjectCoin();
            objects[1].worldX = 27 * Config.tileSize;
            objects[1].worldY = 27 * Config.tileSize;

            objects[2] = new ObjectCoin();
            objects[2].worldX = 23 * Config.tileSize;
            objects[2].worldY = 25 * Config.tileSize;

            objects[3] = new ObjectPig();
            objects[3].worldX = 24 * Config.tileSize;
            objects[3].worldY = 21 * Config.tileSize;
        }
    

        // Draws objects to screen
        public void draw(Graphics graphics)
        {
            // Process only existing objects
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    int screenX = objects[i].worldX - gamePanel.player.worldX + gamePanel.player.screenX;
                    int screenY = objects[i].worldY - gamePanel.player.worldY + gamePanel.player.screenY;


                        // Render the objects that only inside of the screen
                        if (objects[i].worldX + Config.tileSize > gamePanel.player.worldX - gamePanel.player.screenX && 
                            objects[i].worldX - Config.tileSize < gamePanel.player.worldX + gamePanel.player.screenX &&
                            objects[i].worldY + Config.tileSize > gamePanel.player.worldY - gamePanel.player.screenY &&
                            objects[i].worldY - Config.tileSize < gamePanel.player.worldY + gamePanel.player.screenY)
                            {
                                graphics.DrawImage(objects[i].sprite, new Rectangle (screenX, screenY, Config.tileSize, Config.tileSize));
                            }
                }
            }
        }
    }
}