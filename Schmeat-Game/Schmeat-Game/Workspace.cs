﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Schmeat_Game
{
    public abstract class Workspace : GameObject
    {
        public Vector2 EmployeePosition { get; protected set; }
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }
    }
}
