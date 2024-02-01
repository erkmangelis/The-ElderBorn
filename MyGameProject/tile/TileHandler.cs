namespace MyGameProject
{
    public class TileHandler
    {
        GamePanel gamePanel;
        Tile[] tile;
        int[,] mapTileNumber;


        // Initialization
        public TileHandler(GamePanel gameP)
        {
            this.gamePanel = gameP;

            tile = new Tile[40];
            mapTileNumber = new int[Config.maxWorldCol, Config.maxWorldRow];

            getTileSprite();
            // Set the map path
            loadMap("./maps/map.txt");
        }


        // TODO: Yeni algoritma yaz
        // Gets tile sprites from assets
        public void getTileSprite()
        {
            try
            {
                tile[0] = new Tile();
                tile[0].sprite = new Bitmap("./assets/tiles/dirt.png");
                tile[1] = new Tile();
                tile[1].sprite = new Bitmap("./assets/tiles/dirt_up.png");
                tile[2] = new Tile();
                tile[2].sprite = new Bitmap("./assets/tiles/dirt_rightup.png");
                tile[3] = new Tile();
                tile[3].sprite = new Bitmap("./assets/tiles/dirt_leftup.png");
                tile[4] = new Tile();
                tile[4].sprite = new Bitmap("./assets/tiles/dirt_left.png");
                tile[5] = new Tile();
                tile[5].sprite = new Bitmap("./assets/tiles/dirt_right.png");
                tile[6] = new Tile();
                tile[6].sprite = new Bitmap("./assets/tiles/dirt_rightdown.png");
                tile[7] = new Tile();
                tile[7].sprite = new Bitmap("./assets/tiles/dirt_leftdown.png");
                tile[8] = new Tile();
                tile[8].sprite = new Bitmap("./assets/tiles/dirt_down.png");
                tile[9] = new Tile();
                tile[9].sprite = new Bitmap("./assets/tiles/grass.png");
                tile[10] = new Tile();
                tile[10].sprite = new Bitmap("./assets/tiles/grass_dirty.png");
                tile[11] = new Tile();
                tile[11].sprite = new Bitmap("./assets/tiles/grass_leafy.png");
                tile[12] = new Tile();
                tile[12].sprite = new Bitmap("./assets/tiles/grass_lilrocks.png");
                tile[13] = new Tile();
                tile[13].sprite = new Bitmap("./assets/tiles/grass_rock.png");
                tile[14] = new Tile();
                tile[14].sprite = new Bitmap("./assets/tiles/grass_redflowers.png");
                tile[15] = new Tile();
                tile[15].sprite = new Bitmap("./assets/tiles/grass_whiteflower.png");
                tile[16] = new Tile();
                tile[16].sprite = new Bitmap("./assets/tiles/grass_whiteflowers.png");
                tile[17] = new Tile();
                tile[17].sprite = new Bitmap("./assets/tiles/water.png");
                tile[18] = new Tile();
                tile[18].sprite = new Bitmap("./assets/tiles/water_leftup.png");
                tile[19] = new Tile();
                tile[19].sprite = new Bitmap("./assets/tiles/water_rightup.png");
                tile[20] = new Tile();
                tile[20].sprite = new Bitmap("./assets/tiles/water_up.png");
                tile[21] = new Tile();
                tile[21].sprite = new Bitmap("./assets/tiles/water_left.png");
                tile[22] = new Tile();
                tile[22].sprite = new Bitmap("./assets/tiles/water_right.png");
                tile[23] = new Tile();
                tile[23].sprite = new Bitmap("./assets/tiles/water_leftdown.png");
                tile[24] = new Tile();
                tile[24].sprite = new Bitmap("./assets/tiles/water_rightdown.png");
                tile[25] = new Tile();
                tile[25].sprite = new Bitmap("./assets/tiles/water_down.png");
                tile[26] = new Tile();
                tile[26].sprite = new Bitmap("./assets/tiles/water_lilypad.png");
                tile[27] = new Tile();
                tile[27].sprite = new Bitmap("./assets/tiles/water_seagrass.png");
                tile[28] = new Tile();
                tile[28].sprite = new Bitmap("./assets/tiles/water_smallwave.png");
                tile[29] = new Tile();
                tile[29].sprite = new Bitmap("./assets/tiles/water_bigwave.png");
                tile[30] = new Tile();
                tile[30].sprite = new Bitmap("./assets/tiles/wall.png");
                tile[31] = new Tile();
                tile[31].sprite = new Bitmap("./assets/tiles/tree.png");
                tile[32] = new Tile();
                tile[32].sprite = new Bitmap("./assets/tiles/dirt_innerleftu.png");
                tile[33] = new Tile();
                tile[33].sprite = new Bitmap("./assets/tiles/dirt_innerrightu.png");
                tile[34] = new Tile();
                tile[34].sprite = new Bitmap("./assets/tiles/dirt_innerleftd.png");
                tile[35] = new Tile();
                tile[35].sprite = new Bitmap("./assets/tiles/dirt_innerrightd.png");
            }
            
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        // Loads map from map file
        public void loadMap(string path)
        {
            try
            {
                // Path to map file
                string mapPath = path;


                if (File.Exists(mapPath))
                {
                    // Reading the map file
                    using (StreamReader reader = new StreamReader(mapPath))
                    {
                        string? line;
                        
                        // Iterating rows
                        for (int row = 0; row < Config.maxWorldRow; row++)
                        {
                            // Reads every line in map file
                            line = reader.ReadLine();

                            // Holds values temporary
                            string[] numbers = line.Split(" ");
                            
                            // Iterating columns
                            for (int col = 0; col < Config.maxWorldCol; col++)
                            {
                                // Assigning every tile after read from map file
                                mapTileNumber[col, row] = int.Parse(numbers[col]);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

 
        // TODO: Yeni Algoritma Yaz
        // Draws tiles to every screen cell
        public void draw(Graphics graphics)
        {
            int worldCol = 0;
            int worldRow = 0;


            while (worldCol < Config.maxWorldCol && worldRow < Config.maxWorldRow)
            {
                int tileNumber = mapTileNumber[worldCol, worldRow];

                int worldX = worldCol * Config.tileSize;
                int worldY = worldRow * Config.tileSize;
                int screenX = worldX - gamePanel.player.worldX + gamePanel.player.screenX;
                int screenY = worldY - gamePanel.player.worldY + gamePanel.player.screenY;


                // Render the tiles that only inside of the screen
                if (worldX + Config.tileSize > gamePanel.player.worldX - gamePanel.player.screenX && 
                    worldX - Config.tileSize < gamePanel.player.worldX + gamePanel.player.screenX &&
                    worldY + Config.tileSize > gamePanel.player.worldY - gamePanel.player.screenY &&
                    worldY - Config.tileSize < gamePanel.player.worldY + gamePanel.player.screenY)
                    {
                        graphics.DrawImage(tile[tileNumber].sprite, new Rectangle (screenX, screenY, Config.tileSize, Config.tileSize));
                    }

                worldCol++;

                if ( worldCol == Config.maxWorldCol)
                {
                    worldCol = 0;
                    worldRow++;
                }
            }
        }

    }
}