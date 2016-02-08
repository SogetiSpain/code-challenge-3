namespace App
{
    using CrossCutting.Enum;
    using CrossCutting.Resources;
    using System;

    public class ChoiceInputMenu
    {
        public ChoiceStartEnum.ChoiceEnum choiceMenu()
        {
            Console.WriteLine(Display.AskUser);
            string value = Console.ReadLine();
            try
            {
                ChoiceStartEnum.ChoiceEnum option = (ChoiceStartEnum.ChoiceEnum)Enum.Parse(typeof(ChoiceStartEnum.ChoiceEnum), value.ToUpper());
                return option;
            }
            catch (Exception)
            {
                Console.WriteLine(Exceptions.LetterAskException);
                return choiceMenu();
            }
        }
    }
}
