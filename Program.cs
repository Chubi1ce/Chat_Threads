using Chat_Threads;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Server.StartServer();
        }
        else
        {
            // new Thread(() =>
            // {
            //     Client.StartClient(args[0]);
            // }).Start();

            Client.StartClient(args[0]);
        }
    }
}
