﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SadConsole.Surfaces;

namespace SadConsole.Input
{
    public class MouseConsoleState
    {
        public readonly IConsole Console;

        public readonly Mouse Mouse;

        public readonly Cell Cell;

        public readonly Point ConsolePosition;

        public readonly Point CellPosition;

        public readonly Point WorldPosition;

        public readonly Point RelativePixelPosition;

        /// <summary>
        /// Indicates that the mouse is on a console this frame.
        /// </summary>
        public readonly bool IsOnConsole;

        public MouseConsoleState(IConsole console, Mouse mouseData)
        {
            this.Mouse = mouseData.Clone();
            this.Console = console;

            if (console == null || console.TextSurface == null)
            {
                return;
            }
            else
            {
                if (console.UsePixelPositioning)
                {
                    RelativePixelPosition = mouseData.ScreenPosition - console.RelativePosition;
                    WorldPosition = mouseData.ScreenPosition;
                    ConsolePosition = RelativePixelPosition.PixelLocationToConsole(console.TextSurface.Font);
                    IsOnConsole = new Rectangle(0, 0, console.TextSurface.Width, console.TextSurface.Height).Contains(ConsolePosition);

                    if (IsOnConsole)
                    {
                        CellPosition = ConsolePosition + Console.TextSurface.RenderArea.Location;
                        Cell = console.TextSurface[CellPosition.X, CellPosition.Y];
                    }
                }
                else
                {
                    RelativePixelPosition = mouseData.ScreenPosition - console.RelativePosition.ConsoleLocationToPixel(console.TextSurface.Font);
                    WorldPosition = mouseData.ScreenPosition.PixelLocationToConsole(console.TextSurface.Font);
                    ConsolePosition = WorldPosition - console.RelativePosition;
                    IsOnConsole = new Rectangle(0, 0, console.TextSurface.Width, console.TextSurface.Height).Contains(ConsolePosition);

                    if (IsOnConsole)
                    {
                        CellPosition = ConsolePosition + Console.TextSurface.RenderArea.Location;
                        Cell = console.TextSurface[CellPosition.X, CellPosition.Y];
                    }
                }
            }
        }

        private MouseConsoleState(MouseConsoleState clonedCopy)
        {
            Console = clonedCopy.Console;
            Mouse = clonedCopy.Mouse.Clone();
            Cell = clonedCopy.Cell;
            ConsolePosition = clonedCopy.ConsolePosition;
            CellPosition = clonedCopy.CellPosition;
            WorldPosition = clonedCopy.WorldPosition;
            RelativePixelPosition = clonedCopy.RelativePixelPosition;
        }

        /// <summary>
        /// Creates a copy.
        /// </summary>
        /// <returns>A copy of this class instance.</returns>
        public MouseConsoleState Clone()
        {
            return new MouseConsoleState(this);
        }
    }
}
