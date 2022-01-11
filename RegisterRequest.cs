using System;
namespace RestCSharp
{
    public class RegisterRequest
    {
        public String fullName { get; set; }
        public String userName { get; set; }
        public String password { get; set; }
    }
}