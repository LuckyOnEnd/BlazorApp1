using Newtonsoft.Json;

namespace BlazorApp1
{
    public class Models
    {
    }

    public class Line
    {
        public int Id { get; set; }
        [JsonProperty("ordernumber")]
        public string Ordernumber { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("revision")]
        public string Revision { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("drawingmaterial")]
        public string Drawingmaterial { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class Material
    {
        public int Nr { get; set; } = 0;
        public string Index { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        public string Comments { get; set; }
    }

    public class Operation
    {
        public int Nr { get; set; }
        [JsonProperty("operationname")]
        public string Operationname { get; set; }

        [JsonProperty("workplacename")]
        public string Workplacename { get; set; }

        [JsonProperty("plannedstopdate")]
        public string Plannedstopdate { get; set; }

        [JsonProperty("plannedtime")]
        public int Plannedtime { get; set; }

        public string Time { get; set; }
        public string Initials { get; set; }

        [JsonProperty("isqualitycontrol")]
        public bool Isqualitycontrol { get; set; }

        public bool Cheked { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }
    }

    public class Other
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("values")]
        public List<Value> Values { get; set; }
    }

    public class Root
    {
        [JsonProperty("template")]
        public string Template { get; set; }

        [JsonProperty("copynumber")]
        public int Copynumber { get; set; }

        [JsonProperty("drawingpath")]
        public string Drawingpath { get; set; }

        [JsonProperty("collectionnumber")]
        public string Collectionnumber { get; set; }

        [JsonProperty("ordernumber")]
        public string Ordernumber { get; set; }

        [JsonProperty("projectwithclient")]
        public string Projectwithclient { get; set; }

        [JsonProperty("goalindex")]
        public string Goalindex { get; set; }

        [JsonProperty("goalrevision")]
        public string Goalrevision { get; set; }

        [JsonProperty("goalname")]
        public string Goalname { get; set; }

        [JsonProperty("goalquantity")]
        public int Goalquantity { get; set; }

        [JsonProperty("colour")]
        public string Colour { get; set; }

        [JsonProperty("drawingmaterial")]
        public string Drawingmaterial { get; set; }

        [JsonProperty("goalcomments")]
        public string Goalcomments { get; set; }

        [JsonProperty("createdby")]
        public string Createdby { get; set; }

        [JsonProperty("creasationdate")]
        public string Creasationdate { get; set; }

        [JsonProperty("materials")]
        public List<Material> Materials { get; set; }

        [JsonProperty("operations")]
        public List<Operation> Operations { get; set; }

        [JsonProperty("semiproducts")]
        public List<Semiproduct> Semiproducts { get; set; }

        [JsonProperty("others")]
        public List<Other> Others { get; set; }
    }

    public class Semiproduct
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("lines")]
        public List<Line> Lines { get; set; }
    }

    public class Value
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string StringValue { get; set; }
    }
}
