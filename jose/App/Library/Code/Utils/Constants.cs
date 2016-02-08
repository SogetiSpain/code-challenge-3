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
            StartState,
            ErrorState,
            BookCreationState,
            BookReturnState,
            BookBookingState,
            PayPenlaty,
            ExitState
        }

        public enum Command
        {
            InvalidInput,   //Press Invalid Key!
            Exit,           //Press ESC Key! 
            BookCreation,	//Press B Key! 
            BookReturn,	    //Press D Key! 
            BookBooking, 	//Press P Key! 
            PenaltyPayment, //Press C Key
            Begin		    //StateMachine-Auto        	
        }

        public static Dictionary<string, Command> LibAppInstructions = new Dictionary<string, Command>()
        {
            {"R", Command.BookCreation},
            {"D", Command.BookReturn},
            {"P", Command.BookBooking},
            {"C", Command.PenaltyPayment},
            {"ESCAPE", Command.Exit}
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
