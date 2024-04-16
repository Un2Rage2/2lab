using System.Collections.Generic;

namespace TestProject1
{
    public class Chair
    {

        public List<Worker> workers { get; set; }
        
        public MaterialEnum materialEnum { get; set; }

        public bool deffect { get; set; }

        public string name;
            
        public bool returnStatus { get; set; }

        public double price { get; set; }
        
        public Design design { get; set; }
        
        public Chair(MaterialEnum materialEnum, List<Worker> workers, string name)
        {
            this.materialEnum = materialEnum;
            this.workers = workers;
            deffect = false;
            this.name = name;
        }
    }
}