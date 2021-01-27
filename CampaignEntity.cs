namespace DurFunc
{
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;

    public class CampaignEntity
    {
        public int Count = 0;
        public string Status = "barney";

        public void Add(int amount) => this.Count += amount;

        public void Increment() => this.Count++;

        public void Reset() => this.Count = 0;

        public int Get() => this.Count;

        public void SetStatusProcessing() => this.Status = CampaignEntityStatusEnum.Processing.ToString();

        public void SetStatusComplete() => this.Status = CampaignEntityStatusEnum.Complete.ToString();

        [FunctionName(nameof(CampaignEntity))]
        public static Task Run([EntityTrigger] IDurableEntityContext ctx)
            => ctx.DispatchAsync<CampaignEntity>();
    }

    public enum CampaignEntityStatusEnum
    {
        Unknown = 0,
        Processing = 1,
        Complete = 2
    }
}
