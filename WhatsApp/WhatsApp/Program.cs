using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WhatsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instanciamos la clase Users que contendrá todos los usuarios que usan la app
            var UsersList = new Users("WhatsApp");
            //Configuramos un usuario para WhatsApp para ser origen y destinos de mensajes.
            UsersList.WhatsApp = new User("WhatsApp", "1-574745-457");
            
            //Instanciamos un nuevo usuario
            var Usuario1 = new User("Guillermo", "6025407");
            //Lo agregamos a la lista de usuario
            UsersList.Add(Usuario1, false);


            //Agregamos dos nuevos usuarios al tiempo que creamos dos nuevas instancias de usuario
            UsersList.Add(new User("Gerardo", "15652011"), false);
            UsersList.Add(new User("Gerardo", "2305457"), false);
            UsersList.Add(new User("Mauro", "15802022"), false);

            //Agregamos más usuarios
            UsersList.Add(new User("Pedro", "152353392"), false);
            User Mario =  new User("Mario", "156984747");
            UsersList.Add(Mario,false);

            //Mostramos la lista de usuarios en la app
            Console.WriteLine("Usuarios de {0}", UsersList.GetName);
            Console.WriteLine(UsersList.ToString());
            Console.ReadKey(true);
            Console.Clear();

            //leemos el id del usuario Mauro
            var id = UsersList.GetUser("Mauro");
            //creamos una variable que hace referencia al usuario que nos devuelve el método GetUser(id)
            User Mauro = UsersList.GetUser(id);
            //Agregamos un contacto al usuario Mauro, en este caso el usuario Guillermo que esta siendo referenciado por la variable Usuario1
            Mauro.AddContact(Usuario1);

            //Mostramos los contactos en la lista de Mauro
            Console.WriteLine(Mauro.ShowContacts());
           
            //creamos una referencia al usuario Gerardo utilizando dos métodos en el mismo momento
            //primermetodo = ListUsers.GetUser("Gerardo") busca en la lista de usuarios el usuario Gerardo y devuelve el Id
            //segundometodo = ListUsers.GetUser(primermetodo) que busca el usuario con el id devuelto por el primer método y nos devuelve la referencia al usuario encontrado
            User Gerardo = UsersList.GetUser(UsersList.GetUser("Gerardo"));
            //al usuario encontrado le agregamos un nuevo contacto Mauro usando la misma técnica
            Gerardo.AddContact(UsersList.GetUser(UsersList.GetUser("Mauro")));

            //Muestra todos los contactos de gerardo
            Console.WriteLine(Gerardo.ShowContacts());

            //Gerardo le envía un mensaje a mauro
            Gerardo.SendMessage(Mauro, "Mauro tenés listas las lecciones para hoy???");
            Mario.SendMessage(Gerardo, "Hola Gera soy mauro, como estás?");

            //Console.WriteLine("Ahora Mauro lee el mensaje");
            //Console.ReadKey(true);

            //mauro lee el mensaje
            Mauro.ReadMessage("Gerardo");

            Console.WriteLine();

            //leemos los mensajes enviados por Gerardo que aún no han sido leidos
            Gerardo.ShowMessagesUnread();

            Console.WriteLine();

            Mauro.SendMessage(Gerardo, "todo piola!!! tranka gera");
            Gerardo.SendMessage(Mauro, "perfecto, nos vemos esta noche!");
            Mauro.SendMessage(Gerardo, "ok");
            Gerardo.SendMessage(Mauro, "pulgar para arriba");
            Mauro.SendMessage(Gerardo, "no vemos");
            Gerardo.OffLine = true;
            Mauro.SendMessage(Gerardo, "llevá el mate");
            Mauro.SendMessage(Gerardo, "y unos biscochitos");

            Console.ReadKey(true);

            Console.WriteLine();

            //Se muestra el Chat entre Gerardo y Mauro
            Gerardo.ShowChat(Mauro);
            Console.ReadKey(true);

            Console.WriteLine();

            //Se muestra el Chat entre Mauro y Gerardo
            Mauro.ShowChat(Gerardo);
            Console.ReadKey(true);



            //Se envía un mensaje a todos los usuarios de WhatsApp
            UsersList.SendMenssage("El servicio comenzará a ser pago desde Septiembre de 2019");
            Console.ReadKey(true);

            //Se leen todos los mensajes que ha recibido Gerardo aún no leidos
            Gerardo.ReadMessages();
            Console.ReadKey(true);

            //Muestra todos los mensajes sin leer de todos los usuarios 
            UsersList.WhatsApp.ShowMessagesUnread();
            Console.ReadKey(true);

            //Usa el método eliminar usuario por nombre
            UsersList.RemoveUser("Gerardo");

            Console.WriteLine(UsersList.ToString());
            Console.ReadKey(true);


        }

    }
}
