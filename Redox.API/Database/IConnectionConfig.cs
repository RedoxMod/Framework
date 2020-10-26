namespace Redox.API.Database
{
    public interface IConnectionConfig
    { 
        bool Enabled { get; set; }
            
        string Server { get; set; }

        string Username { get; set; }
        
        string Password { get; set;}
        
        string Database { get; set;}

        uint Port { get; set; }
    }
}