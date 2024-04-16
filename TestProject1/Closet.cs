using System.Collections.Generic;

namespace TestProject1
{
    public class Closet
    {
        public List<Worker> workers { get; set; }
        
        public MaterialEnum materialEnum { get; set; }
        
        public bool returnStatus { get; set; }

        public double price { get; set; }
        
        public bool deffect { get; set; }

        public string name;
        
        public Design design { get; set; }
        public Closet(MaterialEnum materialEnum, List<Worker> workers, string name)
        {
            this.materialEnum = materialEnum;
            this.name = name;
            this.workers = workers;
            deffect = false;
        }

        public void setDesign(Design design)
        {
            this.design = design;
        }
    }
}