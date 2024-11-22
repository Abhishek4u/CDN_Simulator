namespace CDN_Simulator
{
    class OriginServer : Server
    {
        private static OriginServer instance;

        private OriginServer(string name, string location, int weight)
            : base(name, location, weight)
        {
        }

        public static OriginServer Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new InvalidOperationException("Origin server is not initialized.");
                }
                return instance;
            }
        }

        public static void Initialize(string name, string location, int weight)
        {
            if (instance != null)
            {
                throw new InvalidOperationException("Origin server is already initialized.");
            }
            instance = new OriginServer(name, location, weight);
        }

        public string GetFilePath(string file)
        {
            string filePath = $"{Location}\\{file}";
            if (File.Exists(filePath))
            {
                return filePath;
            }
            else
            {
                return null;
            }
        }
    }
}