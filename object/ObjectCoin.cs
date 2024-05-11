namespace ElderBorn
{
    public class ObjectCoin : SuperObject
    {
        public ObjectCoin()
        {
            name = "Coin";
            
            try
            {
                sprite = new Bitmap("./assets/objects/coin.png");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}