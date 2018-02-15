using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            long beaconCount = 0;
            long.TryParse(line, out beaconCount);

            if (beaconCount <= 2) continue;

            var polygon = new Polygon();
            for (var i = 0; i < beaconCount; i++)
            {
                var coordinates = Console.ReadLine().Split(' ');

                var point = new Point
                {
                    X = long.Parse(coordinates[0]),
                    Y = long.Parse(coordinates[1]),
                };
                polygon.Points.Add(point);
            }

            var roundedArea = Math.Round(polygon.Area);
            Console.WriteLine(roundedArea);
        }
    }
}

public class Polygon
{
    public List<Point> Points { get; set; }

    public double Area
    {
        get
        {
            var points = ComputeConvexHull();

            // https://www.codeproject.com/Tips/601272/Calculating-the-area-of-a-polygon
            var e = points.GetEnumerator();
            if (!e.MoveNext()) return 0;
            Point first = e.Current, last = first;

            double area = 0;
            while (e.MoveNext())
            {
                Point next = e.Current;
                area += next.X * last.Y - last.X * next.Y;
                last = next;
            }
            area += first.X * last.Y - last.X * first.Y;

            return area / 2;
        }
    }

    public Polygon()
    {
        Points = new List<Point>();
    }

    // http://loyc-etc.blogspot.com/2014/05/2d-convex-hull-in-c-45-lines-of-code.html
    private List<Point> ComputeConvexHull()
    {
        Points = new List<Point>(Points);
        Points.Sort((a, b) => a.X == b.X ? a.Y.CompareTo(b.Y) : (a.X > b.X ? 1 : -1));

        var hull = new List<Point>();
        int l = 0, u = 0; // size of lower and upper hulls

        // Builds a hull such that the output polygon starts at the leftmost point.
        for (int i = Points.Count - 1; i >= 0; i--)
        {
            Point p = Points[i], p1;

            // build lower hull (at end of output list)
            while (l >= 2 && ((p1 = hull.Last()) - hull[hull.Count - 2]) * (p - p1) >= 0)
            {
                hull.RemoveAt(hull.Count - 1);
                l--;
            }
            hull.Add(p);
            l++;

            // build upper hull (at beginning of output list)
            while (u >= 2 && ((p1 = hull.First()) - hull[1]) * (p - p1) <= 0)
            {
                hull.RemoveAt(0);
                u--;
            }
            if (u != 0) // when U=0, share the point added above
                hull.Insert(0, p);
            u++;
        }
        hull.RemoveAt(hull.Count - 1);
        return hull;
    }
}

public struct Point : IComparable<Point>
{
    public long X;
    public long Y;

    public int CompareTo(Point other)
    {
        if (X < other.X)
            return -1;
        if (X > other.X)
            return +1;
        if (Y < other.Y)
            return -1;
        if (Y > other.Y)
            return +1;
        return 0;
    }

    public static Point operator - (Point p1, Point p2)
    {
        return new Point { X = p1.X - p2.X, Y = p1.Y - p2.Y };
    }

    public static long operator *(Point p1, Point p2)
    {
        return p1.X * p2.Y - p1.Y * p2.X;
    }
}
