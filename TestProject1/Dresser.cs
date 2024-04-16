using System.Collections.Generic;

namespace TestProject1
{
    public class Dresser
    {
        public List<Worker> workers { get; set; }
        
        public MaterialEnum materialEnum;
        
        public bool deffect { get; set; }

        public string name;
        
        public double price { get; set; }
        
        
        public Design design { get; set; }
        
        public bool returnStatus { get; set; }
        
        public Dresser(MaterialEnum materialEnum, List<Worker> workers, string name)
        {
            this.materialEnum = materialEnum;
            this.workers = workers;
            this.name = name;
            deffect = false;
        }
        
        public void setDesign(Design design)
        {
            this.design = design;
        }
    }
}