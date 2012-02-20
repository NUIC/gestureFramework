using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace KinectPoseDemo
{
    class PoseCrouch: PoseList
    {
        public void checkPose(Skeleton sk)
        {
            //Crouching Logic
            if (sk.Joints[JointType.Head].Position.Y < 0.5f && sk.Joints[JointType.Head].Position.Y > 0.2f) Game1.addPose(Poses.crouch);
        }
    }
}
