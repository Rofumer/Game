
using OpenTK.Mathematics;


namespace Minecraft_Clone_Tutorial_Series_videoproj.World
{
    internal class Collisions
    {


        public static Vector3 vd;
        public static Vector3 vu;


        /*vec3 move = Entity.MoveTime(timeFrame);
if (Entity.HitBox.CollisionBodyXZ(move) && Entity.OnGround)
{
    // Авто прыжок
    Entity.SetMotionY(Entity.HitBox.IsLegsWater? VE.SPEED_WATER_AUTOJAMP : VE.SPEED_AUTOJAMP);
    vec3 move2 = Entity.MoveTime(timeFrame);
        move.y = move2.y;
}
    Entity.CollisionBodyY(move);

public bool CollisionBodyXZ(vec3 vec)
    {
        EnumCollisionBody cxz = _IsCollisionBody(new vec3(vec.x, 0, vec.z));
        if (cxz == EnumCollisionBody.None)
        {
            // Если в коллизии нет проблемы смещения
            SetPos(new vec3(Position.x + vec.x, Position.y, Position.z + vec.z));
        }
        else
        {
            // проверяем авто прыжок тут
            if (vec.y == 0 && _IsCollisionBody(new vec3(vec.x, 1f, vec.z)) == EnumCollisionBody.None)
            {
                // Если с прыжком нет колизии то надо прыгать!!!
                // TODO:: реализовать авто прыжок мягким
                //SetPos(new vec3(Position.x + vec.x, Position.y, Position.z + vec.z));
                return true;
            }
            else
            {
                // одна из сторон не может проходить
                EnumCollisionBody cx = _IsCollisionBody(new vec3(vec.x, 0, 0));
                EnumCollisionBody cz = _IsCollisionBody(new vec3(0, 0, vec.z));
                if (cx == EnumCollisionBody.None || (cx == EnumCollisionBody.None && cz == EnumCollisionBody.None))
                {
                    // Если обе стороны могут, это лож, будет глюк колизии угла, идём по этой стороне только
                    // TODO:: определить по какой стороне идём можно по Yaw углу
                    SetPos(new vec3(Position.x + vec.x, Position.y, Position.z));
                }
                else if (cz == EnumCollisionBody.None)
                {
                    SetPos(new vec3(Position.x, Position.y, Position.z + vec.z));
                }
            }
        }
        return false;
    }*/

        public  struct Collision
        {
            public int result;
            public Vector3 vector;
        }

        public static int Floor(float d)
        {
            int i = (int)d;
            return d < i ? i - 1 : i;
        }

        public static Collision _IsCollisionBody(Vector3 vec)
        {

            Collision  vector = new Collision(); 

            Vector3 pos = Camera.absposition + vec + new Vector3(0.5f,-1f,0.5f);

            Vector3[] hbs = HitBoxSizeUD(pos);
            vd = new Vector3(Floor(hbs[0].X), Floor(hbs[0].Y), Floor(hbs[0].Z));
            vu = new Vector3(Floor(hbs[1].X), Floor(hbs[1].Y), Floor(hbs[1].Z));

            //int y = vd.y;
            for (int y = (int)vd.Y; y <= vu.Y; y++)
            {
                for (int x = (int)vd.X; x <= vu.X; x++)
                {
                    for (int z = (int)vd.Z; z <= vu.Z; z++)
                    {
                        //if (BlockCollision(new vec3i(x, y, z), hbs))
                        if (Game.GetBlock(new Vector3(x, y, z)).type != BlockType.EMPTY)
                        {
                            if (vec.Y < 0)
                            {
                                //  SetPos(new vec3(Position.x, vu.y, Position.z));
                                vector.result = 1;
                                vector.vector = new Vector3(x, y, z);
                                return vector; //coldown
                            }

                            //Camera.absposition.X = x-2;

                            vector.result = 2;
                            vector.vector = new Vector3(x, y, z);
                            return vector; //col
                        }

                    }
                }
            }
            vector.result = 3;
            return vector; //none
        }


        public static Vector3[] HitBoxSizeUD(Vector3 pos)
        {
            Vector3[] size = new Vector3[2];

            float w = 0.3f;
            float h = 1.8f;

            size[0] = new Vector3(pos.X-w, pos.Y, pos.Z-w);
            size[1] = new Vector3(pos.X + w, pos.Y+h, pos.Z + w);

            return size;
        }
    }
}
/*
    protected bool BlockCollision(vec3i blockPos, HitBoxSizeUD hbs)
    {
        BlockBase block = World.GetBlock(blockPos);
        if (block.IsCollision)
        {
            // Цельный блок на коллизию
            if (block.HitBox.IsHitBoxAll) return true;
            // Выбираем часть блока
            vec3 bpos = new vec3(blockPos);
            vec3 vf = block.HitBox.From + bpos;
            vec3 vt = block.HitBox.To + bpos;
            if (vf.x > hbs.Vu.x || vt.x < hbs.Vd.x
                || vf.y > hbs.Vu.y || vt.y < hbs.Vd.y
                || vf.z > hbs.Vu.z || vt.z < hbs.Vd.z)
            {
                // пересечения нет
                return false;
            }
            return true;
        }
        return false;
    }

    public void CollisionBodyY(vec3 move)
    {
        if (HitBox.CollisionBodyY(move))
        {
            OnGround = true;
            // надо упереться в блок, чтоб не падать
            HitBox.SetPos(new vec3(HitBox.Position.x, Mth.Floor(HitBox.Position.y), HitBox.Position.z));
            motion.y = 0;
        }
    }

    public bool CollisionBodyY(vec3 vec)
    {
        // Коллизия вертикали
        EnumCollisionBody onGround = _IsCollisionBody(new vec3(0, vec.y, 0));
        if (onGround == EnumCollisionBody.None)
        {
            SetPos(new vec3(Position.x, Position.y + vec.y, Position.z));
        }
        else if (onGround == EnumCollisionBody.CollisionDown)
        {
            SetPos(new vec3(Position.x, Position.y - vec.y, Position.z));
        }

        return onGround == EnumCollisionBody.CollisionDown;
    }




}
}
*/