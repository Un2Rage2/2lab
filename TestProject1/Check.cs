using System.Collections.Generic;

namespace TestProject1
{
    public class Check
    {   
        public Client client { get; }

        public CheckStatus checkStatus { get; }

        public double summ { get; }

        public EnumFurniture furniture { get; }

        public Check(Client client, CheckStatus checkStatus, double summ, EnumFurniture furniture)
        {
            this.client = client;
            this.checkStatus = checkStatus;
            this.summ = summ;
            this.furniture = furniture;
        }
    }
}