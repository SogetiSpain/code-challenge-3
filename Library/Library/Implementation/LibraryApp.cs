namespace Library.App.Program.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Castle.Windsor;
    using Infrastructure.Log.Installer;
    using Castle.MicroKernel.Registration;
    using Castle.DynamicProxy;
    using Library.App.Program.Code;
    using Library.App.Program.Interface;
    using Library.App.Program.Code.Utils;
    using Library.App.Program.Code.StateMachine;
    using Library.App.ServiceLayer.Interface;
    using Library.App.ServiceLayer.Implementation;
    using Library.App.ServiceLayer.DTO;

    public class LibraryApp : ILibraryApp
    {
        private StateMachine _appSM { get; set; }
        private static readonly IWindsorContainer _container = new WindsorContainer();
        private static ILibraryApp _currentApp;
        private static IUserService _userService;
        private static ILibraryService _libService;
        private static ConstantsApp.Command _userCommand;


        internal static IWindsorContainer Container
        {
            get { return LibraryApp._container; }
        }

        /// <summary>
        /// 
        /// </summary>
        public LibraryApp()
        {
            this._appSM = new StateMachine();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialization()
        {
            LibraryApp._container.Install(new Log4NetInstaller());
            LibraryApp._container.Register(Component.For<IInterceptor>().ImplementedBy<TraceAspect>().Named("Trace").LifestyleSingleton());
            LibraryApp._container.Install(new LibraryAppInstaller());

            LibraryApp._container.Register(Component.For<IUserService>().ImplementedBy<UserService>());
            LibraryApp._container.Register(Component.For<ILibraryService>().ImplementedBy<LibraryService>());

            _libService = LibraryApp._container.Resolve<ILibraryService>();            
            _userService = LibraryApp._container.Resolve<IUserService>();
            _currentApp = LibraryApp._container.Resolve<ILibraryApp>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            do{
                try
                {
                    Console.TreatControlCAsInput = true;    // Prevent example from ending if CTL+C is pressed.
                    StartOp();
                    _appSM.Transition(_userCommand);
                    switch (_appSM.GetCurrentState())
                    {
                        case ConstantsApp.ProcessState.ErrorState:
                            ErrorOp();
                            _appSM.Transition(ConstantsApp.Command.Begin);
                            break;

                        case ConstantsApp.ProcessState.BookCreationState:
                            BookCreationOp();
                            _appSM.Transition(ConstantsApp.Command.Begin);
                            break;

                        case ConstantsApp.ProcessState.BookBookingState:
                            BookBookimgOp();
                            _appSM.Transition(ConstantsApp.Command.Begin);
                            break;

                        case ConstantsApp.ProcessState.BookReturnState:
                            BookReturnOp();
                            _appSM.Transition(ConstantsApp.Command.Begin);
                            break;
                        case ConstantsApp.ProcessState.PayPenlaty:
                            PayPenaltyOp();
                            _appSM.Transition(ConstantsApp.Command.Begin);
                            break;
                    }
                }
                catch
                {
                    ErrorOp();
                    _appSM.Transition(ConstantsApp.Command.Begin);
                }

            } while (!_appSM.IsFinish());
        }

        #region ProceedStatusOps

        private void StartOp() 
        {
            Console.WriteLine("Presiona Escape (Esc) para cerrar la aplicación");
            Console.WriteLine("Selecciona una operación: ");
            Console.WriteLine("- (C)ancelar multa ");
            Console.WriteLine("- (R)egistrar un libro ");
            Console.WriteLine("- (P)réstamo ");
            Console.WriteLine("- (D)evolución ");

            ConsoleKeyInfo cki;
            cki = Console.ReadKey(true);
            ConstantsApp.LibAppInstructions.TryGetValue(cki.Key.ToString().ToUpper(), out _userCommand);
            Console.WriteLine("---------------------");

        }

        /// <summary>
        /// 
        /// </summary>
        private void BookCreationOp()
        {
            BookDto newBook = new BookDto();
            string lineInput = string.Empty;

            Console.WriteLine("Iniciando proceso de registro del libros..");
            Console.TreatControlCAsInput = false;    

            // - Get book's name
            Console.WriteLine("Introduzca el nombre del libro:");
            lineInput = Console.ReadLine();
            newBook.BookTitle = lineInput;

            // - Get Author's book
            Console.WriteLine("Introduzca el nombre del autor: ");
            lineInput = Console.ReadLine();
            newBook.AuthorName = lineInput;

            try 
            {
                if (!_libService.RegisterBook(newBook))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("El libro ya existe en el sistema \n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("El libro registrado correctamente \n");
                Console.ForegroundColor = ConsoleColor.White;                

            } 
            catch {}
            Console.WriteLine("---------------------");

        }

        /// <summary>
        /// 
        /// </summary>
        private void BookBookimgOp()
        {
            LibraryAppUserDto libUserDto = new LibraryAppUserDto();
            BookDto bookDto = new BookDto();
            DateTime endBooking;
            string inputLine = string.Empty;
            Console.TreatControlCAsInput = false;  


            do
            {
                Console.WriteLine("Introduzca el usuario: ");
                inputLine = Console.ReadLine();
                libUserDto = _userService.GetUser(inputLine);
                if (libUserDto == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nombre de usuario no válido");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } while (libUserDto == null);

            if (_libService.IsUserAllowed(libUserDto.Username))
            {
                do
                {
                    Console.WriteLine("Introduzca el libro: ");
                    inputLine = Console.ReadLine();
                    bookDto = _libService.GetBook(inputLine);
                    if (bookDto == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nombre de libro no válido");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else 
                    {
                        if(_libService.IsBooked(bookDto.BookTitle))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Libro no disponible");
                            Console.ForegroundColor = ConsoleColor.White;
                            bookDto = null;
                        }
                    }
                    
                } while (bookDto == null);

                Console.WriteLine("Iniciando proceso de préstamo \n");
                BookingDto newBooking = new BookingDto();

                bool validDAte = false;
                do
                {
                    Console.WriteLine("Introduzca la fecha de la devolución: ");
                    Console.WriteLine("{dd/mm/aaaa} ej: 20/08/2016");
                    inputLine = Console.ReadLine();
                    validDAte = (Common.ValidateDate(inputLine, out endBooking) && (endBooking < DateTime.Today.AddDays(30)));

                    if (!validDAte)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Fecha de devolucióno no válida");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                } while (!validDAte);

                newBooking.BookId = bookDto.Id;
                newBooking.LibraryAppUsername = libUserDto.Username;
                newBooking.StartBookingDate = DateTime.Today;
                newBooking.EndBookingDate = endBooking;
                _libService.RegisterBooking(newBooking);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Préstamo registrado correctamente \n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Se ha denagado el préstamo\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("---------------------");
          
        }

        /// <summary>
        /// 
        /// </summary>
        private void BookReturnOp()
        {
            LibraryAppUserDto libUserDto = new LibraryAppUserDto();
            BookDto bookDto = new BookDto();
            string inputLine = string.Empty;
            Console.TreatControlCAsInput = false;
            bool endProcess = false;

            do
            {
                Console.WriteLine("Introduzca el usuario: ");
                inputLine = Console.ReadLine();
                libUserDto = _userService.GetUser(inputLine);
                if (libUserDto == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nombre de usuario no válido");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } while (libUserDto == null);

            do
            {
                Console.WriteLine("Introduzca el libro: ");
                inputLine = Console.ReadLine();
                bookDto = _libService.GetBook(inputLine);
                if (bookDto == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nombre de libro no válido");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    try
                    {
                        if (_libService.GetBooking(libUserDto.Username, bookDto.BookTitle) != null)
                        {
                            if (_libService.ReturnBook(libUserDto.Username, bookDto.BookTitle, DateTime.Today) != null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Ha sido multado. Este libro debería haberse devuelto antes");
                                Console.ForegroundColor = ConsoleColor.White;
                                endProcess = true;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("El libro ha sido devuelto. Gracias por devolverlo a tiempo!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        else 
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No se ha realizado la devolución.");
                            Console.WriteLine("¿Estás seguro que {0} tenía el libro {1}?", libUserDto.Username, bookDto.BookTitle);
                            Console.ForegroundColor = ConsoleColor.White;
                            endProcess = true;
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No se ha realizado la devolución.");
                        Console.WriteLine("¿Estás seguro que '{0}' tenía el libro '{1}'?", libUserDto.Username, bookDto.BookTitle);
                        Console.ForegroundColor = ConsoleColor.White;
                        endProcess = true;
                    }
                }


            } while (!endProcess);
            Console.WriteLine("---------------------");

        }

        /// <summary>
        /// 
        /// </summary>
        private void ErrorOp()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Operación no válida. Por favor, introduzca una instrucción permitida. \n");
            Console.ForegroundColor = ConsoleColor.White;

            _appSM.Transition(ConstantsApp.Command.Begin);

        }

        /// <summary>
        /// 
        /// </summary>
        private void PayPenaltyOp() 
        {
            LibraryAppUserDto libUserDto = new LibraryAppUserDto();
            BookDto bookDto;
            string inputLine = string.Empty;
            Console.TreatControlCAsInput = false;
            do
            {
                Console.WriteLine("Introduzca el usuario: ");
                inputLine = Console.ReadLine();
                libUserDto = _userService.GetUser(inputLine);
                if (libUserDto == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nombre de usuario no válido");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            } while (libUserDto == null);

            do
            {
                Console.WriteLine("Introduzca el libro: ");
                inputLine = Console.ReadLine();
                bookDto = _libService.GetBook(inputLine);
                if (bookDto == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nombre de libro no válido");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (bookDto == null);

            try
            {
                PenaltyDto penalty = _libService.GetPenalty(libUserDto.Username, bookDto.BookTitle);
                Console.TreatControlCAsInput = true;
                Console.WriteLine("¿Confirma que desea cancelar la multa? (Y/N)");
                ConsoleKeyInfo cki;
                cki = Console.ReadKey(true);

                if (cki.Key.ToString().ToLower() == "y")
                {
                    _libService.PayPenalty(penalty);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Se ha realizadp el pago correctamente");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (cki.Key.ToString().ToLower() == "n")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ha decidido no cancelar la multa... allá usted");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se ha cancelado la multa");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("---------------------");

        }
        #endregion ProceedStatusOps



    }
}
