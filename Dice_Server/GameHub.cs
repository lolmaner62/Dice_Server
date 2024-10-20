using Microsoft.AspNetCore.SignalR;
using System.Data;
using System.Data.SqlClient;

namespace Dice_Server
{
    public class GameHub : Hub
    {
        public List<Client> clients = new List<Client>();
        public async Task<bool> TestConnection()
        {
            return await Task<bool>.Run(() =>
            {
                return true;
            });
            
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
                        clients.Add(new Client(user));
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
               
            });

        }


    }
}
