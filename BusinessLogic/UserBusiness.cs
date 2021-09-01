using CommonInterfaces.Entities;
using CommonInterfaces.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class UserBusiness : IUser
    {
        #region Variables
        UserData data = new UserData();
        #endregion

        #region Public Methods
        public bool CreateUser(User user)
        {
            if (user == null)
                throw new Exception("Datos de usuario no validos");
            if(user.identification == null)
                throw new Exception("Identificación de usuario no valida");
            if (user.identification.Length < Convert.ToInt16(Environment.GetEnvironmentVariable("MinIdentificationLength")) || user.identification.Length > Convert.ToInt16(Environment.GetEnvironmentVariable("MaxIdentificationLength")))
                throw new Exception("Longitud de identificación de usuario no permitida");
            if(user.nameUser == null)
                throw new Exception("Nombre de usuario no valido");
            if(user.nameUser == string.Empty)
                throw new Exception("Nombre de usuario no valido");
            if(user.balance < 0)
                throw new Exception("El saldo debe ser mayor a cero");

            return data.CreateUser(user);
        }

        public User GetUserXId(User user)
        {
            if (user == null)
                throw new Exception("Datos de usuario no validos");
            if (user.identification == null)
                throw new Exception("Identificación de usuario no valida");
            if (user.identification.Length < Convert.ToInt16(Environment.GetEnvironmentVariable("MinIdentificationLength")) || user.identification.Length > Convert.ToInt16(Environment.GetEnvironmentVariable("MaxIdentificationLength")))
                throw new Exception("Longitud de identificación de usuario no permitida");

            return data.GetUserXId(user);
        }

        public bool UpdateBalanceUser(User user)
        {
            if (user == null)
                throw new Exception("Datos de usuario no validos");
            if (user.identification == null)
                throw new Exception("Identificación de usuario no valida");
            if (user.identification.Length < Convert.ToInt16(Environment.GetEnvironmentVariable("MinIdentificationLength")) || user.identification.Length > Convert.ToInt16(Environment.GetEnvironmentVariable("MaxIdentificationLength")))
                throw new Exception("Longitud de identificación de usuario no permitida");
            if (user.balance < 0)
                throw new Exception("El saldo del usuario no puede ser menor a cero");

            return data.UpdateBalanceUser(user);
        }
        #endregion
    }
}
