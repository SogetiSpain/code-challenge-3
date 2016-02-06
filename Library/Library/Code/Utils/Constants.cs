namespace Library.App.Program.Code.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class ConstantsApp
    {
        public enum ProcessState
        {
            Start,
            BookRegisterOperation,
            BookBookingOperation,
            BookReturnOperation,
            Exit,
            BookRegistered,
            BookReturned,
            UserFined,
            BookBooked,
            Error
        }

        public enum Command
        {
            PressKeyB,
            StoreBook,
            Begin,
            ReturningBook,
            PressKeyR,
            UserFinning,
            PressKeyN,
            PressKeyESC,
            NotAllowed,
            BookingBook,
        }

        public static Dictionary<string, Command> UserActions = new Dictionary<string, Command>()
        {
            {"B", Command.PressKeyB},
            {"R", Command.PressKeyR},
            {"N", Command.PressKeyN},
            {"ESCAPE", Command.PressKeyESC}
        };

    }

}

/*
    ¿Quiere (R)egistrar un libro, hacer un (P)réstamo o una (D)evolución: P
    Introduzca el usuario: edin
    Introduzca el libro: HEA1
    Introduzca la fecha del préstamo: 15/01/2016
    Préstamo realizado. Fecha de devolución: 15/02/2016. 

    ¿Quiere (R)egistrar un libro, hacer un (P)réstamo o una (D)evolución: P
    Introduzca el usuario: roberto
    Introduzca el libro: HEA1
    El libro HEA1 (Head First Design Patterns) no está disponible ahora mismo. 

    ¿Quiere (R)egistrar un libro, hacer un (P)réstamo o una (D)evolución: D
    Introduzca el usuario: edin
    Introduzca el libro: HEA1
    Introduzca la fecha de la devolución: 11/02/2016
    El libro HEA1 (Head First Design Patterns) está disponible para ser prestado.
 */
