using Oneview.Client.Base.Events;
using static System.Console;

namespace EventUsingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var eventAggregator = new EventAggregator();

            eventAggregator.GetEvent<SimpleEvent>().Subscribe(() => WriteLine("Subscription 1: Simple event raised"));
            //eventAggregator.GetEvent<SimpleEvent>().Subscribe(null);
            eventAggregator.GetEvent<SimpleEvent>().Subscribe(() => WriteLine("Subscription 2: Simple event raised"));
            WriteLine("ENTER to raise simple event");
            ReadLine();
            eventAggregator.GetEvent<SimpleEvent>().Publish();

            eventAggregator.GetEvent<PayloadEvent>().Subscribe(value => WriteLine("Subscription 1: Payload event raised with value: " + value));
            //eventAggregator.GetEvent<PayloadEvent>().Subscribe(null);
            eventAggregator.GetEvent<PayloadEvent>().Subscribe(value => WriteLine("Subscription 2: Payload event raised with value: " + value));
            WriteLine("ENTER to raise payload event");
            ReadLine();
            eventAggregator.GetEvent<PayloadEvent>().Publish("value in Event fired");


            


            WriteLine("Done!!!");
            ReadLine();
        }
    }

    public class PayloadEvent : PubSubEvent<string>
    {
        
    }

    public class SimpleEvent : PubSubEvent
    { }
}
