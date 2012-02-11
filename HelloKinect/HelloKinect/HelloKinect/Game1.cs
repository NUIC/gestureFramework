using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;

namespace HelloKinect
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        private KinectSensorCollection sensorCollection;
        private KinectSensor sensor;
        private Skeleton[] skeletonData;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            sensorCollection = KinectSensor.KinectSensors;
            sensor = sensorCollection[0];
            sensor.SkeletonStream.Enable();
            sensor.Start();

            // register event handler for the allframesready event.
            sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(KinectAllFramesReady);
           
           
            
            
            // allocate memory for skeleton data.
            this.skeletonData = new Skeleton[sensor.SkeletonStream.FrameSkeletonArrayLength];



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void KinectAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletonFrame.CopySkeletonDataTo(this.skeletonData);
                    int i = 0;
                    foreach (Skeleton cSkeleton in skeletonData) {
                        
                        if(cSkeleton.TrackingState.Equals(SkeletonTrackingState.Tracked)) {
                            Console.WriteLine("Tracked Skeleton Tracking ID: " + cSkeleton.TrackingId);

                        }
                        //Console.WriteLine("Skeleton Count: " + skeletonData.Count());
                        i++;
                    }
                   
                }
                else
                {
                    // skeletonFrame is null because the request did not arrive in time
                }
            }
        }

        /**
         * Event handler that is called whenever the state of any of the sensors changes.
         * 
         */
        /*
        private void KinectSensorStatusChanged(object sender, StatusChangedEventArgs e)
        {

            if(e.Status.Equals(KinectStatus.Connected)) {
                Console.WriteLine("Kinect Connected");

                // initialize the kinect and its skeleton data.
                e.Sensor.SkeletonStream.Enable();
                e.Sensor.Start();
                //connectedSensors.AddLast(new KinectObject(e.Sensor,new Skeleton[e.Sensor.SkeletonStream.FrameSkeletonArrayLength]));
                if (kinectOne == null)
                {
                    kinectOne = e.Sensor;
                }
                else if (kinectTwo == null)
                {
                    kinectTwo = e.Sensor;
                }
                else
                {
                    // all kinects detected.
                }
                

                // register event listener the processes each skeleton frame. (Put this in another process??)
                e.Sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(KinectAllFramesReady);



            } else if(e.Status.Equals(KinectStatus.Disconnected)) {
                Console.WriteLine("Kinect Disconnected");

                if (kinectOne != null)
                {
                }
                if (kinectTwo != null)
                {
                }
  
            }

        }
         */
    }
}
