﻿using AngouriMath.Core;
using AngouriMath.Core.FromString;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


namespace AngouriMath
{
    /// <summary>
    /// Use functions from this class
    /// </summary>
    public static partial class MathS
    {
        public static readonly OneArg Sin = Sinf.Hang;
        public static readonly OneArg Cos = Cosf.Hang;
        public static readonly TwoArg Log = Logf.Hang;
        public static readonly TwoArg Pow = Powf.Hang;
        public static readonly OneArg Sqrt = a => Powf.Hang(a, 0.5);
        public static readonly OneArg Sqr = a => Powf.Hang(a, 2);
        public static readonly OneArg Tan = a => MathS.Sin(a) / MathS.Cos(a);
        public static readonly OneArg Cotan = a => 1 / Tan(a);
        public static readonly OneArg Sec = a => 1 / Cos(a);
        public static readonly OneArg Cosec = a => 1 / Sin(a);
        public static readonly OneArg B = a => a * Sin(a);
        public static readonly OneArg TB = a => a * Cos(a);
        public static OneArg Ln => v => Logf.Hang(v, e);
        public static readonly VarFunc Var = v => new VariableEntity(v);
        public static readonly VarFunc Symbol = v => new VariableEntity(v);
        public static readonly NumFunc Num = (a, b) => new Number(a, b);
        public static readonly Number e = 2.718281828459045235;
        public static readonly Number i = new Number(0, 1);
        public static readonly Number pi = 3.141592653589793;
        public static double EQUALITY_THRESHOLD { get; set; } = 1.0e-11;

        /// <summary>
        /// Convert an expression from a string
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static Entity FromString(string expr)
        {
            var lexer = new Lexer(expr);
            var res = Parser.Parse(lexer);
            return SynonimFunctions.Synonimize(res);
        }
    }
}