namespace pdf_generate
{
    public class Produkts
    {
        public int Id { get; set; } = 0;
        public string Number { get; set; } = string.Empty;
        public string SketchName { get; set; } = string.Empty;
        public string Revision { get; set; } = string.Empty;
        public int Count { get; set; } = 0;
        public string Path { get; set; } = string.Empty;
        public List<OneProdukts> Produktss { get; set; } = new() { new() { Id = 1, Number = "adssa" },
        new() { Id = 1, Number = "adssa" },
        new() { Id = 1, Number = "adssa" },
        new() { Id = 1, Number = "adssa" },
        new() { Id = 1, Number = "adssa" },
        new() { Id = 1, Number = "adssa" },
        new() { Id = 1, Number = "adssa" },
        new() { Id = 1, Number = "adssa" }
        ,new() { Id = 1, Number = "adssa" }
        ,new() { Id = 1, Number = "adssa" }
        ,new() { Id = 1, Number = "adssa" }};
    }
}
