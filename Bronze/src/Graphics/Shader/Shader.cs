using System;
using System.Collections.Generic;
using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class Shader : GraphicsResource
    {
        internal Shader(uint handle) : base(handle, Gl.UseProgram, Gl.DeleteProgram) => UniformLocations = new Dictionary<string, int>();

        private Dictionary<string, int> UniformLocations { get; }

        private int GetUniformLocation(string name)
        {
            if(UniformLocations.ContainsKey(name)) return UniformLocations[name];

            int location = Gl.GetUniformLocation(Handle, name);
            UniformLocations.Add(name, location);

            if(location == -1) throw new Exception($"Uniform variable \"{name}\" could not be found.");
            return location;
        }

        public void SetUniform(string name, float value) => Gl.ProgramUniform1(Handle, GetUniformLocation(name), value);

        public void SetUniform(string name, int value) => Gl.ProgramUniform1(Handle, GetUniformLocation(name), value);

        public void SetUniform(string name, uint value) => Gl.ProgramUniform1(Handle, GetUniformLocation(name), value);

        public void SetUniform(string name, params float[] values) => Gl.ProgramUniform1(Handle, GetUniformLocation(name), values);

        public void SetUniform(string name, params int[] values) => Gl.ProgramUniform1(Handle, GetUniformLocation(name), values);

        public void SetUniform(string name, params uint[] values) => Gl.ProgramUniform1(Handle, GetUniformLocation(name), values);

        public void SetUniform(string name, Vector2 vector) => Gl.ProgramUniform2(Handle, GetUniformLocation(name), vector.X, vector.Y);

        public void SetUniform(string name, Vector3 vector) => Gl.ProgramUniform3(Handle, GetUniformLocation(name), vector.X, vector.Y, vector.Z);

        public void SetUniform(string name, Vector4 vector) =>
            Gl.ProgramUniform4(Handle, GetUniformLocation(name), vector.X, vector.Y, vector.Z, vector.W);

        public void SetUniform(string name, params Vector2[] vectors)
        {
            var list = new List<float>();
            foreach(var vector in vectors) list.AddRange(vector.Values);

            Gl.ProgramUniform2(Handle, GetUniformLocation(name), list.ToArray());
        }

        public void SetUniform(string name, params Vector3[] vectors)
        {
            var list = new List<float>();
            foreach(var vector in vectors) list.AddRange(vector.Values);

            Gl.ProgramUniform3(Handle, GetUniformLocation(name), list.ToArray());
        }

        public void SetUniform(string name, params Vector4[] vectors)
        {
            var list = new List<float>();
            foreach(var vector in vectors) list.AddRange(vector.Values);

            Gl.ProgramUniform4(Handle, GetUniformLocation(name), list.ToArray());
        }

        public void SetUniform(string name, Matrix2 matrix) =>
            Gl.ProgramUniformMatrix2(Handle, GetUniformLocation(name), true, matrix.SingleIndexedValues);

        public void SetUniform(string name, Matrix3 matrix) =>
            Gl.ProgramUniformMatrix3(Handle, GetUniformLocation(name), true, matrix.SingleIndexedValues);

        public void SetUniform(string name, Matrix4 matrix) =>
            Gl.ProgramUniformMatrix4(Handle, GetUniformLocation(name), true, matrix.SingleIndexedValues);

        public void SetUniform(string name, params Matrix2[] matrices)
        {
            var list = new List<float>();
            foreach(var matrix in matrices) list.AddRange(matrix.SingleIndexedValues);

            Gl.ProgramUniformMatrix2(Handle, GetUniformLocation(name), true, list.ToArray());
        }

        public void SetUniform(string name, params Matrix3[] matrices)
        {
            var list = new List<float>();
            foreach(var matrix in matrices) list.AddRange(matrix.SingleIndexedValues);

            Gl.ProgramUniformMatrix3(Handle, GetUniformLocation(name), true, list.ToArray());
        }

        public void SetUniform(string name, params Matrix4[] matrices)
        {
            var list = new List<float>();
            foreach(var matrix in matrices) list.AddRange(matrix.SingleIndexedValues);

            Gl.ProgramUniformMatrix4(Handle, GetUniformLocation(name), true, list.ToArray());
        }

        public void SetUniform(string name, Transform transform) => SetUniform(name, transform.TransformationMatrix);

        public void SetUniform(string name, Color color) => SetUniform(name, color.ToVector());
    }
}