using System;
using System.Collections.Generic;

namespace TestProject1
{
    public class Delivery
    {
        public string deliveryAdress { get;}
        
        public bool isCompleted { get; set; }

        public Chair chair { get; set; }
        
        public Closet closet { get; set; }
        
        public Dresser dresser { get; set; }
        
        public Delivery(string deliveryAdress, bool isCompleted)
        {
            this.deliveryAdress = deliveryAdress;
            this.isCompleted = isCompleted;
        }

        public void deliveryChair(Chair chair)
        {
            this.chair = chair;
            isCompleted = true;
        }
        
        public void deliveryCloset(Closet closet)
        {
            this.closet = closet;
            this.isCompleted = true;
        }
        
        public void deliveryDresser(Dresser dresser)
        {
            this.dresser = dresser;
            isCompleted = true;
        }

        public void returnCloset(Closet closet, Store store)
        {
            List<Closet> closetsReturn = new List<Closet>();
            closet.returnStatus = true;
            closetsReturn.Add(closet);
            store.closets = closetsReturn;
        }
        
        public void returnDresser(Dresser dresser, Store store)
        {
            List<Dresser> dressersReturn = new List<Dresser>();
            dresser.returnStatus = true;
            dressersReturn.Add(dresser);
            store.dressers = dressersReturn;
        }
        
        public void returnChair(Chair chair, Store store)
        {
            List<Chair> chairsReturn = new List<Chair>();
            chair.returnStatus = true;
            chairsReturn.Add(chair);
            store.chairs = chairsReturn;
        }
        
    }
}