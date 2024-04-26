using Minecraft_Clone_Tutorial_Series_videoproj.Graphics;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmSharp;
using Game.World;
using Game.Graphics;
using static System.Reflection.Metadata.BlobBuilder;
using System.Diagnostics;
using Game;
using static Minecraft_Clone_Tutorial_Series_videoproj.Game;
//using GlmSharp;

namespace Minecraft_Clone_Tutorial_Series_videoproj.World
{
    internal class Chunk
    {
        public List<Vector3> chunkVerts;
        public List<Vector2> chunkUVs;
        public List<Vector3> chunkColors;
        public List<uint> chunkIndices;
        public uint chunkIndicesCount;

        public float[,] heightmap;

        public System.Boolean Builded = false;
        public System.Boolean Genered = false;
        public System.Boolean Rebuild = false;
        

        const int SIZE = 16;
        const int HEIGHT = 256;
        public Vector3 position;
        

        public uint indexCount;

        public VAO chunkVAO;
        public VBO chunkVertexVBO;
        public VBO chunkUVVBO;
        public VBO chunkColorsVBO;
        public IBO chunkIBO;
        //protected Stopwatch stopwatch = new Stopwatch();

        Texture7 texture;
        public Block[,,] chunkBlocks;

        //public Block DirtBlock = new Block(new Vector3(0, 0, 0),BlockType.DIRT);
        //public Block GrassBlock = new Block(new Vector3(0, 0, 0), BlockType.GRASS);
        //public Block EmptyBlock = new Block(new Vector3(0, 0, 0), BlockType.EMPTY);
        public Chunk(Vector3 postition)
        {
            this.position = postition;

            chunkVerts = new List<Vector3>();
            chunkUVs = new List<Vector2>();
            chunkColors = new List<Vector3>();
            chunkIndices = new List<uint>();
            chunkBlocks = new Block[SIZE, HEIGHT, SIZE];

        //stopwatch.Start();
        //Console.WriteLine("Chunk GeN Started {0}", stopwatch.ElapsedMilliseconds);
        //GenChunk(new Vector3(0,0,0));
        //Console.WriteLine("Chunk GeN hightmap Stopped {0}", stopwatch.ElapsedMilliseconds);
        //GenBlocks();
        //Console.WriteLine("Chunk GeN Blocks Stopped {0}", stopwatch.ElapsedMilliseconds);
        //GenFaces(new Vector3mine(0, 0, 0));
        //Console.WriteLine("Chunk GeN Faces Stopped {0}", stopwatch.ElapsedMilliseconds);
        //Builded = true;
        //BuildChunk();
        //    Builded = true;
            //Console.WriteLine("Chunk Build Chunk Stopped {0}", stopwatch.ElapsedMilliseconds);
            //stopwatch.Stop();
            /*chunkVerts.Clear();
            chunkUVs.Clear();
            chunkColors.Clear();
            //chunkBlocks=new Block[0,0,0];
            chunkIndicesCount = (uint)chunkIndices.Count;
            chunkIndices.Clear();


            ////chunkVerts = null;
            ////chunkUVs = null;
            ////chunkColors = null;
            ////chunkIndices = null;

            GC.Collect();*/

            //chunkBlocks = new Block[0, 0, 0];

        }

        public void  GenChunk(Vector3 absposition) {
            heightmap = new float[SIZE, SIZE];

            SimplexNoise.Noise.Seed = 1212121212;

            for (int x = 0; x < SIZE; x++)
            {
                for (int z = 0; z < SIZE; z++)
                {
                    heightmap[x,z]=SimplexNoise.Noise.CalcPixel2D(x+(int)position.X+2000000,z+(int)position.Z+2000000, 0.01f);
                }
            }

            
        } // generate the data
        public void GenBlocks() {

            for (int x = 0; x < SIZE; x++)
            {
                for (int z = 0; z < SIZE; z++)
                {

                    int columnHeight = (int)(heightmap[x,z]/10);
                    for (int y = 0; y < HEIGHT; y++)
                    {
                        BlockType type = BlockType.EMPTY;
                        if (y < columnHeight - 1)
                        {
                            type = BlockType.DIRT;
                            //chunkBlocks[x, y, z] = new Block(new Vector3(x, y, z), BlockType.DIRT);
                        }
                        if (y == columnHeight - 1)
                        {
                            type= BlockType.GRASS;
                        }

                        chunkBlocks[x, y, z] = new Block(type);
                        
                    }
                }
            }

            // old code
            /*for(int i = 0; i < 3; i++)
            {
                Block block = new Block(new Vector3(i, 0, 0));

                int faceCount = 0;

                if(i == 0)
                {
                    var leftFaceData = block.GetFace(Faces.LEFT);
                    chunkVerts.AddRange(leftFaceData.vertices);
                    chunkUVs.AddRange(leftFaceData.uv);
                    faceCount++;
                }
                if (i == 2)
                {
                    var rightFaceData = block.GetFace(Faces.RIGHT);
                    chunkVerts.AddRange(rightFaceData.vertices);
                    chunkUVs.AddRange(rightFaceData.uv);
                    faceCount++;
                }

                var frontFaceData = block.GetFace(Faces.FRONT);
                chunkVerts.AddRange(frontFaceData.vertices);
                chunkUVs.AddRange(frontFaceData.uv);

                var backFaceData = block.GetFace(Faces.BACK);
                chunkVerts.AddRange(backFaceData.vertices);
                chunkUVs.AddRange(backFaceData.uv);

                var topFaceData = block.GetFace(Faces.TOP);
                chunkVerts.AddRange(topFaceData.vertices);
                chunkUVs.AddRange(topFaceData.uv);

                var bottomFaceData = block.GetFace(Faces.BOTTOM);
                chunkVerts.AddRange(bottomFaceData.vertices);
                chunkUVs.AddRange(bottomFaceData.uv);

                faceCount += 4;

                AddIndices(faceCount);
            }*/
        } // generate the appropriate block faces given the data

        public void GenFaces(Vector3mine vector)
        {
            indexCount = 0;

            for (int x = 0; x < SIZE; x++)
            {
                for (int z = 0; z < SIZE; z++)
                {
                    int columnHeight = (int)(heightmap[x,z]/10);

                    for (int y = 0; y < HEIGHT; y++)
                    {
                        int numFaces = 0;

                        if (x == vector.X && y == vector.Y && z == vector.Z)

                        {

                            Console.WriteLine("test");
                        
                        }

                        if (chunkBlocks[x, y, z].type != BlockType.EMPTY)
                        {
                            //left faces
                            if (x > 0)
                            {
                                if (chunkBlocks[x - 1, y, z].type == BlockType.EMPTY)
                                {
                                    IntegrateFace(new Vector3(x, y, z), Faces.LEFT, chunkBlocks[x, y, z]);
                                    numFaces++;
                                }

                            }
                            else
                            {
                                if (y == columnHeight-1)
                                {
                                    IntegrateFace(new Vector3(x, y, z), Faces.LEFT, chunkBlocks[x, y, z]);
                                    numFaces++;
                                }
                            }
                            //right faces
                            if (x < SIZE - 1)
                            {
                                if (chunkBlocks[x + 1, y, z].type == BlockType.EMPTY)
                                {
                                    IntegrateFace(new Vector3(x, y, z), Faces.RIGHT, chunkBlocks[x, y, z]);
                                    numFaces++;
                                }

                            }
                            else
                            {
                                if (y == columnHeight-1)
                                {
                                    IntegrateFace(new Vector3(x, y, z), Faces.RIGHT, chunkBlocks[x, y, z]);
                                    numFaces++;
                                }
                            }
                            //top faces
                            if (y < HEIGHT - 1)
                            {
                                if (chunkBlocks[x, y + 1, z].type == BlockType.EMPTY)
                                {
                                    IntegrateFace(new Vector3(x, y, z), Faces.TOP, chunkBlocks[x, y, z]);
                                    numFaces++;
                                }
                            }
                            else
                            {
                                IntegrateFace(new Vector3(x, y, z), Faces.TOP, chunkBlocks[x, y, z]);
                                numFaces++;
                            }
                            //bottom faces
                            if (y > 0)
                            {
                                if (chunkBlocks[x, y - 1, z].type == BlockType.EMPTY)
                                {
                                    IntegrateFace(new Vector3(x, y, z), Faces.BOTTOM, chunkBlocks[x, y, z]);
                                    numFaces++;
                                }
                            }
                            else
                            {
                                IntegrateFace(new Vector3(x, y, z), Faces.BOTTOM, chunkBlocks[x, y, z]);
                                numFaces++;
                            }
                            //front faces
                            if (z < SIZE - 1)
                            {
                                if (chunkBlocks[x, y, z + 1].type == BlockType.EMPTY)
                                {
                                    IntegrateFace(new Vector3(x, y, z), Faces.FRONT, chunkBlocks[x, y, z]);
                                    numFaces++;
                                }
                            }
                            else
                            {
                                if (y == columnHeight-1)
                                {
                                    IntegrateFace(new Vector3(x, y, z), Faces.FRONT, chunkBlocks[x, y, z]);
                                    numFaces++;
                                }
                            }
                            //back faces
                            if (z > 0)
                            {
                                if (chunkBlocks[x, y, z - 1].type == BlockType.EMPTY)
                                {
                                    IntegrateFace(new Vector3(x, y, z), Faces.BACK, chunkBlocks[x, y, z]);
                                    numFaces++;
                                }
                            }
                            else
                            {
                                if (y == columnHeight-1)
                                {
                                    IntegrateFace(new Vector3(x, y, z), Faces.BACK, chunkBlocks[x, y, z]);
                                    numFaces++;
                                }
                            }
                            AddIndices(numFaces);
                        }
                    }
                }
            }
        }

        public bool IsBlockedAO(float x, float y, float z)
        {

            if (x >= SIZE || x<0) return false;
            if (z >= SIZE || z<0) return false;
            if (y >= HEIGHT || y<0) return false;

            if (chunkBlocks[(int)x, (int)y, (int)z].type == BlockType.EMPTY) return false;
            return true;

        }

        public List<Vector3> GetAO(Vector3 vector, Faces face)
        {

            // Параметр затемнение одного угла к блоку
            float aoFactor = 0.2f;
            // Параметры углов
            float a, b, c, d, e, f, g, h;
            // Вершины затемнения
            vec4 lg = new vec4(1.0f);

            var color = new List<Vector3>() {
                        new Vector3(lg.x,lg.x,lg.x),
                        new Vector3(lg.w,lg.w,lg.w),
                        new Vector3(lg.z,lg.z,lg.z),
                        new Vector3(lg.y,lg.y,lg.y),

                    };

            switch (face)
            {
                case Faces.TOP:
                    a = IsBlockedAO(vector.X + 1, vector.Y + 1, vector.Z) ? aoFactor : 0;
                    b = IsBlockedAO(vector.X, vector.Y + 1, vector.Z + 1) ? aoFactor : 0;
                    c = IsBlockedAO(vector.X - 1, vector.Y + 1, vector.Z) ? aoFactor : 0;
                    d = IsBlockedAO(vector.X, vector.Y + 1, vector.Z - 1) ? aoFactor : 0;

                    e = IsBlockedAO(vector.X - 1, vector.Y + 1, vector.Z - 1) ? aoFactor : 0;
                    f = IsBlockedAO(vector.X - 1, vector.Y + 1, vector.Z + 1) ? aoFactor : 0;
                    g = IsBlockedAO(vector.X + 1, vector.Y + 1, vector.Z + 1) ? aoFactor : 0;
                    h = IsBlockedAO(vector.X + 1, vector.Y + 1, vector.Z - 1) ? aoFactor : 0;
                    lg.x -= c + d + e;
                    lg.y -= c + b + f;
                    lg.z -= a + b + g;
                    lg.w -= a + d + h;
                    color = new List<Vector3>() {
                        new Vector3(lg.x,lg.x,lg.x),
                        new Vector3(lg.w,lg.w,lg.w),
                        new Vector3(lg.z,lg.z,lg.z),
                        new Vector3(lg.y,lg.y,lg.y),

                    };
                    break;
                case Faces.BOTTOM:
                    a = IsBlockedAO(vector.X + 1, vector.Y - 1, vector.Z) ? aoFactor : 0;
                    b = IsBlockedAO(vector.X, vector.Y - 1, vector.Z + 1) ? aoFactor : 0;
                    c = IsBlockedAO(vector.X - 1, vector.Y - 1, vector.Z) ? aoFactor : 0;
                    d = IsBlockedAO(vector.X, vector.Y - 1, vector.Z - 1) ? aoFactor : 0;

                    e = IsBlockedAO(vector.X - 1, vector.Y - 1, vector.Z - 1) ? aoFactor : 0;
                    f = IsBlockedAO(vector.X - 1, vector.Y - 1, vector.Z + 1) ? aoFactor : 0;
                    g = IsBlockedAO(vector.X + 1, vector.Y - 1, vector.Z + 1) ? aoFactor : 0;
                    h = IsBlockedAO(vector.X + 1, vector.Y - 1, vector.Z - 1) ? aoFactor : 0;
                    lg.x -= c + d + e;
                    lg.y -= a + b + g;
                    lg.z -= c + b + f;
                    lg.w -= a + d + h;
                    color = new List<Vector3>() {
                        new Vector3(lg.x,lg.x,lg.x),
                        new Vector3(lg.w,lg.w,lg.w),
                        new Vector3(lg.z,lg.z,lg.z),
                        new Vector3(lg.y,lg.y,lg.y),

                    };
                    break;
                case Faces.RIGHT:
                    a = IsBlockedAO(vector.X + 1, vector.Y + 1, vector.Z) ? aoFactor : 0;
                    b = IsBlockedAO(vector.X + 1, vector.Y, vector.Z + 1) ? aoFactor : 0;
                    c = IsBlockedAO(vector.X + 1, vector.Y - 1, vector.Z) ? aoFactor : 0;
                    d = IsBlockedAO(vector.X + 1, vector.Y, vector.Z - 1) ? aoFactor : 0;

                    e = IsBlockedAO(vector.X + 1, vector.Y - 1, vector.Z - 1) ? aoFactor : 0;
                    f = IsBlockedAO(vector.X + 1, vector.Y - 1, vector.Z + 1) ? aoFactor : 0;
                    g = IsBlockedAO(vector.X + 1, vector.Y + 1, vector.Z + 1) ? aoFactor : 0;
                    h = IsBlockedAO(vector.X + 1, vector.Y + 1, vector.Z - 1) ? aoFactor : 0;
                    lg.x -= c + d + e;
                    lg.y -= d + a + h;
                    lg.z -= a + b + g;
                    lg.w -= b + c + f;
                    color = new List<Vector3>() {
                        new Vector3(lg.z,lg.z,lg.z),
                        new Vector3(lg.y,lg.y,lg.y),
                        new Vector3(lg.x,lg.x,lg.x),
                        new Vector3(lg.w,lg.w,lg.w),

                    };
                    break;
                case Faces.LEFT:
                    a = IsBlockedAO(vector.X - 1, vector.Y + 1, vector.Z) ? aoFactor : 0;
                    b = IsBlockedAO(vector.X - 1, vector.Y, vector.Z + 1) ? aoFactor : 0;
                    c = IsBlockedAO(vector.X - 1, vector.Y - 1, vector.Z) ? aoFactor : 0;
                    d = IsBlockedAO(vector.X - 1, vector.Y, vector.Z - 1) ? aoFactor : 0;

                    e = IsBlockedAO(vector.X - 1, vector.Y - 1, vector.Z - 1) ? aoFactor : 0;
                    f = IsBlockedAO(vector.X - 1, vector.Y - 1, vector.Z + 1) ? aoFactor : 0;
                    g = IsBlockedAO(vector.X - 1, vector.Y + 1, vector.Z + 1) ? aoFactor : 0;
                    h = IsBlockedAO(vector.X - 1, vector.Y + 1, vector.Z - 1) ? aoFactor : 0;
                    lg.x -= c + d + e;
                    lg.y -= a + b + g;
                    lg.z -= d + a + h;
                    lg.w -= b + c + f;
                    color = new List<Vector3>() {
                        new Vector3(lg.z,lg.z,lg.z),
                        new Vector3(lg.y,lg.y,lg.y),
                        new Vector3(lg.w,lg.w,lg.w),
                        new Vector3(lg.x,lg.x,lg.x),

                    };
                    break;
                case Faces.FRONT:
                    a = IsBlockedAO(vector.X, vector.Y + 1, vector.Z + 1) ? aoFactor : 0;
                    b = IsBlockedAO(vector.X + 1, vector.Y, vector.Z + 1) ? aoFactor : 0;
                    c = IsBlockedAO(vector.X, vector.Y - 1, vector.Z + 1) ? aoFactor : 0;
                    d = IsBlockedAO(vector.X - 1, vector.Y, vector.Z + 1) ? aoFactor : 0;

                    e = IsBlockedAO(vector.X - 1, vector.Y - 1, vector.Z + 1) ? aoFactor : 0;
                    f = IsBlockedAO(vector.X + 1, vector.Y - 1, vector.Z + 1) ? aoFactor : 0;
                    g = IsBlockedAO(vector.X + 1, vector.Y + 1, vector.Z + 1) ? aoFactor : 0;
                    h = IsBlockedAO(vector.X - 1, vector.Y + 1, vector.Z + 1) ? aoFactor : 0;
                    lg.x -= c + d + e;
                    lg.y -= a + b + g;
                    lg.z -= a + d + h;
                    lg.w -= b + c + f;
                    color = new List<Vector3>() {
                        new Vector3(lg.z,lg.z,lg.z),
                        new Vector3(lg.y,lg.y,lg.y),
                        new Vector3(lg.w,lg.w,lg.w),
                        new Vector3(lg.x,lg.x,lg.x),

                    };
                    break;
                case Faces.BACK:
                    a = IsBlockedAO(vector.X, vector.Y + 1, vector.Z - 1) ? aoFactor : 0;
                    b = IsBlockedAO(vector.X + 1, vector.Y, vector.Z - 1) ? aoFactor : 0;
                    c = IsBlockedAO(vector.X, vector.Y - 1, vector.Z - 1) ? aoFactor : 0;
                    d = IsBlockedAO(vector.X - 1, vector.Y, vector.Z - 1) ? aoFactor : 0;

                    e = IsBlockedAO(vector.X - 1, vector.Y - 1, vector.Z - 1) ? aoFactor : 0;
                    f = IsBlockedAO(vector.X + 1, vector.Y - 1, vector.Z - 1) ? aoFactor : 0;
                    g = IsBlockedAO(vector.X + 1, vector.Y + 1, vector.Z - 1) ? aoFactor : 0;
                    h = IsBlockedAO(vector.X - 1, vector.Y + 1, vector.Z - 1) ? aoFactor : 0;
                    lg.x -= c + d + e;
                    lg.y -= a + d + h;
                    lg.z -= a + b + g;
                    lg.w -= b + c + f;
                    color = new List<Vector3>() {
                        new Vector3(lg.z,lg.z,lg.z),
                        new Vector3(lg.y,lg.y,lg.y),
                        new Vector3(lg.x,lg.x,lg.x),
                        new Vector3(lg.w,lg.w,lg.w),

                    };
                    break;
            }
            return color;
        }
        public void IntegrateFace(Vector3 vector, Faces face, Block block)
        {
            var faceData = GetFace(face,vector, block);
            chunkVerts.AddRange(faceData.vertices);
            chunkUVs.AddRange(faceData.uv);
            faceData.color = GetAO(vector, face);
            chunkColors.AddRange(faceData.color);


        }
        public void AddIndices(int amtFaces)
        {
            for(int i = 0; i < amtFaces; i++)
            {
                chunkIndices.Add(0 + indexCount);
                chunkIndices.Add(1 + indexCount);
                chunkIndices.Add(2 + indexCount);
                chunkIndices.Add(2 + indexCount);
                chunkIndices.Add(3 + indexCount);
                chunkIndices.Add(0 + indexCount);

                indexCount += 4;
            }
        }


        public void ReBuildChunk()
        {

            //chunkVAO.VAOrebind();

            chunkVertexVBO.VBOrebind(chunkVerts);
            
            //chunkVAO.LinkToVAO(0, 3, chunkVertexVBO);

            chunkUVVBO.VBOrebind(chunkUVs);
            
            //chunkVAO.LinkToVAO(1, 2, chunkUVVBO);

            chunkColorsVBO.VBOrebind(chunkColors);
            
            //chunkVAO.LinkToVAO(2, 3, chunkColorsVBO);

            chunkIBO.IBOrebind(chunkIndices);
            chunkIBO.Unbind();

            chunkVerts.Clear();
            chunkUVs.Clear();
            chunkColors.Clear();
            ////
            //chunkBlocks=new Block[0,0,0];
            chunkIndicesCount = (uint)chunkIndices.Count;
            chunkIndices.Clear();


            ////chunkVerts = null;
            ////chunkUVs = null;
            ////chunkColors = null;
            ////chunkIndices = null;

            //******//GC.Collect();


            //texture = new Texture("atlas.png");
            //texture.Bind();
        } // take data and process it for rendering
        public void BuildChunk() {
            chunkVAO = new VAO();
            chunkVAO.Bind();

            chunkVertexVBO = new VBO(chunkVerts);
            chunkVertexVBO.Bind();
            chunkVAO.LinkToVAO(0, 3, chunkVertexVBO);

            chunkUVVBO = new VBO(chunkUVs);
            chunkUVVBO.Bind();
            chunkVAO.LinkToVAO(1, 2, chunkUVVBO);

            chunkColorsVBO = new VBO(chunkColors);
            chunkColorsVBO.Bind();
            chunkVAO.LinkToVAO(2, 3, chunkColorsVBO);

            chunkIBO = new IBO(chunkIndices);
            chunkIBO.Unbind();

            chunkVerts.Clear();
            chunkUVs.Clear();
            chunkColors.Clear();
            ////
            //chunkBlocks=new Block[0,0,0];
            chunkIndicesCount = (uint)chunkIndices.Count;
            chunkIndices.Clear();


            ////chunkVerts = null;
            ////chunkUVs = null;
            ////chunkColors = null;
            ////chunkIndices = null;

            ////****////GC.Collect();
            //this.Builded= true;

            //texture = new Texture("atlas.png");
            //texture.Bind();
        } // take data and process it for rendering
        public void Render(ShaderProgram program) // drawing the chunk
        {
            //program.Bind();
            chunkVAO.Bind();
            chunkIBO.Bind();
            //texture.Bind();
            GL.DrawElements(PrimitiveType.Triangles, (int)chunkIndicesCount, DrawElementsType.UnsignedInt, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public void Delete()
        {
            chunkVAO.Delete();
            chunkVertexVBO.Delete();
            chunkUVVBO.Delete();
            chunkIBO.Delete();
            //texture.Delete();
        }
    }
}
