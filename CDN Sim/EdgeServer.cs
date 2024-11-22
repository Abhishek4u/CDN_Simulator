namespace CDN_Simulator
{
    class EdgeServer : Server
    {
        private HashSet<string> cachedFiles;

        public EdgeServer(string name, string location, int weight)
            : base(name, location, weight)
        {
            cachedFiles = new HashSet<string>();

            // Create folder if it doesn't exist
            if (!Directory.Exists(Location))
            {
                Directory.CreateDirectory(Location);
                Console.WriteLine($"Created directory: {Location}");
            }
        }

        public bool HasFile(string file)
        {
            return cachedFiles.Contains(file);
        }

        public void HandleRequest(Request request)
        {
            if (HasFile(request.File))
            {
                Console.WriteLine($"Request for {request.File} handled by {Name} at {Location} ( ==> Time: {Weight}ms)");
            }
            else
            {
                string originFilePath = OriginServer.Instance.GetFilePath(request.File);
                if (!string.IsNullOrEmpty(originFilePath))
                {
                    string edgeFilePath = FetchFileFromOrigin(originFilePath, request.File);
                    Console.WriteLine($"Request for {request.File} handled by {Name} after fetching from origin. File stored at {edgeFilePath} ( ==> Time: {Weight + OriginServer.Instance.Weight}ms)");
                }
                else
                {
                    Console.WriteLine($"File {request.File} not found on origin server.");
                }
            }
        }

        private string FetchFileFromOrigin(string originFilePath, string file)
        {
            string edgeFilePath = $"{Location}\\{file}";
            File.Copy(originFilePath, edgeFilePath, true);
            cachedFiles.Add(file);
            return edgeFilePath;
        }
    }
}