using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Input;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using Game;
using Minecraft_Clone_Tutorial_Series_videoproj.Graphics;

namespace Game.Graphics
{
    class Model
    {


        Texture7 texture;

        public Vector3 position;

        private List<Vector3> vertices = new List<Vector3>();
        private List<Vector3> normals = new List<Vector3>();
        private List<Vector2> texCoords = new List<Vector2>();
        private List<int> indices = new List<int>();

        private int vaoID,vboID, eboID;
        private int vertexCount;

        public Model(string fileName, Vector3 position)
        {
            this.position = position;
            LoadObj(fileName, position);
            InitBuffers();
            
            
        }

        private void LoadObj(string fileName,Vector3 position)
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(fileName);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("v "))
                    {
                        string[] tokens = line.Split(' ');
                        float x = float.Parse(tokens[1], CultureInfo.InvariantCulture);
                        float y = float.Parse(tokens[2], CultureInfo.InvariantCulture);
                        float z = float.Parse(tokens[3], CultureInfo.InvariantCulture);
                        vertices.Add(new Vector3(x, y, z));
                    }
                    else if (line.StartsWith("vn "))
                    {
                        string[] tokens = line.Split(' ');
                        float x = float.Parse(tokens[1], CultureInfo.InvariantCulture);
                        float y = float.Parse(tokens[2], CultureInfo.InvariantCulture);
                        float z = float.Parse(tokens[3], CultureInfo.InvariantCulture);
                        normals.Add(new Vector3(x, y, z));
                    }
                    else if (line.StartsWith("vt "))
                    {
                        string[] tokens = line.Split(' ');
                        float u = float.Parse(tokens[1],  CultureInfo.InvariantCulture);
                        float v = float.Parse(tokens[2],  CultureInfo.InvariantCulture);
                        //float u = Convert.ToSingle(tokens[1]);
                        //float v = Convert.ToSingle(tokens[2]);
                        texCoords.Add(new Vector2(u, v));
                    }
                    else if (line.StartsWith("f "))
                    {
                        string[] tokens = line.Split(' ');
                        for (int i = 1; i < tokens.Length-1; i++)
                        {
                            string[] parts = tokens[i].Split('/');
                            int vi = int.Parse(parts[0]) - 1;
                            int ti = int.Parse(parts[1]) - 1;
                            //int ni = int.Parse(parts[2]) - 1;
                            vertices.Add(vertices[vi]);
                            texCoords.Add(texCoords[ti]);
                            //normals.Add(normals[ni]);
                            indices.Add(vertexCount++);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        private void InitBuffers()
        {
            float[] vertexData = new float[vertexCount * 5];
            for (int i = 0, vi = 0, ti = 0, ni = 0; i < vertexCount; i++, vi += 1, ti += 1, ni += 1)
            {
                vertexData[i * 5] = vertices[vi].X;
                vertexData[i * 5 + 1] = vertices[vi].Y;
                vertexData[i * 5 + 2] = vertices[vi].Z;
                //vertexData[i * 8 + 3] = normals[ni].X;
                //vertexData[i * 8 + 4] = normals[ni].Y;
                //vertexData[i * 8 + 5] = normals[ni].Z;
                //vertexData[i * 8 + 3] = 0;
                //vertexData[i * 8 + 4] = 0;
                //vertexData[i * 8 + 5] = 0;
                vertexData[i * 5 + 3] = texCoords[ti].X;
                vertexData[i * 5 + 4] = texCoords[ti].Y;
            }

            GL.GenVertexArrays(1, out vaoID);
            GL.BindVertexArray(vaoID);

            GL.GenBuffers(1, out vboID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboID);
            GL.BufferData(BufferTarget.ArrayBuffer, vertexData.Length * sizeof(float), vertexData, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            //GL.EnableVertexAttribArray(2);
            //GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            GL.GenBuffers(1, out eboID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(int), indices.ToArray(), BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);

            //texture = new Texture("metal.jpg");
            //texture.Bind();
        }

        public void Draw()
        {
            
            GL.BindVertexArray(vaoID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboID);
            GL.DrawElements(BeginMode.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
        }
    }
}
