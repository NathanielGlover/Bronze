using System.Collections.Generic;
using System.Linq;

namespace Bronze.Maths
{
    public abstract class Polygon : Shape
    {
        public abstract int NumVertices { get; }

        public abstract List<float> SideLengths { get; }

        public abstract List<float> ExteriorAngles { get; }

        public override Vertices GenerateFromInitial(Vector2 initialVertex, float initialExteriorAngle = 0,
            WindingOrder windingOrder = WindingOrder.CounterClockwise)
        {
            var tempSides = new List<Complex>(NumVertices);
            var tempVertices = new List<Complex>(NumVertices)
                {(Complex) initialVertex, (Complex) initialVertex + SideLengths[0] * Math.Exp(initialExteriorAngle * Math.I)};

            tempSides.Add(SideLengths[0] * Math.Exp(initialExteriorAngle * Math.I));

            for(int i = 1; i < NumVertices - 1; i++)
            {
                tempSides.Add(tempSides[i - 1] * Math.Exp(ExteriorAngles[i - 1] * Math.I) * SideLengths[i] / tempSides[i - 1].Magnitude);
                tempVertices.Add(tempVertices[i] + tempSides[i]);
            }

            if(windingOrder == WindingOrder.Clockwise)
            {
                tempVertices.Reverse(1, NumVertices - 1);
            }

            return new Vertices((from tempVertex in tempVertices select (Vector2) tempVertex).ToList());
        }

        public override Vertices GenerateAroundCentroid(Vector2 centroid, float vertexAlignmentRay = 0, bool alignAroundRay = true,
            WindingOrder windingOrder = WindingOrder.CounterClockwise)
        {
            var vertices = GenerateFromInitial(Vector2.Zero, 0, windingOrder);
            var transformedVertices = new List<Vector2>(NumVertices);

            var currentCentroid = new Vector2();
            currentCentroid = vertices.Aggregate(currentCentroid, (current, tempVertex) => (Vector2) (current + tempVertex));
            currentCentroid *= 1f / NumVertices;

            for(int i = 0; i < NumVertices; i++)
            {
                transformedVertices.Add((Vector2) vertices[i]);
                transformedVertices[i] -= currentCentroid;
            }

            float alignmentAngle = vertexAlignmentRay - (alignAroundRay
                                       ? (transformedVertices[0].Direction + transformedVertices[1].Direction) / 2
                                       : transformedVertices[0].Direction);

            vertices = new Vertices(transformedVertices);
            var transform = new Transform();
            transform.Rotate(alignmentAngle);
            transform.Translate(centroid);

            return transform.ApplyTransform(vertices);
        }
    }
}