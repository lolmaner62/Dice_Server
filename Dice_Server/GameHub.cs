using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Data.SqlClient;
using Dice_Server;

namespace Dice_Server
{
    public  class GameHub : Hub
    {
        private Client? _client;
        public async Task<bool> TestConnection()
        {
            return await Task<bool>.Run(() =>
            {
                return true;
            });
            
        }
        public async Task<string> GenerateSession(string user)
        {
            string id = await Rng.GenerateSessionID();
            Client client = await Task.Run(() => Variables.FindClientByUser(user));
            Variables.sessions.Add(new Session(id, client));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"New session being created by: {user}");
            Console.ForegroundColor = ConsoleColor.White;
            return id;
        }
        public async Task<bool> JoinSession(string id, string user)
        {
            Client client = await Task.Run(() => Variables.FindClientByUser(user));
            Session session = Variables.FindSessionById(id);
            if (session != null && session.player2 == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"Session: [{id}] was joined by {user}");
                Console.ForegroundColor = ConsoleColor.White;
                session.player2 = client;
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"{user} tried to join session that doesnt exist or is full");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
        }
        public async Task<bool> ValidateCredentials(string user, string pass)
        {
            return await Task<bool>.Run(() =>
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Incoming Login request: {user}");
                    Console.ForegroundColor= ConsoleColor.White;
                    string connectionString = @"Data Source=localhost;Initial Catalog=DiceGame;Integrated Security=True";
                    SqlConnection con = new SqlConnection(connectionString);
                    string querry = "SELECT * FROM Users WHERE username ='" + user + "' AND password = '" + pass + "'";
                    SqlDataAdapter cmd = new SqlDataAdapter(querry, con);
                    DataTable table = new DataTable();
                    cmd.Fill(table);
                    if (table.Rows.Count == 1)
                    {
                        _client = new Client(user, Context.ConnectionId);
                        Variables.clients.Add(_client);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{user} Sucessfully logged in!");
                        Console.ForegroundColor = ConsoleColor.White;
                        return true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{user} Invalid credentials!!");
                        Console.ForegroundColor = ConsoleColor.White;
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
               
            });

        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            var client = Variables.clients.Find(c => c.ConID == connectionId);

            if (client != null)
            {
                Console.WriteLine($"[{client.Username}] has disconnected");
            }
            else
            {
                Console.WriteLine($"[{connectionId}] has disconnected");
            }
            Variables.clients.RemoveAll(c => c.ConID == connectionId);
            

            await base.OnDisconnectedAsync(exception);
        }



    }
}
