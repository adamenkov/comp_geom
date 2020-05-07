using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulation
{
	struct Vertex
	{
		public Vertex(int index, int x, int y)
		{
			this.index = index;
			this.x = x;
			this.y = y;

			isEar = false;
		}

		public bool IsLeftFrom(Vertex start, Vertex end) => (new Triangle(start, end, this).SignedAreaDoubled > 0);
		public bool IsLeftFrom(Segment segment) => IsLeftFrom(segment.start, segment.end);
		public bool IsLeftFromOrOn(Vertex start, Vertex end) => (new Triangle(start, end, this).SignedAreaDoubled >= 0);
		public bool IsOnLine(Vertex start, Vertex end) => new Triangle(start, end, this).SignedAreaDoubled == 0;

		internal bool IsOnSegment(Segment segment)
		{
			if (!IsOnLine(segment.start, segment.end))
			{
				return false;
			}

			if (segment.start.x == segment.end.x)
			{
				return ((segment.start.y <= y) && (y <= segment.end.y)) || ((segment.end.y <= y) && (y <= segment.start.y));
			}

			return ((segment.start.x <= x) && (x <= segment.end.x)) || ((segment.end.x <= x) && (x <= segment.start.x));
		}

		public override string ToString() => $"({x}, {y})";

		public int x, y;

		public int index;
		public bool isEar;
	}
}
