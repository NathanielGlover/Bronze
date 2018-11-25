using System;
using System.Collections.Generic;
using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class Shader
    {
        internal readonly uint Handle;

        internal Shader(uint handle)
        {
            Handle = handle;
            UniformLocations = new Dictionary<string, int>();
        }

        private Dictionary<string, int> UniformLocations { get; }

        private int GetUniformLocation(string name)
        {
            Bind();
            if(UniformLocations.ContainsKey(name)) return UniformLocations[name];

            int location = Gl.GetUniformLocation(Handle, name);
            if(location == -1)
            {
                throw new Exception($"Uniform variable \"{name}\" could not be found.");
            }

            UniformLocations.Add(name, location);
            return location;
        }

        public void SetUniform(string name, float value) => Gl.Uniform1(GetUniformLocation(name), value);

        public void SetUniform(string name, int value) => Gl.Uniform1(GetUniformLocation(name), value);

        public void SetUniform(string name, uint value) => Gl.Uniform1(GetUniformLocation(name), value);

        public void SetUniform(string name, params float[] values) => Gl.Uniform1(GetUniformLocation(name), values);

        public void SetUniform(string name, params int[] values) => Gl.Uniform1(GetUniformLocation(name), values);

        public void SetUniform(string name, params uint[] values) => Gl.Uniform1(GetUniformLocation(name), values);

        public void SetUniform(string name, Vector2 vector) => Gl.Uniform2(GetUniformLocation(name), vector.X, vector.Y);

        public void SetUniform(string name, Vector3 vector) => Gl.Uniform3(GetUniformLocation(name), vector.X, vector.Y, vector.Z);

        public void SetUniform(string name, Vector4 vector) =>
            Gl.Uniform4(GetUniformLocation(name), vector.X, vector.Y, vector.Z, vector.W);

        public void SetUniform(string name, params Vector2[] vectors)
        {
            var list = new List<float>();
            foreach(var vector in vectors)
            {
                list.AddRange(vector.Values);
            }

            Gl.Uniform2(GetUniformLocation(name), list.ToArray());
        }

        public void SetUniform(string name, params Vector3[] vectors)
        {
            var list = new List<float>();
            foreach(var vector in vectors)
            {
                list.AddRange(vector.Values);
            }

            Gl.Uniform3(GetUniformLocation(name), list.ToArray());
        }

        public void SetUniform(string name, params Vector4[] vectors)
        {
            var list = new List<float>();
            foreach(var vector in vectors)
            {
                list.AddRange(vector.Values);
            }

            Gl.Uniform4(GetUniformLocation(name), list.ToArray());
        }

        public void SetUniform(string name, Matrix2 matrix) => Gl.UniformMatrix2(GetUniformLocation(name), true, matrix.SingleIndexedValues);

        public void SetUniform(string name, Matrix3 matrix) => Gl.UniformMatrix3(GetUniformLocation(name), true, matrix.SingleIndexedValues);

        public void SetUniform(string name, Matrix4 matrix) => Gl.UniformMatrix4(GetUniformLocation(name), true, matrix.SingleIndexedValues);

        public void SetUniform(string name, params Matrix2[] matrices)
        {
            var list = new List<float>();
            foreach(var matrix in matrices)
            {
                list.AddRange(matrix.SingleIndexedValues);
            }

            Gl.UniformMatrix2(GetUniformLocation(name), true, list.ToArray());
        }

        public void SetUniform(string name, params Matrix3[] matrices)
        {
            var list = new List<float>();
            foreach(var matrix in matrices)
            {
                list.AddRange(matrix.SingleIndexedValues);
            }

            Gl.UniformMatrix3(GetUniformLocation(name), true, list.ToArray());
        }

        public void SetUniform(string name, params Matrix4[] matrices)
        {
            var list = new List<float>();
            foreach(var matrix in matrices)
            {
                list.AddRange(matrix.SingleIndexedValues);
            }

            Gl.UniformMatrix4(GetUniformLocation(name), true, list.ToArray());
        }

        public void SetUniform(string name, Transform transform) => SetUniform(name, transform.TransformationMatrix);

        public void Bind() => Gl.UseProgram(Handle);

        public void Unbind() => Gl.UseProgram(0);
    }
}