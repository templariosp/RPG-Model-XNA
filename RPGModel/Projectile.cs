using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RPGModel
{
    internal class Projectile
    {
        public static List<Projectile> projectiles = new List<Projectile>();

        private bool collided = false;
        private int speed = 1000;
        private int radius = 18;
        private Vector2 position;
        private Direction direction;

        public Projectile(Vector2 newPos, Direction newDirection)
        {
            position = newPos;
            direction = newDirection;
        }

        public bool Collided 
        { 
            get { return collided; } 
            set { collided = value; }
        }
        public int Radius { get { return radius; } }
        public Vector2 Position { get { return position;  } }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (direction)
            {
                case Direction.Right:
                    position.X += speed * deltaTime;
                    break;
                case Direction.Left:
                    position.X -= speed * deltaTime;
                    break;
                case Direction.Down:
                    position.Y += speed * deltaTime;
                    break;
                case Direction.Up:
                    position.Y -= speed * deltaTime;
                    break;
            }
        }
    }
}
