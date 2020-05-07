using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Triangulation
{
	class Polygon
	{
		public void AddVertex(int x, int y)
		{
			vertices.Add(new Vertex(vertices.Count, x, y));
		}

		public bool IsDiagonal(int indexA, int indexB)
		{
			return IsInCone(indexA, indexB) && IsInCone(indexB, indexA) && IsDiagonalHelper(indexA, indexB);
		}

		private bool IsDiagonalHelper(int indexA, int indexB)
		{
			Segment potentialDiagonal = new Segment(vertices[indexA], vertices[indexB]);

			for (int index = 0; index < vertices.Count; ++index)
			{
				int nextIndex = NextIndex(index);

				if ((index != indexA) && (nextIndex != indexA) &&
					(index != indexB) && (nextIndex != indexB) &&
					potentialDiagonal.Intersects(new Segment(vertices[index], vertices[nextIndex])))
				{
					return false;
				}
			}

			return true;
		}

		private bool IsInCone(int indexA, int indexB)
		{
			Vertex prevVertex = PrevVertex(indexA);
			Vertex vertexA = vertices[indexA];
			Vertex nextVertex = NextVertex(indexA);

			Vertex vertexB = vertices[indexB];

			if (nextVertex.IsLeftFromOrOn(prevVertex, vertexA))
			{
				return prevVertex.IsLeftFrom(vertexA, vertexB) && nextVertex.IsLeftFrom(vertexB, vertexA);
			}
			else
			{
				return !(nextVertex.IsLeftFromOrOn(vertexA, vertexB) && prevVertex.IsLeftFromOrOn(vertexB, vertexA));
			}
		}

		int NextIndex(int index) => ++index % vertices.Count;
		int PrevIndex(int index) => (--index < 0) ? (vertices.Count - 1) : index;

		Vertex NextVertex(int index) => vertices[NextIndex(index)];
		Vertex PrevVertex(int index) => vertices[PrevIndex(index)];

		public IList<Diagonal> Triangulate()
		{
			IList<Diagonal> diagonals = new List<Diagonal>();

			for (int index = 0; index < vertices.Count; ++index)
			{
				ComputeIsEar(index);
			}

			IList<Vertex> verticesCopy = new List<Vertex>(vertices);

			while (vertices.Count > 3)
			{
				for (int index = 0; index < vertices.Count; ++index)
				{
					Vertex vertex = vertices[index];
					if (vertex.isEar)
					{
						diagonals.Add(new Diagonal(PrevVertex(index).index, NextVertex(index).index));
						
						vertices.RemoveAt(index);
						if (vertices.Count <= 3)
							break;

						ComputeIsEar(PrevIndex(index));
						ComputeIsEar(NextIndex(PrevIndex(index)));
						--index;
					}
				}
			}

			vertices = new List<Vertex>(verticesCopy);

			return diagonals;
		}

		private void ComputeIsEar(int index)
		{
			Vertex vertex = vertices[index];
			vertex.isEar = IsDiagonal(PrevIndex(index), NextIndex(index));
			vertices[index] = vertex;
		}

		public IList<Vertex> vertices = new List<Vertex>();
	}
}
