namespace DataServiceLayer.Services
{
    using IDataServiceLayer.Interfaces;
    using IDataServiceLayer.Models;
    using System;
    using System.Threading.Tasks;
    using System.Linq;
    using CrossCutting.Constants;

    public class UserDataService : IUserDataService
    {
        private readonly PublicLibraryContext ctx = new PublicLibraryContext();

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return this.ctx.Users.Where(x => x.Id == id).FirstOrDefault();
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> HasAnyFine(int userId)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return this.ctx.Fines.Any(x => x.UserId == userId && x.IsPaid == false);
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task PayFine(int userId)
        {
            try
            {
                var fines = this.ctx.Fines.Where(x => x.UserId == userId && x.IsPaid == false).ToList();
                foreach (var fine in fines)
                {
                    fine.IsPaid = true;
                    fine.PaidDate = DateTime.Now;
                    fine.UpdateUser = Constants.DefaultUser;
                    fine.UpdateDate = DateTime.Now;
                    fine.FineValue = Constants.BooksPropertyForLoan.FineValue;
                }

                await this.ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Fine> SetFine(int userId)
        {
            try
            {
                var entity = new Fine()
                {
                    CreateDate = DateTime.Now,
                    CreateUser = Constants.DefaultUser,
                    FineValue = Constants.BooksPropertyForLoan.FineValue,
                    IsPaid = false,
                    UserId = userId
                };

                this.ctx.Fines.Add(entity);
                await this.ctx.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
