using System;
using System.Drawing;
using System.Windows.Forms;

namespace PingPong
{
    public class Player : Object
    {
        public Player(int x, int y, int w, int h) : base(x, y, w, h) {
        }

        // renders player
        public override void render(Graphics g) {

            int[] bounds = getBounds();

            if (bounds[0] < 0) {
                g.FillRectangle(
                    new SolidBrush(Color.White),
                    500 + bounds[0], bounds[1], bounds[2] - bounds[0], bounds[3]
                );
            }

            if (bounds[0] + bounds[2] > 500) {
                int w = bounds[0] + bounds[2];
                g.FillRectangle(
                    new SolidBrush(Color.White),
                    0, bounds[1], w - 500, bounds[3]
                );
            }

            g.FillRectangle(
                new SolidBrush(Color.White),
                bounds[0], bounds[1], bounds[2], bounds[3]
            );
        }

        // handles speed
        public override void tick() {
            point.x += direction.Fx * Form1.DeltaTime;

            if (point.x + size.width < 0) {
                point.x += 500;
                return;
            }

            if (point.x > 500) {
                point.x -= 500;
                return;
            }
        }

        // check for collission between 2 rectangles
        public bool boxCollission(int[] bounds1, int[] bounds2) {
            bool s1 = bounds1[0] < bounds2[0] + bounds2[2],
                 s2 = bounds1[0] + bounds1[2] > bounds2[0],

                 s3 = bounds1[1] < bounds2[1] + bounds2[3],
                 s4 = bounds1[1] + bounds1[3] > bounds2[1];

            return s1 && s2 && s3 && s4;
        }

        // handles collission between ball and player
        public bool collide(int[] bounds) {
            int[] tB = getBounds();

            if (point.x < 0) {
                int[] b1 = {
                    0, tB[1], tB[2] + tB[0], tB[3]
                };

                int[] b2 = {
                    500 + tB[0], tB[1], -tB[0], tB[3]
                };

                return boxCollission(b1, bounds) || boxCollission(b2, bounds);
            }

            if (point.x + size.width > 500)
            {
                int[] b1 = {
                    tB[0], tB[1], 500 - tB[0], tB[3]
                };

                int[] b2 = {
                    0, tB[1], tB[2] + tB[0] - 500, tB[3]
                };

                return boxCollission(b1, bounds) || boxCollission(b2, bounds);
            }

            return boxCollission(getBounds(), bounds);
        }

        // set directional speed with accordence to keypress
        public void PlayerPress(object sender, KeyEventArgs e) {

            if (e.KeyCode == Keys.A) {
                direction.Fx = -200;
            }
            else if (e.KeyCode == Keys.D) {
                direction.Fx = 200;
            }
        }

        // set directional speed to 0 if player is not holding any movement keys.
        public void PlayerRelease(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D) {
                direction.Fx = 0;
            }
        }
    }
}
