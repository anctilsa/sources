using System;

namespace St_Pierre.models
{
    public class itemCompatibility
    {
        private int item1Id;
        private int item2Id;

        public int Item1Id
        {
            set
            {
                this.item1Id = value;
            }
            get
            {
                return this.item1Id;
            }
        }
        public int Item2Id
        {
            set
            {
                this.item2Id = value;
            }
            get
            {
                return this.item2Id;
            }
        }
    }
}