﻿#pragma warning disable IDE1006

namespace MoipCSharp.Models
{
    public class ContaTransparenteRequest
    {
        public Email email { get; set; }
        public Person person { get; set; }
        public string type { get; set; }
        public bool transparentAccount { get; set; }
    }
}