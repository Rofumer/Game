using Game.World;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Minecraft_Clone_Tutorial_Series_videoproj.Game;

namespace Minecraft_Clone_Tutorial_Series_videoproj.World
{

    internal class RayCastBlock
    {
        public Vector3mine position;
        public BlockType type;
        public Vector3 end;
        public Vector3 iend;
        public Vector3 norm;

        public RayCastBlock(Vector3mine position, Vector3 end, Vector3 iend, Vector3 norm, BlockType blockType = BlockType.EMPTY)
        {
            type = blockType;
            this.position = position;
            this.end = end;
            this.iend = iend;
            this.norm = norm;

        }
    }
}
