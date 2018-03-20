using PCL.Client;
using System;

namespace PCLClientTester
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TheClient.Start();
                ////Address address = new Address("amqp://localhost:5672");
                //var address = new Address("ampq://guest:guest@localhost:5672/");
                //Connection connection = new Connection(address);
                //Session session = new Session(connection);
                //ReceiverLink receiver = new ReceiverLink(session, "theExchange", "theQueue");

                //Console.WriteLine("Receiver connected to broker.");
                //Message message = receiver.Receive();
                //Console.WriteLine("Received " + message.GetBody<string>());
                //receiver.Accept(message);

                //receiver.Close();
                //session.Close();
                //connection.Close();
                Console.WriteLine("Listening...");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            
        }
    }
}
