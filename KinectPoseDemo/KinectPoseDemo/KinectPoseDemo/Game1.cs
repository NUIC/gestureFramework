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
using System.Text;

 using System.Runtime.InteropServices;

using Microsoft.Kinect;
using System.IO;


namespace KinectPoseDemo
{

    public enum Poses { lefthandup, righthandup, handleft, handright, crouch }
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Skeleton[] skeletons;
        //private EventHandler<SkeletonFrameReadyEventArgs> kinect_SkeletonFrameReady;
        Texture2D blank;
        GestureModule gestureModule;

        public static List<Poses> p = new List<Poses>();
        String output = "";
        SpriteFont Font1;
        Vector2 FontPos;    

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            gestureModule = new GestureModule();

            
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
            KinectSensor kinect;

            // Check to see if a Kinect is available
            if (KinectSensor.KinectSensors.Count == 0)
            {
                MessageBox(new IntPtr(0), "Failed to initialize Kinect", "Error", 0);
                Exit();
            }

            // Get the first Kinect on the computer
            kinect = KinectSensor.KinectSensors[0];

            // Start the Kinect running and select the depth camera
            try
            {
                kinect.SkeletonStream.Enable(new TransformSmoothParameters{
                    Smoothing = 0.1f,
                    Correction = 0,
                    Prediction = 0,
                    JitterRadius = 1,
                    MaxDeviationRadius = .5f


                });
                kinect.Start();
            }
            catch
            {
                MessageBox(new IntPtr(0), "Failed to initialize Kinect", "Error", 0);
                Exit();
            }

            kinect.SkeletonFrameReady +=new EventHandler<SkeletonFrameReadyEventArgs>(kinect_SkeletonFrameReady);

            skeletons = new Skeleton[6];

            base.Initialize();
        }


        void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            SkeletonFrame frame = e.OpenSkeletonFrame();
            if (frame == null) return;
            
            frame.CopySkeletonDataTo(skeletons);

            frame.Dispose();

            
        }

        private void addLine(Joint joint, Joint joint_2)
        {

            DrawLine(this.spriteBatch, blank, 3, Color.Yellow, new Vector2(joint.Position.X * (150) + graphics.PreferredBackBufferWidth / 2, joint.Position.Y * (-150) + graphics.PreferredBackBufferHeight / 2), new Vector2(joint_2.Position.X * (150) + graphics.PreferredBackBufferWidth/2, joint_2.Position.Y * (-150) + graphics.PreferredBackBufferHeight/2));
            
        }

        void DrawLine(SpriteBatch batch, Texture2D blank,
              float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);
            //batch.Begin();
            batch.Draw(blank, point1, null, color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
            //batch.End();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {


            spriteBatch = new SpriteBatch(GraphicsDevice);
            blank = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });

            // Create a new SpriteBatch, which can be used to draw textures.
            Font1 = Content.Load<SpriteFont>("SpriteFont1");

            // TODO: Load your game content here            
            FontPos = new Vector2(100,100);
            // Create a new SpriteBatch, which can be used to draw textures.
            

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


            if (p.Count != 0)
            {

                foreach (Poses pose in p)
                {
                    //Console.WriteLine(pose.ToString());
                    output += pose.ToString();
                    output += '\n'; 

                }
                
                p.Clear();

            }
            // TODO: Add your update logic here

            //SkeletonFrame frame = kinect_SkeletonFrameReady

            //if (frame == null) return;

            base.Update(gameTime);
        }


       

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if (skeletons == null) return;

            foreach (Skeleton skeleton in skeletons)
            {

                drawSkeleton(skeleton);
                gestureModule.processSkeleton(skeleton);

                
            }

            // Find the center of the string
            Vector2 FontOrigin = Font1.MeasureString(output) / 2;
            // Draw the string
            spriteBatch.DrawString(Font1, output, FontPos, Color.LightGreen,
                0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            output = "";
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }



        void drawSkeleton(Skeleton skeleton)
        {

            if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
            {
                Joint headJoint = skeleton.Joints[JointType.Head];
                Joint hipCenter = skeleton.Joints[JointType.HipCenter];

                if (headJoint.TrackingState != JointTrackingState.NotTracked)
                {
                    SkeletonPoint headPosition = headJoint.Position;

                    //HeadPositionPrintline wrecks code efficiency!!!
                    //Console.WriteLine(skeleton.Joints[JointType.Head].Position.Y);

                    // Spine
                    addLine(skeleton.Joints[JointType.Head], skeleton.Joints[JointType.ShoulderCenter]);
                    addLine(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.Spine]);

                    // Left leg
                    addLine(skeleton.Joints[JointType.Spine], skeleton.Joints[JointType.HipCenter]);
                    addLine(skeleton.Joints[JointType.HipCenter], skeleton.Joints[JointType.HipLeft]);
                    addLine(skeleton.Joints[JointType.HipLeft], skeleton.Joints[JointType.KneeLeft]);
                    addLine(skeleton.Joints[JointType.KneeLeft], skeleton.Joints[JointType.AnkleLeft]);
                    addLine(skeleton.Joints[JointType.AnkleLeft], skeleton.Joints[JointType.FootLeft]);

                    // Right leg
                    addLine(skeleton.Joints[JointType.HipCenter], skeleton.Joints[JointType.HipRight]);
                    addLine(skeleton.Joints[JointType.HipRight], skeleton.Joints[JointType.KneeRight]);
                    addLine(skeleton.Joints[JointType.KneeRight], skeleton.Joints[JointType.AnkleRight]);
                    addLine(skeleton.Joints[JointType.AnkleRight], skeleton.Joints[JointType.FootRight]);

                    // Left arm
                    addLine(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.ShoulderLeft]);
                    addLine(skeleton.Joints[JointType.ShoulderLeft], skeleton.Joints[JointType.ElbowLeft]);
                    addLine(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.WristLeft]);
                    addLine(skeleton.Joints[JointType.WristLeft], skeleton.Joints[JointType.HandLeft]);

                    // Right arm
                    addLine(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.ShoulderRight]);
                    addLine(skeleton.Joints[JointType.ShoulderRight], skeleton.Joints[JointType.ElbowRight]);
                    addLine(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.WristRight]);
                    addLine(skeleton.Joints[JointType.WristRight], skeleton.Joints[JointType.HandRight]);

                }


            }


        }




        internal static void addPose(Poses poseToAdd)
        {
           p.Add(poseToAdd);
        }
    }
}
