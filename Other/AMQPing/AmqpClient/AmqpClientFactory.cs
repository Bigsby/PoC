namespace AmqpClient
{
    public static class AmqpClientFactory
    {
        private static IAmqpClient _client;

        public static IAmqpClient GetClient()
        {
            return _client ?? (_client = new AmqpClient());
        }
    }
}
