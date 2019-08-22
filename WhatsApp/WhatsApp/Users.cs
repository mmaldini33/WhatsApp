using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatsApp
{
    class Users 
    {
        private string Name = "";
        public User WhatsApp;
        private string NameUserToRemove;
        public string GetName { get { return Name; } }

        private List<User> UserList = new List<User>();

        public Users(string Name)
        {
            this.Name = Name;
        }

        public void Add(User User, bool Show)
        {
            UserList.Add(User);
            if (Show)
                Console.WriteLine("Se ha agregado el usuario {0} Id {1} en la lista {2}", User.Name, User.Id, this.Name);
        }

        public int Count()
        {
            return UserList.Count;
        }

        public Guid GetUser(string name)
        {
            foreach (User user in UserList)
            {
                if (user.Name == name)
                {
                    return user.Id;
                }
                    
            }
            return Guid.Empty;
        }

        public User GetUser(Guid id)
        {
            foreach (User user in UserList)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public void RemoveUser(User objUser)
        {
            //Elimina el primero objeto que encuentra.
            if (UserList.Remove(objUser))
                Console.WriteLine("Se ha removido a {0} de la lista {1}", objUser.Name, this.Name);
            //foreach (User user in UserList)
            //{
            //    if (user.Id == objUser.Id)
            //    {
            //        UserList.Remove(user);
            //        break;
            //    }
            //}
        }
        public void RemoveUser(string pName)
        {
            this.NameUserToRemove = pName;
            Console.WriteLine("Se ha/n removido {0} usuario/s.",UserList.RemoveAll(GetEqualName));
            //foreach (User user in UserList)
            //{
            //    if (user.Id == objUser.Id)
            //    {
            //        UserList.Remove(user);
            //        break;
            //    }
            //}
        }

        private bool GetEqualName(User Usuario)
        {
            return Usuario.Name == this.NameUserToRemove;
        }

        public override string ToString()
        {
            string message = "";
            foreach (User user in UserList)
            {
                message += string.Format(user.ToString() + "\r\n");
            }
            return message;
        }
        public string ShowUsers()
        {
            string message = "\r\n";
            foreach (User user in UserList)
            {
                message += string.Format("Usuarios de {0}\r\n", user.Name);
                message += user.ShowContacts();
                message += string.Format("\r\n");
            }
            return message;
        }

        public void SendMenssage(string Message)
        {
            foreach (User user in UserList)
            {
                WhatsApp.SendMessage(user, Message);
            }
        }

    }
}
