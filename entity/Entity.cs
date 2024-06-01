namespace ElderBorn
{

    public class Entity
    {
        public GamePanel gamePanel;
        public int worldX, worldY;
        public int speed;

        public Bitmap? up1, up2, down1, down2, left1, left2, right1, right2;
        public string? movingDirection;
        public string? lookingDirection;
        public int spriteNumber;
        public int spriteCounter;

        public bool collisionOn = false;
        public int cbX; // CollisioBox's x value
        public int cbY; // CollisionBox's y value
        public int cbDefaultX;
        public int cbDefaultY;
        public int cbWidth; // CollisionBox's width value
        public int cbHeight; // CollisionBox's Height value
        public Rectangle collisionBox;

        public Entity(GamePanel gamePanel)
        {
            this.gamePanel = gamePanel;
        }
    }
}