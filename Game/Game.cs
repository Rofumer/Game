using Lib3d;
using OpenTK.Graphics.OpenGL4;
//using OpenTK.Graphics.ES11;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
//using System.Numerics;

using FontStashSharp.Platform;
//using FontStash.NET;
using OpenTK.Mathematics;
//using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Minecraft_Clone_Tutorial_Series_videoproj.Graphics;
using Minecraft_Clone_Tutorial_Series_videoproj.World;
using GlmSharp;
using Game.Graphics;
using System.Diagnostics;
//using SixLabors.ImageSharp;
using FontStashSharp;


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SkiaSharp;
using SixLabors.ImageSharp;
using System.Reflection.Metadata;
//using System.Numerics;
using Vector3 = OpenTK.Mathematics.Vector3;
using Vector4 = OpenTK.Mathematics.Vector4;
using System.Net.NetworkInformation;
using Game.World;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.CompilerServices;
//using System.Numerics;

namespace Minecraft_Clone_Tutorial_Series_videoproj
{


    // Game class that inherets from the Game Window Class
    internal class Game : GameWindow
    {


        public static Dictionary<Faces, List<Vector2>> GetUVsFromCoordinates(Dictionary<Faces, Vector2> coords)
        {
            Dictionary<Faces, List<Vector2>> faceData = new Dictionary<Faces, List<Vector2>>();
            foreach (var faceCoord in coords)
            {
                faceData[faceCoord.Key] = new List<Vector2>()
                {
                    new Vector2((faceCoord.Value.X+1f)/16f, (faceCoord.Value.Y+1f)/16f),
                    new Vector2((faceCoord.Value.X)/16f, (faceCoord.Value.Y+1f)/16f),
                    new Vector2((faceCoord.Value.X)/16f, (faceCoord.Value.Y)/16f),
                    new Vector2((faceCoord.Value.X+1f)/16f, (faceCoord.Value.Y)/16f),
                };
            }
            return faceData;
        }

        
        public static List<Vector3> AddTransformedVertices(List<Vector3> vertices, Vector3 vector)
        {
            List<Vector3> transformedVertices = new List<Vector3>();
            foreach (var vert in vertices)
            {
                transformedVertices.Add(new Vector3(vert.X + vector.X, vert.Y + vector.Y, vert.Z + vector.Z));
            }
            return transformedVertices;
        }
        public static FaceData GetFace(Faces face, Vector3 vector, Block block)
        {



            Dictionary<Faces, List<Vector2>> blockUV;/* = new Dictionary<Faces, List<Vector2>>(6)
        {
            {Faces.FRONT, new List<Vector2>(4)},
            {Faces.BACK, new List<Vector2>(4)},
            {Faces.TOP, new List<Vector2>(4)},
            {Faces.BOTTOM, new List<Vector2>(4)},
            {Faces.LEFT, new List<Vector2>(4)},
            {Faces.RIGHT, new List<Vector2>(4)},
        };*/


            var blockType = block.type;


            blockUV = GetUVsFromCoordinates(TextureData.blockTypeUVCoord[blockType]);



            var faces = new Dictionary<Faces, FaceData>
            {
                {Faces.FRONT, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.FRONT],vector),
                    uv = blockUV[Faces.FRONT]
                } },
                {Faces.BACK, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.BACK],vector),
                    uv = blockUV[Faces.BACK]
                } },
                {Faces.LEFT, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.LEFT],vector),
                    uv = blockUV[Faces.LEFT]
                } },
                {Faces.RIGHT, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.RIGHT], vector),
                    uv = blockUV[Faces.RIGHT]
                } },
                {Faces.TOP, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.TOP], vector),
                    uv = blockUV[Faces.TOP]
                } },
                {Faces.BOTTOM, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.BOTTOM], vector),
                    uv = blockUV[Faces.BOTTOM]
                } },

            };


            return faces[face];

        }

        public struct Vector3mine
        {
            //
            // Сводка:
            //     The X component of the Vector3.
            public int X;

            //
            // Сводка:
            //     The Y component of the Vector3.
            public int Y;

            //
            // Сводка:
            //     The Z component of the Vector3.
            public int Z;

            public Vector3mine(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }


        public struct Block
        {

            
            public BlockType type;

            public Block(BlockType type) { 
            
                
                this.type = type;   

            }


        }


        Thread myThread = new Thread(Point)
        { IsBackground = true,
            Priority= ThreadPriority.Highest
    };

        public static Thread myThread2 = new Thread(Point2)
        {
            IsBackground = true,
            Priority = ThreadPriority.Highest
        };

        

        public MainRenderWindow mainRenderWindow=new MainRenderWindow(2500,2000,"title",60);
        //mainRenderWindow = new MainRenderWindow(2500,2000,"title",60);

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            
                       

            if (e.Button == MouseButton.Right)
            {
                Console.WriteLine($"Looking at block X: {RayCastBlock.position.X}, Y: {RayCastBlock.position.Y}, Z: {RayCastBlock.position.Z}, Type: {RayCastBlock.type}");

                //if 



                PlaceBlock(new Vector3(RayCastBlock.position.X+ (int)RayCastBlock.norm.X, RayCastBlock.position.Y + (int)RayCastBlock.norm.Y, RayCastBlock.position.Z + (int)RayCastBlock.norm.Z), new Block(BlockType.DIRT));
                
            }
            if (e.Button == MouseButton.Left)
            {
                Console.WriteLine($"Looking at block X: {RayCastBlock.position.X}, Y: {RayCastBlock.position.Y}, Z: {RayCastBlock.position.Z}, Type: {RayCastBlock.type}");

                //if 



                PlaceBlock(new Vector3(RayCastBlock.position.X, RayCastBlock.position.Y, RayCastBlock.position.Z), new Block(BlockType.EMPTY));
            }


            // Pass coordinates of point to a_Position

        }


        public static Block PlaceBlock(Vector3 vector, Block block)
        {


            vector.X+=ChunksRenderDistance * 16 / 2;
            vector.Z+=ChunksRenderDistance * 16 / 2;

            vector.X = (int)(vector.X % (16 * ChunksRenderDistance));
            vector.Z = (int)(vector.Z % (16 * ChunksRenderDistance));

            if (vector.X < 0) { vector.X += (ChunksRenderDistance * 16); }
            if (vector.Z < 0) { vector.Z += (ChunksRenderDistance * 16); }

            var number = Array.IndexOf(temparray1, (int)(vector.X / 16) * ChunksRenderDistance + (int)(vector.Z / 16));

            var xdelta = (int)(number / ChunksRenderDistance);
            var zdelta = (int)(number % ChunksRenderDistance);
            
            chunk[xdelta, zdelta].chunkBlocks[((int)vector.X % 16), (int)vector.Y, ((int)vector.Z % 16)]=block;
            chunk[xdelta, zdelta].GenFaces(new Vector3mine((int)(0), (int)(0), (int)(0)));
            chunk[xdelta, zdelta].ReBuildChunk();

            return block;
        }


        public static Block GetBlock(Vector3 vector) {
            Block block = new Block(BlockType.EMPTY);

            //nreturn block;

            //Camera.absposition

            ////var deltax= ((int)(Camera.absposition.X / 16)) * 16;
            ////var deltaz = ((int)(Camera.absposition.Z / 16)) * 16;

            ////vector.X-=deltax;
            ////vector.Z -=deltaz;

            vector.X=(int)(vector.X%(16*ChunksRenderDistance));
            vector.Z=(int)(vector.Z%(16*ChunksRenderDistance));

            if (vector.X < 0) {vector.X+=(ChunksRenderDistance*16); }
            if (vector.Z < 0) { vector.Z += (ChunksRenderDistance * 16); }

            //vector.X = Math.Sign(vector.X) * (int)(vector.X % (16 * ChunksRenderDistance));
            //vector.Z = Math.Sign(vector.Z) * (int)(vector.Z % (16 * ChunksRenderDistance));

            //int chunkXPos = (int)(vector.X) / 16;
            //int chunkZPos = (int)(vector.Z) / 16;


            //var number = Array.IndexOf(temparray1, chunkXPos * ChunksRenderDistance + chunkZPos);

            //chunkXPos = (int)(number / ChunksRenderDistance);
            //chunkZPos = (int)(number % ChunksRenderDistance);

            //int blockchunkXPos = (int)(vector.X);
            //int blockchunkZPos = (int)(vector.Z);

            if (vector.X  <0 || vector.X >= ChunksRenderDistance * 16 || vector.Y < 0 || vector.Y>255 || vector.Z>= ChunksRenderDistance * 16 || vector.Z <0) return block;

            if(chunkX> ChunksRenderDistance/2)
            { 
            //if (Game.chunkX >0)
            //{


            var number = Array.IndexOf(temparray1, (int)(vector.X / 16) * ChunksRenderDistance + (int)(vector.Z / 16));

            var xdelta = (int)(number/ChunksRenderDistance);
            var zdelta = (int)(number%ChunksRenderDistance);


            //return chunk[(int)vector.X / 16, (int)vector.Z / 16].chunkBlocks[(int)vector.X % 16, (int)vector.Y, (int)vector.Z % 16];
            return chunk[xdelta, zdelta].chunkBlocks[((int)vector.X % 16), (int)vector.Y, ((int)vector.Z % 16)];

            }
            else

            {

              return block;
            }

            //return new Block(new Vector3mine(),BlockType.EMPTY) ;

        }

        public RayCastBlock RayCast(Vector3 a, Vector3 dir, float maxDist)
        {

            Block block= new Block(BlockType.EMPTY);
            RayCastBlock RayCastBlock = new RayCastBlock(new Vector3mine(0,0,0),new Vector3(0,0,0),new Vector3(0, 0, 0),new Vector3(0, 0, 0),BlockType.EMPTY);


            //dir.Y = -30;

            //float sqr = dir.X * dir.X + dir.Y * dir.Y + dir.Z * dir.Z;
            //dir= Vector3.Multiply(dir,(float)(1.0f / Math.Sqrt(sqr)));


            //dir.Scale(4,1,4);

            //float sqr = v.x * v.x + v.y * v.y + v.z * v.z;
            //return v * (1.0f / Mth.Sqrt(sqr));

            float px = a.X+0.5f;
            float py = a.Y+0.5f;
            float pz = a.Z+0.5f;

            float dx = dir.X;
            float dy = dir.Y;
            float dz = dir.Z;

            float t = 0.0f;
            int ix = (int)Math.Floor(px);
            int iy = (int)Math.Floor(py);
            int iz = (int)Math.Floor(pz);

            int stepx = (dx > 0.0f) ? 1 : -1;
            int stepy = (dy > 0.0f) ? 1 : -1;
            int stepz = (dz > 0.0f) ? 1 : -1;

            float infinity = 600f;// std::numeric_limits<float>::infinity();

            float txDelta = (dx == 0.0f) ? infinity : Math.Abs(1.0f / dx);
            float tyDelta = (dy == 0.0f) ? infinity : Math.Abs(1.0f / dy);
            float tzDelta = (dz == 0.0f) ? infinity : Math.Abs(1.0f / dz);

            float xdist = (stepx > 0) ? (ix + 1 - px) : (px - ix);
            float ydist = (stepy > 0) ? (iy + 1 - py) : (py - iy);
            float zdist = (stepz > 0) ? (iz + 1 - pz) : (pz - iz);

            float txMax = (txDelta < infinity) ? txDelta * xdist : infinity;
            float tyMax = (tyDelta < infinity) ? tyDelta * ydist : infinity;
            float tzMax = (tzDelta < infinity) ? tzDelta * zdist : infinity;

            int steppedIndex = -1;

            while (t <= maxDist)
            {
                //block = GetBlock(new Vector3((int)ix - ChunksRenderDistance * 16 / 2, (int)iy, (int)iz - ChunksRenderDistance * 16 / 2));
                block = GetBlock(new Vector3(ix + ChunksRenderDistance * 16/2, iy, iz + ChunksRenderDistance * 16/2));
                //block = GetBlock(new Vector3((int)ix, (int)iy, (int)iz));
                if (block.type != BlockType.EMPTY)
                {
                    vec3 end;
                    vec3 norm;
                    vec3 iend;

                    end.x = px + t * dx;
                    end.y = py + t * dy;
                    end.z = pz + t * dz;

                    iend.x = ix;
                    iend.y = iy;
                    iend.z = iz;

                    norm.x = norm.y = norm.z = 0;
                    if (steppedIndex == 0) norm.x = -stepx;
                    if (steppedIndex == 1) norm.y = -stepy;
                    if (steppedIndex == 2) norm.z = -stepz;

                    //Console.WriteLine($"Looking at block END: {end}, IEND: {iend}, NORM: {norm}");

                    //block = GetBlock(new Vector3mine((int)end.x, (int)end.y, (int)end.z));

                    RayCastBlock = new RayCastBlock(new Vector3mine((int)ix, (int)iy, (int)iz), new Vector3(end.x, end.y, end.z), new Vector3(iend.x, iend.y, iend.z), new Vector3(norm.x, norm.y, norm.z), block.type);
                    return RayCastBlock;
                }
                if (txMax < tyMax)
                {
                    if (txMax < tzMax)
                    {
                        ix += stepx;
                        t = txMax;
                        txMax += txDelta;
                        steppedIndex = 0;
                    }
                    else
                    {
                        iz += stepz;
                        t = tzMax;
                        tzMax += tzDelta;
                        steppedIndex = 2;
                    }
                }
                else
                {
                    if (tyMax < tzMax)
                    {
                        iy += stepy;
                        t = tyMax;
                        tyMax += tyDelta;
                        steppedIndex = 1;
                    }
                    else
                    {
                        iz += stepz;
                        t = tzMax;
                        tzMax += tzDelta;
                        steppedIndex = 2;
                    }
                }
            }
            return RayCastBlock;
        }

        public int RayCastCube;
        public int PlayerCube;
        public RayCastBlock RayCastBlock;

        private Renderer renderer;
        private FontSystem fontSystem;
        private float _rads = 0.0f;

        // set of vertices to draw the triangle with (x,y,z) for each vertex

        //ShaderProgram1 defaultShader;
        //ShaderProgram1 sunShader;
        //ShaderProgram1 lineShader;
        //ShaderProgram1 earthShader;



        //FonsParams prams;
        // the atlas's initial size




        protected Stopwatch stopwatch = new Stopwatch();

        

        

        const int TICKS_PER_SECOND = 50;
        const int SKIP_TICKS = 1000 / TICKS_PER_SECOND;
        const int MAX_FRAMESKIP = 10;

        public const int ChunksRenderDistance = 16;
        public static int[] temparray1 = new int[ChunksRenderDistance * ChunksRenderDistance];

        public static int chunkX=0;
        public static int chunkZ=0;

        int next_game_tick = 0;

        private int loops;

        Texture7 texture1;
        Texture7 texture2;
        Texture7 texture3;

        //amebuffer buffer;

        private float FrameTime;
        private int fps;
        private int rendfps;

        private float UpdateTime;
        private int tps;
        private int rendtps;

        public static Chunk[,] chunk=      new Chunk[ChunksRenderDistance, ChunksRenderDistance];
        public static Chunk[,] chunktemp = new Chunk[ChunksRenderDistance, ChunksRenderDistance];
        const int SIZE1 =1;
        const int HEIGHT1 = 1;
        public VAO1[,,] meshs = new VAO1[SIZE1, HEIGHT1, SIZE1];
        public Model axe;

        public ObjLoader tank;

        public VAO1 mesh;

        ShaderProgram program;

        public SkyboxExample skybox = new SkyboxExample();
        public SkyboxExample sunmoon = new SkyboxExample();
        public SkyboxExample sky = new SkyboxExample();




        // camera
        Camera camera;

        public double _time;

        // transformation variables
        float yRot = 0f;

        // width and height of screen
        int width, height;
        private static int chunkZ_;
        private static int chunkX_;

        // Constructor that sets the width, height, and calls the base constructor (GameWindow's Constructor) with default args


        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {

            

            this.width = width;
            this.height = height;

            VSync = VSyncMode.On;

            // center window
            CenterWindow(new Vector2i(width, height));
        }
        // called whenever window is resized
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0,0, e.Width, e.Height);
            //GL.Ortho(0, 1, 0, 1, -1, 1);
            this.width = e.Width;
            this.height = e.Height;
            if (camera == null)
            {
                camera = new Camera(width, height, new Vector3(0, 0, 0));
                CursorState = CursorState.Grabbed;
            }

            camera.UpdateMatrix(this.width,this.height);
            mainRenderWindow.Size = new Vector2i(width, height);
        }

        // called once when game is started

        

        public static void Point()
        {


            chunkX = 0;chunkZ = 0;

            static int[] f(int[,] a)
            {
                var r = new List<int>();
                var n = a.GetLength(0);
                int j = -1, i = 0;
                bool h = true; bool d = false; int c = 0; int p = n; int max = n;
                for (var cnt = 1; cnt <= a.Length; cnt++)
                { i = h ? i : !d ? ++i : --i; j = !h ? j : !d ? ++j : --j; p--; r.Add(a[i, j]);
                    if (p <= 0) { h = !h; if ((c + 1) % 2 == 0) { d = !d; }
                        if (cnt == n || c > 1 && (c + 1) % 2 != 0) { --max; } p = max; c++; }
                } return r.ToArray();
            }

            int[,] temparray=new int[ChunksRenderDistance,ChunksRenderDistance];
            

            for (int x = 0; x < ChunksRenderDistance; x++)
            {
                for (int z = 0; z < ChunksRenderDistance; z++)
                {
                    temparray[x, z] = x * ChunksRenderDistance + z;
                }
            }


            ////for (int x = 0; x < ChunksRenderDistance * ChunksRenderDistance; x++)

            ////{

                ////temparray1[x] = x;

            ////}

            temparray1 = f(temparray);

            Array.Reverse(temparray1);

            int arrlength=temparray1.Length
;
            while (true)
            {
                if (chunkZ == ChunksRenderDistance)
                {

                    if (chunkX < ChunksRenderDistance)
                    {
                        chunkX++; chunkZ = 0;
                    }

                }

                if (chunkX < ChunksRenderDistance && chunkZ < ChunksRenderDistance)
                {


                    var xdelta = (int)(temparray1[chunkX*ChunksRenderDistance+chunkZ]/ChunksRenderDistance);
                    var zdelta = (int)(temparray1[chunkX * ChunksRenderDistance + chunkZ] % ChunksRenderDistance);

                    //chunk[chunkX, chunkZ] = new Chunk(new Vector3((int)(temparray1[arrlength-1-chunkX*ChunksRenderDistance - chunkZ]/ChunksRenderDistance)*16-(int)(ChunksRenderDistance*16/2), 0, (temparray1[arrlength - 1- chunkX * ChunksRenderDistance - chunkZ]% ChunksRenderDistance) * 16 - (int)(ChunksRenderDistance * 16 / 2)));
                    //chunk[chunkX, chunkZ] = new Chunk(new Vector3(chunkX * 16 - (ChunksRenderDistance * 16 / 2), 0,chunkZ*16-ChunksRenderDistance * 16 / 2));
                    chunk[chunkX, chunkZ] = new Chunk(new Vector3(xdelta * 16 - (ChunksRenderDistance * 16 / 2), 0, zdelta * 16 - ChunksRenderDistance * 16 / 2));

                    chunk[chunkX, chunkZ].GenChunk(new Vector3(0,0,0));
                    //Console.WriteLine("Chunk GeN hightmap Stopped {0}", stopwatch.ElapsedMilliseconds);
                    chunk[chunkX, chunkZ].GenBlocks();
                    //Console.WriteLine("Chunk GeN Blocks Stopped {0}", stopwatch.ElapsedMilliseconds);
                    chunk[chunkX, chunkZ].GenFaces(new Vector3mine(0, 0, 0));
                    //?/chunk[x, z] = new Chunk(new Vector3(chunkX * 16, 0, chunkZ * 16));
                    //chunk[chunkX, chunkZ].BuildChunk();
                    chunk[chunkX, chunkZ].Genered = true;

                Console.WriteLine("Chunk {0} builded", chunkX * ChunksRenderDistance + chunkZ + 1);


                    chunkZ++;


                    //Thread.Sleep(1000);


                    //Thread.Sleep(1);
                    //Console.WriteLine("Time elapsed chunk {0}", stopwatch.ElapsedMilliseconds);
                }else
                {
                
                    Console.WriteLine("Второй поток:");
                
                    Thread.Sleep(1000);
                }
                
            }
        }

        
            public static void Point2(object? absposition)
        {

            


            /*for (int x = 0; x < ChunksRenderDistance; x++)
            {
                for (int z = 0; z < ChunksRenderDistance; z++)
                {
                    temparray[x, z] = x * ChunksRenderDistance + z;
                }
            }

            
            
            for (int x = 0; x < ChunksRenderDistance * ChunksRenderDistance; x++) 
            
            {

                temparray1[x] = x;
            
            }

            ////temparray1 = f(temparray);

            ////Array.Reverse(temparray1);


            */


            /*
            int[] tempVAO = new int[ChunksRenderDistance];
            int[] tempIBO = new int[ChunksRenderDistance];
            Vector3[] temposition = new Vector3[ChunksRenderDistance];
            uint[] tempind = new uint[ChunksRenderDistance];
            int[] tempVertexVBO = new int[ChunksRenderDistance];
            int[] tempUVVBO = new int[ChunksRenderDistance];
            int[] tempColorsVBO = new int[ChunksRenderDistance];


        System.Boolean[] tempBuilded = new System.Boolean[ChunksRenderDistance];
            System.Boolean[] tempGenered = new System.Boolean[ChunksRenderDistance];
            System.Boolean[] tempRebuild = new System.Boolean[ChunksRenderDistance];


            for (int x = 0; x < 1; x++)
            {
                for (int z = 0; z < ChunksRenderDistance; z++)
                {
                    tempVAO[z] = chunk[x, z].chunkVAO.ID;
                    tempIBO[z] = chunk[x, z].chunkIBO.ID;
                    temposition[z] = chunk[x, z].position;
                    tempind[z] = chunk[x, z].chunkIndicesCount;
                    tempBuilded[z]= chunk[x, z].Builded;
                    tempGenered[z]=chunk[x, z].Genered;
                    tempRebuild[z]= chunk[x, z].Rebuild;
                    tempVertexVBO[z]= chunk[x, z].chunkVertexVBO.ID;
                    tempUVVBO[z] = chunk[x, z].chunkUVVBO.ID;
                    tempColorsVBO[z] = chunk[x, z].chunkColorsVBO.ID;

                    


                }
            }

            for (int x = 1; x < ChunksRenderDistance; x++)
            {
                for (int z = 0; z < ChunksRenderDistance; z++)
                {
                    chunk[x-1, z].chunkVAO.ID= chunk[x, z].chunkVAO.ID;
                    chunk[x-1, z].chunkIBO.ID = chunk[x, z].chunkIBO.ID;
                    chunk[x-1, z].position = chunk[x, z].position;
                    chunk[x-1, z].chunkIndicesCount = chunk[x, z].chunkIndicesCount;
                    chunk[x - 1, z].Builded = chunk[x, z].Builded;
                    chunk[x - 1, z].Genered = chunk[x, z].Genered;
                    chunk[x - 1, z].Rebuild = chunk[x, z].Rebuild;
                    chunk[x - 1, z].chunkVertexVBO.ID = chunk[x, z].chunkVertexVBO.ID;
                    chunk[x - 1, z].chunkUVVBO.ID = chunk[x, z].chunkUVVBO.ID;
                    chunk[x - 1, z].chunkColorsVBO.ID = chunk[x, z].chunkColorsVBO.ID;
                    //Array.Copy(chunk[x-1, z].chunkBlocks, 0, chunk[x , z].chunkBlocks, 0, chunk[x,z].chunkBlocks.GetLength(0) * chunk[x,z].chunkBlocks.GetLength(1) - 1);
                }
            }

            
            

            for (int x = ChunksRenderDistance-1; x < ChunksRenderDistance; x++)
            { 

                

                for (int z = 0; z < ChunksRenderDistance; z++)
                {
                    chunk[x, z].chunkVAO.ID = tempVAO[z];
                    chunk[x, z].chunkIBO.ID = tempIBO[z];
                    chunk[x, z].position = temposition[z];
                    chunk[x, z].chunkIndicesCount = tempind[z];
                    chunk[x, z].Builded = tempBuilded[z];
                    chunk[x, z].Genered = tempGenered[z];
                    chunk[x, z].Rebuild = tempRebuild[z];
                    chunk[x, z].chunkVertexVBO.ID = tempVertexVBO[z];
                    chunk[x, z].chunkUVVBO.ID = tempUVVBO[z];
                    chunk[x, z].chunkColorsVBO.ID = tempColorsVBO[z];
                }
            }

            
            */


            

            //while(false)
            var counter = 0;

            var tempvector = Camera.absposition-Camera.previousposition;
            var temposition = Camera.absposition;

            for (int x = 0; x <ChunksRenderDistance; x++)
            //for (int x = ChunksRenderDistance; x < ChunksRenderDistance; x++)
            {



                for (int z = 0; z < ChunksRenderDistance; z++)
                {

                    //if (chunkX_ < ChunksRenderDistance && chunkZ_ < ChunksRenderDistance)
                    ////if (chunkX_ < ChunksRenderDistance && chunkZ_ < ChunksRenderDistance)



                    var tempvector2 = new Vector3((int)(temposition.X / 16) * 16 , 0, (int)(temposition.Z / 16) * 16) - chunk[x, z].position;

                    


                    


                        ////var xdelta = (int)(temparray1[x * ChunksRenderDistance + z] / ChunksRenderDistance);
                        /////var zdelta = (int)(temparray1[x * ChunksRenderDistance + z] % ChunksRenderDistance);

                        //chunk[chunkX, chunkZ] = new Chunk(new Vector3((int)(temparray1[arrlength-1-chunkX*ChunksRenderDistance - chunkZ]/ChunksRenderDistance)*16-(int)(ChunksRenderDistance*16/2), 0, (temparray1[arrlength - 1- chunkX * ChunksRenderDistance - chunkZ]% ChunksRenderDistance) * 16 - (int)(ChunksRenderDistance * 16 / 2)));
                        //chunk[chunkX, chunkZ] = new Chunk(new Vector3(chunkX * 16 - (ChunksRenderDistance * 16 / 2), 0,chunkZ*16-ChunksRenderDistance * 16 / 2));
                        //chunk[chunkX, chunkZ] = new Chunk(new Vector3(xdelta * 16 - (ChunksRenderDistance * 16 / 2), 0, zdelta * 16 - ChunksRenderDistance * 16 / 2));
                        //chunk[chunkX_, chunkZ_] = new Chunk(new Vector3(xdelta * 16 - (ChunksRenderDistance * 16 / 2), 0, zdelta * 16 - ChunksRenderDistance * 16 / 2));



                        


                        if (Math.Abs(tempvector2.X) > ChunksRenderDistance * 16 / 2 && Math.Abs(tempvector2.Z) > ChunksRenderDistance * 16 / 2)
                        {

                        chunk[x, z].Genered = false;
                        chunk[x, z].Builded = false;

                        chunk[x, z].position = new Vector3(((int)(temposition.X / 16) * 16) + (ChunksRenderDistance / 2 - 1) * 16 * Math.Sign(tempvector.X), 0, ((int)(temposition.Z / 16) * 16) + (ChunksRenderDistance / 2 - 1) * 16 * Math.Sign(tempvector.Z));

                        chunk[x, z].GenChunk(new Vector3((int)(((Vector3)absposition).X), 0, (int)(((Vector3)absposition).Z)));
                        //Console.WriteLine("Chunk GeN hightmap Stopped {0}", stopwatch.ElapsedMilliseconds);
                        chunk[x, z].GenBlocks();
                        //Console.WriteLine("Chunk GeN Blocks Stopped {0}", stopwatch.ElapsedMilliseconds);
                        chunk[x, z].GenFaces(new Vector3mine(0, 0, 0));
                        //?/chunk[x, z] = new Chunk(new Vector3(chunkX * 16, 0, chunkZ * 16));
                        //chunk[chunkX, chunkZ].BuildChunk();
                        chunk[x, z].Genered = true;

                        counter++;
                        Console.WriteLine("Chunk {0} builded", counter);

                    }
                        else
                        {
                            if (Math.Abs(tempvector2.X) > ChunksRenderDistance * 16 / 2)
                            {

                            chunk[x, z].Genered = false;
                            chunk[x, z].Builded = false;

                            chunk[x, z].position = new Vector3(((int)(temposition.X / 16) * 16) + (ChunksRenderDistance / 2 - 1) * 16 * Math.Sign(tempvector.X), 0, chunk[x, z].position.Z);
                            //chunk[x, z].position = new Vector3(chunk[x, z].position.X,0,((int)(Camera.absposition.Z / 16) * 16) - 16);

                            chunk[x, z].GenChunk(new Vector3((int)(((Vector3)absposition).X), 0, (int)(((Vector3)absposition).Z)));
                            //Console.WriteLine("Chunk GeN hightmap Stopped {0}", stopwatch.ElapsedMilliseconds);
                            chunk[x, z].GenBlocks();
                            //Console.WriteLine("Chunk GeN Blocks Stopped {0}", stopwatch.ElapsedMilliseconds);
                            chunk[x, z].GenFaces(new Vector3mine(0, 0, 0));
                            //?/chunk[x, z] = new Chunk(new Vector3(chunkX * 16, 0, chunkZ * 16));
                            //chunk[chunkX, chunkZ].BuildChunk();
                            chunk[x, z].Genered = true;

                            counter++;
                            Console.WriteLine("Chunk {0} builded", counter);

                        }
                            if (Math.Abs(tempvector2.Z) > ChunksRenderDistance * 16 / 2)
                            {

                            chunk[x, z].Genered = false;
                            chunk[x, z].Builded = false;

                            chunk[x, z].position = new Vector3(chunk[x, z].position.X, 0, ((int)(temposition.Z / 16) * 16) + (ChunksRenderDistance / 2 - 1) * 16 * Math.Sign(tempvector.Z));
                            //chunk[x, z].position = new Vector3(chunk[x, z].position.X,0,((int)(Camera.absposition.Z / 16) * 16) - 16);

                            chunk[x, z].GenChunk(new Vector3((int)(((Vector3)absposition).X), 0, (int)(((Vector3)absposition).Z)));
                            //Console.WriteLine("Chunk GeN hightmap Stopped {0}", stopwatch.ElapsedMilliseconds);
                            chunk[x, z].GenBlocks();
                            //Console.WriteLine("Chunk GeN Blocks Stopped {0}", stopwatch.ElapsedMilliseconds);
                            chunk[x, z].GenFaces(new Vector3mine(0, 0, 0));
                            //?/chunk[x, z] = new Chunk(new Vector3(chunkX * 16, 0, chunkZ * 16));
                            //chunk[chunkX, chunkZ].BuildChunk();
                            chunk[x, z].Genered = true;

                            counter++;
                            Console.WriteLine("Chunk {0} builded", counter);
                        }
                        }
                        

                        



                        //}


                        //chunk[x, z].position = new Vector3(chunk[x, z].position.X + ((int)(tempvector.X/16))*ChunksRenderDistance*16, 0,chunk[x, z].position.Z+((int)(tempvector.Z / 16)) * ChunksRenderDistance * 16);
                        //chunk[x, z].position = new Vector3((int)((int)(Camera.absposition.X/16)*16 + xdelta * 16 - (ChunksRenderDistance * 16 / 2)), 0, (int)((int)(Camera.absposition.Z / 16) * 16+zdelta * 16 - ChunksRenderDistance * 16 / 2));
                        //chunk[chunkX_, chunkZ_].position = new Vector3((int)(chunkX_ * 16 - (ChunksRenderDistance * 16 / 2)), 0, (int)(chunkZ_ * 16 - ChunksRenderDistance * 16 / 2));



                        

                        

                    
                                   
                }
                

                    
            }

            Camera.previousposition=Camera.absposition;
            Camera.previousposition.Y = 0;



            Console.WriteLine("Третий поток закончился");
        }

        protected override void OnLoad()
        {

            

            base.OnLoad();

            

            

            //запускаем поток myThread

            



            //mainRenderWindow = new MainRenderWindow(2500,2000,"title",60);



            mainRenderWindow.OnLoad();

            mainRenderWindow.CreateMainLight(new Vector3(0, 0, 0), new Vector3(255, 0, 255));

            RayCastCube=mainRenderWindow.CreateCube(new Color4(255,0,0,255),1,1,1);
            PlayerCube= mainRenderWindow.CreateCube(new Color4(0, 0, 255, 255), 0.6f, 1.8f, 0.6f);
            //mainRenderWindow.DrawText("text",0,0, new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), new Color4(1,0,0,1));










            renderer = new Renderer();

            var settings = new FontSystemSettings
            {
                FontResolutionFactor = 2,
                KernelWidth = 2,
                KernelHeight = 2
            };

            fontSystem = new FontSystem(settings);
            fontSystem.AddFont(File.ReadAllBytes("../../../font/OpenSans-Bold.ttf"));
            //fontSystem.AddFont(File.ReadAllBytes(@"Fonts/DroidSansJapanese.ttf"));
            //fontSystem.AddFont(File.ReadAllBytes(@"Fonts/Symbola-Emoji.ttf"));



            stopwatch.Start();

            Console.WriteLine("Time elapsed {0}", stopwatch.ElapsedMilliseconds);

            //GL.PolygonMode((MaterialFace)OpenGL.GL_FRONT_AND_BACK, (PolygonMode)OpenGL.GL_LINE);
            //GL.Disable((EnableCap)OpenGL.GL_CULL_FACE);

            GL.Enable(EnableCap.Blend);
            //GL.BlendFunc((BlendingFactor)BlendingFactorSrc.SrcAlpha, (BlendingFactor)BlendingFactorDest.Src1Alpha);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            skybox.OnLoad("",1.1f);
            sunmoon.OnLoad("sunmoon",0.9f);
            sky.OnLoad("sky",1f);

            //buffer = new Framebuffer(width, height);
            //buffer.AttachColorBuffer(internalFormat: OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgba16f, type: OpenTK.Graphics.OpenGL.PixelType.Float);
            //buffer.AttachColorBuffer(internalFormat: OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgba16f, type: OpenTK.Graphics.OpenGL.PixelType.Float);
            //buffer.AttachDepthStencilBuffer();




            //skybox = new SkyBox("../../../Textures/skybox");

            texture1 = new Texture7("atlas.png");
            //texture3 = new Texture("02_-_Default_baseColor.png");
            //texture2 = new Texture("PzVl_Tiger_I.png");

            /*Console.WriteLine("Time elapsed {0}", stopwatch.ElapsedMilliseconds);

            for (int x = 0; x < ChunksRenderDistance; x++)
            {
                for (int z = 0; z < ChunksRenderDistance; z++)
                {


                    chunk[x,z] = new Chunk(new Vector3(x*16, 0, z*16));
                    Console.WriteLine("Chunk {0} builded", (x+1)*(z+1)-1);

                }
            }


            Console.WriteLine("Time elapsed chunk {0}", stopwatch.ElapsedMilliseconds);*/



            program = new ShaderProgram("Default.vert", "Default.frag");

            program.Bind();

            //texture = new Texture("metal.jpg");
            //texture.Bind();

            //mesh = ObjLoader.LoadAsVAO("../../../Textures/Tiger_I.obj");

            var objects = 0;

            /*for (int x = 0; x < SIZE1; x++)
            {
                for (int z = 0; z < SIZE1; z++)
                {


                    for (int y = 0; y < HEIGHT1; y++)
                    {

                        objects++;


                        //meshs[x,y,z] = ObjLoader.LoadAsVAO("../../../Textures/scene.obj");

                       // Console.WriteLine("Loaded {0} with objects", objects);
                    }

                }
            }*/

            
            //GL.FrontFace(FrontFaceDirection.Cw);
            //GL.Enable(EnableCap.CullFace);
            //GL.CullFace(CullFaceMode.Front);
            
            //camera = new Camera(width, height, new Vector3(1,30,1));
            //CursorState = CursorState.Grabbed;

            ////////////////skybox.OnLoad();
            ///
            next_game_tick = 0;
            Console.WriteLine("Time elapsed {0}", stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();

            //myThread.Start();

            //Thread myThread = new Thread(Print);
            // запускаем поток myThread
            //myThread.Start();

            Console.WriteLine("Starting Thread");
            myThread.Start();
            Console.WriteLine("ThreadStarted");

            /*for (int x = 0; x < ChunksRenderDistance; x++)
            {
                for (int z = 0; z < ChunksRenderDistance; z++)
                {


                    chunk[x, z] = new Chunk(new Vector3(chunkX * 16, 0, chunkZ * 16));


                }
            }*/


            

        }
        // called once when game is closed
        protected override void OnUnload()
        {
            base.OnUnload();


            for (int x = 0; x < ChunksRenderDistance; x++)
            {
                for (int z = 0; z < ChunksRenderDistance; z++)
                {


                    chunk[x,z].Delete();

                    

                }
            }

            
            // Delete, VAO, VBO, Shader Program

        }
        // called every frame. All rendering happens here

        public double NextRandomRange(double minimum, double maximum)
        {
            Random rand = new Random();
            return rand.NextDouble() * (maximum - minimum) + minimum;
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {

            FrameTime += (float)args.Time;
            fps++;
            if (FrameTime >= 1.0f)
            {
                //Title = $"FPS - {fps}, TPS = {tps}";
                Console.WriteLine($"FPS - {fps}");
                FrameTime = 0.0f;
                rendfps = fps;
                fps = 0;
                
            }

            //GL.ClearColor(0.3f, 0.3f, 1f, 1f);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            _time += 100.0 * args.Time;

            
            
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetSkyMatrix();
            //Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();
            Vector4 light = new Vector4((float)MathHelper.Sin(_time / 1000) / 3 , 1, 1, 1);
            //Vector4 light = new Vector4(0.1f, 1, 1, 1);

            GL.Disable(EnableCap.DepthTest);


            

            int modelLocation = GL.GetUniformLocation(program.ID, "model");
            int viewLocation = GL.GetUniformLocation(program.ID, "view");
            int projectionLocation = GL.GetUniformLocation(program.ID, "projection");
            int lightLocation = GL.GetUniformLocation(program.ID, "light");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.Uniform4(lightLocation, light.X, light.Y, light.Z, light.W);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

            model = model *  Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(_time / 100));

            skybox.OnRender(model, view, projection, new Vector4(0, 0, 0, 0));

            model = Matrix4.Identity;

            sky.OnRender(model, view, projection,light);

            model = model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(_time/20)+(float)Math.PI/4000000);

            //Console.WriteLine("time:"+light.X);


            



            //GL.Enable(EnableCap.DepthTest);






            sunmoon.OnRender(model, view, projection,new Vector4(0,0,0,0));

            //GL.Disable(EnableCap.Blend);

            //GL.Disable(EnableCap.DepthTest);

            //skybox.OnRender(model, view, projection);


            //GL.Disable(EnableCap.DepthTest);



            model = Matrix4.Identity;

            view = camera.GetViewMatrix();

            GL.Enable(EnableCap.DepthTest);
            //GL.FrontFace(FrontFaceDirection.Cw);
            //GL.Enable(EnableCap.CullFace);
            //GL.CullFace(CullFaceMode.Front);

            program.Bind();

            

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.Uniform4(lightLocation, light.X,light.Y,light.Z,light.W);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);
            texture1.Bind();




            for (int x = 0; x < ChunksRenderDistance; x++)
            {
                for (int z = 0; z < ChunksRenderDistance; z++)
                {
                    if (chunk[x, z] != null)
                    {

                        if (chunk[x, z].Genered  && !chunk[x, z].Builded)
                            //if (!chunk[x, z].Builded)
                            {


                            if (chunk[x, z].Rebuild)
                            {
                                chunk[x, z].ReBuildChunk();
                            }
                            else
                            {
                                chunk[x, z].BuildChunk();
                                chunk[x, z].Rebuild = true;

                            }

                                chunk[x, z].Builded = true;
                                







                        }
                        if (chunk[x, z].Builded) {


                            model = Matrix4.Identity;

                            model = model * Matrix4.CreateTranslation(chunk[x, z].position-Camera.absposition);

                            

                            GL.UniformMatrix4(modelLocation, true, ref model);
                            

                            chunk[x, z].Render(program);

                        }
                    }
                }
            }




            texture1.Unbind();

            //GL.Disable(EnableCap.DepthTest);

            //GL.Translate(MapWidth * SolidSize + PipeMarginX, 0, 0);

            var text = "Camera position:"+camera.position;
            var text2 = $"View at front: {Camera.front}, up: {camera.up}, right: {camera.right}";
            var text3 = "Looking at block:";
            var text4 = $"FPS: {rendfps}, TPS: {rendtps}";
            var text5 = "Player position:" + Camera.absposition;
            var text6 = "Collision blockX:" + Camera.colblockX+" ColPos1: "+ (Camera.colblockXvd - new Vector3(ChunksRenderDistance * 16 / 2,0, ChunksRenderDistance * 16 / 2)) + ":" + (Camera.colblockXvu - new Vector3(ChunksRenderDistance * 16 / 2, 0, ChunksRenderDistance * 16 / 2));
            var text7 = "Collision blockZ:" + Camera.colblockZ + " ColPos2: " + (Camera.colblockZvd - new Vector3(ChunksRenderDistance * 16 / 2, 0, ChunksRenderDistance * 16 / 2)) + ":" + (Camera.colblockZvu - new Vector3(ChunksRenderDistance * 16 / 2, 0, ChunksRenderDistance * 16 / 2));


            if (RayCastBlock.type != BlockType.EMPTY){
                text3 = $"Looking at block X: {RayCastBlock.position.X}, Y: {RayCastBlock.position.Y}, Z: {RayCastBlock.position.Z}, Type: {RayCastBlock.type}, END: {RayCastBlock.end}, IEND: {RayCastBlock.iend}, Normal: {RayCastBlock.norm}";
            }
            var scale = new System.Numerics.Vector2(0.5f, 0.5f);

            var font = fontSystem.GetFont(32);

            var size = font.MeasureString(text, scale);
            var origin = new System.Numerics.Vector2(size.X / 2.0f, size.Y / 2.0f);

            renderer.Begin();


            font.DrawText(renderer, text, new System.Numerics.Vector2(50, 0), FSColor.Blue, _rads, origin, scale);
            font.DrawText(renderer, text2, new System.Numerics.Vector2(50, 25), FSColor.Blue, _rads, origin, scale);
            font.DrawText(renderer, text3, new System.Numerics.Vector2(50, 50), FSColor.Blue, _rads, origin, scale);
            font.DrawText(renderer, text4, new System.Numerics.Vector2(50, 75), FSColor.Blue, _rads, origin, scale);
            font.DrawText(renderer, text5, new System.Numerics.Vector2(50, 100), FSColor.Blue, _rads, origin, scale);
            font.DrawText(renderer, text6, new System.Numerics.Vector2(50, 125), FSColor.Blue, _rads, origin, scale);
            font.DrawText(renderer, text7, new System.Numerics.Vector2(50, 150), FSColor.Blue, _rads, origin, scale);
            Camera.colblockZ = new Vector3(0,0,0);
            Camera.colblockX = new Vector3(0, 0, 0);
            renderer.End();

            //_rads += 0.01f;

            //OpenTK.Graphics..setClearColor(new Color4(0.0f, 0.0f, 0.0f, 1.0f));


            //CreateMainLight(Vector3 pos, Vector3 color);


            //mainRenderWindow.OnLoad();


            //if (RayCastBlock.type != BlockType.EMPTY)
            //{
                mainRenderWindow.Render3DObjects(camera);
            //}

            mainRenderWindow.DrawLine(width/2, height/2-25, width / 2, height/2+25, new Color4(255,0,0,255));
            mainRenderWindow.DrawLine(width / 2-25, height / 2, width / 2+25, height / 2, new Color4(255, 0, 0, 255));
            //mainRenderWindow.DrawLine(width / 2 - 50, height / 2 - 100, width / 2 - 50, height / 2, new Color4(255, 0, 0, 255));

            //mainRenderWindow.DrawText("text", 300, 300, new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), new Color4(100, 0, 0, 255));

            //SwapBuffers();

            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }
        // called every frame. All updating happens here
        protected override void OnUpdateFrame(FrameEventArgs args)
        {


            //Point();

            //Console.WriteLine("Time elapsed {0}", stopwatch.ElapsedMilliseconds);

            /*if (chunkZ == ChunksRenderDistance) {

                if (chunkX < ChunksRenderDistance)
                {
                    chunkX++; chunkZ = 0;
                }
            
            }

            if (chunkX< ChunksRenderDistance && chunkZ < ChunksRenderDistance) {


                chunk[chunkX, chunkZ] = new Chunk(new Vector3(chunkX * 16, 0, chunkZ * 16));
                Console.WriteLine("Chunk {0} builded", chunkX * ChunksRenderDistance + chunkZ + 1);


                chunkZ++;

                

                Console.WriteLine("Time elapsed chunk {0}", stopwatch.ElapsedMilliseconds);
            }*/

            RayCastBlock = RayCast(Camera.absposition, new Vector3(Camera.front.X,Camera.front.Y,Camera.front.Z), 7f);
            if (RayCastBlock.type != BlockType.EMPTY)
            {
                mainRenderWindow.TranslateObject(RayCastBlock.position.X-Camera.absposition.X, RayCastBlock.position.Y-Camera.absposition.Y, RayCastBlock.position.Z - Camera.absposition.Z, RayCastCube);

            }

            mainRenderWindow.TranslateObject(camera.position.X, camera.position.Y-0.6f, camera.position.Z, PlayerCube);

            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;

            base.OnUpdateFrame(args);

            //Point();
            camera.Update(input, mouse, args);

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            
                loops = 0;

            UpdateTime += (float)args.Time;
            while (stopwatch.ElapsedMilliseconds > next_game_tick && loops < MAX_FRAMESKIP)
            {

                next_game_tick += SKIP_TICKS;

                loops++;

                

                //UpdateTime += (float)args.Time;
                tps++;
                if (UpdateTime >= 1.0f)
                {
                    //Title = $"FPS - {fps}, TPS = {tps}";
                    Console.WriteLine($"TPS = {tps}");
                    UpdateTime = 0.0f;
                    rendtps = tps;

                    tps = 0;
                    
                    
                    
                }



                //if (GetBlock(new Vector3((int)Camera.absposition.X+ChunksRenderDistance * 16/2, (int)(Camera.absposition.Y - 2), (int)(Camera.absposition.Z)+ChunksRenderDistance * 16/2)).type == BlockType.EMPTY)
                if (World.Collisions._IsCollisionBody(new Vector3(ChunksRenderDistance * 16 / 2, -0.1f, ChunksRenderDistance * 16 / 2)).result == 3)
                {

                    //if()

                    Camera.absposition.Y -= 0.1f;

                }
                else if(World.Collisions._IsCollisionBody(new Vector3(ChunksRenderDistance * 16 / 2, -0.1f, ChunksRenderDistance * 16 / 2)).result == 1)
                { Camera.absposition.Y = (float)Math.Floor(Camera.absposition.Y); }


                
            }
        }

        // Function to load a text file and return its contents as a string


    }
}
