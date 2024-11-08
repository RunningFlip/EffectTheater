﻿using System;
using System.Collections.Generic;

//--------------------------------------------------------------------------------

namespace Theater.JSON {

    public class JSONLong : JSONValue {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public long Value { get; set; }

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public JSONLong(long number) {
            this.Value = number;
        }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public override bool Equals(JSONValue value) {

            if (value is JSONLong other) {
                return this.Value == other.Value;
            }

            return false;
        }

        //--------------------------------------------------------------------------------

        public override string ToString() {
            return this.Value.ToString();
        }

        //--------------------------------------------------------------------------------
    }
}