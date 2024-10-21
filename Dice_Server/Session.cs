namespace Dice_Server
{
    public class Session
    {
        public string SessionID;
        public Client player1;
        public Client? player2;

        public Session(string sessionID, Client _player1)
        {
            SessionID = sessionID;
            player1 = _player1;
        }

    }
}
