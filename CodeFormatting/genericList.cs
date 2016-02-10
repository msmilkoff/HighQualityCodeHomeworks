using System;
using System.Linq;
using System.Text;

namespace _03.GenericList
{
    class GenericList<T>{
        private const int InitialSize = 16;
        private T[] internalArray;
        private int currentIndex;

        public bool Contains(T item){
            if (this.internalArray.Any
                (x => x.Equals(item)
                )
                ){
                return true;
            }
            return false;
        }
        public int IndexOf(T item){
            for (int index = 0;
                index < internalArray.Length;
                index++) {
                if (this.internalArray[index].Equals(item)) {
                    return index;
                }
            }
            return -1;
        }
        public void InsertAt(int index,
            T item){
            if (index < 0 ||
                index >= this.Count){
                throw new IndexOutOfRangeException(
                    "Index is outside the boundaries of the List."
                    );
            }
            internalArray[index] = item;
        }
        public void Clear() {
            this.internalArray = 
                new T[InitialSize];
            this.currentIndex = 0;
        }
        public void RemoveAt(int index){
            if (index < 0 
                || this.Count <= index){
                throw new IndexOutOfRangeException(
                    "Index is outside the boundaries of the List.");
            }
            T[] modifiedArray 
                = new T[this.internalArray.Length];
            Array.Copy(this.internalArray
                , 0, modifiedArray,
                0, index);
            Array.Copy(this.internalArray,
                index + 1,modifiedArray, index,
                this.Count -
                index -
                1);
            this.currentIndex--;
        }
        public void Add(T item){
            if (this.Count + 1 >= this.internalArray.Length){
                this.Resize();
            }
            this.internalArray[this.currentIndex] = item;
            this.currentIndex++;
        }
        private void Resize(){
            int addedLength = this.internalArray.Length * 2;
            T[] resizedArray = new T[addedLength];
            Array.Copy(this.internalArray
                , resizedArray,
                this.internalArray.Length);
            this.internalArray = resizedArray;
        }
        public GenericList(){
            this.internalArray 
                = new T[InitialSize];
            this.currentIndex = 0;
        }
        public int Count{
            get { return this.currentIndex; }
        }
        public T this[int index]{
            get {
                if (index < 0 || index >=
                    this.Count)
                {
                    throw new IndexOutOfRangeException("Index is outside the boundaries of the List");
                }
                return this.internalArray[index];
            }
            set{
                if (index < 0 || index >= this.Count) {
                    throw new
                        IndexOutOfRangeException(
                        "Index is outside the boundaries of the List.");
                }
                this.internalArray[index] = value;
            }
        }
        public override string ToString() {
            var result = new T[this.Count];
            for (int i = 0; 
                i < this.Count; 
                i++) {
                result[i] = this.internalArray[i];
            }
            return (string
                .Format("[{0}]"
                , string.Join(", "
                , result)
                )
                );
        }
    }
}