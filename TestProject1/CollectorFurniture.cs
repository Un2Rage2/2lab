namespace TestProject1
{
    public class CollectorFurniture
    {
        public Client client { get; set; }

        public int experience { get; set; }

        public bool buildStatus { get; set; }

        public CollectorFurniture(Client client, int experience, bool buildStatus, Store store)
        {
            this.client = client;
            this.experience = experience;
            this.buildStatus = buildStatus;
            this.store = store;
        }

        public Store store { get; set; }

        public void startCollectFurniture()
        {
            buildStatus = false;
        }

        public void endCollectFurniture()
        {
            buildStatus = true;
        }
    }
}