namespace Dice_Server
{
    public static class Variables
    {
        static public List<Client> clients;
        static public List<Session> sessions;
        static Variables() 
            {
                clients = new List<Client>();
                sessions = new List<Session>();
            }

        public static Client FindClientByUser(string user)
        {

            foreach (var item in clients)
            {
                if (item.Username == user)
                {
                    return item;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("SKULL");
            Console.ForegroundColor = ConsoleColor.White;
            return null;
        }
        public static Session FindSessionById(string id)
        {

            foreach (var item in sessions)
            {
                if (item.SessionID == id)
                {
                    return item;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("SKULL");
            Console.ForegroundColor = ConsoleColor.White;
            return null;
        }
    }
}
