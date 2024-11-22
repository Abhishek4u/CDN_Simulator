namespace CDN_Simulator
{
        class Program
    {
        static void Main(string[] args)
        {
            CreateOriginServer();

            List<EdgeServer> edgeServers = CreateEdgeServers();

            // Create CDN and add the edge servers
            CDN cdn = new CDN(edgeServers);

            FetchFiles(cdn);
        }

        static void CreateOriginServer()
        {
            // Create the OriginServer singleton instance
            Console.Write("Enter the folder location for the Origin Server: ");
            string originFolderPath = Console.ReadLine();
            OriginServer.Initialize("Origin Server", originFolderPath, 30); // Initialize the origin server
        }

        static List<EdgeServer> CreateEdgeServers()
        {
            // Get number of edge servers from the user
            Console.Write("Enter the number of edge servers: ");
            int numEdgeServers = int.Parse(Console.ReadLine());

            List<EdgeServer> edgeServers = new List<EdgeServer>();
            for (int i = 0; i < numEdgeServers; i++)
            {
                Console.Write($"Enter name for Edge Server {i + 1}: ");
                string name = Console.ReadLine();

                Console.Write($"Enter full folder path for Edge Server {i + 1}: ");
                string location = Console.ReadLine();

                Console.Write($"Enter weight (time) for Edge Server {i + 1}: ");
                int weight = int.Parse(Console.ReadLine());

                edgeServers.Add(new EdgeServer(name, location, weight));
            }

            return edgeServers;
        }

        static void FetchFiles(CDN cdn)
        {
            while (true)
            {
                Console.Write("Enter the file name to request (or 'exit' to quit): ");
                string fileName = Console.ReadLine();
                if (fileName.ToLower() == "exit")
                    break;

                cdn.HandleRequest(new Request(fileName));
            }
        }
    }
}