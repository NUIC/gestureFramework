using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace KinectPoseDemo
{
    class PoseHandRight : PoseList
    {
        public void checkPose(Skeleton sk)
        {

            //HandRight
            if (sk.Joints[JointType.HandRight].Position.X - sk.Joints[JointType.Spine].Position.X > .4f) Game1.addPose(Poses.handright);
        }
    }
}

