namespace Demo.Solitare.Entities
{
    public class Rank
    {
        public Rank(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}