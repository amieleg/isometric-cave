using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IsometricTest;

public class Game1 : Game
{
    private const float PlayerSpeed = 0.05f;
    private GraphicsDeviceManager _Gdm;
    private SpriteBatch _Sb;
    private Drawer _D;
    private World _World;
    private Player _Player;

    public Game1()
    {
        _Gdm = new GraphicsDeviceManager(this);
        Atlas.InitAtlas();
        _World = new World();
        _Player = new Player(_World);
        _D = new Drawer(this.Window.ClientBounds, _World, _Player);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _World.GenerateOne();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _D.Load(this.Content.Load<Texture2D>("Images/blocksprites.png"));

        _Sb = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime Gt)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var state = Keyboard.GetState();

        Vector3 Direction = new Vector3();

        if (state.IsKeyDown(Keys.Up))
        {
            Direction.Y -= PlayerSpeed;
        }
        if (state.IsKeyDown(Keys.Down))
        {
            Direction.Y += PlayerSpeed;
        }
        if (state.IsKeyDown(Keys.Left))
        {
            Direction.X -= PlayerSpeed;
        }
        if (state.IsKeyDown(Keys.Right))
        {
            Direction.X += PlayerSpeed;
        }
        if (state.IsKeyDown(Keys.Space))
        {
            Direction.Z += PlayerSpeed;
        }
        if (state.IsKeyDown(Keys.RightShift))
        {
            Direction.Z -= PlayerSpeed;
        }
        if (state.IsKeyDown(Keys.N))
        {
            _Player._Sm.SetState(PlayerState.Nodding);
        }
        if (state.IsKeyDown(Keys.T))
        {
            _Player._Sm.SetState(PlayerState.Twirling);
        }

        _Player.Move(Direction);

        Atlas.UpdateTiles(Gt);
        _Player.Update(Gt);

        base.Update(Gt);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //Debug.WriteLine(gameTime.IsRunningSlowly);
        // TODO: Add your drawing code here
        _Sb.Begin(samplerState:SamplerState.PointClamp);

        _D.DrawWorld(_Sb, gameTime);

        _Sb.End();

        base.Draw(gameTime);
    }
}
