using System.Collections.Generic;
using Bronze.Maths;
using OpenGL;

namespace Bronze.Graphics
{
    public class Shader
    {
        internal uint Handle;

        internal Shader(uint handle) => Handle = handle;

        public int GetUniformLocation(string name) => Gl.GetUniformLocation(Handle, name);

        public void SetUniform(int location, float value) => Gl.Uniform1(location, value);

        public void SetUniform(int location, int value) => Gl.Uniform1(location, value);

        public void SetUniform(int location, uint value) => Gl.Uniform1(location, value);

        public void SetUniform(int location, params float[] values) => Gl.Uniform1(location, values);

        public void SetUniform(int location, params int[] values) => Gl.Uniform1(location, values);

        public void SetUniform(int location, params uint[] values) => Gl.Uniform1(location, values);

        public void SetUniform(int location, Vector2 vector) => Gl.Uniform2(location, vector.X, vector.Y);

        public void SetUniform(int location, Vector3 vector) => Gl.Uniform3(location, vector.X, vector.Y, vector.Z);

        public void SetUniform(int location, Vector4 vector) =>
            Gl.Uniform4(location, vector.X, vector.Y, vector.Z, vector.W);

        public void SetUniform(int location, params Vector2[] vectors)
        {
            var list = new List<float>();
            foreach(var vector in vectors)
            {
                list.AddRange(vector.Values);
            }

            Gl.Uniform2(location, list.ToArray());
        }

        public void SetUniform(int location, params Vector3[] vectors)
        {
            var list = new List<float>();
            foreach(var vector in vectors)
            {
                list.AddRange(vector.Values);
            }

            Gl.Uniform3(location, list.ToArray());
        }

        public void SetUniform(int location, params Vector4[] vectors)
        {
            var list = new List<float>();
            foreach(var vector in vectors)
            {
                list.AddRange(vector.Values);
            }

            Gl.Uniform4(location, list.ToArray());
        }

        public void SetUniform(int location, Matrix2 matrix) => Gl.UniformMatrix2(location, true, matrix.SingleIndexedValues);

        public void SetUniform(int location, Matrix3 matrix) => Gl.UniformMatrix3(location, true, matrix.SingleIndexedValues);

        public void SetUniform(int location, Matrix4 matrix) => Gl.UniformMatrix4(location, true, matrix.SingleIndexedValues);

        public void SetUniform(int location, params Matrix2[] matrices)
        {
            var list = new List<float>();
            foreach(var matrix in matrices)
            {
                list.AddRange(matrix.SingleIndexedValues);
            }

            Gl.UniformMatrix2(location, true, list.ToArray());
        }

        public void SetUniform(int location, params Matrix3[] matrices)
        {
            var list = new List<float>();
            foreach(var matrix in matrices)
            {
                list.AddRange(matrix.SingleIndexedValues);
            }

            Gl.UniformMatrix3(location, true, list.ToArray());
        }

        public void SetUniform(int location, params Matrix4[] matrices)
        {
            var list = new List<float>();
            foreach(var matrix in matrices)
            {
                list.AddRange(matrix.SingleIndexedValues);
            }

            Gl.UniformMatrix4(location, true, list.ToArray());
        }

        public void Bind() => Gl.UseProgram(Handle);

        public void Unbind() => Gl.UseProgram(0);
    }
}