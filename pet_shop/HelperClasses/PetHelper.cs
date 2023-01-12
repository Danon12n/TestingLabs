using System;
using System.Reflection.Metadata;
using pet_shop.IRepository;
using pet_shop.Models;

namespace pet_shop.HelperClasses
{
    public class PetHelper
    {
        private readonly IPetMySQLRepository _petMySQLRepository;
        public PetHelper(IPetMySQLRepository petMySQLRepository)
        {
            _petMySQLRepository = petMySQLRepository;
        }

        public string CheckFreeId(int id)
        {
            Pet pet = _petMySQLRepository.GetPetById(id);
            if (pet == null) return "Нету питомца с таким id";
            else return "Такой питомец существует";
        }
    }
}
