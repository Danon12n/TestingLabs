using pet_shop.Models;

namespace pet_shop.IRepository
{
    public interface IPetMySQLRepository
    {
        public int GetPetNewId();
        public Pet GetPetById(int id);
    }
}
