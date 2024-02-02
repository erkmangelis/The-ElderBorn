namespace MyGameProject
{

    public class Entity
    {
        public int worldX, worldY;
        public int speed;

        public Bitmap? up1, up2, down1, down2, left1, left2, right1, right2;
        public string? direction;
        public int spriteNumber;
        public int spriteCounter;

        public bool collisionOn = false;
        public int cbX; // CollisioBox's x value
        public int cbY; // CollisionBox's y value
        public int cbWidth; // CollisionBox's width value
        public int cbHeight; // CollisionBox's Height value
        public Rectangle collisionBox;

    }
}