using System.Collections.Generic;

//--------------------------------------------------------------------------------

namespace Theater.Pool {

    public abstract class Pool<T> where T : class, new() {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public int Size => this.poolSet.Count;

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        private Stack<T> pool = new Stack<T>();
        private HashSet<T> poolSet = new HashSet<T>();

        //--------------------------------------------------------------------------------
        // Constructors
        //--------------------------------------------------------------------------------

        public Pool(int capacity) {
            
            for (int i = 0; i < capacity; i++) {

                T item = this.CreatePoolElement();
                pool.Push(item);
                poolSet.Add(item);
            }
        }

        //--------------------------------------------------------------------------------
        // Abstract methods
        //--------------------------------------------------------------------------------

        protected abstract T CreatePoolElement();
        protected abstract void ResetReturnedItem(T item);

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public T Get() {

            if (pool.Count > 0) {
                return pool.Pop();
            }
            else {

                T newItem = this.CreatePoolElement();
                this.poolSet.Add(newItem);
                return newItem;
            }
        }

        //--------------------------------------------------------------------------------

        public void Return(T item) {

            if (this.poolSet.Contains(item)) {

                this.ResetReturnedItem(item);
                this.pool.Push(item);
            }
        }

        //--------------------------------------------------------------------------------
    }
}