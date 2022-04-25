﻿using System.IO;

//--------------------------------------------------------------------------------

namespace Theater.JSON {

    public class SimpleCharReader : ICharReader {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        private StreamReader reader;

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public SimpleCharReader(StreamReader reader) {
            this.reader = reader;
        }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public bool EndOfStream() => this.reader.EndOfStream;
        public char Peek() => (char) this.reader.Peek();
        public char Read() => (char) this.reader.Read();
        
        //--------------------------------------------------------------------------------

        public void SkipWhitespaces() {
            
            while (char.IsWhiteSpace(this.Peek())) {
                this.Read();
            }
        }

        //--------------------------------------------------------------------------------
    }
}