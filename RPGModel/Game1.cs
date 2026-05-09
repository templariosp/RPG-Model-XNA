using Comora;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGModel
{
    enum Direction
    {
        Down,
        Up,
        Left,
        Right
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D playerSprite;
        Texture2D walkDown;
        Texture2D walkUp;
        Texture2D walkRight;
        Texture2D walkLeft;

        Texture2D background;
        Texture2D ball;
        Texture2D skull;

        Player player = new Player();

        Camera camera;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            camera = new Camera(_graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerSprite = Content.Load<Texture2D>("Player/player");
            walkDown = Content.Load<Texture2D>("Player/walkDown");
            walkUp = Content.Load<Texture2D>("Player/walkUp");
            walkRight = Content.Load<Texture2D>("Player/walkRight");
            walkLeft = Content.Load<Texture2D>("Player/walkLeft");

            background = Content.Load<Texture2D>("background");
            ball = Content.Load<Texture2D>("ball");
            skull = Content.Load<Texture2D>("skull");

            player.animations[0] = new SpriteAnimation(walkDown, 4, 8);
            player.animations[1] = new SpriteAnimation(walkUp, 4, 8);
            player.animations[2] = new SpriteAnimation(walkLeft, 4, 8);
            player.animations[3] = new SpriteAnimation(walkRight, 4, 8);

            player.animation = player.animations[0];
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            if(!player.Dead)
                Controller.Update(gameTime, skull);

            camera.Position = player.Position;
            camera.Update(gameTime);

            foreach (Projectile projectile in Projectile.projectiles)
            {
                projectile.Update(gameTime);
            }

            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.Update(gameTime, player.Position, player.Dead);

                int sum = 32 + enemy.radius;

                if(Vector2.Distance(player.Position, enemy.Position) < sum)
                    player.Dead = true;
            }

            foreach (Projectile projectile in Projectile.projectiles)
            {
                foreach (Enemy enemy in Enemy.enemies)
                {
                    int sum = projectile.Radius + enemy.radius;

                    if (Vector2.Distance(projectile.Position, enemy.Position) < sum)
                    { 
                        projectile.Collided = true;
                        enemy.Dead = true;
                    }
                } 
            }

            Projectile.projectiles.RemoveAll(p => p.Collided);
            Enemy.enemies.RemoveAll(p => p.Dead);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(camera);

            _spriteBatch.Draw(background, new Vector2(-500, -500), Color.White);

            foreach (Projectile projectile in Projectile.projectiles)
            {
                _spriteBatch.Draw(ball, new Vector2(projectile.Position.X - 48, projectile.Position.Y - 48), Color.White);
            }

            foreach (Enemy enemy in Enemy.enemies)
            {
                enemy.animation.Draw(_spriteBatch);
            }

            if(!player.Dead)
                player.animation.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
