using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace KinectPoseDemo
{
    class PoseLeftHandUp : Poses
    {
        public void checkPose(Skeleton sk)
        {
            //LeftHandUp
            if (sk.Joints[JointType.HandLeft].Position.Y - sk.Joints[JointType.Head].Position.Y > 0.1f) Game1.addPose(Poses.lefthandup);
        }
    }
}

