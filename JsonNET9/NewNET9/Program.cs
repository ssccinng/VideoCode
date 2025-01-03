﻿// See https://aka.ms/new-console-template for more information
using NodeId = int;
using Distance = int;

return;
Console.WriteLine("Hello, World!");

PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>();

priorityQueue.Enqueue(1, 1);
priorityQueue.Enqueue(1, 2);

while (priorityQueue.Count > 0)
{
    Console.WriteLine(priorityQueue.Dequeue());
}

return;
PriorityQueue_NonGeneric_Tests.PriorityQueue_DijkstraSmokeTest();



public  class PriorityQueue_NonGeneric_Tests
    {
        public record struct Graph(Edge[][] nodes);
        public record struct Edge(NodeId neighbor, Distance weight);

        public static void PriorityQueue_DijkstraSmokeTest()
        {
            var graph = new Graph([
                [new Edge(1, 7), new Edge(2, 9), new Edge(5, 14)],
                [new Edge(0, 7), new Edge(2, 10), new Edge(3, 15)],
                [new Edge(0, 9), new Edge(1, 10), new Edge(3, 11), new Edge(5, 2)],
                [new Edge(1, 15), new Edge(2, 11), new Edge(4, 6)],
                [new Edge(3, 6), new Edge(5, 9)],
                [new Edge(0, 14), new Edge(2, 2), new Edge(4, 9)],
            ]);

            NodeId startNode = 0;

            (NodeId node, Distance distance)[] expectedDistances =
            [
                (0, 0),
                (1, 7),
                (2, 9),
                (3, 20),
                (4, 20),
                (5, 11),
            ];

            (NodeId node, Distance distance)[] actualDistances = RunDijkstra(graph, startNode);
            foreach (var VARIABLE in actualDistances)
            {
                Console.WriteLine($"{VARIABLE.node} - {VARIABLE.distance}");
            }
            // Assert.Equal(expectedDistances, actualDistances);
        }

        public static (NodeId node, Distance distance)[] RunDijkstra(Graph graph, NodeId startNode)
        {
            Distance[] distances = Enumerable.Repeat(int.MaxValue, graph.nodes.Length).ToArray();
            var queue = new PriorityQueue<NodeId, Distance>();

            distances[startNode] = 0;
            queue.Enqueue(startNode, 0);

            do
            {
                NodeId nodeId = queue.Dequeue();
                Distance nodeDistance = distances[nodeId];

                foreach (Edge edge in graph.nodes[nodeId])
                {
                    Distance distance = distances[edge.neighbor];
                    Distance newDistance = nodeDistance + edge.weight;
                    if (newDistance < distance)
                    {
                        distances[edge.neighbor] = newDistance;
                        // Simulate priority update by attempting to remove the entry
                        // before re-inserting it with the new distance.
                         queue.Remove(edge.neighbor, out _, out _);
                        queue.Enqueue(edge.neighbor, newDistance);
                    }
                }
            }
            while (queue.Count > 0);

            return distances.Select((distance, nodeId) => (nodeId, distance)).ToArray();
        }
    }