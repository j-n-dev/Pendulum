using System;

using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Swing
{
    class Program
    {
        // used for making the animation. i don't even know if this works yet lmfao
        // oh fuck it actually works
        static Vector2f Circle(float val, float scale)
        {
            float x = MathF.Sin(val) * scale;
            float y = MathF.Cos(val) * scale;
            return new(x, y);
        }

        // handle window closing
        static void OnClose(object sender, EventArgs e)
        {
            RenderWindow w = (RenderWindow)sender;
            w.Close();
        }

        static void Main()
        {
            // instantiate window
            RenderWindow app = new(new(640, 480), "Pendulum");

            app.Closed += new EventHandler(OnClose); // handle window closing
            app.KeyPressed += new(KeyboardManager.OnKeyDown);
            app.KeyReleased += new(KeyboardManager.OnKeyUp);

            Clock clock = new();

            Font openSans = new("OpenSans-VariableFont.ttf");

            CircleShape pendulum = new(40.0f);
            pendulum.FillColor = Color.Blue;
            pendulum.Position = new(0, 0);
            pendulum.Origin = new(pendulum.Radius, pendulum.Radius);

            VertexArray line = new(PrimitiveType.Lines, 2);

            Text text = new("test", openSans);
            text.CharacterSize = 15;

            float pendulumPos = 0.0f;
            float deltaTime = 0.0f;

            Vector2f linePin = new(320, 240);

            float pendulumSpeed = 1.0f;
            float pendulumCycle = 1.0f;

            // game loop
            while (app.IsOpen)
            {
                deltaTime = clock.Restart().AsSeconds();

                app.DispatchEvents(); // you get the drill
                // you can update stuff here, i don't care

                // pendelum motion
                Vector2f pos = Circle(MathF.Sin(pendulumPos) * pendulumCycle, 180.0f);
                pos.X += linePin.X; pos.Y += linePin.Y;
                pendulum.Position = pos;
                pendulumPos += pendulumSpeed * deltaTime;
                // why am i doing this

                // setting changing, please don't look at this code
                #region Setting Changer
                const float changeRate = 3.0f;
                if (app.HasFocus())
                {
                    if (KeyboardManager.GetKey(Keyboard.Key.Q))
                    {
                        pendulumSpeed -= changeRate * deltaTime;
                    }
                    if (KeyboardManager.GetKey(Keyboard.Key.W))
                    {
                        pendulumSpeed += changeRate * deltaTime;
                    }
                    if (KeyboardManager.GetKey(Keyboard.Key.A))
                    {
                        pendulumCycle -= changeRate * deltaTime;
                    }
                    if (KeyboardManager.GetKey(Keyboard.Key.S))
                    {
                        pendulumCycle += changeRate * deltaTime;
                    }
                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        linePin.X = (float)Mouse.GetPosition(app).X;
                        linePin.Y = (float)Mouse.GetPosition(app).Y;
                        Console.WriteLine("X: " + linePin.X.ToString() + " Y: " + linePin.Y.ToString());
                    }
                }
                #endregion

                text.DisplayedString = "FPS: " + (1f / deltaTime).ToString() + "\nPendulum Velocity: " + pendulumSpeed.ToString() + "\nPendulum Cycle: " + pendulumCycle.ToString();

                // draw the connecting line for all i care
                line[0] = new(linePin, Color.White);
                line[1] = new(pendulum.Position, Color.White);

                app.Clear(Color.Black);
                // draw stuff here
                app.Draw(line);
                app.Draw(pendulum);
                app.Draw(text);

                app.Display();
            }
        }
    }
}
