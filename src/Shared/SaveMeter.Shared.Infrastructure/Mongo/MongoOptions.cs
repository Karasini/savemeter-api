namespace SaveMeter.Shared.Infrastructure.Mongo
{
    public class MongoOptions
    {
        public string Connection { get; set; }
        public string DatabaseName { get; set; }
        public bool EnableTransactions { get; set; }
    }
}
