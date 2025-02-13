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
        public static Employee Employee { get => employee; private set => employee = value; }
        public static bool HasPickedEmployee { get => hasPickedEmployee; private set => hasPickedEmployee = value; }

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
                if (gameObject.Hitbox.Contains(clickedPoint) & (gameObject is Workspace | gameObject is Employee | gameObject is Button))
                {
                    if (gameObject is Button)
                    {
                        ((Button)gameObject).Action();
                    }
                    else if (!HasPickedEmployee & gameObject is Employee)
                    {
                        Employee = (Employee)gameObject;
                        HasPickedEmployee = true;
                        break;
                    }
                    else if (HasPickedEmployee & gameObject is Workspace)
                    {
                        Employee.DoThing((Workspace)gameObject);
                        HasPickedEmployee = false;
                        break;
                    }
                }
            }
        }
    }
}
