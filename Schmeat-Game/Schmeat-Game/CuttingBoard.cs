using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Schmeat_Game
{
    public class CuttingBoard : Workspace
    {
        //Fields

        //Properties

        //Constructor
        public CuttingBoard(Vector2 position)
        {
            this.position = position;
        }
        //Methods
        public static void Prepare()
        {
            Thread.Sleep(500);
            
        }
    }
}
