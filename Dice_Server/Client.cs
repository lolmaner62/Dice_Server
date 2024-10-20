namespace Dice_Server
{
    public class Client
    {
        string Username;
        Guid ID;

        public Client(string username)
        {
            Username = username;
            ID = Guid.NewGuid();
        }


    }
}
