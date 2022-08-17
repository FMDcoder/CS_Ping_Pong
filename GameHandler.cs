using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PingPong
{
    public class GameHandler {

        public static bool GameOver = false;
        private static long timeSince = 0;

        // renders the game over screen
        public static void render(Graphics g)
        {
            if (GameOver) {
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                g.DrawString(
                    "GAME OVER",
                    new Font("Sans Serif", 30),
                    new SolidBrush(Color.Red),
                    new Rectangle(0, 100, 500, 100),
                    sf
                );

                g.DrawString(
                    "Restarting in... ",
                    new Font("Sans Serif", 20),
                    new SolidBrush(Color.White),
                    new Rectangle(0, 150, 500, 100),
                    sf
                );

                int secLeft = (int)((Form1.getMillis() - timeSince) / 1000) + 1;
                float incS = ((Form1.getMillis() - timeSince) % 1000) / 120f;

                g.DrawString(
                    ""+secLeft,
                    new Font("Sans Serif", 23 + incS),
                    new SolidBrush(Color.White),
                    new Rectangle(0, 200, 500, 100),
                    sf
                );
            }
        }

        // checks if the player lost and when to restart
        public static void tick() {
            if (Form1.ball.point.y > 500 && !GameOver) {
                GameOver = true;
                timeSince = Form1.getMillis();
                return;
            }

            if (Form1.getMillis() - timeSince > 3000 && GameOver) {
                GameOver = false;

                Form1.ball = new Ball(230, 230, 40, 40);
                Form1.player = new Player(175, 400, 150, 30);
                return;
            }
        }
    }
}
