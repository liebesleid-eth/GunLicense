using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunLicense
{
    public class LicenseData
    {
        public Transformation NotePosition { get; set; }
        public List<Transformation> TargetPositions { get; set; }
    }

    public class Transformation
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float rotation { get; set; }
    }
}