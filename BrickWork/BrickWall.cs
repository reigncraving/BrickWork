using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml;

namespace BrickWork
{
    class BrickWall
    {
        public int[,] Layer1 { get; set; }

        public int[,] Layer2 { get; set; }


        public BrickWall(int N, int M)
        {
            this.Layer1 = new int[N, M];
            this.Layer2 = new int[N, M];
        }

        
        public BrickWall(int[,] layer)
        {
            this.Layer1 = layer;
            this.Layer2 = layer;
        }


    }
}
