using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulation
{
	class Triangle
	{
		public Triangle(Vertex a, Vertex b, Vertex c)
		{
			this.a = a;
			this.b = b;
			this.c = c;
		}

		public int SignedAreaDoubled => b.x * c.y - b.y * c.x		// | a.x a.y 1 |
								+ c.x * a.y - c.y * a.x		// | b.x b.y 1 |
								+ a.x * b.y - a.y * b.x;	// | c.x c.y 1 |

		Vertex a, b, c;
	}
}
