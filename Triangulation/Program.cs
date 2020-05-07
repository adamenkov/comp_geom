using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Triangulation
{
	class Program
	{
		static void Main(string[] args)
		{
			Polygon polygon = new Polygon();

			using (var reader = new StreamReader(args[0]))
			{
				string line;

				while ((line = reader.ReadLine()) != null)
				{
					var coordinates = Regex.Matches(line, @"\-?\d+");
					polygon.AddVertex(int.Parse(coordinates[0].Value), int.Parse(coordinates[1].Value));
				}
			}

			IList<Diagonal> diagonals = polygon.Triangulate();

			for (int index = 0; index < polygon.vertices.Count; ++index)
			{
				Vertex vertex = polygon.vertices[index];

				System.Console.WriteLine($"{index,3}: {vertex,-10} ear: {vertex.isEar}");
			}

			System.Console.WriteLine();

			for (int index = 0; index < diagonals.Count; ++index)
			{
				Diagonal diagonal = diagonals[index];

				System.Console.WriteLine($"{index + 1,3}: {diagonal,-10}");
			}
		}
	}
}
