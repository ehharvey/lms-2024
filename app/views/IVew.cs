namespace Lms.Views {
    public interface IView {
        public string Stringify(object o);

        public string Stringify<T>(IEnumerable<T> o) where T : notnull;
    }
}