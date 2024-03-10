namespace BegrepAPI.Models
{
    public class Concept
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string PrefLabel { get; set; }
        public string AltLabel { get; set; }
        public Definition Definition { get; set; }
    }
}
