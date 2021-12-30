using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace TestGame
{
    
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static Texture2D padoru;
        public static SpriteFont font1;

        public static Game1 game;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        static KeyboardState oldKeyboard;

        static public ArrayList stepList;
        static private ArrayList unsyncedStepList = new ArrayList();
        static private ArrayList queuedForStepList = new ArrayList();
        static private ArrayList queuedForRemoval = new ArrayList();

        static public int framesSinceStart = 0;
        //static private ConcurrentBag< stepList = new ConcurrentBag();

        public static Vector3 Camera = new Vector3(0, 0, 50 ); //z is used for zoom

        public static int fps;
        

        public Game1()
        {
            game = this; //.reference to game
            this.IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            stepList = ArrayList.Synchronized(unsyncedStepList);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            this.Window.Title = "testGame";
            base.Initialize();
            new objects.Player(400,500);
            new objects.Enemy(400, 200);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            padoru = Content.Load<Texture2D>("damnMaku");
            font1 = Content.Load<SpriteFont>("font1");
            //Console.WriteLine(Assembly.GetExecutingAssembly());
            
            List<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(t => { return t.Namespace.EndsWith("objects"); }).ToList();
            foreach (Type type in types)
            {
                Console.WriteLine(type.GetMethod("LoadContent"));
                if (type.GetMethod("LoadContent") != null)
                {
                    type.GetMethod("LoadContent").Invoke(this, null);
                }
            }
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        private int lastSecond = 0;
        private int framesThisSecond = 0;
        protected override void Update(GameTime gameTime)
        {
            framesSinceStart++;
            foreach( object obj in queuedForRemoval)
            {
                stepList.Remove(obj);
            }
            queuedForRemoval.Clear();
            foreach( object obj in queuedForStepList) //.all new spawning of objects happens a frame later else steplist cries
            {
                stepList.Add(obj);
            }
            queuedForStepList.Clear();
            lock (stepList.SyncRoot)
            {
                foreach (GameObject i in stepList)
                {
                    i.Step();
                }
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (KeysPressed(Keys.F2))
            {
                Console.Out.WriteLine(stepList.Count);
            }
            //Camera.X += (Convert.ToInt32(Keyboard.GetState().IsKeyDown(Keys.J)) - Convert.ToInt32(Keyboard.GetState().IsKeyDown(Keys.L)))*5f;
            //Camera.Y += (Convert.ToInt32(Keyboard.GetState().IsKeyDown(Keys.I)) - Convert.ToInt32(Keyboard.GetState().IsKeyDown(Keys.K)))*5f;
            
            base.Update(gameTime);
            UpdatePressedKeys();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Seconds > lastSecond)
            {
                fps = framesThisSecond;
                framesThisSecond = 1;
                lastSecond = gameTime.TotalGameTime.Seconds;
            }
            else
            {
                framesThisSecond++;
            }
            //System.Console.Out.WriteLine(gameTime.ElapsedGameTime.Milliseconds);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            lock (stepList.SyncRoot)
            {
                foreach (GameObject i in stepList)
                {
                    i.position.X -= Camera.X;
                    i.position.Y -= Camera.Y;
                    i.Draw(spriteBatch);
                    i.position.X += Camera.X;
                    i.position.Y += Camera.Y;
                }
            }
            spriteBatch.DrawString(font1, fps.ToString(), new Vector2(5, 20), Color.White);
            spriteBatch.DrawString(font1, stepList.Count.ToString(), new Vector2(5, 50), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
            
        }

        public static void AddToStepList(GameObject obj)
        {
            queuedForStepList.Add(obj);
            //System.Console.Out.Write("GameObject made!");
        }
        public static bool RemoveFromStepList(GameObject obj) //.returns if object exists
        {
            bool success = stepList.Contains(obj);
            if (success)
            {
                queuedForRemoval.Add(obj);
                return true;
            }
            return false;
        }
        public static bool KeysPressed(Keys key)
        {
            bool old = false;
            bool neu = false;
            Keys[] daPressedKeys = Keyboard.GetState().GetPressedKeys();
            foreach(Keys i in daPressedKeys)
            {
                if (key.Equals(i))
                {
                    neu = true;
                }
            }
            foreach(Keys i in oldKeyboard.GetPressedKeys())
            {
                if (key.Equals(i))
                {
                    old = true;
                }
            }
            if(neu == true && old == false)
            {
                return true;
            }
            return false;
        }
        public static void UpdatePressedKeys()
        {
            oldKeyboard = Keyboard.GetState();
        }
        
        public static void SwitchRooms()
        {

        }

    }
}
