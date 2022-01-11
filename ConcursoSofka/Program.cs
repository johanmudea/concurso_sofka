//importar librerias

using ConcursoSofka.Data;
using ConcursoSofka.Data.AccessData;
using Microsoft.EntityFrameworkCore;

//instancias declaradas

var context = new BusinessContext();
var answersRepository = new Repository<Answer>(context);
var questionsRepository = new Repository<Questions>(context);
var categoriesRepository = new Repository<Category>(context);
var historicalsRepository = new Repository<Historical>(context);

var categories = categoriesRepository.Get().OrderBy(c => c.Dificult).ToList();

Console.WriteLine("ingrese 1 si es admin, 2 si es jugador");

//lógica para agregar preguntas por parte del admin a la BD

var x = int.Parse(Console.ReadLine());

if (x == 1)
{
    foreach (var category in categories)
    {
        //bandera para controlar minimo de preguntas

        var i = 1;
        var isContinue = true;
        while (i <= 5 || isContinue)
        {
            Console.WriteLine($"Ingresar texto de la pregunta de categoria {category.Name}");
            var question = new Questions();
            question.Question = Console.ReadLine();
            question.Idcategory = category.Id;


            for (int j = 1; j <= 4; j++)
            {
                var answer = new Answer();
                Console.WriteLine($"Crear respuesta {j}");
                answer.Text = Console.ReadLine();
                Console.WriteLine($"Ingresa 1 si esta es la respuesta correcta,0 si es falso");
                var valueCorrect = Console.ReadLine();
                var isCorrect = false;
                if (int.TryParse(valueCorrect, out _))
                    isCorrect = int.Parse(valueCorrect) == 1;

                answer.IsCorrect = isCorrect;
                question.Answers.Add(answer);
            }
            questionsRepository.Add(question);
            questionsRepository.Commit();
            if (i == 5)
            {
                Console.WriteLine("Presione 1 si deseas crear otra pregunta para esta categoria.");
                isContinue = int.Parse(Console.ReadLine()) == 1;
            }
            i++;
        }
    }
}

//lógica para asignarle preguntas al usuario a medida que avanza en el juego.

else if(x == 2)
{
    var questions = questionsRepository.Get(include: q => q.Include(c => c.Answers));
    Console.WriteLine("Cual es tu usuario?");
    var user = Console.ReadLine();

    foreach (var category in categories.OrderBy(c => c.Dificult))
    {
        var question = questions.Where(q => q.Idcategory == category.Id).OrderBy(o => Guid.NewGuid()).FirstOrDefault();

        Console.WriteLine(question.Question);

        for (int i = 0; i < question.Answers.Count; i++)
        {
            var answer = question.Answers.ToArray()[i];
            Console.WriteLine($"{i+1}. {answer.Text}");
        }
        Console.WriteLine($"Escoge la respuesta correcta.");
        var inputAnswer = int.Parse(Console.ReadLine()) - 1;

        var chosenAnswer = question.Answers.ToArray()[inputAnswer];
        var history = new Historical();
        history.IdAnswer = chosenAnswer.Id;
        history.User = user;
        historicalsRepository.Add(history);
        historicalsRepository.Commit();
        if (chosenAnswer.IsCorrect)
        {
            if(category.Dificult == categories.Max(c => c.Dificult))
            {
                Console.WriteLine("Haz ganado el premio mayor.");
                break;
            }
            Console.WriteLine("Felicitaciones, sigue asi.");
        }
        else
        {
            Console.WriteLine("Game Over");
            break;
        }
    }

}


