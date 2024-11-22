using System;
using System.Collections.Generic;
using System.IO;

namespace CDN_Simulator
{
    class CDN
    {
        private List<EdgeServer> edgeServers;

        public CDN(List<EdgeServer> edgeServers)
        {
            this.edgeServers = edgeServers;
        }

        public void HandleRequest(Request request)
        {
            // Handle the request using the edge servers
            foreach (EdgeServer edgeServer in edgeServers)
            {
                if (edgeServer.HasFile(request.File))
                {
                    edgeServer.HandleRequest(request);
                    return;
                }
            }

            // If no edge server has the file, use the nearest edge server to fetch from the origin
            EdgeServer closestEdgeServer = GetClosestEdgeServer();
            if (closestEdgeServer != null)
            {
                closestEdgeServer.HandleRequest(request);
            }
            else
            {
                Console.WriteLine("No suitable server found to handle the request.");
            }
        }

        private EdgeServer GetClosestEdgeServer()
        {
            if (edgeServers.Count == 0)
                return null;

            EdgeServer closest = edgeServers[0];
            foreach (EdgeServer edgeServer in edgeServers)
            {
                if (edgeServer.Weight < closest.Weight)
                {
                    closest = edgeServer;
                }
            }
            return closest;
        }
    }
}