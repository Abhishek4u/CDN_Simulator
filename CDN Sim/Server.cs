namespace CDN_Simulator
{
    abstract class Server
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Weight { get; set; }

        public Server(string name, string location, int weight)
        {
            Name = name;
            Location = location;
            Weight = weight;
        }
    }
}