using System;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace PingPong
{
    public partial class Form1 : Form
    {
        public static float DeltaTime = 0;
        public static int frames = 0, fc = 0;
        public static Player player = new Player(175, 400, 150, 30);
        public static Ball ball = new Ball(230, 230, 40, 40);

        // initialization of app
        public Form1() {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            new Thread(new ThreadStart(run)).Start();
        }
        
        // get unix milliseconds
        public static long getMillis() {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        // Game engine
        void run() {
            long now = 0,
                last = getMillis(),
                timer = 0;

            while (true) {
                now = getMillis();
                DeltaTime = (now - last) / 1000f;
                last = now;

                tick();
                Invalidate();

                if ((getMillis() - timer) > 1000) {
                    timer = getMillis();
                    frames = fc;
                    fc = 0;
                }

            }
        }

        // ticks game objects for calculations
        void tick() {
            ball.tick();
            player.tick();
            GameHandler.tick();
        }

        // close program when windows closes
        private void closing(object sender, FormClosedEventArgs e) {
            Environment.Exit(0);
        }

        // handles when the player press on the keyboard
        private void PlayerPress(object sender, KeyEventArgs e) {
            if (!GameHandler.GameOver) {
                player.PlayerPress(sender, e);
            }
        }

        // handles when the player releases the keyboard
        private void PlayerRelease(object sender, KeyEventArgs e){
            if (!GameHandler.GameOver) {
                player.PlayerRelease(sender, e);
            }
        }

        // handles all renders of game objects, FPS, gameOverHandler
        private void render(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            fc++;

            g.FillRectangle(
               new SolidBrush(Color.Black),
               0, 0, 500, 500
           );

            player.render(g);
            ball.render(g);

            g.DrawString(
                "FPS: "+frames,
                new Font("Sans Serif", 12),
                new SolidBrush(Color.White),
                new PointF(20, 20)
            );

            GameHandler.render(g);
        }
    }
}
