using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Ex03.ConsoleUI
{
    // $G$ CSS-999 (-7) StyleCop errors : SA1117 SA1116 SA1115 
    public class Program
    {
        // $G$ SFN-999 (-3) Nice ui , but i got stuck in infinite loop
        // when try to add fuel to a vehicle

        public static void Main()
        {
            Console.CursorVisible = false;
            UI uiForGarageConsole = new UI();
            uiForGarageConsole.WorkingInTheGarage();      
        }
    }
}
