using System.Collections.Generic;

namespace TestProject1
{
    public class FurnitureFactory
    {
        public  List<Worker> workers { get; set; }

        public Chair createChair(List<Worker> workers, MaterialEnum materialEnum, string name)
        {
            Chair chair = new Chair(materialEnum, workers, name);
            foreach (var worker in workers)
            {
                worker.chair = chair;
            }

            return chair;
        }
        
        public Closet createCloset(List<Worker> workers, MaterialEnum materialEnum, string name)
        {   
            Closet closet = new Closet(materialEnum, workers, name);
            foreach (var worker in workers)
            {
                worker.closet = closet;
            }

            return closet;
        }
        
        public Dresser createDresser(List<Worker> workers, MaterialEnum materialEnum, string name)
        {
            Dresser dresser = new Dresser(materialEnum, workers, name);
            foreach (var worker in workers)
            {
                worker.dresser = dresser;
            }

            return dresser;
        }
    }
}