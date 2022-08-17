using System;
using System.Drawing;

namespace PingPong
{
    public abstract class Object {

        // basic properties of game objects 
        public struct Point
        {
            public float x;
            public float y;
        }

        public int[] getBounds() {

            int x = (int)point.x,
                y = (int)point.y,
                w = (int)size.width,
                h = (int)size.height;

            return new int[]{x, y, w, h};
        }

        public struct Size
        {
            public float width;
            public float height;
        }

        public struct Direction
        {
            public float Fx;
            public float Fy;
        }

        public Point point = new Point();
        public Size size = new Size();
        public Direction direction = new Direction();

        public Object(int x, int y, int w, int h) {
            point.x = x;
            point.y = y;
            size.width = w;
            size.height = h;

            direction.Fx = 0;
            direction.Fy = 0;
        }

        public abstract void render(Graphics g);
        public abstract void tick();
    }
}
