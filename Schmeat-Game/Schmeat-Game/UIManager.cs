using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schmeat_Game
{
    public static class UIManager
    {
        private static Employee employee;
        private static bool hasPickedEmployee = false;

        static UIManager()
        {

        }

        /// <summary>
        /// Is called when screen is clicked, and handles the selection of gameobjects.
        /// </summary>
        /// <param name="clickedPoint">The coordinates that was clicked.</param>
        public static void ScreenClicked(Vector2 clickedPoint)
        {
            foreach (GameObject gameObject in GameWorld.ActiveGameObjects)
            {
                if (gameObject.Hitbox.Contains(clickedPoint) & (gameObject is Workspace | gameObject is Employee))
                {
                    if (!hasPickedEmployee & gameObject is Employee)
                    {
                        employee = (Employee)gameObject;
                        hasPickedEmployee = true;
                        break;
                    }
                    else if (hasPickedEmployee & gameObject is Workspace)
                    {
                        employee.DoThing((Workspace)gameObject);
                        hasPickedEmployee = false;
                        break;
                    }
                }
            }
        }
    }
}
