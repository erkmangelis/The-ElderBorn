namespace ElderBorn
{
    public class ObjectPig : SuperObject
    {
        public ObjectPig()
        {
            name = "Pig";
            collision = true;
            
            try
            {
                sprite = new Bitmap("./assets/objects/pig.png");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}