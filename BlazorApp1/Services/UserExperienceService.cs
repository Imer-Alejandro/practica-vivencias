using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
    public class UserExperienceService
    {
        private readonly AppDbContext _context;

        public UserExperienceService(AppDbContext context)
        {
            _context = context;
        }

        // Registro de un nuevo usuario
            public async Task<bool> RegisterUserAsync(User newUser)
            {
                // Validar si ya existe el usuario o el correo
                if (await _context.Users.AnyAsync(u => u.Username == newUser.Username || u.Email == newUser.Email))
                    return false;

                // Agregar el nuevo usuario al contexto
                _context.Users.Add(newUser);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                return true;
            }
        // Login
        public async Task<User?> LoginAsync(string usernameOrEmail, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.Password == password);
        }

        // Agregar una vivencia
       public async Task<bool> AddExperienceAsync(Experience experience)
        {
            var user = await _context.Users.FindAsync(experience.UserId);
            if (user == null) return false;

            // Asignar la fecha si no se proporciona
            if (experience.Date == default)
            {
                experience.Date = DateTime.Now; // Usar la fecha actual si no se especifica
            }

            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();
            return true;
        }

        // Obtener vivencias del usuario
        public async Task<List<Experience>> GetUserExperiencesAsync(int userId)
        {
            return await _context.Experiences
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        // Botón de pánico: borrar todas las vivencias del usuario
        public async Task<bool> DeleteAllExperiencesAsync(int userId, string password)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.Password != password) return false;

            var userExperiences = await _context.Experiences
                .Where(e => e.UserId == userId)
                .ToListAsync();

            _context.Experiences.RemoveRange(userExperiences);
            await _context.SaveChangesAsync();
            return true;
        }

        // Verificar existencia de usuario por nombre o correo
        public async Task<bool> UserExistsAsync(string usernameOrEmail)
        {
            return await _context.Users.AnyAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
        }
    }
}
