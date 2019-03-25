using System;
using System.Collections.Generic;
using System.Linq;
using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class VertexArrayBuilder
    {
        private class VertexAttribute
        {
            public int Id { get; }
            public byte[] Data { get; } //Serialized data
            public VertexAttribType Type { get; } //Type of unserialized data
            public int AttribCount { get; } //Number of items in unserialized data that constitute a full vertex
            public int AttribSize => AttribCount * Type.SizeOf(); //Bytes occupied by one full vertex
            public bool ShouldNormalize { get; }
            public bool ShouldInterleave { get; }

            public VertexAttribute(int id, byte[] data, VertexAttribType type, int attribCount, bool shouldNormalize, bool shouldInterleave)
            {
                Id = id;
                Data = data;
                Type = type;
                AttribCount = attribCount;
                ShouldNormalize = shouldNormalize;
                ShouldInterleave = shouldInterleave;
            }
        }

        private static byte[] ToByteArray<T>(IEnumerable<T> objects)
        {
            var byteList = new List<byte>();
            foreach(dynamic obj in objects)
            {
                var bytes = (byte[]) BitConverter.GetBytes(obj);
                byteList.AddRange(bytes);
            }

            return byteList.ToArray();
        }

        private readonly List<VertexAttribute> vertexAttributes = new List<VertexAttribute>();

        public void ClearCurrentBuild() => vertexAttributes.Clear();

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<float> data, int size, bool interleave = true)
        {
            var bytes = ToByteArray(data);
            var attribute = new VertexAttribute(location, bytes, VertexAttribType.Float, size, false, interleave);
            vertexAttributes.Add(attribute);
            return this;
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<double> data, int size, bool interleave = true)
        {
            var bytes = ToByteArray(data);
            var attribute = new VertexAttribute(location, bytes, VertexAttribType.Double, size, false, interleave);
            vertexAttributes.Add(attribute);
            return this;
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<short> data, int size, bool normalize = false, bool interleave = true)
        {
            var bytes = ToByteArray(data);
            var attribute = new VertexAttribute(location, bytes, VertexAttribType.Short, size, normalize, interleave);
            vertexAttributes.Add(attribute);
            return this;
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<int> data, int size, bool normalize = false, bool interleave = true)
        {
            var bytes = ToByteArray(data);
            var attribute = new VertexAttribute(location, bytes, VertexAttribType.Int, size, normalize, interleave);
            vertexAttributes.Add(attribute);
            return this;
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<byte> data, int size, bool normalize = false, bool interleave = true)
        {
            var attribute = new VertexAttribute(location, data.ToArray(), VertexAttribType.UnsignedByte, size, normalize, interleave);
            vertexAttributes.Add(attribute);
            return this;
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<ushort> data, int size, bool normalize = false, bool interleave = true)
        {
            var bytes = ToByteArray(data);
            var attribute = new VertexAttribute(location, bytes, VertexAttribType.UnsignedShort, size, normalize, interleave);
            vertexAttributes.Add(attribute);
            return this;
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<uint> data, int size, bool normalize = false, bool interleave = true)
        {
            var bytes = ToByteArray(data);
            var attribute = new VertexAttribute(location, bytes, VertexAttribType.UnsignedInt, size, normalize, interleave);
            vertexAttributes.Add(attribute);
            return this;
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<Vector2> data, bool interleave = true)
        {
            var rawData = new List<float>();
            foreach(var vector in data)
            {
                rawData.AddRange(vector.Values);
            }

            return AddVertexAttribute(location, rawData, 2, interleave);
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<Vector3> data, bool interleave = true)
        {
            var rawData = new List<float>();
            foreach(var vector in data)
            {
                rawData.AddRange(vector.Values);
            }

            return AddVertexAttribute(location, rawData, 3, interleave);
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<Vector4> data, bool interleave = true)
        {
            var rawData = new List<float>();
            foreach(var vector in data)
            {
                rawData.AddRange(vector.Values);
            }

            return AddVertexAttribute(location, rawData, 4, interleave);
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<Vector2D> data, bool interleave = true)
        {
            var rawData = new List<double>();
            foreach(var vector in data)
            {
                rawData.AddRange(vector.Values);
            }

            return AddVertexAttribute(location, rawData, 2, interleave);
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<Vector3D> data, bool interleave = true)
        {
            var rawData = new List<double>();
            foreach(var vector in data)
            {
                rawData.AddRange(vector.Values);
            }

            return AddVertexAttribute(location, rawData, 3, interleave);
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<Vector4D> data, bool interleave = true)
        {
            var rawData = new List<double>();
            foreach(var vector in data)
            {
                rawData.AddRange(vector.Values);
            }

            return AddVertexAttribute(location, rawData, 4, interleave);
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<Vector2I> data, bool normalize = false, bool interleave = true)
        {
            var rawData = new List<int>();
            foreach(var vector in data)
            {
                rawData.AddRange(vector.Values);
            }

            return AddVertexAttribute(location, rawData, 2, normalize, interleave);
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<Vector3I> data, bool normalize = false, bool interleave = true)
        {
            var rawData = new List<int>();
            foreach(var vector in data)
            {
                rawData.AddRange(vector.Values);
            }

            return AddVertexAttribute(location, rawData, 3, normalize, interleave);
        }

        public VertexArrayBuilder AddVertexAttribute(int location, IEnumerable<Vector4I> data, bool normalize = false, bool interleave = true)
        {
            var rawData = new List<int>();
            foreach(var vector in data)
            {
                rawData.AddRange(vector.Values);
            }

            return AddVertexAttribute(location, rawData, 4, normalize, interleave);
        }

        public VertexArray BuildVertexArray(Vertices.DataType primitiveType)
        {
            //Variables for entire buffer
            int dataCount = (from attribute in vertexAttributes select attribute.Data.Length).Aggregate((i, i1) => i + i1);
            int vertexSize = (from attribute in vertexAttributes select attribute.AttribSize).Aggregate((i, i1) => i + i1);
            int numVertices = dataCount / vertexSize;

            //Variables for first (interleaved) section of buffer
            var interleavedData = (from a in vertexAttributes where a.ShouldInterleave select a).ToList();
            int interleavedCount = interleavedData.Count == 0 ? 0 : (from a in interleavedData select a.Data.Length).Aggregate((i, i1) => i + i1);
            int interleavedVertexSize = interleavedCount == 0 ? 0 : (from a in interleavedData select a.AttribSize).Aggregate((i, i1) => i + i1);
            int numInterleavedVertices = interleavedVertexSize == 0 ? 0 : interleavedCount / interleavedVertexSize;

            //Variables for second (batched) section of buffer
            var batchedData = (from a in vertexAttributes where !a.ShouldInterleave select a).ToList();

            //Initialize vertex buffer
            uint bufferHandle = Gl.GenBuffer();
            var bufferData = new byte[dataCount];

            //Interleave any data requested for interleaving into first section of buffer
            for(int i = 0; i < numInterleavedVertices; i++)
            {
                int offset = 0;
                foreach(var attribute in interleavedData)
                {
                    for(int k = 0; k < attribute.AttribSize; k++)
                    {
                        bufferData[i * interleavedVertexSize + offset + k] = attribute.Data[i * attribute.AttribSize + k];
                    }

                    offset += attribute.AttribSize;
                }
            }

            //Populate second section of buffer with remaining (batched) data
            int batchedOffset = interleavedCount;
            foreach(var attribute in batchedData)
            {
                for(int j = 0; j < attribute.Data.Length; j++)
                {
                    bufferData[batchedOffset + j] = attribute.Data[j];
                }

                batchedOffset += attribute.Data.Length;
            }

            //Begin setting vertex array parameters
            uint handle = Gl.GenVertexArray();
            Gl.BindVertexArray(handle);
            Gl.BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) bufferData.Length, bufferData, BufferUsage.StaticDraw);

            //Set vertex attribute pointer for interleaved attributes
            int attribOffset = 0;
            foreach(var a in interleavedData)
            {
                if(a.Type == VertexAttribType.Double)
                {
                    Gl.VertexAttribLPointer((uint) a.Id, a.AttribCount, a.Type, interleavedVertexSize, new IntPtr(attribOffset));
                }
                else
                {
                    Gl.VertexAttribPointer((uint) a.Id, a.AttribCount, a.Type, a.ShouldNormalize, interleavedVertexSize, new IntPtr(attribOffset));
                }

                Gl.EnableVertexAttribArray((uint) a.Id);

                attribOffset += a.AttribSize;
            }

            //Set vertex attribute pointer for batched attributes
            foreach(var a in batchedData)
            {
                if(a.Type == VertexAttribType.Double)
                {
                    Gl.VertexAttribLPointer((uint) a.Id, a.AttribCount, a.Type, a.AttribSize, new IntPtr(attribOffset));
                }
                else
                {
                    Gl.VertexAttribPointer((uint) a.Id, a.AttribCount, a.Type, a.ShouldNormalize, a.AttribSize, new IntPtr(attribOffset));
                }

                Gl.EnableVertexAttribArray((uint) a.Id);

                attribOffset += a.Data.Length;
            }

            //Stop setting vertex array parameters and clear vertex attribute list
            Gl.BindVertexArray(0);
            ClearCurrentBuild();

            return new VertexArray(handle, numVertices, primitiveType);
        }
    }
}