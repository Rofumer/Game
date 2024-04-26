using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;

//using SixLabors.ImageSharp;
using System.Security.Policy;
using Examples.Shaders;
using Minecraft_Clone_Tutorial_Series_videoproj.Graphics;
using ObjectTK.Buffers;
using ObjectTK.Shaders;
using ObjectTK.Textures;
using ObjectTK.Tools.Shapes;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

using OpenTK.Mathematics;
using SharpGL.VertexBuffers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Minecraft_Clone_Tutorial_Series_videoproj { 


    public class SkyboxExample 
        
    {
        private SkyboxProgram _program;
        private TextureCubemap _skybox;
        private VertexArray _vao;
        private Cube _cube;



        public void OnLoad(string name, float koef)
        {
            // initialize shader
            _program = ProgramFactory.Create<SkyboxProgram>();
            // initialize cube shape
            _cube = new Cube(koef);

            _cube.UpdateBuffers();
            // initialize vertex array and attributes
            _vao = new VertexArray();
            _vao.Bind();
            _vao.BindAttribute(_program.InPosition, _cube.VertexBuffer);
            _vao.BindElementBuffer(_cube.IndexBuffer);
            // create cubemap texture and load all faces


            int heightAll, widthAll;

            using (var img = Image.FromFile("Data/Textures/sky4_stars4.png"))
            {
                heightAll = img.Height;
                widthAll = img.Width;
            }
            Bitmap source = new Bitmap("Data/Textures/sky4_stars4.png");
            //Bitmap CroppedImage = source.Clone(new System.Drawing.Rectangle(x, y, width, height), source.PixelFormat);

            int number = 0;

            int[] faces = [0, 5, 4, 2, 1, 3];

            string path = "Data/Textures";
            string ext = ".png";

            string[] paths = new string[] { path + "/px1" + ext, path + "/nx1" + ext, path + "/py1" + ext,
            path + "/ny1" + ext, path + "/pz1" + ext, path + "/nz1" + ext};

            if (name == "sunmoon")
            {
                paths = new string[] { path + "/alpha" + ext, path + "/alpha" + ext, path + "/sun" + ext,
            path + "/moon" + ext, path + "/alpha" + ext, path + "/alpha" + ext};
            }
            else
            {
                if (name == "sky")
                {
                    paths = new string[] { path + "/side" + ext, path + "/side" + ext, path + "/top" + ext,
            path + "/bottom" + ext, path + "/side" + ext, path + "/side" + ext};
                }
                else
                {
                    paths = new string[] { path + "/px1" + ext, path + "/nx1" + ext, path + "/py1" + ext,
            path + "/ny1" + ext, path + "/pz1" + ext, path + "/nz1" + ext};

                }
            }
            /*for (var i = 0; i < 6; i++)
            {
                using (var bitmap = new Bitmap(paths[i]))
                {
                    //bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    if (_skybox == null) BitmapTexture.CreateCompatible(bitmap, out _skybox, 1);
                    _skybox.LoadBitmap(bitmap, i);
                }
            }*/

            if (name == "") {
                for (var j = 2; j >= 0; j--)
                {
                    for (var i = 1; i >= 0; i--) {

                        var cubeWidthStart = j * (widthAll / 3);
                        var cubeHeightStart = i * (heightAll / 2);
                        var cubeWidthEnd = (widthAll / 3);
                        var cubeHeightEnd = (heightAll / 2);

                        var bitmap = source.Clone(new System.Drawing.Rectangle(cubeWidthStart, cubeHeightStart, cubeWidthEnd, cubeHeightEnd), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        if (_skybox == null) BitmapTexture.CreateCompatible(bitmap, out _skybox, 1);
                        _skybox.LoadBitmap(bitmap, faces[number]);
                        number++;
                    }
                }
            }


            if (name != "")
            {
                for (var i = 0; i < 6; i++)
                {
                    using (var bitmap = new Bitmap(paths[i]))
                    {
                        //bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        if (_skybox == null) BitmapTexture.CreateCompatible(bitmap, out _skybox, 1);
                        _skybox.LoadBitmap(bitmap, i);
                    }
                }
            }

                /*for (var i = 0; i < 6; i++)
                {
                    using (var bitmap = new Bitmap(string.Format("Data/Textures/n{0}.png", i)))
                    {
                        //bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        if (_skybox == null) BitmapTexture.CreateCompatible(bitmap, out _skybox, 1);
                        _skybox.LoadBitmap(bitmap, i);
                    }
                }*/

            


                // activate shader and bind texture to it








            }

        public void OnUnload(object sender, EventArgs e)
        {
            _cube.VertexBuffer.Dispose();
            _cube.IndexBuffer.Dispose();
        }

        public void OnRender(Matrix4 model1, Matrix4 view1, Matrix4 proj1, Vector4 light)
        {

            _program.Use();
            _program.Texture.BindTexture(TextureUnit.Texture0, _skybox);
            // enable seamless filtering to reduce artifacts at the edges of the cube faces
            GL.Enable(EnableCap.TextureCubeMapSeamless);
            // cull front faces because we are inside the cube
            // this is not really necessary but removes some artifacts when the user leaves the cube
            // which should be impossible for a real skybox, but in this demonstration it is possible by zooming out
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);
            // set a nice clear color
            GL.ClearColor(Color.MidnightBlue);
            // note: normally you want to clear the translation part of the ModelView matrix to prevent the user from leaving the cube
            // to do that you can use ModelView.ClearTranslation() instead of the unmodified ModelView matrix
            //_program.ModelViewProjectionMatrix.Set(Matrix4.CreateScale(1)*model1*view1*proj1);
            _program.ModelViewProjectionMatrix.Set(model1 * view1 * proj1);
            _program.light.Set(light);
            // draw cube
            _vao.Bind();
            _vao.BindAttribute(_program.InPosition, _cube.VertexBuffer);
            _vao.BindElementBuffer(_cube.IndexBuffer);
            _vao.DrawElements(_cube.DefaultMode, _cube.IndexBuffer.ElementCount);

            // swap buffers
            
        }
    }
}
