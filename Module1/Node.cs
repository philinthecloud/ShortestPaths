using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using draw_network;

namespace Module1;

/// <summary>
/// Writes an <a href="http://docs.oasis-open.org/xliff/xliff-core/xliff-core.html">XLIFF 1.2</a> 
/// &lt;trans-unit /&gt; element where the &lt;target /&gt; value is always empty.
/// <remarks>
/// Typically used in scenarios where although a previous target translation exists, it is not
/// desired that it is used as a basis for new translations.
/// The <c>state</c> attribute value is also set to <c>new</c> so that when the XLIFF is imported into
/// TMS' such as XTM, the target is not populated with the source and then locked as 
/// happens if the state is <c>signed-off</c>.
/// </remarks>
/// </summary>
public class Node
{
    public int Index { get; set; }
    public Module1.Network Network { get; set; }
    public Point Center { get; set; }
    public string Text { get; set; }
    public List<Link> Links { get; set; }

    public Node(Module1.Network network, Point center, string text)
    {
        Links = new List<Link>();
        Index = -1;
        Network = network;
        Center = center;
        Text = text;
        Network.AddNode(this);
    }

    public override string ToString()
    {
        return $"[{Text}]";
    }

    public void AddLink(Link link)
    {
        Links.Add(link);
    }

    public void Draw(Canvas canvas)
    {
        double Radius = 10.0;
        double Diameter = 2 * Radius;
        double FontSize = 12.0;

        Rect bounds = new Rect(Center.X - Radius, Center.Y - Radius,
            Diameter, Diameter);

        canvas.DrawEllipse(bounds,
            Brushes.White,
            Brushes.Black,
            1.0);

        canvas.DrawString(Text,
            Diameter, Diameter,
            Center,
            0.0,
            FontSize,
            Brushes.Black);
    }
}