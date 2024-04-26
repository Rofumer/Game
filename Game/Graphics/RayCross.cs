using OpenTK.Mathematics;

namespace Minecraft_Clone_Tutorial_Series_videoproj.Graphics
{
    /// <summary>
    /// Луч пересечения
    /// </summary>
    public class RayCross
    {
        protected Vector3 pos1, pos2;
        
        /// <summary>
        /// Создать объект луча задав сразу ночальную и конечную точку луча
        /// </summary>
        /// <param name="pos1">начальная точка</param>
        /// <param name="pos2">конечная точка</param>
        public RayCross(Vector3 pos1, Vector3 pos2)
        {
            this.pos1 = pos1;
            this.pos2 = pos2;
        }
        /// <summary>
        /// Создать объект луча задав положение луча и вектор направление
        /// </summary>
        /// <param name="pos">начальная точка</param>
        /// <param name="dir">вектор луча</param>
        /// <param name="maxDist">максимальная дистания</param>
        public RayCross(Vector3 pos, Vector3 dir, float maxDist)
        {
            pos1 = pos;
            pos2 = pos + dir * maxDist; 
        }

        /// <summary>
        /// Пересекает ли отрезок прямоугольник в объёме 
        /// </summary>
        /// <param name="from">меньшый угол прямоугольника</param>
        /// <param name="to">больший угол прямоугольника</param>
        public bool CrossLineToRectangle(Vector3 from, Vector3 to)
        {
            bool bxy = CrossLineToRectangle(
                new Vector2(from.X,from.Y), new Vector2(to.X,to.Y), new Vector2(pos1.X, pos1.Y), new Vector2(pos2.X, pos2.Y)
            );
            if (!bxy) return false;
            bool bxz = CrossLineToRectangle(
                new Vector2(from.X , from.Z), new Vector2(to.X, to.Z), new Vector2(pos1.X, pos1.Z), new Vector2(pos2.X, pos2.Z)
            );
            if (!bxz) return false;
            bool byz = CrossLineToRectangle(
                new Vector2(from.Y, from.Z), new Vector2(to.Y, to.Z), new Vector2(pos1.Y, pos1.Z), new Vector2(pos2.Y, pos2.Z)
            );
            return byz;
        }

        /// <summary>
        /// Пересекает ли отрезок прямоугольник в плоскости
        /// </summary>
        /// <param name="from">меньшый угол прямоугольника</param>
        /// <param name="to">больший угол прямоугольника</param>
        /// <param name="a">сторона отрезка</param>
        /// <param name="b">сторона отрезка</param>
        protected bool CrossLineToRectangle(Vector2 from, Vector2 to, Vector2 a, Vector2 b)
        {
            bool bc = CrossLineToTriangle(from, to, new Vector2(from.X, to.Y), a, b);
            if (bc) return true;
            return CrossLineToTriangle(from, new Vector2(to.X, from.Y), to, a, b);
        }

        /// <summary>
        /// Пересекает ли отрезок треугольник в плоскости
        /// </summary>
        /// <param name="a">вершина треугольника</param>
        /// <param name="b">вершина треугольника</param>
        /// <param name="c">вершина треугольника</param>
        /// <param name="x">сторона отрезка</param>
        /// <param name="y">сторона отрезка</param>
        /// <returns>true - пересекают</returns>
        protected bool CrossLineToTriangle(Vector2 a, Vector2 b, Vector2 c, Vector2 x, Vector2 y)
        {
            // r1 == 3 -> треугольник по одну сторону от отрезка
            bool r1 = (3 != Math.Abs(Relatively(x, y, a) + Relatively(x, y, b) + Relatively(x, y, c)));
            // r2 == 2 -> точки x,y по одну сторону от стороны ab
            bool r2 = (2 != Math.Abs(Relatively(a, b, x) + Relatively(a, b, y)));
            // r3 == 2 -> точки x,y по одну сторону от стороны bc
            bool r3 = (2 != Math.Abs(Relatively(b, c, x) + Relatively(b, c, y)));
            // r4 == 2 -> точки x,y по одну сторону от стороны ca
            bool r4 = (2 != Math.Abs(Relatively(c, a, x) + Relatively(c, a, y)));
            // r2 == r3 == r4 == 2 -> точки x,y по одну сторону от треугольника abс

            return (r1 && (r2 || r3 || r4));
        }

        /// <summary>
        /// Вычисляет положение точки D относительно AB
        /// </summary>
        /// <returns>важен знак</returns>
        protected int Relatively(Vector2 a, Vector2 b, Vector2 d)
        {
            float r = (d.X - a.X) * (b.Y - a.Y) - (d.Y - a.Y) * (b.X - a.X);
            if (Math.Abs(r) < 0.000001) return 0;
            else if (r < 0) return -1;
            else return 1;
        }
    }
}
