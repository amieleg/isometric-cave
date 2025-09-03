using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IsometricTest;

public class Game1 : Game
{
    private const float PlayerSpeed = 0.05f;
    private GraphicsDeviceManager _gdm;
    private SpriteBatch _sb;
    private Drawer _d;
    private World _world;
    private Player _player;

    public Game1()
    {
        _gdm = new GraphicsDeviceManager(this);
        _world = new World();
        _player = new Player();
        _d = new Drawer(this.Window.ClientBounds, _world, _player);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _world.Generate();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _d.Load(this.Content.Load<Texture2D>("Images/blocksprites.png"));

        _sb = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.Up))
        {
            _player._position.Y += PlayerSpeed;
        }
        if (state.IsKeyDown(Keys.Down))
        {
            _player._position.Y -= PlayerSpeed;
        }
        if (state.IsKeyDown(Keys.Left))
        {
            _player._position.X -= PlayerSpeed;
        }
        if (state.IsKeyDown(Keys.Right))
        {
            _player._position.X += PlayerSpeed;
        }
        if (state.IsKeyDown(Keys.Space))
        {
            _player._position.Z += PlayerSpeed;
        }
        if (state.IsKeyDown(Keys.RightShift))
        {
            _player._position.Z -= PlayerSpeed;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        Debug.WriteLine(gameTime.ElapsedGameTime);
        // TODO: Add your drawing code here
        _sb.Begin(samplerState:SamplerState.PointClamp);

        _d.DrawWorld(_sb, gameTime);

        _sb.End();

        base.Draw(gameTime);
    }
}
