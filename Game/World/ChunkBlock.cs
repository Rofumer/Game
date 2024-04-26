using Game.World;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Clone_Tutorial_Series_videoproj.World
{
    internal class ChunkBlock
    {
        public Vector3 position;
        public BlockType type;


        public ChunkBlock(Vector3 position, BlockType blockType = BlockType.EMPTY)
        {
            type = blockType;
            this.position = position;


        }

    }

    
}
