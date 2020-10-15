using System;

namespace BasicGenericsExampleUse
{
    public class CustomList<T>
    {
        private T[] items = new T[3];
        private int currentIndex;

        public int Length { get { return items.Length; } }

        public void Add(T i)
        {
            if (currentIndex == items.Length)
            {
                Grow();
            }

            items[currentIndex++] = i;
        }

        public T Get(int index)
        {
            return items[index];
        }

        private void Grow()
        {
            var temp = new T[items.Length * 2];
            Array.Copy(items, temp, items.Length);
            items = temp;
        }
    }

    public class CustomListInt
    {
        private int[] ints = new int[3];
        private int currentIndex;

        public int Length { get { return ints.Length; } }

        public void Add(int i)
        {
            if (currentIndex == ints.Length)
            {
                Grow();
            }

            ints[currentIndex++] = i;
        }

        public int Get(int index)
        {
            return ints[index];
        }

        private void Grow()
        {
            var temp = new int[ints.Length * 2];
            Array.Copy(ints, temp, ints.Length);
            ints = temp;
        }
    }

    public class CustomListString
    {
        private string[] strings = new string[3];
        private int currentIndex;

        public int Length { get { return strings.Length; } }

        public void Add(string i)
        {
            if (currentIndex == strings.Length)
            {
                Grow();
            }

            strings[currentIndex++] = i;
        }

        public string Get(int index)
        {
            return strings[index];
        }

        private void Grow()
        {
            var temp = new string[strings.Length * 2];
            Array.Copy(strings, temp, strings.Length);
            strings = temp;
        }
    }
}