using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulation
{
	struct Diagonal
	{
		public Diagonal(int start, int end)
		{
			this.start = start;
			this.end = end;
		}

		public override string ToString() => $"({start}, {end})";

		int start, end;
	}
}
