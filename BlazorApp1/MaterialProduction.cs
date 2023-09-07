namespace BlazorApp1;

    public class MaterialProduction
    {
        public string index { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public string unit { get; set; }
    }

    public class OperationProduction
    {
        public string operationname { get; set; }
        public string workplacename { get; set; }
        public string plannedstopdate { get; set; }
        public int plannedtime { get; set; }
        public bool isqualitycontrol { get; set; }
        public string comments { get; set; }
    }

    public class OtherProduction
    {
        public string title { get; set; }
        public List<Value> values { get; set; }
    }

    public class RootProduction
    {
        public string template { get; set; }
        public int copynumber { get; set; }
        public string drawingpath { get; set; }
        public string collectionnumber { get; set; }
        public string ordernumber { get; set; }
        public string projectwithclient { get; set; }
        public string goalindex { get; set; }
        public string goalrevision { get; set; }
        public string goalname { get; set; }
        public int goalquantity { get; set; }
        public string goalcomments { get; set; }
        public string productlindex { get; set; }
        public string productrevision { get; set; }
        public string productname { get; set; }
        public int productquantity { get; set; }
        public string drawingmaterial { get; set; }
        public string drawingdimensions { get; set; }
        public string preoductcomments { get; set; }
        public string createdby { get; set; }
        public string creasationdate { get; set; }
        public List<Material> materials { get; set; }
        public List<Operation> operations { get; set; }
        public List<Other> others { get; set; }
    }

    public class ValueProduction
    {
        public string name { get; set; }
        public string value { get; set; }
    }

