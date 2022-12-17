using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;
using pet_shop.DB;
using pet_shop.Models;

namespace pet_shop.MySQLRepository
{
    public class PetMySQLRepository
    {
        private MySqlCommand cmd = new MySqlCommand(); // команда для совершения sql-запроса
        private MySqlConnection conn = DBUtils.GetDBConnection(); //подключение базы

        public PetMySQLRepository()
        {
            try
            {
                this.conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        ~PetMySQLRepository() //чтобы деструктор завершал соединение при закрытии программы
        {
            conn.Close();
            conn.Dispose();
        }

        public void AddPetInfo(PetInfo PI)
        {
            // Команда Insert.
            string sql = "Insert into `pet_info` (pet_id, pet_type, name, age, color, can_swim, reproduce_ability, gender, pet_breed) "
                                             + " values (@pet_id, @pet_type, @name, @age, @color, @can_swim, @reproduce_ability, @gender, @pet_breed) ";
            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметры @login @password @name @surname
            cmd.Parameters.Add("@pet_id", MySqlDbType.Int32).Value = PI.pet_id;
            cmd.Parameters.Add("@pet_type", MySqlDbType.String).Value = PI.pet_type;
            cmd.Parameters.Add("@age", MySqlDbType.Int32).Value = PI.age;
            cmd.Parameters.Add("@name", MySqlDbType.String).Value = PI.name;
            cmd.Parameters.Add("@color", MySqlDbType.String).Value = PI.color;
            cmd.Parameters.Add("@can_swim", MySqlDbType.Int32).Value = PI.can_swim;
            cmd.Parameters.Add("@reproduce_ability", MySqlDbType.Int32).Value = PI.reproduce_ability;
            cmd.Parameters.Add("@gender", MySqlDbType.String).Value = PI.gender;
            cmd.Parameters.Add("@pet_breed", MySqlDbType.String).Value = PI.pet_breed;

            // Выполнить Command (использованная для  delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();

            Console.WriteLine("Row Count affected = " + rowCount);

        }

        public void AddPetPrice(Pet pet)
        {
            string sql = "Insert into `pets` (pet_id, shop_id, price) "
                                             + " values (@pet_id, @shop_id, @price) ";
            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметры @login @password @name @surname
            cmd.Parameters.Add("@pet_id", MySqlDbType.Int32).Value = pet.pet_id;
            cmd.Parameters.Add("@shop_id", MySqlDbType.Int32).Value = pet.shop_id;
            cmd.Parameters.Add("@price", MySqlDbType.Int32).Value = pet.price;


            // Выполнить Command (использованная для  delete, insert, update).
            int rowCount = cmd.ExecuteNonQuery();

            Console.WriteLine("Row Count affected = " + rowCount);
        }

        public int GetPetNewId()
        {
            // Команда select.
            string sql = "Select MAX(pet_id) from `pets`";

            cmd.CommandText = sql;
            cmd.Connection = conn;
            int newPetId;
            try
            {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            newPetId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("MAX(pet_id)"))) + 1;
                            return newPetId;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            return 1;
            
        }

        public List<PetInfo> GetPets() //получение всех пользователей
        {
            string sql = "SELECT * FROM `pet_info`";

            cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            var pets = new List<PetInfo>();

            try
            {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int petId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pet_id")));
                            int age = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("age")));
                            int canSwim = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("can_swim")));
                            int reproduceAbility = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("reproduce_ability")));
                            string petType = reader.GetString(reader.GetOrdinal("pet_type"));
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            string color = reader.GetString(reader.GetOrdinal("color"));
                            string gender = reader.GetString(reader.GetOrdinal("gender"));
                            string petBreed = reader.GetString(reader.GetOrdinal("pet_breed"));

                            pets.Add(new PetInfo(petId, petType, name, age, color, canSwim, reproduceAbility, gender, petBreed));

                        }
                        return pets;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There is no pets or error");
                Console.WriteLine("Error: " + e);
            }
            
            return pets;
        }

        public List<int> GetShopIdByPetId(int petId)
        {
            // Команда select.
            string sql = "Select * from `pets` where pet_id = @petId";

            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметр @login в запрос
            cmd.Parameters.Add("@petId", MySqlDbType.String).Value = petId;
            var info = new List<int>() { -10, -10, 0};
            string availability;

            try {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            info[0] = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("shop_id")));
                            info[1] = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("price")));
                            availability = reader.GetString(reader.GetOrdinal("available"));
                            if (availability == "yes")
                                info[2] = 1;
                            return info; // info[0] = shop_id || info[1] = price of the pet || info[2] = 0(no)/1(yes) pet availability
                        }
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There is no pets or error");
                Console.WriteLine("Error: " + e);
            }
            return info; // значения -10 значит ничего нет по запросу
        }

        public void DeletePetById(int id)
        {
            // Команда select.
            string sql = "Delete from `pets` where pet_id = @id";

            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметр @id в запрос
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            try
            {
                int rowCount = cmd.ExecuteNonQuery();
                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }

            sql = "Delete from `pet_info` where pet_id = @id";
            cmd.CommandText = sql;

            try
            {
                int rowCount = cmd.ExecuteNonQuery();
                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
        }

        public void ChangePetAvailability(int petId, string availability)
        {
            // Команда update.
            string sql = "Update `pets` set available = @available where pet_id = @petId";

            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметр @login в запрос

            cmd.Parameters.Add("@available", MySqlDbType.String).Value = availability;
            cmd.Parameters.Add("@petId", MySqlDbType.Int32).Value = petId;
            try
            {
                int rowCount = cmd.ExecuteNonQuery();
                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }

            
            Console.WriteLine("Статус животного успешно изменен!");

        }
        public Pet GetPetById(int id)
        {
            // Команда select.
            string sql = "Select * from `pets` where pet_id = @id";

            cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;

            // Добавляем параметр @id в запрос
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            var pet = new Pet();
            try
            {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            pet.pet_id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pet_id")));
                            pet.price = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("price")));
                            pet.shop_id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("shop_id")));
                            pet.availability = reader.GetString(reader.GetOrdinal("availability"));


                            Console.WriteLine("Питомец успешно найден!");
                            return pet;
                        }
                    }
                    
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            Console.WriteLine("Не найден питомец по данному id!");
            return null;


        }

        public PetInfo GetPetInfoById(int id) //получение всех пользователей
        {
            string sql = "SELECT * FROM `pet_info` where pet_id = @id ";

            cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;

            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            try
            {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int petId = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pet_id")));
                            int age = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("age")));
                            int canSwim = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("can_swim")));
                            int reproduceAbility = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("reproduce_ability")));
                            string petType = reader.GetString(reader.GetOrdinal("pet_type"));
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            string color = reader.GetString(reader.GetOrdinal("color"));
                            string gender = reader.GetString(reader.GetOrdinal("gender"));
                            string petBreed = reader.GetString(reader.GetOrdinal("pet_breed"));

                            return new PetInfo(petId, petType, name, age, color, canSwim, reproduceAbility, gender, petBreed);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                
                Console.WriteLine("Error: " + e);
            }
            Console.WriteLine("Нет данных по данному питомцу!");

            return new PetInfo();
        }

        public List<DisplayInfo> FilterResult(string petType, string gender, int canSwim, int reproduceAbility, int priceFrom, int priceTo, string adress)
        {
            string sql = "SELECT * " +
                "FROM (SELECT pet_id, adress, price FROM `pets` join `shops` on pets.shop_id = shops.Shop_id) AS info join " +
                "`pet_info` on info.pet_id = pet_info.pet_id ";

            string sqlConditions = "";
            cmd = new MySqlCommand();
            cmd.Connection = conn;

            var DisplayInfo = new List<DisplayInfo>();

            int counter = 0;

            if (petType != "Any")
            {
                sqlConditions += " pet_type = @petType ";
                cmd.Parameters.Add("@petType", MySqlDbType.String).Value = petType;
                
                counter++;
            }

            if (gender != "Any")
            {
                if (counter == 0)
                    sqlConditions += " gender = @gender ";
                else
                    sqlConditions += " AND gender = @gender ";
                cmd.Parameters.Add("@gender", MySqlDbType.String).Value = gender;
                counter++;
            }

            if (canSwim != 2) //2 == any
            {
                if (counter == 0)
                    sqlConditions += " can_swim = @canSwim ";
                else
                    sqlConditions += " AND can_swim = @canSwim ";
                cmd.Parameters.Add("@canSwim", MySqlDbType.Int32).Value = canSwim;
                counter++;
            }

            if (reproduceAbility != 2) //2 == any
            {
                if (counter == 0)
                    sqlConditions += " reproduce_ability = @reproduceAbility ";
                else
                    sqlConditions += " AND reproduce_ability = @reproduceAbility ";
                cmd.Parameters.Add("@reproduceAbility", MySqlDbType.Int32).Value = reproduceAbility;
                counter++;
            }

            //Console.WriteLine("adress = <" + adress + ">");
            if (adress != "Any")
            {
                if (counter == 0)
                    sqlConditions += " adress = @adress ";
                else
                    sqlConditions += " AND adress = @adress ";
                cmd.Parameters.Add("@adress", MySqlDbType.String).Value = adress;
                counter++;
            }

           
            if (counter == 0)
                sqlConditions += " price between @priceFrom AND @priceTo ";
            else
                sqlConditions += " AND price between @priceFrom AND @priceTo ";
            cmd.Parameters.Add("@priceFrom", MySqlDbType.Int32).Value = priceFrom;
            cmd.Parameters.Add("@priceTo", MySqlDbType.Int32).Value = priceTo;
            counter++;
            

            //Console.WriteLine("sqlConditions: " + sqlConditions);

            if (counter > 0)
                cmd.CommandText = sql + " where " + sqlConditions;
            else
                cmd.CommandText = sql;

            //Console.WriteLine("sql: " + sql + " where " + sqlConditions);

            try
            {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var PI = new PetInfo();
                            PI.pet_id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("pet_id")));
                            PI.age = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("age")));
                            PI.can_swim = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("can_swim")));
                            PI.reproduce_ability = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("reproduce_ability")));
                            PI.pet_type = reader.GetString(reader.GetOrdinal("pet_type"));
                            PI.name = reader.GetString(reader.GetOrdinal("name"));
                            PI.color = reader.GetString(reader.GetOrdinal("color"));
                            PI.gender = reader.GetString(reader.GetOrdinal("gender"));
                            PI.pet_breed = reader.GetString(reader.GetOrdinal("pet_breed"));
                            string Radress = reader.GetString(reader.GetOrdinal("adress"));
                            int price = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("price")));

                            DisplayInfo.Add(new DisplayInfo(PI, Radress, price));

                        }
                        return DisplayInfo;
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Error: " + e);
            }
            Console.WriteLine("I didnt find anything");
            return DisplayInfo;

        }


    }
}
