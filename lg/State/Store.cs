namespace LostGen {
    public class Store : Redux.Store<Board> {
        public Store(params Redux.Middleware<Board>[] middlewares)
            : base(Reducers.BoardReducer, new Board(), middlewares) { }
    }
}