namespace ElderBorn
{
    public class KeyHandler
    {
        public bool upPressed, downPressed, leftPressed, rightPressed;
        public bool logOn = false;


        // Handle Key Press Event
        public void HandleKeyPress(Keys key, bool isPressed)
        {
            if (key == Keys.W)
            {
                upPressed = isPressed;
            }
            if (key == Keys.S)
            {
                downPressed = isPressed;
            }
            if (key == Keys.A)
            {
                leftPressed = isPressed;
            }
            if (key == Keys.D)
            {
                rightPressed = isPressed;
            }
            if (key == Keys.L)
            {
                if (!logOn)
                {
                    logOn = true;
                }
                else
                {
                    logOn = false;
                }
            }
        }


        // Handle Key Release Event
        public void HandleKeyRelease(Keys key, bool isPressed)
        {
            if (key == Keys.W)
            {
                upPressed = isPressed;
            }
            if (key == Keys.S)
            {
                downPressed = isPressed;
            }
            if (key == Keys.A)
            {
                leftPressed = isPressed;
            }
            if (key == Keys.D)
            {
                rightPressed = isPressed;
            }
        }


        // Check is player moving
        public bool isMoving()
        {
            bool isMoving = (leftPressed || rightPressed || upPressed || downPressed);
            return isMoving;
        }


        // Check is player moving diagonal
        public bool DiagonalMovement()
        {
            //      Diagonal    =         Moving Horizontal     and      Moving Vertical
            bool isDiagonalMove = (leftPressed || rightPressed) && (upPressed || downPressed);
            
            return  isDiagonalMove;
        }
    }
}