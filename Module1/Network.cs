using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using draw_network;

namespace Module1;

public class Network
{
    public List<Node> Nodes { get; set; } = new List<Node>();
    public List<Link> Links { get; set; } = new List<Link>();

    public Network()
    {
        Clear();
    }

    public Network(string fileName)
    {
        ReadFromFile(fileName);
    }

    public void Clear()
    {
        Nodes.Clear();
        Links.Clear();
    }

    public void AddNode(Node node)
    {
        node.Index = Nodes.Count;
        Nodes.Add(node);
    }

    public void AddLink(Link link)
    {
        Links.Add(link);
    }

    public string Serialization()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"{Nodes.Count} # Number of Nodes.");
        sb.AppendLine($"{Links.Count} # Number of Links.");
        
        foreach (Node node in Nodes)
        {
            sb.AppendLine($"{node.Center.X},{node.Center.Y},{node.Text}");
        }

        foreach (Link link in Links)
        {
            sb.AppendLine($"{link.FromNode.Index},{link.ToNode.Index},{link.Cost}");
        }

        return sb.ToString();
    }

    public void SaveIntoFile(string fileName)
    {
        File.WriteAllText(fileName, Serialization());
    }

    public string ReadNextLine(StringReader reader)
    {
        string line = reader.ReadLine();

        while (String.IsNullOrEmpty(line))
        {
            
        }
        if (line.Contains('#'))
            line = line.Substring(0, line.IndexOf('#'));

        return line.Trim();
    }

    public void Deserialize(string fileContent)
    {
        Clear();
        using (StringReader sr = new StringReader(fileContent))
        {
            int numOfNodes = Int32.Parse(ReadNextLine(sr));
            int numOfLinks = Int32.Parse(ReadNextLine(sr));

            for (int i = 0; i < numOfNodes; i++)
            {
                string[] nodeValues = ReadNextLine(sr).Split(',');
                Node node = new Node(this, 
                    new Point(double.Parse(nodeValues[0]), double.Parse(nodeValues[1])),
                    nodeValues[2]);
            }

            for (int j = 0; j < numOfLinks; j++)
            {
                String[] linkValues = ReadNextLine(sr).Split(',');
                int fromNodeIndex = Int32.Parse(linkValues[0]);
                int toNodeIndex = Int32.Parse(linkValues[1]);
                int cost = Int32.Parse(linkValues[2]);

                Link link = new Link(this,
                    Nodes[fromNodeIndex],
                    Nodes[toNodeIndex],
                    cost);
            }
        }
    }

    public void ReadFromFile(String fileName)
    {
        Deserialize(File.ReadAllText(fileName));
    }

    public bool ValidateNetwork(Network network, string fileName)
    {
        string serializedNetwork = Serialization();
        SaveIntoFile(fileName);
        ReadFromFile(fileName);
        string deserializedNetwork = Serialization();

        if (serializedNetwork.Equals(deserializedNetwork))
            return true;
        return false;
    }

    private Rect GetBounds()
    {
        double minX = Nodes.Select(n => n.Center.X).Min();
        double minY = Nodes.Select(n => n.Center.Y).Min();
        double maxX = Nodes.Select(n => n.Center.X).Max();
        double maxY = Nodes.Select(n => n.Center.Y).Max();

        /*
        foreach (Node node in Nodes)
        {
            if (node.Center.X < minX) minX = node.Center.X;
            if (node.Center.Y < minY) minY = node.Center.Y;
            if (node.Center.X > maxX) maxX = node.Center.X;
            if (node.Center.Y > maxY) maxY = node.Center.Y;
        }
        */

        return new Rect(new Point(minX - 10.0, minY - 10.0), new Point(maxX + 10.0, maxY + 10.0));
    }
 
    public void Draw(Canvas canvas)
    {
        Rect bounds = GetBounds();
        canvas.Width = bounds.Width;
        canvas.Height = bounds.Height;
        canvas.ClipToBounds = true;
        canvas.DrawRectangle(bounds, Brushes.Transparent, Brushes.Orange, 2.0);

        foreach (Link link in Links)
        {
            link.Draw(canvas);
        }

        foreach (Link link in Links)
        {
            link.DrawLabel(canvas);
        }
        
        foreach (Node node in Nodes)
        {
            node.Draw(canvas);
        }
    }
}