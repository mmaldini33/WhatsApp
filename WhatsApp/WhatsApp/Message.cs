using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatsApp
{
    class Message
    {
        public Guid Id; 
        public User Source;
        public User Target;
        private string TextMessage;
        public DateTime Creation { get; set; }
        public bool Sent { get; set; }
        public bool Received { get; set; }
        public bool Read { get; set; }
        public string State {
            get
            {
                string state = "";
                if (Sent)
                {
                    state = " (Enviado) ";
                    if (Received)
                    {
                        state = " (Recibido)";
                        if (Read)
                            state = " (Leido) ";
                    }
                }
                return state;
                } 
        }
        public void ShowState()
        {
            if (Sent)
            {
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" /");
                }
                if (Received)
                {
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("/");
                    }
                    if (Read)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(Console.CursorLeft - 2, Console.CursorTop);
                        Console.Write(" //");
                    }
                }
            }
        }

        public Message(User Source, User Target, string TextMessage)
        {
            this.Id = Guid.NewGuid();
            this.Creation = DateTime.Now;
            this.Source = Source;
            this.Target = Target;
            this.TextMessage = TextMessage;
        }

        public override string ToString()
        {
            return string.Format(TextMessage);
        }

    }
}
