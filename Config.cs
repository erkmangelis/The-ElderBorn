namespace MyGameProject
{

    public static class Config
    {
        
        // Screen Settings
        public static int tileSize = 48;
        public static int maxScreenCol = 16;
        public static int maxScreenRow = 12;
        public static int screenWidth = tileSize * maxScreenCol;
        public static int screenHeight = tileSize * maxScreenRow;
        public static int FPS = 60;


        // World Settings
        public static int maxWorldCol = 50;
        public static int maxWorldRow = 50;
        public static int worldHeight = tileSize * maxWorldRow;
        public static int worldWidth = tileSize * maxWorldCol;

    }
}