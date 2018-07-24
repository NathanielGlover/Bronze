using System.Collections.Generic;
using System.Linq;
using Bronze.Graphics;

namespace Bronze.Math
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
                {(Complex) initialVertex, (Complex) initialVertex + SideLengths[0] * Maths.Exp(initialExteriorAngle * Maths.I)};

            tempSides.Add(SideLengths[0] * Maths.Exp(initialExteriorAngle * Maths.I));

            for(int i = 1; i < NumVertices - 1; i++)
            {
                tempSides.Add(tempSides[i - 1] * Maths.Exp(ExteriorAngles[i - 1] * Maths.I) * SideLengths[i] / tempSides[i - 1].Magnitude);
                tempVertices.Add(tempVertices[i] + tempSides[i]);
            }

            if(windingOrder == WindingOrder.Clockwise)
            {
                tempVertices.Reverse(1, NumVertices - 1);
            }

            return new Vertices((from tempVertex in tempVertices select (Vector2) tempVertex).ToList(), Vertices.DataType.TriangleFan);
        }

        public override Vertices GenerateAroundCentroid(Vector2 centroid, float vertexAlignmentRay = 0, bool alignAroundRay = true,
            WindingOrder windingOrder = WindingOrder.CounterClockwise)
        {
            var vertices = GenerateFromInitial(Vector2.Zero, 0, windingOrder);
            var transformedVertexData = new List<Vector2>(NumVertices);

            var currentCentroid = new Vector2();
            currentCentroid = vertices.VertexData.Aggregate(currentCentroid, (current, tempVertex) => current + tempVertex);
            currentCentroid *= 1f / NumVertices;

            for(int i = 0; i < NumVertices; i++)
            {
                transformedVertexData.Add(vertices.VertexData[i]);
                transformedVertexData[i] -= currentCentroid;
            }

            float alignmentAngle = vertexAlignmentRay - (alignAroundRay
                                       ? (transformedVertexData[0].Direction + transformedVertexData[1].Direction) / 2
                                       : transformedVertexData[0].Direction);

            vertices = new Vertices(transformedVertexData, vertices.VertexDataType);
            var transform = new Transform();
            transform.Rotate(alignmentAngle);
            transform.Translate(centroid);

            return transform.ApplyTransform(vertices);
        }
    }
}