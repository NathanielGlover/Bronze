using System;
using System.Collections.Generic;
using Bronze.Graphics;

namespace Bronze.Maths
{
    public abstract class ParametricShape : Shape
    {
        public static int VertexApproximationCount { get; set; } = 64;

        public abstract Func<float, Vector2> ParametricFunction { get; }

        public override Vertices GenerateFromInitial(Vector2 initialVertex, float initialExteriorAngle = 0,
            WindingOrder windingOrder = WindingOrder.CounterClockwise)
        {
            var tempVertices = GenerateAroundCentroid(Vector2.Zero, 0, false, windingOrder);
            float alignmentAngle = initialExteriorAngle - (tempVertices[1] - tempVertices[0]).Direction;
            var transform = new Transform {LocalOrigin = tempVertices[0]};
            transform.Translate(-tempVertices[0]);
            transform.Translate(initialVertex);
            transform.Rotate(alignmentAngle);
            return transform.ApplyTransform(tempVertices);
        }

        public override Vertices GenerateAroundCentroid(Vector2 centroid, float vertexAlignmentRay = 0, bool alignAroundRay = true,
            WindingOrder windingOrder = WindingOrder.CounterClockwise)
        {
            var vertices = new List<Vector2>(VertexApproximationCount);

            float parameterStep = 2 * Math.Pi / VertexApproximationCount;
            for(int i = 0; i < VertexApproximationCount; i++)
            {
                vertices.Add(ParametricFunction(i * parameterStep));
            }

            float alignmentAngle = vertexAlignmentRay - (alignAroundRay
                                       ? (vertices[0].Direction + vertices[1].Direction) / 2
                                       : vertices[0].Direction);
            
            if(windingOrder == WindingOrder.Clockwise)
            {
                vertices.Reverse(1, VertexApproximationCount - 1);
            }

            var result = new Vertices(vertices, Vertices.DataType.TriangleFan);
            var transform = new Transform();
            transform.Translate(centroid);
            transform.Rotate(alignmentAngle);

            return transform.ApplyTransform(result);
        }
    }
}