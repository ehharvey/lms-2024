namespace Lms.Views {
    interface IView {
        public string Stringify(object o);

        public string Stringify<T>(IEnumerable<T> o);
    }
}