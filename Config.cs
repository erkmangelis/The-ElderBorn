namespace ElderBorn
{

    public static class Config
    {
        // Game Settings
        public static int FPS = 60;
        public static int tileSize = 64;

        
        // Screen Settings
        public static int maxScreenCol = 16;
        public static int maxScreenRow = 12;
        public static int screenWidth = tileSize * maxScreenCol;
        public static int screenHeight = tileSize * maxScreenRow;


        // World Settings
        public static int maxWorldCol = 50;
        public static int maxWorldRow = 50;
        public static int worldHeight = tileSize * maxWorldRow;
        public static int worldWidth = tileSize * maxWorldCol;

    }
}