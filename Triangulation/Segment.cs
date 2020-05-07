using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulation
{
	struct Segment
	{
		public Segment(Vertex start, Vertex end)
		{
			this.start = start;
			this.end = end;
		}

		public bool Intersects(Segment other)
		{

			if (((other.start.IsLeftFrom(start, end) && other.end.IsLeftFrom(end, start)) ||
				(other.end.IsLeftFrom(start, end) && other.start.IsLeftFrom(end, start))) &&
				((start.IsLeftFrom(other.start, other.end) && end.IsLeftFrom(other.end, other.start)) ||
				(end.IsLeftFrom(other.start, other.end) && start.IsLeftFrom(other.end, other.start))))
			{
				return true;
			}

			return start.IsOnSegment(other) || end.IsOnSegment(other) ||
				other.start.IsOnSegment(this) || other.end.IsOnSegment(this);
		}

		public Vertex start, end;
	}
}
