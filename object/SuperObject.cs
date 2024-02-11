namespace MyGameProject
{
    public class SuperObject
    {
        public Bitmap? sprite;
        public String name;
        public bool collision = false;
        public int worldX, worldY;
        public Rectangle collisionBox = new Rectangle(0, 0, Config.tileSize, Config.tileSize);
        public int cbDefaultX = 0;
        public int cbDefaultY = 0;
    }
}