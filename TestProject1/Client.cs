using System.Collections.Generic;

namespace TestProject1
{
    public class Client
    {
        public Check check { get; set; }

        public double budget { get; set; }

        public bool needDelivery { get; set; }

        public Chair chair;

        public Closet closet;

        public Dresser dresser;
        
        public Store store { get; set; }

        public bool quality { get; set; }

        public string adress { get; set; }

        public bool payFurniture { get; set; }

        public bool payDelivery { get; set; }

        public bool isCompleteDelivery { get; set; }
        
        public bool stockChair { get; set; }
        
        public bool stockCloset{ get; set; }
        
        public bool stockDresser { get; set; }
        
        public CollectorFurniture CollectorFurniture { get; set; }
        

        public bool checkQualityChair(Chair chair)
        {
            return chair.deffect;
        }
        
        public bool checkQualityDresser(Dresser dresser)
        {
            return dresser.deffect;
        }
        
        public bool checkQualityCloset(Closet closet)
        {
            return closet.deffect;
        }
        

        public void selectChair(Store store, string name)
        {
            foreach (var chair in store.chairs)
            {
                if (chair.name.Equals(name))
                    this.chair = chair;
            }

        }

        public void checkChair(Chair chair)
        {
            if (chair.name.Equals(this.chair.name))
                isCompleteDelivery = true;
            else
                isCompleteDelivery = false;
        }

        public void selectCloset(Store store, string name)
        {
            foreach (var closet in store.closets)
            {
                if (closet.name.Equals(name))
                    this.closet = closet;
            }

        }
        public void selectDresser(Store store, string name)
        {
            foreach (var dresser in store.dressers)
            {
                if (dresser.name.Equals(name))
                    this.dresser = dresser;
            }

        }
        
        public void checkCloset(Closet closet)
        {
            if (closet.name.Equals(this.closet.name))
                isCompleteDelivery = true;
            else
                isCompleteDelivery = false;
        }
        
        public void checkDresser(Dresser dresser)
        {
            if (dresser.name.Equals(this.dresser.name))
                isCompleteDelivery = true;
            else
                isCompleteDelivery = false;
        }

        public void goStore(Store store)
        {
            store.clients.Add(this);
        }

        public CollectorFurniture orderCollectorFurniture(Store store)
        {
            return new CollectorFurniture(this, 2000, false, store);
        }

        public void delivery()
        {
            this.payDelivery = true;
        }
    }
}