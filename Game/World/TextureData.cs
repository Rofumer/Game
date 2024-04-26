using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minecraft_Clone_Tutorial_Series_videoproj.World;
using OpenTK.Mathematics;

namespace Game.World
{
    internal static class TextureData
    {

        public static Dictionary<BlockType, Dictionary<Faces, Vector2>> blockTypeUVCoord= new Dictionary<BlockType, Dictionary<Faces, Vector2>>()
        {
            {BlockType.DIRT, new Dictionary<Faces, Vector2>() 
            {
                {Faces.FRONT, new Vector2(2f,15f) },
                {Faces.LEFT, new Vector2(2f,15f) },
                {Faces.RIGHT, new Vector2(2f,15f) },
                {Faces.BACK, new Vector2(2f,15f) },
                {Faces.TOP, new Vector2(2f,15f) },
                {Faces.BOTTOM, new Vector2(2f,15f) },
            } },
            {BlockType.GRASS, new Dictionary<Faces, Vector2>()
            {
                {Faces.FRONT, new Vector2(3f,15f) },
                {Faces.LEFT, new Vector2(3f,15f) },
                {Faces.RIGHT, new Vector2(3f,15f) },
                {Faces.BACK, new Vector2(3f,15f) },
                {Faces.TOP, new Vector2(7f,13f) },
                {Faces.BOTTOM, new Vector2(2f,15f) },
            } }
        };

        /*public static readonly Dictionary<BlockType, Dictionary<Faces, List<Vector2>>> blockTypeUVs = new Dictionary<BlockType, Dictionary<Faces, List<Vector2>>>() {
            { BlockType.DIRT, new Dictionary<Faces, List<Vector2>>(){
                {Faces.FRONT, new List<Vector2>()
                {
                    new Vector2(2f/16f, 15f/16f),
                    new Vector2(3f/16f, 15f/16f),
                    new Vector2(3f/16f, 1f),
                    new Vector2(2f/16f, 1f),
                } },
                {Faces.BACK, new List<Vector2>()
                {
                    new Vector2(2f/16f, 15f/16f),
                    new Vector2(3f/16f, 15f/16f),
                    new Vector2(3f/16f, 1f),
                    new Vector2(2f/16f, 1f),
                } },
                {Faces.TOP, new List<Vector2>()
                {
                    new Vector2(2f/16f, 15f/16f),
                    new Vector2(3f/16f, 15f/16f),
                    new Vector2(3f/16f, 1f),
                    new Vector2(2f/16f, 1f),
                } },
                {Faces.BOTTOM, new List<Vector2>()
                {
                    new Vector2(2f/16f, 15f/16f),
                    new Vector2(3f/16f, 15f/16f),
                    new Vector2(3f/16f, 1f),
                    new Vector2(2f/16f, 1f),
                } },
                {Faces.LEFT, new List<Vector2>()
                {
                    new Vector2(2f/16f, 15f/16f),
                    new Vector2(3f/16f, 15f/16f),
                    new Vector2(3f/16f, 1f),
                    new Vector2(2f/16f, 1f),
                } },
                {Faces.RIGHT, new List<Vector2>()
                {
                    new Vector2(2f/16f, 15f/16f),
                    new Vector2(3f/16f, 15f/16f),
                    new Vector2(3f/16f, 1f),
                    new Vector2(2f/16f, 1f),
                } },
            } },
            { BlockType.GRASS, new Dictionary<Faces, List<Vector2>>(){
                {Faces.FRONT, new List<Vector2>()
                {
                    new Vector2(4f/16f, 1f),
                    new Vector2(3f/16f, 1f),
                    new Vector2(3f/16f, 15f/16f),
                    new Vector2(4f/16f, 15f/16f),
                    
                } },
                {Faces.BACK, new List<Vector2>()
                {
                    new Vector2(4f/16f, 1f),
                    new Vector2(3f/16f, 1f),
                    new Vector2(3f/16f, 15f/16f),
                    new Vector2(4f/16f, 15f/16f),
                } },
                {Faces.TOP, new List<Vector2>()
                {
                    new Vector2(8f/16f, 14f/16f),
                    new Vector2(7f/16f, 14f/16f),
                    new Vector2(7f/16f, 13f/16f),
                    new Vector2(8f/16f, 13f/16f),
                } },
                {Faces.BOTTOM, new List<Vector2>()
                {
                    new Vector2(2f/16f, 15f/16f),
                    new Vector2(3f/16f, 15f/16f),
                    new Vector2(3f/16f, 1f),
                    new Vector2(2f/16f, 1f),
                } },
                {Faces.LEFT, new List<Vector2>()
                {
                    new Vector2(4f/16f, 1f),
                    new Vector2(3f/16f, 1f),
                    new Vector2(3f/16f, 15f/16f),
                    new Vector2(4f/16f, 15f/16f),
                } },
                {Faces.RIGHT, new List<Vector2>()
                {
                    new Vector2(4f/16f, 1f),
                    new Vector2(3f/16f, 1f),
                    new Vector2(3f/16f, 15f/16f),
                    new Vector2(4f/16f, 15f/16f),
                } },
            } }
        };*/
    }
}
