namespace TurkishNumberToIntegerConverter.Services
{
    public interface IAppConverterService
    {

        public string SeparateTheWords(string text);

        public int ConvertTurkishNumberToInteger(string text);

        public string ParseTheSentence(string output);
    }
}
