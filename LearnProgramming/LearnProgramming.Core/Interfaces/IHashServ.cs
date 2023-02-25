﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnProgramming.Core.Interfaces
{
    public record HashPasswordResponse(byte[] PasswordHash, byte[] PasswordSalt);

    public interface IHashServ
    {
        HashPasswordResponse HashPassword(string password);
        bool VerifyPassword(string password, byte[] hash, byte[] salt);
    }

}
