﻿namespace ECommerceAPI.Helpers.Interfaces
{
    public interface IPasswordManager
    {
        Task<string> Hash(string password);
    }
}
