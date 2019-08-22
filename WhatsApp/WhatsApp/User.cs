using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WhatsApp
{
    [Serializable]
    class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        private string Phone { get; set; }
        public string Info { get; set; }
        public bool OffLine { get; set; }
        private Users ContactList;
        private List<Message> ListMessages = new List<Message>();

        public User(string Name, string Phone)
        {
            this.Id = Guid.NewGuid(); 
            this.Name = Name;
            this.Phone = Phone;
            ContactList = new Users(Name);
        }
        public void AddContact(User User)
        {
            ContactList.Add(User,false);
        }
        public void RemoveContact(User User)
        {
            ContactList.RemoveUser(User);
        }
        public string ShowContacts()
        {
            return string.Format("Contactos de {0}\r\n{1}",this.Name, ContactList.ToString());
        }        
        
        public override string ToString()
        {
            return string.Format("Id Usuario {0} Name:{1} Phone: {2}", Id, Name, Phone);
        }


        public void SendMessage(User Target, string TextMessage)
        {
            //cuando un usuario esta enviando un mensaje se debe dar lecturas a todos los mensajes del 
            //usuario destino
            this.ReadMessage(Target.Name);
            
            //crea un nuevo mensaje
            var message = new Message(this, Target, TextMessage);
            //lo marca como enviado
            message.Sent = true;
            //envía el mensaje al destino
            Target.NewMessage(message);
            //agrega el mensaje a la lista de mensajes enviados del usuario actual
            ListMessages.Add(message);
        }

        public void NewMessage(Message MessageReceived)
        {
            if (!OffLine)
            {
                Console.WriteLine(this.Name + " ha recibido un mensaje de {0} : {1}", MessageReceived.Source.Name, MessageReceived);
                //lo marca como recibido
                MessageReceived.Received = true;
                //lo agrega a la lista de mensajes recibidos
                ListMessages.Add(MessageReceived);
            }
        }    

        //metodos llamado para marcar un mensaje como leido
        public void SetMessageRead(Guid Id)
        {
            foreach (var mensaje in ListMessages)
            {
                if (mensaje.Id == Id)
                {
                    mensaje.Read = true;
                }
            }
        }

        //metodo para consultar los mensajes enviados por el usuario que no han sido leidos
        public void ShowMessagesUnread()
        {
            int cantidad = 0;
            Console.WriteLine("Mensajes enviados por " + this.Name + " sin leer.");
            foreach (var pmessage in ListMessages)
            {
                if (pmessage.Source.Name == this.Name && !pmessage.Read)
                {
                    Console.WriteLine("Enviado el {0} a {1} mensaje {2}", pmessage.Creation, pmessage.Target.Name, pmessage);
                    cantidad++;
                }
            }
            if (cantidad == 0)
            {
                Console.WriteLine(this.Name + " no tiene mensajes enviados sin leer.");
            }
        }

        //leer los mensajes recibidos de un determinado usuario
        public void ReadMessage(string Name)
        {
            foreach (var pmessage in ListMessages)
            {
                //el origen es el usuario del que quiero leer los mensajes
                //procesa solamente los no leidos anteriormente
                if (pmessage.Source.Name == Name && !pmessage.Read)
                {
                    Console.WriteLine("{0} lee el mensaje de {1} : {2}", this.Name, pmessage.Source.Name, pmessage);
                    //envía la confirmación de lectura al usuario que envio el mensaje
                    pmessage.Source.SetMessageRead(pmessage.Id);
                }
            }
        }
        public void ReadMessages()
        {
            foreach (var pmessage in ListMessages)
            {
                //el origen es el usuario del que quiero leer los mensajes
                //procesa solamente los no leidos anteriormente
                if (!pmessage.Read)
                {
                    Console.WriteLine("{0} lee el mensaje de {1} : {2}", this.Name, pmessage.Source.Name, pmessage);
                    //envía la confirmación de lectura al usuario que envio el mensaje
                    pmessage.Source.SetMessageRead(pmessage.Id);

                }
            }
        }


        public void ShowChat(User pUser)
        {
            Console.WriteLine("Chat entre {0} y {1}", this.Name, pUser.Name);
            foreach (Message pmessage in ListMessages)
            {
                //El mensaje fue enviado al usuario del cual queremos visualizar el chat
                if (pUser.Id == pmessage.Target.Id)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    //Console.WriteLine(pmessage.ToString() + pmessage.State);
                    Console.Write(pmessage.ToString());
                    pmessage.ShowState();
                    Console.WriteLine();

                }
                //El mensaje fue enviado del usuario del cual queremos visualizar el chat
                if (pUser.Id == pmessage.Source.Id)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.SetCursorPosition(10, Console.CursorTop);
                    //Console.WriteLine(pmessage.ToString() + pmessage.State);
                    Console.Write(pmessage.ToString());
                    pmessage.ShowState();
                    Console.WriteLine();
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }


    }
}
