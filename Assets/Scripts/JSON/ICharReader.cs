
namespace Theater.JSON {

    public interface ICharReader {

        char Peek();
        char Read();
        bool EndOfStream();
        void SkipWhitespaces();
    }
}