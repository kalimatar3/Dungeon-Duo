using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class AstarAlgorthm
{
    public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal, HashSet<Vector2Int> mapposition) {
        if(!mapposition.Contains(goal)) return null;
        List<Node> openList = new List<Node>();
        HashSet<Vector2Int> closedList = new HashSet<Vector2Int>();

        Node startNode = new Node(start, null, 0, Heuristic(start, goal));
        openList.Add(startNode);

        while (openList.Count > 0) {
            // Lấy node có chi phí F nhỏ nhất trong open list
            Node currentNode = openList.OrderBy(n => n.F).First();

            // Nếu đã đến mục tiêu, xây dựng lại đường đi
            if (currentNode.Position == goal) {
                return ReconstructPath(currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode.Position);

            // Kiểm tra các neighbor
            foreach (Vector2Int neighbor in GetNeighbors(currentNode.Position, mapposition)) {
                if (closedList.Contains(neighbor)) {
                    continue;  // Bỏ qua nếu đã trong closed list
                }

                float tentativeG = currentNode.G + 1;  // Giả định rằng tất cả các ô có cùng chi phí

                Node neighborNode = openList.FirstOrDefault(n => n.Position == neighbor);
                if (neighborNode == null) {
                    // Nếu neighbor chưa có trong open list, thêm vào
                    neighborNode = new Node(neighbor, currentNode, tentativeG, Heuristic(neighbor, goal));
                    openList.Add(neighborNode);
                } else if (tentativeG < neighborNode.G) {
                    // Nếu tìm thấy đường tốt hơn đến neighbor, cập nhật nó
                    neighborNode.G = tentativeG;
                    neighborNode.Parent = currentNode;
                }
            }
        }
        // Không tìm thấy đường đi
        return null;
    }
    public static List<Vector2Int> GetNeighbors(Vector2Int nodePosition, HashSet<Vector2Int> mappositions) {
        HashSet<Vector2Int> neighbors = new HashSet<Vector2Int>();
        Vector2Int[] directions = {
            new Vector2Int(0, 1),  // Lên
            new Vector2Int(1, 0),  // Phải
            new Vector2Int(0, -1), // Xuống
            new Vector2Int(-1, 0)  // Trái
        };
        foreach (Vector2Int direction in directions) {
            Vector2Int neighborPos = nodePosition + direction;
            neighbors.Add(neighborPos);
        }
        neighbors.IntersectWith(mappositions);
        return neighbors.ToList();
    }
    public static List<Vector2Int> ReconstructPath(Node currentNode) {
        List<Vector2Int> path = new List<Vector2Int>();
        while (currentNode != null) {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }
        path.Reverse(); // Đường đi ngược lại từ đích về nguồn
        return path;
    }    
    public static float Heuristic(Vector2Int a, Vector2Int b) {
        return Vector2Int.Distance(a, b);
    }
}
public class Node {
    public Vector2Int Position;  // Vị trí của Node trong lưới
    public Node Parent;          // Node cha để xây dựng lại đường đi

    // G, H, F: Các chi phí trong thuật toán A*
    public float G; // Chi phí từ điểm bắt đầu đến Node này
    public float H; // Ước lượng chi phí từ Node này đến đích (heuristic)
    public float F { get { return G + H; } } // Tổng chi phí

    public Node(Vector2Int position, Node parent, float g, float h) {
        Position = position;
        Parent = parent;
        G = g;
        H = h;
    }
}
