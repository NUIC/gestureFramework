using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;


namespace HelloKinect
{
    public class KinectObject
    {
        public KinectSensor sensor;
        public Skeleton[] skeletonData;


        public KinectObject(KinectSensor sensor, Skeleton[] skeleton)
        {
            this.sensor = sensor;
            this.skeletonData = skeleton;
        }

    }

    
}
