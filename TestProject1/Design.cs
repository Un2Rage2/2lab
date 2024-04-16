namespace TestProject1
{
    public class Design
    {
        public string nameDesign { get; set; }

        public string style { get; set; }

        public string color { get; set; }

        public Design(string nameDesign, string style, string color)
        {
            this.nameDesign = nameDesign;
            this.style = style;
            this.color = color;
        }
    }
}