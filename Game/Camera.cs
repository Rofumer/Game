using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minecraft_Clone_Tutorial_Series_videoproj.Graphics;
using Minecraft_Clone_Tutorial_Series_videoproj.World;
using static Minecraft_Clone_Tutorial_Series_videoproj.Game;
using System.Threading;
//using static Minecraft_Clone_Tutorial_Series_videoproj.Program;

namespace Minecraft_Clone_Tutorial_Series_videoproj
{
    public class Camera
    {
        // CONSTANTS
        private float SPEED = 80f;
        private float SCREENWIDTH;
        private float SCREENHEIGHT;
        private float SENSITIVITY = 5f;

        // position vars
        public Vector3 position;
        // position vars
        public static Vector3 absposition;

        public Vector3 tempabsposition;
        public static Vector3 previousposition;

        public Vector3 up = Vector3.UnitY;
        //Vector3 front = -Vector3.UnitZ;
        public static Vector3 front = Vector3.UnitZ;
        public Vector3 right = -Vector3.UnitX;

        // --- view rotations ---
        private float pitch;
        private float yaw = -90.0f;

        private bool firstMove = true;
        public Vector2 lastPos;
        public Camera(float width, float height, Vector3 position) {
            SCREENWIDTH = width;
            SCREENHEIGHT = height;
            this.position = position;
            absposition = position;
            absposition.Y = position.Y+30;
            previousposition = absposition;
        }

        public void UpdateMatrix(float width, float height)
        {
            SCREENWIDTH = width;
            SCREENHEIGHT = height;
            
        }

        public Matrix4 GetViewMatrix() {
            return Matrix4.LookAt(position, position + front, up);
            //return Matrix4.LookAt(position, position, up);
        }

        public Matrix4 GetSkyMatrix()
        {
            return Matrix4.LookAt(Vector3.Zero, Vector3.Zero+front , Vector3.UnitY);
        }

        public Matrix4 GetProjectionMatrix() {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), SCREENWIDTH / SCREENHEIGHT, 0.1f, 1000.0f);
        }

        private void UpdateVectors()
        {
            if (pitch > 89.0f)
            {
                pitch = 89.0f;
            }
            if (pitch < -89.0f)
            {
                pitch = -89.0f;
            }


            front.X = MathF.Cos(MathHelper.DegreesToRadians(pitch)) * MathF.Cos(MathHelper.DegreesToRadians(yaw));
            front.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
            front.Z = MathF.Cos(MathHelper.DegreesToRadians(pitch)) * MathF.Sin(MathHelper.DegreesToRadians(yaw));

            front = Vector3.Normalize(front);

            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));
        }

        public void InputController(KeyboardState input, MouseState mouse, FrameEventArgs e) {


            tempabsposition=absposition;

            if (input.IsKeyDown(Keys.W))
            {
                //var block = Game.GetBlock(new Vector3((int)(position.X + ChunksRenderDistance * 16 / 2 + front.X * SPEED * (float)e.Time), (int)position.Y, (int)(position.Z + ChunksRenderDistance * 16 / 2 + front.X * SPEED * (float)e.Time)));

                //if (block.type == BlockType.EMPTY)
                //{

                    //position.X += front.X * SPEED * (float)e.Time;
                    //position.Z += front.Z * SPEED * (float)e.Time;
                    absposition.X += front.X * SPEED * (float)e.Time;
                    absposition.Z += front.Z * SPEED * (float)e.Time;
                //}
            }
            if (input.IsKeyDown(Keys.A))
            {
                //var block = Game.GetBlock(new Vector3((int)(position.X + ChunksRenderDistance * 16 / 2 - right.X * SPEED * (float)e.Time), (int)position.Y, (int)(position.Z + ChunksRenderDistance * 16 / 2 - right.Z * SPEED * (float)e.Time)));
                //if (block.type == BlockType.EMPTY)
                //{
                    //position.X -= right.X * SPEED * (float)e.Time;
                    //position.Z -= right.Z * SPEED * (float)e.Time;
                    absposition.X -= right.X * SPEED * (float)e.Time;
                    absposition.Z -= right.Z * SPEED * (float)e.Time;
                //}
            }
            if (input.IsKeyDown(Keys.S))
            {
                //var block = Game.GetBlock(new Vector3((int)(position.X + ChunksRenderDistance * 16 / 2 - front.X * SPEED * (float)e.Time), (int)position.Y, (int)(position.Z + ChunksRenderDistance * 16 / 2 - front.Z * SPEED * (float)e.Time)));
                //if (block.type == BlockType.EMPTY)
                //{
                    //position.X -= front.X * SPEED * (float)e.Time;
                    //position.Z -= front.Z * SPEED * (float)e.Time;
                    absposition.X -= front.X * SPEED * (float)e.Time;
                    absposition.Z -= front.Z * SPEED * (float)e.Time;
               // }
            }
            if (input.IsKeyDown(Keys.D))
            {
                //var block = Game.GetBlock(new Vector3((int)(position.X + ChunksRenderDistance * 16 / 2 + right.X * SPEED * (float)e.Time), (int)position.Y, (int)(position.Z + ChunksRenderDistance * 16 / 2 + right.Z * SPEED * (float)e.Time)));
                //if (block.type == BlockType.EMPTY)
                //{
                    //position.X += right.X * SPEED * (float)e.Time;
                    //position.Z += right.Z * SPEED * (float)e.Time;
                    absposition.X += right.X * SPEED * (float)e.Time;
                    absposition.Z += right.Z * SPEED * (float)e.Time;
                //}
            }

            if (input.IsKeyDown(Keys.Space))
            {
                //position.Y += SPEED * (float)e.Time;
                absposition.Y += SPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                //var block = Game.GetBlock(new Vector3((int)position.X + ChunksRenderDistance * 16 / 2, (int)(position.Y-SPEED * (float)e.Time), (int)position.Z + ChunksRenderDistance * 16 / 2));
                //if (block.type == BlockType.EMPTY)
                //{
                //position.Y -= SPEED * (float)e.Time;
                absposition.Y -= SPEED * (float)e.Time;
                //}
            }


            //previousposition.Y = 0;

            if ((Math.Abs(absposition.X)%16< Math.Abs(tempabsposition.X)%16)&&(Math.Abs(absposition.X)> Math.Abs(tempabsposition.X))|| (Math.Abs(absposition.X) % 16 > Math.Abs(tempabsposition.X) % 16) && (Math.Abs(absposition.X) < Math.Abs(tempabsposition.X)))
            {


                previousposition.Y = 1;
                

                //Game.myThread2.Abort();
                Console.WriteLine("Starting Thread");
                //if (!myThread2.IsAlive)
                //{

                    myThread2 = null;
                    myThread2 = new Thread(Point2)
                    {
                        IsBackground = true,
                        Priority = ThreadPriority.Highest
                    };
                //}
                //else
                //{
                    Game.myThread2.Start(absposition);
                //}
                Console.WriteLine("ThreadStarted");

            }
            if ((Math.Abs(absposition.Z) % 16 < Math.Abs(tempabsposition.Z) % 16) && (Math.Abs(absposition.Z) > Math.Abs(tempabsposition.Z)) || (Math.Abs(absposition.Z) % 16 > Math.Abs(tempabsposition.Z) % 16) && (Math.Abs(absposition.Z) < Math.Abs(tempabsposition.Z)))
            {

                if (previousposition.Y == 1) {
                    previousposition.Y = 3;
                }
                else {
                    previousposition.Y = 2;
                }


                //Game.myThread2.Abort();
                Console.WriteLine("Starting Thread");
                // if (!myThread2.IsAlive)
                //{

                    myThread2 = null;
                    myThread2 =new Thread(Point2)
                    {
                        IsBackground = true,
                        Priority = ThreadPriority.Highest
                    };
                //}
                //else { 
                    Game.myThread2.Start(absposition);
                //}
                Console.WriteLine("ThreadStarted");
            }

            

            if (firstMove)
            {
                lastPos = new Vector2(mouse.X, mouse.Y);
                firstMove = false;
            } else
            {
                var deltaX = mouse.X - lastPos.X;
                var deltaY = mouse.Y - lastPos.Y;
                lastPos = new Vector2(mouse.X, mouse.Y);

                yaw += deltaX * SENSITIVITY * (float)e.Time;
                pitch -= deltaY * SENSITIVITY * (float)e.Time;
            }
            UpdateVectors();
        }
        public void Update(KeyboardState input, MouseState mouse, FrameEventArgs e) {
            InputController(input, mouse, e);
        }
    }
}
