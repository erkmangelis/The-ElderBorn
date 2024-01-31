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

            tile = new Tile[10];
            mapTileNumber = new int[Config.maxScreenRow, Config.maxScreenCol];

            getTileSprite();
            loadMap();
        }


        // Gets tile sprites from assets
        public void getTileSprite()
        {
            try
            {
                tile[0] = new Tile();
                tile[0].sprite = new Bitmap("./assets/tiles/grass.png");
                tile[1] = new Tile();
                tile[1].sprite = new Bitmap("./assets/tiles/dirt.png");
                tile[2] = new Tile();
                tile[2].sprite = new Bitmap("./assets/tiles/water.png");
                tile[3] = new Tile();
                tile[3].sprite = new Bitmap("./assets/tiles/tree.png");
                tile[4] = new Tile();
                tile[4].sprite = new Bitmap("./assets/tiles/wall.png");
            }
            
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        // Loads map from map file
        public void loadMap()
        {
            try
            {
                // Path to map file
                string mapPath = "./maps/map1.txt";


                if (File.Exists(mapPath))
                {
                    // Reading the map file
                    using (StreamReader reader = new StreamReader(mapPath))
                    {
                        string line;
                        
                        // Iterating rows
                        for (int row = 0; row < Config.maxScreenRow; row++)
                        {
                            // Reads every line in map file
                            line = reader.ReadLine();

                            // Holds values temporary
                            string[] numbers = line.Split(" ");
                            
                            // Iterating columns
                            for (int col = 0; col < Config.maxScreenCol; col++)
                            {
                                // Assigning every tile after read from map file
                                mapTileNumber[row, col] = int.Parse(numbers[col]);
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
            int col = 0;
            int row = 0;
            int x = 0;
            int y = 0;


            while (col < Config.maxScreenCol && row < Config.maxScreenRow)
            {
                int tileNumber = mapTileNumber[row, col];

                graphics.DrawImage(tile[tileNumber].sprite, new Rectangle (x, y, Config.tileSize, Config.tileSize));
                col++;
                x += Config.tileSize;

                if ( col == Config.maxScreenCol)
                {
                    col = 0;
                    x = 0;
                    row++;
                    y += Config.tileSize;
                }
            }
        }

    }
}