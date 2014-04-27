using System.Threading;
namespace ClassLibrary1
{
    /// <summary>
    /// Переменная поля подстановки
    /// </summary>
    public class Variable
    {
        string _value;
        /// <summary>
        /// Строковое представление значения переменной
        /// </summary>
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        string _type;
        /// <summary>
        /// Тип переменной из поля подстановки
        /// </summary>
        public string Type
        {
            get
            {
                return _type;
            }
        }

        string _name;
        /// <summary>
        /// Имя переменной поля подстановки
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Создание новой переменной поля подстановки
        /// </summary>
        /// <param name="str">инициализирующее выражение</param>
        public Variable(string str)
        {
            string[] variab = str.Split(' ');
            if (variab.Length != 2)
                throw new BadFieldException(str, "Неверное количество слов в инициализирующем выражении");
            foreach (string badName in _badNames)
                if (variab[1] == badName)
                    throw new BadFieldException(str, "Имя совпадает с ключевым словом");
            if ((variab[1][0]).In('0', '9'))
                throw new BadFieldException(str, "Имя начинается с цифры");
            foreach (char letter in variab[1])
                if (!letter.In('0', '9') && !letter.In('A', 'Z') && !letter.In('a', 'z') && !letter.In('а', 'я') && !letter.In('А', 'Я') && letter != '_')
                    throw new BadFieldException(str, "Имя должно содержать только цифры или русские или английские буквы");
            _type = variab[0];
            _name = variab[1];
            if ((_type == "sbyte") || (_type == "short") ||
                        (_type == "int") || (_type == "long"))
            {
                _value = Randomizer.RandomSigned();
                Thread.Sleep(100);
            }
            else
                if ((_type == "byte") ||
                    (_type == "uint") ||
                    (_type == "ulong"))
                {
                    _value = Randomizer.RandomUnsigned();
                    Thread.Sleep(100);

                }
                else
                    if (_type == "double")
                    {
                        _value = Randomizer.RandomReal();
                        Thread.Sleep(100);
                    }
                    else
                        if (_type == "float")
                        {
                            _value = Randomizer.RandomReal() + "F";
                            Thread.Sleep(100);
                        }
                        else
                            if (_type == "decimal")
                            {
                                _value = Randomizer.RandomReal() + "M";
                            }
                            else
                                if (_type == "char")
                                {
                                    string value = Randomizer.RandomSymbol();
                                    _value = value;
                                    Thread.Sleep(100);
                                }
                                else
                                    if (_type == "string")
                                    {
                                        string value = Randomizer.RandomLine();
                                        _value = value;
                                    }
                                    else
                                        if (_type == "bool")
                                        {
                                            _value = Randomizer.RandomBoolean();
                                        }
                                        else
                                        {
                                            throw new BadFieldException(str, "Неверный тип");
                                        }
        }

        readonly string[] _badNames =
        {
           "abstract", "as", "base", "bool", "break",
           "byte", "case", "catch", "char", "checked", 
           "class", "const", "continue", "decimal", "default", 
           "delegate", "do", "double", "else", "enum",
           "event", "explicit", "extern", "false", "finally",
           "fixed", "float", "for", "foreach", "goto",
           "if", "implicit", "in", "int", "interface",
           "internal", "is", "lock", "long", "namespace",
           "new", "null", "object", "operator", "out",
           "override", "params", "private", "protected", "public", 
           "readonly", "ref", "return", "sbyte", "sealed", 
           "short", "sizeof", "stackalloc", "static", "string",
           "struct", "switch", "this", "throw", "true",
           "try", "typeof", "uint", "ulong", "unchecked", 
           "unsafe", "ushort", "using", "virtual", "void",
           "volatile", "while"
        };
    }
}
