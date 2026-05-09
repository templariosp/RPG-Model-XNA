using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RPGModel
{
    public class Enemy
    {
        public static List<Enemy> enemies = new List<Enemy>();

        private bool dead = false;
        private int speed = 150;
        private Vector2 position = new Vector2(0, 0);

        public int radius = 30;
        public SpriteAnimation animation;

        public bool Dead 
        { 
            get { return dead; } 
            set { dead = value; }
        }

        public Enemy(Vector2 newPosition, Texture2D spriteSheet)
        {
            position = newPosition;
            animation = new SpriteAnimation(spriteSheet, 10, 6);
        }

        public Vector2 Position { get { return position; } }

        public void Update(GameTime gameTime, Vector2 playerPosition, bool isPlayerDead)
        {
            animation.Position = new Vector2(position.X - 48, position.Y - 66);
            animation.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!isPlayerDead)
            {
                Vector2 moveDirection = playerPosition - position;
                moveDirection.Normalize();
                position += moveDirection * speed * deltaTime;
            }
        }
    }
}
