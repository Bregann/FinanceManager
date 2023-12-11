namespace FinanceManager.Domain.Dtos.ThirdParty
{
    public class MonzoDataResponse
    {
        [JsonProperty("transactions")]
        public List<Transaction> Transactions { get; set; }
    }

    public class Transaction
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("merchant")]
        public Merchant Merchant { get; set; }
    }

    public partial class Merchant
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }
    }
}
