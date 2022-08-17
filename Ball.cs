using System.Drawing;

namespace PingPong
{
    public class Ball : Object
    {
        // constructer and inital values
        public Ball(int x, int y, int w, int h) : base(x, y, w, h){
            direction.Fx = -100f;
            direction.Fy = -40f;
        }

        // renders ball
        public override void render(Graphics g) {
            int[] bounds = getBounds();

            g.FillEllipse(
                new SolidBrush(Color.White),
                bounds[0], bounds[1], bounds[2], bounds[3]
            );
        }

        // calculates and handles collission and stop ball from falling if it goes over 500 pixels down
        public override void tick() {

            if (point.y - size.height > 500) {
                return;
            }

            direction.Fy += 20f * Form1.DeltaTime;

            point.x = point.x + direction.Fx * Form1.DeltaTime;
            point.y = point.y + direction.Fy * Form1.DeltaTime;

            if (point.y < 0 && direction.Fy < 0) {
                direction.Fy *= -1;
            }

            if((point.x < 0 && direction.Fx < 0) || (point.x > 500 - size.width && direction.Fx > 0)) {
                direction.Fx *= -1;
            }

            if (Form1.player.collide(getBounds()) && direction.Fy > 0) {
                direction.Fy -= 200;
                point.y = point.y - direction.Fy * Form1.DeltaTime;
            }
        }
    }
}
