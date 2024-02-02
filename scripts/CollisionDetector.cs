namespace MyGameProject
{

    public class CollisionDetector
    {
        GamePanel gamePanel;

        public CollisionDetector(GamePanel gamePanel)
        {
            this.gamePanel = gamePanel;
        }


        // Check for the tiles that has collision
        public void checkTile(Entity entity)
        {
            // Set Player's CollisionBox's sides
            int entityLeftWorldX = entity.worldX + entity.cbX;                      // Left side
            int entityRightWorldX = entity.worldX + entity.cbX + entity.cbWidth;    // Right side
            int entityTopWorldY = entity.worldY + entity.cbY;                       // Top side
            int entityBottomWorldY = entity.worldY + entity.cbY + entity.cbHeight;  // Bottom side

            int entityLeftCol = entityLeftWorldX / Config.tileSize;
            int entityRightCol = entityRightWorldX / Config.tileSize;
            int entityTopRow = entityTopWorldY / Config.tileSize;
            int entityBottomRow = entityBottomWorldY / Config.tileSize;            

            int tile1, tile2;


            // Direction that entity going
            switch (entity.direction)
            {
                case "up":
                    entityTopRow = (entityTopWorldY - entity.speed) / Config.tileSize;       // The row on above the player
                    tile1 = gamePanel.tileH.mapTileNumber[entityLeftCol, entityTopRow];      // Top left block on above the player
                    tile2 = gamePanel.tileH.mapTileNumber[entityRightCol, entityTopRow];     // Top right block on above the player

                    // Check any block has collision on above the player
                    if (gamePanel.tileH.tile[tile1].collision || gamePanel.tileH.tile[tile2].collision)
                    {
                        entity.collisionOn = true;
                    }
                    break;

                case "down":
                    entityBottomRow = (entityBottomWorldY + entity.speed) / Config.tileSize; // The row on above the player
                    tile1 = gamePanel.tileH.mapTileNumber[entityLeftCol, entityBottomRow];   // Bottom left block on below the player
                    tile2 = gamePanel.tileH.mapTileNumber[entityRightCol, entityBottomRow];  // Bottom right block on below the player

                    // Check any block has collision on below the player has collision
                    if (gamePanel.tileH.tile[tile1].collision || gamePanel.tileH.tile[tile2].collision)
                    {
                        entity.collisionOn = true;
                    }
                    break;

                case "left":
                    entityLeftCol = (entityLeftWorldX - entity.speed) / Config.tileSize;     // The column on left the player
                    tile1 = gamePanel.tileH.mapTileNumber[entityLeftCol, entityTopRow];      // Left top block on left the player
                    tile2 = gamePanel.tileH.mapTileNumber[entityLeftCol, entityBottomRow];   // Left bottom block on left the player

                    // Check any block has collision on left the player 
                    if (gamePanel.tileH.tile[tile1].collision || gamePanel.tileH.tile[tile2].collision)
                    {
                        entity.collisionOn = true;
                    }
                    break;

                case "right":
                    entityRightCol = (entityRightWorldX + entity.speed) / Config.tileSize;   // The column on right the player
                    tile1 = gamePanel.tileH.mapTileNumber[entityRightCol, entityTopRow];     // Right top block on right the player
                    tile2 = gamePanel.tileH.mapTileNumber[entityRightCol, entityBottomRow];  // Right bottom block on right the player

                    // Check any block has collision on right the player 
                    if (gamePanel.tileH.tile[tile1].collision || gamePanel.tileH.tile[tile2].collision)
                    {
                        entity.collisionOn = true;
                    }
                    break;
            }
        }
    }

}