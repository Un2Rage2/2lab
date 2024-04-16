using System.Collections.Generic;

namespace TestProject1
{
    public class CashRegister
    {
        public  Worker cashier { get; set; }

        public Check check { get; set; }

        public Client client { get; set; }
        
        public StatusCashRegister status { get; set; }

        public CashRegister(Worker cashier, Client client)
        {
            this.cashier = cashier;
            this.client = client;
        }

        public Check createCheck(Client client, double summ, EnumFurniture furniture)
        {
            return new (client, CheckStatus.payed, summ, furniture);
        }

        public void payFurniture(Client client)
        {
            client.payFurniture = true;
        }
        
        public void payDelivery(Client client)
        {
            client.payDelivery = true;
        }
    }
}