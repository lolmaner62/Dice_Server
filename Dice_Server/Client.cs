namespace Dice_Server
{
    public class Client
    {
        public string Username;
        Guid ID;

        public Client(string username)
        {
            Username = username;
            ID = Guid.NewGuid();
        }


    }
}
