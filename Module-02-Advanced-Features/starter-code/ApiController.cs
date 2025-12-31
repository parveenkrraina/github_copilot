using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.StarterCode
{
    public class ApiController
    {
        private UserManager userManager;
        
        public ApiController(UserManager manager)
        {
            this. userManager = manager;
        }
        
        public ApiResponse<User> GetUser(int id)
        {
            try
            {
                var users = userManager.GetActiveUsers();
                var user = users.FirstOrDefault(u => u.Id == id);
                
                if (user == null)
                {
                    return new ApiResponse<User>
                    {
                        Success = false,
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }
                
                return new ApiResponse<User>
                {
                    Success = true,
                    StatusCode = 200,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
        
        public ApiResponse<User> CreateUser(CreateUserRequest request)
        {
            try
            {
                var user = userManager.CreateUser(
                    request.Username, 
                    request.Email, 
                    request.Password
                );
                
                return new ApiResponse<User>
                {
                    Success = true,
                    StatusCode = 201,
                    Data = user
                };
            }
            catch (ArgumentException ex)
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    StatusCode = 400,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<User>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex. Message
                };
            }
        }
        
        public ApiResponse<bool> DeleteUser(int id)
        {
            try
            {
                userManager.DeactivateUser(id);
                
                return new ApiResponse<bool>
                {
                    Success = true,
                    StatusCode = 200,
                    Data = true
                };
            }
            catch (ArgumentException)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "User not found"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }
    }
    
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}