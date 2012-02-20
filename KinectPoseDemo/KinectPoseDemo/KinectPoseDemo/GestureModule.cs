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


/*
 * 
 * 
 * make sure the main class has this:
 * public enum Pose { lefthandup, righthandup, handleft, handright, crouch }
 * 
 * 
 * 
 * 
 * 
 */

namespace KinectPoseDemo
{
    class GestureModule
    {
        private List<PoseList> poses;
        private PoseCrouch crouch;
        private PoseHandLeft handleft;
        private PoseHandRight handright;
        private PoseLeftHandUp lefthandup;
        private PoseRightHandUp righthandup;
        

        public GestureModule()
        {

            this.crouch = new PoseCrouch();
            this.handleft = new PoseHandLeft();
            this.handright = new PoseHandRight();
            this.lefthandup = new PoseLeftHandUp();
            this.righthandup = new PoseRightHandUp();
            this.poses.Add(crouch);
            this.poses.Add(handleft);
            this.poses.Add(handright);
            this.poses.Add(lefthandup);
            this.poses.Add(righthandup);
        }

        public void processSkeleton(Skeleton sk)
        {


            foreach (PoseList p in poses)
            {
                p.checkPose(sk);
            }


         

        }

    }
}
