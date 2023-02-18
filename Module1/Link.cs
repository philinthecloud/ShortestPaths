using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using draw_network;

namespace Module1;

public class Link
{
    public Network Network { get; set; }
    public Node FromNode { get; set; }
    public Node ToNode { get; set; }
    public double Cost { get; set; }

    public Link(Module1.Network network, Node fromNode, Node toNode, double cost)
    {
        Network = network;
        FromNode = fromNode;
        ToNode = toNode;
        Cost = cost;
        Network.AddLink(this);
        FromNode.AddLink(this);
    }

    public override string ToString()
    {
        return $"[{FromNode.Text}] --> [{ToNode.Text}] ({Cost})";
    }

    public void Draw(Canvas canvas)
    {
        double Radius = 10.0;
        double Diameter = 2 * Radius;
        double FontSize = 12.0;
    
        canvas.DrawLine(FromNode.Center,
            ToNode.Center,
            Brushes.Brown,
            1.0);
    }

    public void DrawLabel(Canvas canvas)
    {
        double Radius = 10.0;
        double Diameter = 2 * Radius;
        double FontSize = 12.0;

        double dx = ToNode.Center.X - FromNode.Center.X;
        double dy = ToNode.Center.Y - FromNode.Center.Y;

        double angle = ((180 / Math.PI) * Math.Atan2(dx, dy)) - 90.0;

        double posX = 0.67 * FromNode.Center.X + 0.33 * ToNode.Center.X;
        double posY = 0.67 * FromNode.Center.Y + 0.33 * ToNode.Center.Y;

        Rect labelBounds = new Rect(posX - Radius, posY - Radius,
            Diameter, Diameter);

        canvas.DrawEllipse(labelBounds,
            Brushes.White,
            null,
            0.0);

        canvas.DrawString(Cost.ToString(),
            Diameter, Diameter,
            new Point(posX, posY),
            angle,
            FontSize,
            Brushes.Black);
    }
}