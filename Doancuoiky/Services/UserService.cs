
using Doancuoiky.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Doancuoiky.Services
{
    public class UserService
    {
        FirebaseClient client;

        public UserService()
        {
            client = new FirebaseClient("https://laptop-shop-26c01-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }

        public async Task<bool> isPasswordRight(string pword)
        {
            var user = (await client.Child("Users")
                .OnceAsync<User>()).Where(u => u.Object.Password == pword).FirstOrDefault();
            return (user != null);
        }

        public async Task<bool> isUserExists(string uname)
        {
            var user = (await client.Child("Users")
                .OnceAsync<User>()).Where(u => u.Object.Username == uname).FirstOrDefault();
            return (user != null);
        }


        public async Task<bool> RegisterUser(string uname, string passwd, string niname, string address, string sdt)
        {
            if (await isUserExists(uname)==false) 
            {
                await client.Child("Users")
                    .PostAsync(new User()
                    {
                        Username = uname,
                        Password = passwd,
                        Nickname = niname,
                        Address = address,
                        Phone= sdt
                    });
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateUser(string uname, string passwd)
        {
            var user = (await client
            .Child("Users")
            .OnceAsync<User>()).Where(u => u.Object.Username == uname).FirstOrDefault();

            if (user!=null)
            {
                user.Object.Password = passwd;
                await client
                    .Child("Users")
                    .Child(user.Key)
                    .PutAsync(user.Object);
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UpdateUserInfo(string uname, string nname, string naddress, string nphone)
        {
            var user = (await client
            .Child("Users")
            .OnceAsync<User>()).Where(u => u.Object.Username == uname).FirstOrDefault();

            if (user != null)
            {
                user.Object.Nickname = nname;
                user.Object.Address = naddress;
                user.Object.Phone = nphone;
                await client
                    .Child("Users")
                    .Child(user.Key)
                    .PutAsync(user.Object);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LoginUser(string uname, string passwd)
        {
            var user = (await client.Child("Users")
                .OnceAsync<User>()).Where(u => u.Object.Username == uname)
                .Where(u=>u.Object.Password==passwd).FirstOrDefault();
            return (user != null); 
        }

        public async Task<List<User>> GetUserAsync()
        {
            var user = (await client.Child("Users")
                .OnceAsync<User>())
                .Select(u => new User
                {
                    Username = u.Object.Username,
                    Password = u.Object.Password,
                    Nickname= u.Object.Nickname,
                    Address = u.Object.Address,
                    Phone = u.Object.Phone
                }).ToList();
            return user;
        }

        public async Task<User> GetUserInfoAsync(string name)
        {
            var user = (await GetUserAsync()).Where(u => u.Username == name).FirstOrDefault();
            return user;
        }

        public async Task<ObservableCollection<User>> GetUserByName(string username)
        {
            var userByName = new ObservableCollection<User>();
            var items = (await GetUserAsync()).Where(u => u.Username == username).ToList();
            foreach (var item in items)
            {
                userByName.Add(item);
            }
            return userByName;
        }

        public async Task<ObservableCollection<User>> GetUserByPassword(string pword)
        {
            var userByPassword = new ObservableCollection<User>();
            var items = (await GetUserAsync()).Where(u => u.Password == pword).ToList();
            foreach (var item in items)
            {
                userByPassword.Add(item);
            }
            return userByPassword;
        }
    }
}
