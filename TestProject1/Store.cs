using System.Collections.Generic;

namespace TestProject1
{
    public class Store
    {
        public List<Chair> chairs { get; set; } = new List<Chair>();

        public List<Closet> closets{ get; set; }= new List<Closet>();

        public List<Dresser> dressers{ get; set; } = new List<Dresser>();

        public List<Worker> workers{ get; set; } = new List<Worker>();
        
        public List<Client> clients{ get; set; }= new List<Client>();
        
        public CashRegister cashRegister { get; set; }
        
        public CollectorFurniture collectorFurniture { get; set; }
    

        public void addFurniture(List<Chair> chairs, List<Closet> closets, List<Dresser> dressers)
        {
            if(chairs.Count != 0)
                this.chairs.AddRange(chairs);
            if(closets.Count != 0)
                this.closets.AddRange(closets);
            if(dressers.Count != 0)
                this.dressers.AddRange(dressers);
        }
        
        public void addWorker(List<Worker> workers)
        {
            if(workers.Count != 0)
                this.workers.AddRange(workers);
        }
        
        public void addClient(Client client)
        {
            this.clients = new List<Client>();
            clients.Add(client);
        }

        public List<Chair> findChairs(string name)
        {
            List<Chair> tmpChairs = new List<Chair>();
            foreach (var chair in chairs)
                {
                    if (chair.name.Equals(name))
                        tmpChairs.Add(chair);
                }

            return tmpChairs;
        }
        
        public List<Closet> findClosets(string name)
        {
            List<Closet> tmpClosets = new List<Closet>();
            foreach (var closet in closets)
            {
                if(closet.name.Equals(name))
                    tmpClosets.Add(closet);
            }

            return tmpClosets;
        }
        
        public List<Dresser> findDressers(string name)
        {
            List<Dresser> tmpDressers = new List<Dresser>();
            foreach (var dresser in dressers)
            {
                if(dresser.name.Equals(name))
                    tmpDressers.Add(dresser);
            }

            return tmpDressers;
        }

        public void returnMoneyClient(Client client, Check check)
        {
            client.budget = check.summ;
        }

        public bool stock(Chair chair, Dresser dresser, Closet closet)
        {
            if (chair != null)
            {
                var filteredChairs = chairs.FindAll(tmpChair => tmpChair.name == chair.name);
                var count = filteredChairs.Count;
                return count > 1;
            }
            if (dresser != null)
            {
                var filteredDressers = dressers.FindAll(tmpChair => tmpChair.name == dresser.name);
                var count = filteredDressers.Count;
                return count > 1;
            }
            if (closet != null)
            {
                var filteredClosets = closets.FindAll(tmpChair => tmpChair.name == closet.name);
                var count = filteredClosets.Count;
                return count > 1;
            }

            return true;
        }
    }
}