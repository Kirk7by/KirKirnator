using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository
    {      
        #region Выборка данных
        //обычная выборка всей базы целиком TODO: что-то попроваить
        EntityStorage GetEntityStorage();
        IEnumerable<Heroes> GetHeroesSource();
        IEnumerable<Questions> GetQuestionsSource();
        #endregion

        #region Добавление данных
        //Добавляет героя и вопрос в случае, если не угадали
        Exception AddHeroesAndQuestion(string nameHero, string nameQuestion, List<Questions> QustSelected);
        //...
        #endregion

        #region Обновление данных
        //Question:изменение вероятности и веса всех вопросов //Heroes:изменение увеличение количества игр на одну игру
     //   Exception UpdateEndGamePobability();
        //измениение данных о героях, без изменений первичного ключа
        Exception UpdateHeroes();
        //изменение данных о вопросах, без изменений данных первичного ключа
        Exception UpdateQuestion();
        #endregion

        #region удаление данных
        //чистка всех бд целиком
        Exception ClearBdData();
        //удаление конкретного героя
        Exception RemoveHeroes(string NameHeroes);
        //удаление конкретного вопроса
        Exception RemoveQuestion(string NameQuestion);
        #endregion
    }
}
