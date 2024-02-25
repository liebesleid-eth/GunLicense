using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunLicense
{
    public class LicenseData
    {
        public Vector3 NotePosition { get; set; }
        public List<Vector3> TargetPositions { get; set; }
    }

    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}