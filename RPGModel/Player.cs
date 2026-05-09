using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RPGModel
{
    public class Player
    {
        private bool isMoving = false;
        private int speed = 300;
        private Direction direction = Direction.Down;
        private KeyboardState keyboardStateOld;
        private Vector2 position = new Vector2(500, 300);

        public Vector2 Position { get { return position; } }
        public SpriteAnimation animation;
        public SpriteAnimation[] animations = new SpriteAnimation[4];

        public void SetX(float newX)
        {
            position.X = newX;
        }

        public void SetY(float newY)
        {
            position.Y = newY;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyState = Keyboard.GetState();

            isMoving = false;

            if(keyState.IsKeyDown(Keys.Right))
            {
                direction = Direction.Right;
                isMoving = true;
            }

            if(keyState.IsKeyDown(Keys.Left))
            {
                direction = Direction.Left;
                isMoving = true;
            }

            if(keyState.IsKeyDown(Keys.Up))
            {
                direction = Direction.Up;
                isMoving = true;
            }

            if(keyState.IsKeyDown(Keys.Down))
            {
                direction = Direction.Down;
                isMoving = true;
            }

            if (keyState.IsKeyDown(Keys.Escape))
                isMoving = false;

            if(isMoving)
            {
                switch(direction)
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

            animation = animations[(int)direction];

            animation.Position = new Vector2(position.X - 48, position.Y - 48);

            if (keyState.IsKeyDown(Keys.Space))
                animation.SetFrame(0);
            else if (isMoving)
                animation.Update(gameTime);
            else
                animation.SetFrame(1);

            if (keyState.IsKeyDown(Keys.Space) && keyboardStateOld.IsKeyUp(Keys.Space))
                Projectile.projectiles.Add(new Projectile(position, direction));

            keyboardStateOld = keyState;
        }
    }
}