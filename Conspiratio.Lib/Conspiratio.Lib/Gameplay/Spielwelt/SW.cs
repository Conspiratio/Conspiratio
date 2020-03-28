﻿namespace Conspiratio.Lib.Gameplay.Spielwelt
{
    /// <summary>
    /// Spielwelt, die alle untergeordneten Objekte von statischen und dynamischen Daten enthält.
    /// </summary>
    public static class SW
    {
        private static StatischeSpieldaten _statisch = null;

        public static StatischeSpieldaten Statisch
        {
            get
            {
                if (_statisch == null)
                    _statisch = new StatischeSpieldaten();

                return _statisch;
            }
        }
    }
}
